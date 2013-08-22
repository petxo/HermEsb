from hermEsbBalancer.core import loadbalancers
from hermEsbBalancer.core.fsm import Startable
from hermEsbBalancer.endpoints.gateways import SenderGateway, SenderNoDurableGateway

__author__ = 'Sergio'


def CreateClusterController(queue, channels, inBoundAmqpChannel):
    loadBalancer = loadbalancers.CreateRouterFromConfig(None)
    if not queue is None:
        gateway = SenderGateway(loadBalancer, queue, channels=channels, numExtractors=30)
                            ##numExtractors=len(channels))
    else:
        gateway = SenderNoDurableGateway(loadBalancer, channels=channels)
    return ClusterController(loadBalancer, gateway, inBoundAmqpChannel)


class ClusterController(Startable):
    def __init__(self, loadBalancer, senderGateway, inBoundAmqpChannel):
        Startable.__init__(self)
        self.__loadBalancer = loadBalancer
        self.__senderGateway = senderGateway
        self.__inBoundAmqpChannel = inBoundAmqpChannel
        self.__inBoundAmqpChannel.OnMessageReceived += self.__InputMessageReceived

    def getChannels(self):
        return [bal.item for bal in self.__loadBalancer.getItems()]

    def updateProcessTimeLimit(self, nodeName, time):
        for node in self.__loadBalancer.getItems():
            if node.balancingInfo.nodeName == nodeName:
                node.balancingInfo.processTimeLimit = time

    def connect(self):
        self.__senderGateway.connect()
        self.__inBoundAmqpChannel.connect()

    def __InputMessageReceived(self, sender, args):
        self.__senderGateway.send(args.message)

    def _invokeOnStart(self):
        Startable._invokeOnStart(self)
        self.__senderGateway.start()
        self.__inBoundAmqpChannel.start()

    def _invokeOnStopped(self):
        Startable._invokeOnStopped(self)
        self.__senderGateway.stop()
        self.__inBoundAmqpChannel.stop()

    def __del__(self):
        Startable.__del__(self)
        self.__senderGateway.close()
        self.__inBoundAmqpChannel.close()
        del self.__senderGateway
        del self.__inBoundAmqpChannel