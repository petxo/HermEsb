#include "OutputGateway.h"
#include <stdio.h>
#include <stdlib.h>
#include "../messages/MessageBus.h"
#include "../messages/MessageBusFactory.h"
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"

using namespace rapidjson;

namespace HermEsb
{
	namespace Gateways
	{
		OutputGateway::OutputGateway(Identification* identification, OutBoundConnectionPoint *outBoundConnectionPoint, bool useCompression) : 
			BaseGateway(identification, outBoundConnectionPoint, useCompression)
		{
			EVENT_BIND4(connection, outBoundConnectionPoint->OnSendError, &OutputGateway::SendError);
		}

		OutputGateway::~OutputGateway()
		{
		}

		void OutputGateway::Publish(string key, IMessage* message, int priority) 
		{
			StringBuffer sbMessage;
			Writer<StringBuffer> writer(sbMessage);
			message->Serialize(writer);

			//Creamos el MessageBus
			MessageBus* msg = HermEsb::Messages::MessageBusFactory::CreateMessageBus(_identification, key, string(sbMessage.GetString()));
			msg->Header.Priority = priority;
			msg->Header.ReinjectionNumber = 0;
			msg->Header.Type = MESSAGETYPE_GENERAL;
			msg->Header.Id = *_identification;

			void* msgBuffer;
			int messageLen = HermEsb::Messages::MessageBusFactory::CreateRouterMessage(msg, &msgBuffer, _useCompression);
			((OutBoundConnectionPoint*)_connectionPoint)->Send(msgBuffer, messageLen, priority);
			free(msgBuffer);
			delete(msg);
		}

		void OutputGateway::SendError(ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen)
		{
			this->_sendError(*this, exception, message, messageLen);
		}
	}
}