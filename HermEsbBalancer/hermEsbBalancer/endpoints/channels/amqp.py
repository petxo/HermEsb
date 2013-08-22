import re
import pika
from pika.exceptions import ConnectionClosed
from hermEsbBalancer.core import compressors, loggerManager
from hermEsbBalancer.endpoints.channels import reconnectiontimers
from hermEsbBalancer.endpoints.channels.basechannels import OutBoundChannel, Channel, InBoundChannel
from hermEsbBalancer.endpoints.channels.exceptions import ConnectionError, SendError

__author__ = 'sergio'


## Clase base de un canal amqp
class BaseAmqpChannel:

    ## Crea una intancia de BaseAmqpChannel
    def __init__(self, host, channelId):
        self.channelId = channelId
        match = re.search('^amqp://(.*):(\d+)/(.*)/(.*)/(.*)', host)
        if match:
            self._server = match.group(1)
            self._port = int(match.group(2))
            self._exchange = match.group(3)
            self._queue = match.group(4)
            self._key = match.group(5)

        else:
            raise NameError("Invalid host name", host)

        self._connectionParams = pika.ConnectionParameters(host=self._server,
            port=self._port, heartbeat_interval=600)

    def _connect_point(self):
        try:
            self._connection = pika.BlockingConnection(self._connectionParams)
            self._channel = self._connection.channel()

            self._channel.exchange_declare(exchange=self._exchange, durable=True, passive=True)
            self._channel.queue_declare(queue=self._queue, durable=True, arguments={"x-ha-policy": "all"})
            self._channel.queue_bind(queue=self._queue, exchange=self._exchange, routing_key=self._key)

        except Exception as ex:
            raise ConnectionError(ex)

    def _close_point(self):
        self._connection.close()


## Clase que implementa un canal de salida con el protocolo amqp
class OutBoundAmqpChannel(OutBoundChannel, BaseAmqpChannel):

    ## Crea una instancia de OutBoundAmqpChannel
    # @param reconnectionTimer Temporizador de reconexion
    # @param maxReconnections Numero maximo de reconexiones
    # @param host Nombre del host ej: amqp://server:port/exchange/queue/key
    def __init__(self, host="", reconnectionTimer=reconnectiontimers.CreateLogarithmicTimer(),
                 maxReconnections=Channel.MAX_RECONNECTIONS, compressor=compressors.DefaultCompressor(), useAck=False, channelId=""):

        BaseAmqpChannel.__init__(self, host, channelId)
        OutBoundChannel.__init__(self, host, reconnectionTimer, maxReconnections, compressor, useAck=useAck)
        self.messageProperties = pika.BasicProperties(content_type='text/plain', delivery_mode=1, content_encoding='utf-8')

    def _connect_point(self):
        BaseAmqpChannel._connect_point(self)
        if self._useAck:
            self._channel.confirm_delivery()

    def _close_point(self):
        BaseAmqpChannel._close_point(self)

    def _send(self, message):
        try:
            return self._channel.basic_publish(exchange= self._exchange,
                                                routing_key= self._key,
                                                body= unicode(message).encode('utf-8'),
                                                properties= self.messageProperties)
        except ConnectionClosed as ex:
            loggerManager.get_endPoints_logger().error("Error: %s" % ex)
            raise ConnectionError("Se ha perdido la conexcion con el servidor AMPQ")
        except Exception as ex:
            loggerManager.get_endPoints_logger().error("Error: %s" % ex)
            raise SendError('Error al enviar el elemento %s' % ex)


class InBoundAmqpChannel(InBoundChannel, BaseAmqpChannel):

    ## Crea una instancia de InBoundAmqpChannel
    # @param reconnectionTimer Temporizador de reconexion
    # @param maxReconnections Numero maximo de reconexiones
    # @param host Nombre del host ej: amqp://server:port/queue
    # @param receptionTimeout Timeout de recepcion de mensaje en milisegudos por defecto 20000
    def __init__(self, host="", reconnectionTimer=reconnectiontimers.CreateLogarithmicTimer(),
                 maxReconnections=Channel.MAX_RECONNECTIONS, receptionTimeout=10, compressor = compressors.DefaultCompressor(),
                 useAck=False, channelId=""):
        BaseAmqpChannel.__init__(self, host, channelId)
        InBoundChannel.__init__(self, host, reconnectionTimer, maxReconnections, compressor= compressor,
            useAck=useAck)
        self._receptionTimeout = receptionTimeout
        self.__isConsuming = False

    def _connect_point(self):
        BaseAmqpChannel._connect_point(self)
        # self._connection.add_timeout(self._receptionTimeout, self._stopReceive)

    def _close_point(self):
        BaseAmqpChannel._close_point(self)

    def __readMessage(self, ch, method, properties, body):
        self._processMessage(body, { "channel" : ch, "method": method })

    def _startReceive(self):
        self._channel.basic_consume(self.__readMessage, queue=self._queue, no_ack=not self._useAck)
        self._channel.start_consuming()

    def _stopReceive(self):
        self._channel.stop_consuming()

    def _sendAck(self, obj):
        obj["channel"].basic_ack(delivery_tag= obj["method"].delivery_tag)

