#ifndef FAKEIMESSAGE_H_
#define FAKEIMESSAGE_H_
#define BOOST_ALL_DYN_LINK
#include <stdio.h>
#include <string.h>
#include <gtest/gtest.h>
#include <hermesb.h>
#include <boost/property_tree/ptree.hpp>
#include <boost/foreach.hpp>
#include <boost/property_tree/json_parser.hpp>
#include <rapidjson/writer.h>
#include <boost/date_time/posix_time/posix_time.hpp>
#include <rapidjson/stringbuffer.h>

using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;
using namespace HermEsb::Gateways;
using namespace std;
using boost::property_tree::ptree;
using namespace rapidjson;

class FakeIMessage : public HermEsb::Messages::IMessage
{
	public:
		string MiMensaje;
		virtual ptree Serialize()
		{
			ptree pt;
			pt.put("MiMensaje", MiMensaje);
			return pt;
		}

		virtual void Serialize(Writer<StringBuffer>& writer)
		{
			writer.StartObject();
			writer.String("MiMensaje");
			writer.String(MiMensaje.c_str(), (SizeType)MiMensaje.size());

			writer.EndObject();
		}
};

class IMessageBasic : public HermEsb::Messages::IMessage
{
public:
	ptime Fecha;
	string Nombre;
	virtual ptree Serialize()
		{
			ptree pt;
			pt.put("Fecha", boost::posix_time::to_iso_extended_string(Fecha));
			pt.put("Nombre", Nombre);
			return pt;
		}

	virtual	void Serialize(Writer<StringBuffer>& writer)
	{
		writer.StartObject();
		writer.String("Fecha");
		string fecha = boost::posix_time::to_iso_extended_string(Fecha);
		writer.String(fecha.c_str(), (SizeType)fecha.size());
		writer.String("Nombre");
		writer.String(Nombre.c_str(),(SizeType)Nombre.size());
		writer.EndObject();
	}
};

#endif