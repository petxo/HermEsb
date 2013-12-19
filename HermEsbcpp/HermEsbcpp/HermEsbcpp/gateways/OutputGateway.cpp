#include "OutputGateway.h"
#include <stdio.h>
#include <stdlib.h>
#include "../messages/MessageBus.h"
#include "../messages/MessageBusFactory.h"
#include <boost/property_tree/ptree.hpp>
#include <boost/foreach.hpp>
#include <boost/property_tree/json_parser.hpp>
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"

using boost::property_tree::ptree;
using namespace rapidjson;

namespace HermEsb
{
    namespace Gateways
    {
		OutputGateway::OutputGateway(Identification* identification, OutBoundConnectionPoint *outBoundConnectionPoint, bool useCompression) : BaseOutputGateway(outBoundConnectionPoint)
		{
			_identification = identification;
			_useCompression = useCompression;
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
			msg->Header.Identification = *_identification;

			void* msgBuffer;
			int messageLen = HermEsb::Messages::MessageBusFactory::CreateRouterMessage(msg, &msgBuffer, _useCompression);
			_outBoundConnectionPoint->Send(msgBuffer, messageLen, priority);
			free(msgBuffer);
			delete(msg);
		}
	}
}