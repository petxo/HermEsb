#ifndef MESSAGES_IMESSAGE_H_
#define MESSAGES_IMESSAGE_H_
#include "../global.h"
#include <boost/property_tree/ptree.hpp>
#include <rapidjson/writer.h>
#include <rapidjson/stringbuffer.h>

using boost::property_tree::ptree;
using namespace rapidjson;
namespace HermEsb
{
    namespace Messages
    {
		class HERMESB_API IMessage
		{
		public:
			virtual ptree Serialize() = 0;
			
			virtual void Serialize(Writer<StringBuffer>& writer)
			{
			}
			//virtual void Serialize(Writer& writer) = 0;
		};
	}
}

#endif