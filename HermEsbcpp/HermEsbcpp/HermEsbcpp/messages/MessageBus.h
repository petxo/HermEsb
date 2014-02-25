#ifndef MESSAGES_IDENTIFICATION_H_
#define MESSAGES_IDENTIFICATION_H_
#include "../global.h"
#include <stdio.h>
#include <stdlib.h>
#include <vector>
#include <stack>
#include <map>
#include <boost/foreach.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>
#include <boost/uuid/uuid.hpp>
#include <rapidjson/writer.h>
#include <rapidjson/stringbuffer.h>
#include <rapidjson/document.h>
#include "../utils/timeutils.hpp"

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
			virtual ~Identification();
			string Id;
			string Type;
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Value& document);
		};

		class HERMESB_API CallerContext
		{
		public:
			CallerContext();
			virtual ~CallerContext();
			Identification Id;
			Session Storage;
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Value& document);
		};

		typedef std::stack<CallerContext> CallerContextStack;

		class HERMESB_API MessageHeader
		{
		public:
			MessageHeader();
			virtual ~MessageHeader();
			boost::uuids::uuid MessageId;
			string BodyType;
			int EncodingCodePage;
			int ReinjectionNumber;
			int Priority;
			int Type;
			Identification Id;
			Session CallContext;
			CallerContextStack CallStack;
			ptime CreatedAt;
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Value& document);
		};

		class HERMESB_API MessageBus
		{
		public:
			MessageBus();
			virtual ~MessageBus();
			MessageHeader Header;
			string Body;
			virtual void Serialize(Writer<StringBuffer>& writer);
			virtual void Deserialize(Value& document);
		};

	}
}


#endif
