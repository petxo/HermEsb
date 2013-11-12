from threading import Thread
from config import Config
import pika
import pymongo

__author__ = 'Sergio'


class Publisher:
    def __init__(self, amqp_url, exchange, routingKey):
        self._parameters = pika.URLParameters(amqp_url)

        self.exchange = exchange
        self.routingKey = routingKey
        self._closing = False
        self._channel = None
        self._thread = None
        self._thread = Thread(target=self._start)

    def connect(self):
        self._connection = pika.SelectConnection(parameters=self._parameters, on_open_callback=self.on_open,
                                                 on_open_error_callback=self.on_open_error, stop_ioloop_on_close=False)

        self._thread.start()

    def _start(self):
        self._connection.ioloop.start()

    def on_open_error(self, unused_connection):
        pass

    def on_open(self, unused_connection):
        self._connection.add_on_close_callback(self.on_connection_closed)
        self._connection.channel(on_open_callback=self.on_channel_open)

    def on_connection_closed(self, connection, reply_code, reply_text):
        self._channel = None
        if self._closing:
            self._connection.add_timeout(5, self.connect)

    def on_channel_open(self, channel):
        self._channel = channel
        self._channel.add_on_close_callback(self.on_channel_closed)
        self._channel.exchange_declare(self.on_exchange_declareok,
                                       self.exchange,
                                       'direct', passive=True, durable=True)

    def on_channel_closed(self, channel, reply_code, reply_text):
        if not self._closing:
            self._connection.close()

    def on_exchange_declareok(self, unused_frame):
        pass

    def publish(self, message):
        properties = pika.BasicProperties(app_id='Backoffice',
                                          content_type='application/json', delivery_mode=1)

        self._channel.basic_publish(self.exchange,
                                    self.routingKey, message, properties)

    def __del__(self):
        self._closing = True
        self._channel.close()
        self._connection.close()


class Environment:
    def __init__(self, name, mongoserver, rabbitqueue):
        self.__mongoserver = mongoserver
        self.__rabbitqueue = rabbitqueue
        self.__name = name
        self.__identification = "NoService"
        try:
            self.__conn = pymongo.MongoClient(host=mongoserver, max_pool_size="10")
            self.__dbInfo = self.__conn["BusStatistics"]["BusInfo"]
            bus = self.__dbInfo.find_one()
            if not bus is None:
                self.__identification = self.__dbInfo.find_one()["Identification"]['_id']
            self.__CreateRabbitQueue()
        except Exception as ex:
            print(ex.message)

    @property
    def Name(self):
        return self.__name

    @property
    def MongoServer(self):
        return self.__mongoserver

    def GetBusName(self):
        return self.__identification

    def __CreateRabbitQueue(self):
        self.__publisher = Publisher(self.__rabbitqueue['server'], self.__rabbitqueue['exchange'], self.__rabbitqueue['routingKey'])
        self.__publisher.connect()

    def Publish(self, message):
        self.__publisher.publish(message)


class Environments:
    @classmethod
    def Create(cls, file):
        cfg = Config(file)
        cls.__instance = dict()
        for env in cfg["Environments"]:
            if env['active']:
                cls.__instance[env['name']] = Environment(env['name'], env['mongoserver'], env['rabbitqueue'])


    @classmethod
    def GetEnvironment(cls, name):
        return cls.__instance[name]

    @classmethod
    def GetEnvironments(cls):
        return cls.__instance.values()