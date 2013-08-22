# coding=utf-8
## @package clitellum.endpoints.gateways
#  Este paquete contiene las clases que encapsulan el acceso al sistema de mensajeria con el
# resto de la aplicacion
import threading
from hermEsbBalancer.core import queue, loadbalancers
from hermEsbBalancer.core.eventhandling import EventHook
from hermEsbBalancer.core.fsm import Startable
from hermEsbBalancer.core.loadbalancers import BalancingInfo
from hermEsbBalancer.endpoints.channels import factories
from hermEsbBalancer.endpoints.channels.events import MessageReceivedArgs

__author__ = 'sergio'

class BaseNoDurableGateway(Startable):

    ## Crea una instancia de BasicGateway
    # @param channel Canal de comunicacion utilizado por el gateway
    def __init__(self, channels=list()):
        Startable.__init__(self)
        self._channels = list()
        self._queue = queue
        self.OnConnectionError = EventHook()
        for channel in channels:
            self.addChannel(channel, BalancingInfo(channel.channelId))

    ## Añade un nuevo canal al gateway
    def addChannel(self, channel, balancingInfo=None):
        self._channels.append(channel)
        channel.OnConnectionError += self._invokeOnConnectionError

    ## Realiza la conexion del canal
    def connect(self):
        for channel in self._channels:
            channel.connect()

    ## Cierra la conexion del canal
    def close(self):
        for channel in self._channels:
            channel.close()

    ## Indica si el canal esta o no conectado
    @property
    def is_connected(self):
        for channel in self._channels:
            if channel.is_connected():
                return True

        return False

    def _invokeOnConnectionError(self, sender, args):
        #TODO: Es un error de conexion del canal que hay que tratar ademas de disparar
        self.OnConnectionError.fire(self, args)

    def _invokeOnStart(self):
        Startable._invokeOnStart(self)

    def _invokeOnStopped(self):
        Startable._invokeOnStopped(self)

    def __del__(self):
        Startable.__del__(self)
        for channel in self._channels:
            del channel

        self.OnConnectionError.clear()

## Clase que expone los metodos basicos para la encapsulacion del sistema de mensajeria
class BaseGateway(BaseNoDurableGateway):

    ## Crea una instancia de BasicGateway
    # @param channel Canal de comunicacion utilizado por el gateway
    def __init__(self, queue, channels=list(), numExtractors=4):
        BaseNoDurableGateway.__init__(self, channels)
        self._queue = queue
        self._thExtractors = list()
        for count in range(0, numExtractors):
            self._thExtractors.append(threading.Thread(target=self._process_queue))

    def _process_queue(self):
        pass

    def _invokeOnStart(self):
        BaseNoDurableGateway._invokeOnStart(self)
        for th in self._thExtractors:
            th.start()

    def _invokeOnStopped(self):
        BaseNoDurableGateway._invokeOnStopped(self)
        for th in self._thExtractors:
            th.join()

## Crea un SenderGateway desde un config
# { channels : [
#               { type : 0Mq, timer : Logarithmic, host : tcp://server:8080, maxReconnections : 20 }
#               { type : tcp, timer : Logarithmic, host : tcp://server:8082, maxReconnections : 20 }
#               { type : 0Mq, timer : Logarithmic, host : tcp://server:8083, maxReconnections : 20 }
#               ],
#   router : { type: "RoundRobin" },
#   queue : { type : "Berkeley",  path: "./data/queue.db" }
# }
def CreateSenderFromConfig(config):
    channels = list()
    for ch in config["channels"]:
        channel = factories.CreateOutBoundChannelFromConfig(ch)
        channels.append(channel)

    router = loadbalancers.CreateRouterFromConfig(config.get("balancer"))
    cola = queue.CreateQueueFromConfig(config["queue"])

    return SenderGateway(router, cola, channels)

## Clase que implementa un gateway de salida
class SenderNoDurableGateway (BaseNoDurableGateway):
    def __init__(self, loadBalancer, channels=list()):
        self._loadBalancer = loadBalancer
        BaseNoDurableGateway.__init__(self, channels)

    # TODO: añadir la informacion de enrutamiento, y añadir el canal al router
    def addChannel(self, outBoundChannel, balancingInfo=None):
        BaseNoDurableGateway.addChannel(self, outBoundChannel)
        self._loadBalancer.addChannel(outBoundChannel, balancingInfo)
        outBoundChannel.OnSendError += self._errorSending
        outBoundChannel.OnMessageSent += self._onMessageSent

    def send(self, message):
        th = threading.Thread(target=SenderNoDurableGateway._processMessage, args=(self, message))
        th.start()
        # th.join(timeout=None)

    def _processMessage(self, message):
        outBoundChannel = self._loadBalancer.next()
        outBoundChannel.send(message)

    def _errorSending(self, sender, args):
        # Reencolamos el mensaje
        pass

    def _onMessageSent(self, sender, args):
        #Eliminamos el mensaje de la cola definitivamente
        pass

## Clase que implementa un gateway de salida
class SenderGateway (BaseGateway):
    def __init__(self, loadBalancer, queue, channels=list(), numExtractors=4):
        self._loadBalancer = loadBalancer
        BaseGateway.__init__(self, queue, channels, numExtractors)

    # TODO: añadir la informacion de enrutamiento, y añadir el canal al router
    def addChannel(self, outBoundChannel, balancingInfo=None):
        BaseGateway.addChannel(self, outBoundChannel)
        self._loadBalancer.addChannel(outBoundChannel, balancingInfo)
        outBoundChannel.OnSendError += self._errorSending
        outBoundChannel.OnMessageSent += self._onMessageSent

    def send(self, message):
        self._queue.append(message)

    def _process_queue(self):
        while self.state == Startable.RUNNING:
#           TODO: controlar cuando no se puede enviar por uno o por ninguno de los channels
            try:
                item = self._queue.popleft(timeout=10)
                if not item is None:
                    outBoundChannel = self._loadBalancer.next()
                    outBoundChannel.send(item)
                del item
            except Exception:
                pass

    def _errorSending(self, sender, args):
        # Reencolamos el mensaje
        self._queue.task_not_done()

    def _onMessageSent(self, sender, args):
        #Eliminamos el mensaje de la cola definitivamente
        self._queue.task_done()


## Clase que implementa un gateway de entrada
class ReceiverGateway(BaseGateway):

    def __init__(self, queue, channels=list(), numExtractors=4):
        BaseGateway.__init__(self, queue, channels, numExtractors)
        self.OnMessageReceived = EventHook()

    def addChannel(self, channel, balancingInfo=None):
        BaseGateway.addChannel(self, channel)
        channel.OnMessageReceived += self._messageReceivedChannel

    def _process_queue(self):
        while self.state == Startable.RUNNING:
            item = self._queue.popleft(timeout=10)
            if not item is None:
                #TODO: Crear los hilos del demonio con el control de errores
                pass

    def _messageReceivedChannel(self, sender, args):
        self._queue.append(args.message)

    def __del__(self):
        BaseGateway.__del__(self)
        self.OnMessageReceived.clear()

    def _invokeOnReceivedMessage(self, message):
        args = MessageReceivedArgs(message= message)
        self.OnMessageReceived.fire(self, args)
