from time import sleep
from hermEsbBalancer.endpoints.channels.amqp import InBoundAmqpChannel

__author__ = 'Sergio'
def ClusterControlMessageReceived(sender, args):
    print "o"

receiverClusterControlChannel = InBoundAmqpChannel(host='amqp://localhost:5672/HermEsbSamples.Exch/BusCluster.Input/inputBusClusterKey',
                                                                useAck=True)

receiverClusterControlChannel.OnMessageReceived += ClusterControlMessageReceived
receiverClusterControlChannel.connect()
receiverClusterControlChannel.start()

sleep(-1)


