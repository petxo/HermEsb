from hermEsbBalancer import clusterController, serialization
from hermEsbBalancer.core import queue, loggerManager
from hermEsbBalancer.endpoints.channels.amqp import OutBoundAmqpChannel, InBoundAmqpChannel


__author__ = 'Sergio'


class Balancer:

    __instance = None

    @classmethod
    def Create(cls, cfg, handlerRepository):
        cls.__instance = Balancer(cfg, handlerRepository)

    @classmethod
    def Instance(cls):
        return cls.__instance

    def __init__(self, cfg, handlerRepository):
        qInput = None
        qControl = None
        if not cfg['balancer']['inputChannel'].get('queue') is None:
            qInput = queue.CreateQueueFromConfig(cfg['balancer']['inputChannel']['queue'])
        if not cfg['balancer']['controlChannel'].get('queue') is None:
            qControl = queue.CreateQueueFromConfig(cfg['balancer']['controlChannel']['queue'])

        inputChannels = list()
        controlChannels = list()
        for node in cfg['balancer']['nodes']:
            for i in range(0, 10):
                inputChannels.append(OutBoundAmqpChannel(host=node['inputChannel']['url'], channelId=node['name'],
                                                             useAck=bool(node['inputChannel']['useAck'])))
            controlChannels.append(OutBoundAmqpChannel(host=node['controlChannel']['url'], channelId=node['name'],
                                                       useAck=bool(node['controlChannel']['useAck'])))

        receiverInputGateway = InBoundAmqpChannel(host=cfg['balancer']['inputChannel']['url'],
                                                  useAck=bool(cfg['balancer']['inputChannel']['useAck']))

        receiverClusterControlChannel = InBoundAmqpChannel(host=cfg['balancer']['controlChannel']['url'],
                                                           useAck=bool(cfg['balancer']['controlChannel']['useAck']))

        self.inputClusterController = clusterController.CreateClusterController(qInput, inputChannels,
                                                                                  receiverInputGateway)

        self.inputClusterController.connect()
        self.controlClusterController = clusterController.CreateClusterController(qControl, controlChannels,
                                                                                    receiverClusterControlChannel)
        self.controlClusterController.connect()

        self.receiverClusterControlChannel = InBoundAmqpChannel(host=cfg['balancer']['clusterControlChannel']['url'],
                                                                useAck=bool(
                                                                    cfg['balancer']['clusterControlChannel']['useAck']))

        self.receiverClusterControlChannel.OnMessageReceived += self.ClusterControlMessageReceived
        self.receiverClusterControlChannel.connect()

        self.clusterControllerHandler = []
        self.handlerRepository = handlerRepository

    def ClusterControlMessageReceived(self, sender, args):
        loggerManager.getlogger().debug('Control Message Received')
        message = serialization.loads(args.message)
        handler = self.handlerRepository.getHandler(message["Header"]["BodyType"])
        body = serialization.loads(message["Body"])
        handler.HandleMessage(body)
        del handler

    def stop(self):
        loggerManager.getlogger().debug('Parando...')
        self.receiverClusterControlChannel.stop()
        self.receiverClusterControlChannel.close()
        self.inputClusterController.stop()
        self.controlClusterController.stop()
        loggerManager.getlogger().debug('Parado')

    def start(self):
        self.receiverClusterControlChannel.start()
        self.inputClusterController.start()
        self.controlClusterController.start()
