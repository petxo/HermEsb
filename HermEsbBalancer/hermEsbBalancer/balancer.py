from hermEsbBalancer.core import loadbalancers, queue, loggerManager
from hermEsbBalancer.endpoints.channels.amqp import OutBoundAmqpChannel, InBoundAmqpChannel
from hermEsbBalancer.endpoints.gateways import SenderGateway, ReceiverGateway


__author__ = 'Sergio'


class Balancer:
    def __init__(self, cfg):
        lbNodesInput = loadbalancers.CreateRouterFromConfig(None)
        lbNodesControl = loadbalancers.CreateRouterFromConfig(None)
        qInput = queue.CreateQueueFromConfig(cfg['balancer']['inputChannel']['queue'])
        qControl = queue.CreateQueueFromConfig(cfg['balancer']['controlChannel']['queue'])

        inputChannels = list()
        controlChannels = list()
        for node in cfg['balancer']['nodes']:
            inputChannels.append(OutBoundAmqpChannel(host=node['inputChannel']['url'],
                                                     useAck=bool(node['inputChannel']['useAck'])))
            controlChannels.append(OutBoundAmqpChannel(host=node['controlChannel']['url'],
                                                       useAck=bool(node['controlChannel']['useAck'])))

        self.nodesInputGateway = SenderGateway(lbNodesInput, qInput, channels=inputChannels,
                                               numExtractors=len(inputChannels))
        self.nodesInputGateway.connect()

        self.nodesControlGateway = SenderGateway(lbNodesControl, qControl, channels=controlChannels,
                                                 numExtractors=len(controlChannels))
        self.nodesControlGateway.connect()

        self.receiverInputGateway = InBoundAmqpChannel(host=cfg['balancer']['inputChannel']['url'],
                                                       useAck=bool(cfg['balancer']['inputChannel']['useAck']))

        self.receiverInputGateway.OnMessageReceived += self.InputMessageReceived
        self.receiverInputGateway.connect()

        self.receiverControlGateway = InBoundAmqpChannel(host=cfg['balancer']['controlChannel']['url'],
                                                         useAck=bool(cfg['balancer']['controlChannel']['useAck']))
        self.receiverControlGateway.OnMessageReceived += self.ControlMessageReceived
        self.receiverControlGateway.connect()

    def InputMessageReceived(self, sender, args):
        self.nodesInputGateway.send(args.message)

    def ControlMessageReceived(self, sender, args):
        self.nodesControlGateway.send(args.message)

    def stop(self):
        loggerManager.getlogger().debug('Parando...')
        self.nodesInputGateway.stop()
        self.nodesInputGateway.close()
        self.nodesControlGateway.stop()
        self.nodesControlGateway.close()

        self.receiverInputGateway.stop()
        self.receiverInputGateway.close()
        self.receiverControlGateway.stop()
        self.receiverControlGateway.close()
        loggerManager.getlogger().debug('Parado')

    def start(self):
        self.nodesInputGateway.start()
        self.nodesControlGateway.start()
        self.receiverInputGateway.start()
        self.receiverControlGateway.start()
