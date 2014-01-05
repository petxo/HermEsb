#include "MessageBus.h"
#include <boost/uuid/uuid.hpp>            
#include <boost/uuid/uuid_generators.hpp> 
#include <boost/uuid/uuid_io.hpp> 
using namespace rapidjson;
namespace HermEsb
{
	namespace Messages
	{
		boost::uuids::random_generator guidGenerator;

		MessageBus::MessageBus()
		{
		}
		MessageBus::~MessageBus()
		{
		}
		void MessageBus::Serialize(Writer<StringBuffer>& writer)
		{
			writer.StartObject();
			writer.String("Body");
			writer.String(Body.c_str(),(SizeType)Body.size());
			writer.String("Header");
			Header.Serialize(writer);
			writer.EndObject();
		}

		void MessageBus::Deserialize(Value& document)
		{
			Body = document["Body"].GetString();
			Header.Deserialize(document["Header"]);
		}

		Identification::Identification()
		{
		}
		Identification::~Identification()
		{
		}

		void Identification::Serialize(Writer<StringBuffer>& writer)
		{
			writer.StartObject();
			writer.String("Id");
			writer.String(Id.c_str(),(SizeType)Id.size());
			writer.String("Type");
			writer.String(Type.c_str(),(SizeType)Type.size());
			writer.EndObject();
		}

		void Identification::Deserialize(Value& document)
		{
			Id = document["Id"].GetString();
			Type = document["Type"].GetString();
		}
		MessageHeader::MessageHeader()
		{
			MessageId = guidGenerator();
		}

		MessageHeader::~MessageHeader()
		{
		}
		void MessageHeader::Serialize(Writer<StringBuffer>& writer)
		{			
			writer.StartObject();
			writer.String("MessageId");
			string uid = boost::uuids::to_string(MessageId);
			writer.String(uid.c_str(),(SizeType)uid.size());
			writer.String("BodyType");
			writer.String(BodyType.c_str(),(SizeType)BodyType.size());
			writer.String("EncodingCodePage");
			writer.Int(EncodingCodePage);
			writer.String("Priority");
			writer.Int(Priority);
			writer.String("ReinjectionNumber");
			writer.Int(ReinjectionNumber);
			writer.String("Type");
			writer.Int(Type);
			writer.String("CreatedAt");
			string createdAt = to_iso_extended_string(CreatedAt);
			writer.String(createdAt.c_str(), (SizeType)createdAt.size());
			writer.String("IdentificationService");
			Identification.Serialize(writer);

			if (!CallContext.empty())
			{
				writer.String("CallContext");
				writer.StartObject();
				Session::iterator p;
				ptree ptSession;
				for(p = CallContext.begin(); p!=CallContext.end(); ++p)
				{
					writer.String(p->first.c_str(), p->first.size());
					writer.String(p->second.c_str(), p->second.size());
				}
				writer.EndObject();
			}
			CallerContextStack cp(CallStack._Get_container());
			if(!cp.empty())
			{
				writer.String("CallStack");
				writer.StartArray();
				while(!cp.empty())
				{
					cp.top().Serialize(writer);
					cp.pop();
				}
				writer.EndArray();
			}
			writer.EndObject();
	
		}

		void MessageHeader::Deserialize(Value& document)
		{
			boost::uuids::string_generator gen;
			MessageId = gen(document["MessageId"].GetString());
			BodyType = document["BodyType"].GetString();
			EncodingCodePage = document["EncodingCodePage"].GetInt();
			Priority = document["Priority"].GetInt();
			ReinjectionNumber = document["ReinjectionNumber"].GetInt();
			Type = document["Type"].GetInt();
			CreatedAt = HermEsb::Utils::Time::IsoTime::from_iso_extended_string(document["CreatedAt"].GetString());
			this->Identification.Deserialize(document["IdentificationService"]);

			Value::MemberIterator itr = document["CallContext"].MemberBegin();
			while (itr != document["CallContext"].MemberEnd())
			{
				CallContext.insert(SessionPair(string(itr->name.GetString()), string(itr->value.GetString())));
				++itr;
			}

			for (SizeType i = document["CallStack"].Size(); i > 0; i--)
			{
				CallerContext cs;
				cs.Deserialize(document["CallStack"][i - 1]);
				CallStack.push(cs);
			}
		}

		CallerContext::CallerContext()
		{
		}
		CallerContext::~CallerContext()
		{
		}

		void CallerContext::Serialize(Writer<StringBuffer>& writer)
		{
			writer.StartObject();
			writer.String("Identification");
			this->Identification.Serialize(writer);
			if(!this->Session.empty())
			{
				writer.String("Session");
				writer.StartObject();
				Session::iterator p;
				ptree ptSession;
				for(p = this->Session.begin(); p!= this->Session.end(); ++p)
				{
					writer.String(p->first.c_str(), p->first.size());
					writer.String(p->second.c_str(), p->second.size());
				}
				writer.EndObject();
			}
			writer.EndObject();
		}

		void CallerContext::Deserialize(Value& document)
		{
			Identification.Deserialize(document["Identification"]);
			
			if (document.HasMember("Session"))
			{
				Value::MemberIterator itr = document["Session"].MemberBegin();
				while (itr != document["Session"].MemberEnd())
				{
					this->Session.insert(SessionPair(string(itr->name.GetString()), string(itr->value.GetString())));
					++itr;
				}
			}
		}
	}
}