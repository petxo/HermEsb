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
#include "FakeIMessage.hpp"


using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;
using namespace HermEsb::Gateways;
using namespace std;
using namespace HermEsb::Messages;


TEST(OutputGatewayTest, ConnectSuccess)
{
    RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador";
	id.Type = "Publicador";

	OutputGateway* gateway = new OutputGateway(&id, cp, true);

    ASSERT_NO_THROW(gateway->Connect());

	gateway->Close();
    delete (gateway);
}

TEST(OutputGatewayTest, SendMessageSuccess)
{
    //RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());
    RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermEsbSamples.Exch", "inputBasicBusSampleKey","guest", "guest", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador C++ - 1";
	id.Type = "Publicador C++";

	OutputGateway* gateway = new OutputGateway(&id, cp, true);

    ASSERT_NO_THROW(gateway->Connect());
	IMessageBasic msg;
	msg.Nombre = "Manolito Gafotas";
	msg.Fecha = boost::posix_time::microsec_clock::universal_time();
	gateway->Publish("BasicSampleContracts.IMessageBasic,BasicSampleContracts", &msg);
	gateway->Close();
    delete (gateway);
}


TEST(OutputGatewayTest, SendMassiveMessageSuccess)
{
    //RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());
    RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermEsbSamples.Exch", "inputBasicBusSampleKey","guest", "guest", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador C++ - 1";
	id.Type = "Publicador C++";

	OutputGateway* gateway = new OutputGateway(&id, cp, true);

    ASSERT_NO_THROW(gateway->Connect());
	
	for(int i = 0; i < 400000; i++)
	{
		IMessageBasic msg;
		msg.Nombre = "Manolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito GafotasManolito Gafotas";
		msg.Fecha = boost::posix_time::microsec_clock::universal_time();
		gateway->Publish("BasicSampleContracts.IMessageBasic,BasicSampleContracts", &msg);
	}
	gateway->Close();
    delete (gateway);
}