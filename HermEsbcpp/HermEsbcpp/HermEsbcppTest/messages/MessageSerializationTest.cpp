/*
 * ConnectionPointTest.cpp
 *
 *  Created on: 13/02/2012
 *      Author: sergio
 */

#include <stdio.h>
#include <string.h>
#include <gtest/gtest.h>
#include <hermesb.h>
#include "../gateways/FakeIMessage.hpp"
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/document.h"

using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;
using namespace HermEsb::Gateways;
using namespace std;
using namespace HermEsb::Messages;
using namespace rapidjson;


TEST(MessageSerialization, MessageBusSerializer)
{
	HermEsb::Messages::Identification id;
	id.Id = "Id";
	id.Type = "Type";
	HermEsb::Messages::MessageBus *msg = HermEsb::Messages::MessageBusFactory::CreateMessageBus(&id, "key", "body");

	StringBuffer sbMessage;
	Writer<StringBuffer> writer(sbMessage);
	msg->Serialize(writer);
	
	Document document;
	ASSERT_FALSE(document.Parse<0>(sbMessage.GetString()).HasParseError());
	ASSERT_TRUE(document.HasMember("Body"));
	ASSERT_TRUE(strcmp(document["Body"].GetString(), "body") == 0);
	MessageBus d;
	d.Deserialize(document);
	ASSERT_TRUE(d.Body.compare("body")==0);
	ASSERT_TRUE(d.Header.BodyType.compare("key")==0);
	delete(msg);
}
