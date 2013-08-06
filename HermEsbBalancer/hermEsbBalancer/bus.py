# author = sbermudel
# package description
from datetime import datetime


class Identification:
    @classmethod
    def Create(cls, id, type):
        identification = dict()
        identification['Id'] = id
        identification['Type'] = type
        return identification


class MessageHeader:
    @classmethod
    def Create(cls, bodyType, id, type):
        header = dict()
        header['BodyType'] = bodyType
        header['EncodingCodePage'] = 65001
        header['ReinjectionNumber'] = 0
        header['Priority'] = 0
        header['Type'] = 0
        header['CreatedAt'] = datetime.utcnow()
        header['CallContext'] = cls.CreateCallContext(id, type)
        header['CallStack'] = cls.CreateCallStack(id, type)
        header['IdentificationService'] = Identification.Create(id, type)
        return header

    @classmethod
    def CreateCallContext(cls, id, type):
        callContext = dict()
        callContext['Identification'] = Identification.Create(id, type)
        callContext['Session'] = dict()

        return callContext

    @classmethod
    def CreateCallStack(cls, id, type):
        callStack = list()

        item = dict()
        item['Identification'] = Identification.Create(id, type)
        item['Session'] = dict()
        callStack.append(item)

        return callStack


class MessageBus:
    @classmethod
    def Create(cls, body, bodyType, id, type):
        message = dict()
        message['Body'] = body
        message['Header'] = MessageHeader.Create(bodyType, id, type)
        return message

