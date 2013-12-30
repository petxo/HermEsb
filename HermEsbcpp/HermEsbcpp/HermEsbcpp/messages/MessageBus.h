#ifndef MESSAGES_IDENTIFICATION_H_
#define MESSAGES_IDENTIFICATION_H_
#include "../global.h"
#include <stdio.h>
#include <stdlib.h>
#include <vector>
#include <stack>
#include <map>
#include <boost/property_tree/ptree.hpp>
#include <boost/foreach.hpp>
#include <boost/property_tree/json_parser.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>
#include <boost/uuid/uuid.hpp>
#include "../utils/timeutils.hpp"
#include <rapidjson/writer.h>
#include <rapidjson/stringbuffer.h>
#include "rapidjson/document.h"

using boost::property_tree::ptree;
using namespace std;
using namespace boost::posix_time;
using namespace rapidjson;

#define MESSAGETYPE_GENERAL 0
#define MESSAGETYPE_REPLY 1

namespace HermEsb
{
	namespace Messages
	{
		typedef std::map<std::string, std::string> Session;
		typedef std::pair<std::string, std::string> SessionPair;

		
		class HERMESB_API Identification
		{
		public:
			Identification();
			~Identification();
			string Id;
			string Type;
			ptree ToJson();
			void FromJson(ptree pt);
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Document& document);
		};

		class HERMESB_API CallerContext
		{
		public:
			CallerContext();
			~CallerContext();
			Identification Identification;
			Session Session;

			ptree ToJson();
			void FromJson(ptree pt);
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Document& document);
		};

		typedef std::stack<CallerContext> CallerContextStack;

		class HERMESB_API MessageHeader
		{
		public:
			MessageHeader();
			~MessageHeader();
			boost::uuids::uuid MessageId;
			string BodyType;
			int EncodingCodePage;
			int ReinjectionNumber;
			int Priority;
			int Type;
			Identification Identification;
			Session CallContext;
			CallerContextStack CallStack;
			ptime CreatedAt;

			ptree ToJson();
			void FromJson(ptree pt);
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Document& document);
		};

		class HERMESB_API MessageBus
		{
		public:
			MessageBus();
			~MessageBus();
			MessageHeader Header;
			string Body;
			ptree ToJson();
			void FromJson(ptree pt);
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Document& document);
		};

	}
}


#endif