#include <stdio.h>
#include <string.h>
#include <gtest/gtest.h>
#include <hermesb.h>


using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;
using namespace HermEsb::Gateways;
using namespace std;
using namespace HermEsb::Messages;


TEST(InputGatewayTest, ConnectSuccess)
{
	RabbitInBoundConnectionPoint* cp = new RabbitInBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey", "guest", "guest", "QueueTestCpp", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador";
	id.Type = "Publicador";

	InputGateway* gateway = new InputGateway(&id, cp, true);

	ASSERT_NO_THROW(gateway->Connect());
	gateway->Start();

	gateway->Close();
	delete (gateway);
}