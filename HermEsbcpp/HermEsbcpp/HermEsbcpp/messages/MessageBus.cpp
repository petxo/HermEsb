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

		ptree MessageBus::ToJson()
		{
			ptree pt;
			pt.put("Body", Body);
			//pt.put_child("Header", Header.ToJson());
			return pt;
	
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

		void MessageBus::Deserialize(Document& document)
		{
			Body = document["Body"].GetString();
			/*Header.Deserialize(document.F)*/
		}

		void MessageBus::FromJson(ptree pt)
		{
			Body = pt.get<string>("Body");
			Header.FromJson(pt.get_child("Header"));
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
		void Identification::Deserialize(Document& document)
		{

		}
		ptree Identification::ToJson()
		{
			ptree pt;
			pt.put("Id", Id);
			pt.put("Type", Type);
			return pt;
		}

		void Identification::FromJson(ptree pt)
		{
			Id = pt.get<string>("Id");
			Type = pt.get<string>("Type");
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

		void MessageHeader::Deserialize(Document& document)
		{

		}
		ptree MessageHeader::ToJson()
		{
			ptree ptHeader;
			ptHeader.put("MessageId", boost::uuids::to_string(MessageId));
			ptHeader.put("BodyType", BodyType);
			ptHeader.put<int>("EncodingCodePage", EncodingCodePage);
			ptHeader.put<int>("Priority", Priority);
			ptHeader.put<int>("ReinjectionNumber", ReinjectionNumber);
			ptHeader.put<int>("Type", Type);
			ptHeader.put_child("IdentificationService", Identification.ToJson());
			ptHeader.put("CreatedAt", to_iso_extended_string(CreatedAt));

			if (!CallContext.empty())
			{
				Session::iterator p;
				ptree ptSession;
				for(p = CallContext.begin(); p!=CallContext.end(); ++p)
				{
					ptSession.put(p->first, p->second);
				}
				ptHeader.put_child("CallContext", ptSession);
			}
			ptree ptCallStack;
	
			CallerContextStack cp(CallStack._Get_container());
			if(!cp.empty())
			{
				while(!cp.empty())
				{
					ptCallStack.push_back(std::make_pair("", cp.top().ToJson()));
					cp.pop();
				}
				ptHeader.put_child("CallStack", ptCallStack);
			}
			

			return ptHeader;
		}

		void MessageHeader::FromJson(ptree pt)
		{
			BodyType = pt.get<string>("BodyType");
			EncodingCodePage = pt.get<int>("EncodingCodePage");
			Priority = pt.get<int>("Priority");
			ReinjectionNumber = pt.get<int>("ReinjectionNumber");
			Type = pt.get<int>("Type");
			Identification.FromJson(pt.get_child("IdentificationService"));

			CreatedAt =  HermEsb::Utils::Time::IsoTime::from_iso_extended_string(pt.get<string>("CreatedAt"));
			BOOST_FOREACH(const ptree::value_type &v, pt.get_child("CallContext")) 
			{
				CallContext.insert(SessionPair (string(v.first.data()), string(v.second.data())));
			}
	
			BOOST_REVERSE_FOREACH(const ptree::value_type &v, pt.get_child("CallStack")) 
			{
				CallerContext cs;
				cs.FromJson(v.second);
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

		void CallerContext::Deserialize(Document& document)
		{

		}
		ptree CallerContext::ToJson()
		{
			ptree ptHeader;
			ptHeader.put_child("IdentificationService", Identification.ToJson());

			if(!this->Session.empty())
			{
				Session::iterator p;
				ptree ptSession;
				for(p = this->Session.begin(); p!= this->Session.end(); ++p)
				{
					ptSession.put(p->first, p->second);
				}
				ptHeader.put_child("Session", ptSession);
			}
			return ptHeader;
		}

		void CallerContext::FromJson(ptree pt)
		{
			Identification.FromJson(pt.get_child("IdentificationService"));
			BOOST_FOREACH(const ptree::value_type &v, pt.get_child("Session")) 
			{
				this->Session.insert(SessionPair (string(v.first.data()), string(v.second.data())));
			}
		}
	}
}