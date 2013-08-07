from hermEsbBalancer import serialization
from hermEsbBalancer.balancer import Balancer
from hermEsbBalancer.bus import MessageBus

__author__ = 'Sergio'


class HandlerRepository:
    __instance = None

    @classmethod
    def Instance(cls):
        if cls.__instance is None:
            cls.__instance = HandlerRepository()
        return cls.__instance

    def __init__(self):
        self.__repository = dict()

    def addHandler(self, message, cls):
        self.__repository[message] = cls

    def getHandler(self, message):
        return self.__repository[message]()


def handleMessage(**types):
    def addHandler(cls):
        HandlerRepository.Instance().addHandler(types["message"], cls)
    return addHandler


@handleMessage(message="HermEsb.Core.Clustering.Messages.INewClusterSubscriberMessage,HermEsb.Core")
class NewSubscriberClusterMessageHandler:
    def __init__(self):
        self.balancer = Balancer.Instance()

    def HandleMessage(self, message):
        messageType = "HermEsb.Core.Controller.Messages.IAddSubscriberFromClusterMessage,HermEsb.Core"
        body = dict()
        body['Trigger'] = message["Identification"]
        body['SubscriberService'] = message["Service"]
        refreshMessage = MessageBus.Create(serialization.dumps(body), messageType, message["Identification"]["Id"], message["Identification"]["Type"])
        msg = serialization.dumps(refreshMessage)
        for channel in self.balancer.controlClusterController.getChannels():
            channel.send(msg)


@handleMessage(message="HermEsb.Core.Clustering.Messages.IRemoveClusterSubscriberMessage,HermEsb.Core")
class RemoveClusterSubscriberMessageHandler:
    def __init__(self):
        self.clusterController = Balancer.Instance().inputClusterController

    def HandleMessage(self, message):
        pass


@handleMessage(message="HermEsb.Core.Clustering.Messages.IHeartBeatClusterMessage,HermEsb.Core")
class HeartBeatClusterMessageHandler:
    def __init__(self):
        self.clusterController = Balancer.Instance().inputClusterController

    def HandleMessage(self, message):
        pass