from hermEsbBalancer import serialization
from hermEsbBalancer.bus import MessageBus

__author__ = 'Sergio'


class AddSubscriberFromClusterMessage:
    @classmethod
    def Create(cls, message):
        messageType = "HermEsb.Core.Controller.Messages.IAddSubscriberFromClusterMessage,HermEsb.Core"

        body = dict()
        body['Trigger'] = message["Identification"]
        body['SubscriberService'] = message["Service"]

        return MessageBus.Create(serialization.dumps(body), messageType, message["Identification"]["Id"], message["Identification"]["Type"])
