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
	RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermEsbSamples.Exch", "inputBasicBusSampleKey","guest", "guest", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador C++ - 1";
	id.Type = "Publicador C++";

	OutputGateway* gateway = new OutputGateway(&id, cp, true);

	ASSERT_NO_THROW(gateway->Connect());
	IMessageBasic msg;
	msg.Nombre = "Manolito Gafotas";
	msg.Fecha = boost::posix_time::microsec_clock::universal_time();
	gateway->Publish(string("BasicSampleContracts.IMessageBasic,BasicSampleContracts"), &msg);
	gateway->Close();
	delete (gateway);
}


TEST(OutputGatewayTest, SendMassiveMessageSuccess)
{
	RabbitOutBoundConnectionPoint* cp = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermEsbSamples.Exch", "inputBasicBusSampleKey","guest", "guest", new InstantReconnectionTimer());
	Identification id;
	id.Id = "Publicador C++ - 1";
	id.Type = "Publicador C++";

	OutputGateway* gateway = new OutputGateway(&id, cp, true);

	ASSERT_NO_THROW(gateway->Connect());
	
	for(int i = 0; i < 100000; i++)
	{
		IMessageBasic msg;
		msg.Nombre = 
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus eu orci sed elit porttitor commodo. In a elit ullamcorper sem fringilla vulputate. Integer dignissim et elit et tincidunt. Praesent odio arcu, pharetra a eleifend a, congue nec ante. Phasellus dictum urna quam, et porttitor risus facilisis ut. Ut venenatis rhoncus diam eu ultricies. Praesent sed odio nec felis congue molestie."
			"Vivamus condimentum ligula eget lorem eleifend, ac euismod est aliquam. Integer ac interdum metus, at egestas risus. Curabitur consectetur sem ut nulla malesuada commodo. Vivamus mauris ipsum, tempus dignissim tortor eget, auctor vehicula nulla. Pellentesque cursus enim ac lorem malesuada hendrerit. Fusce ut sem elit. Curabitur convallis nibh nec ligula blandit, quis rhoncus mauris porttitor. Vestibulum sit amet turpis ac nisl dictum volutpat nec vel massa. Vivamus ornare leo orci, id convallis quam dignissim et. Phasellus sit amet faucibus felis. Nulla sollicitudin est in ipsum gravida, eget pharetra est luctus. Class sed."
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus eu orci sed elit porttitor commodo. In a elit ullamcorper sem fringilla vulputate. Integer dignissim et elit et tincidunt. Praesent odio arcu, pharetra a eleifend a, congue nec ante. Phasellus dictum urna quam, et porttitor risus facilisis ut. Ut venenatis rhoncus diam eu ultricies. Praesent sed odio nec felis congue molestie."
			"Vivamus condimentum ligula eget lorem eleifend, ac euismod est aliquam. Integer ac interdum metus, at egestas risus. Curabitur consectetur sem ut nulla malesuada commodo. Vivamus mauris ipsum, tempus dignissim tortor eget, auctor vehicula nulla. Pellentesque cursus enim ac lorem malesuada hendrerit. Fusce ut sem elit. Curabitur convallis nibh nec ligula blandit, quis rhoncus mauris porttitor. Vestibulum sit amet turpis ac nisl dictum volutpat nec vel massa. Vivamus ornare leo orci, id convallis quam dignissim et. Phasellus sit amet faucibus felis. Nulla sollicitudin est in ipsum gravida, eget pharetra est luctus. Class sed."
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus eu orci sed elit porttitor commodo. In a elit ullamcorper sem fringilla vulputate. Integer dignissim et elit et tincidunt. Praesent odio arcu, pharetra a eleifend a, congue nec ante. Phasellus dictum urna quam, et porttitor risus facilisis ut. Ut venenatis rhoncus diam eu ultricies. Praesent sed odio nec felis congue molestie."
			"Vivamus condimentum ligula eget lorem eleifend, ac euismod est aliquam. Integer ac interdum metus, at egestas risus. Curabitur consectetur sem ut nulla malesuada commodo. Vivamus mauris ipsum, tempus dignissim tortor eget, auctor vehicula nulla. Pellentesque cursus enim ac lorem malesuada hendrerit. Fusce ut sem elit. Curabitur convallis nibh nec ligula blandit, quis rhoncus mauris porttitor. Vestibulum sit amet turpis ac nisl dictum volutpat nec vel massa. Vivamus ornare leo orci, id convallis quam dignissim et. Phasellus sit amet faucibus felis. Nulla sollicitudin est in ipsum gravida, eget pharetra est luctus. Class sed."
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus eu orci sed elit porttitor commodo. In a elit ullamcorper sem fringilla vulputate. Integer dignissim et elit et tincidunt. Praesent odio arcu, pharetra a eleifend a, congue nec ante. Phasellus dictum urna quam, et porttitor risus facilisis ut. Ut venenatis rhoncus diam eu ultricies. Praesent sed odio nec felis congue molestie."
			"Vivamus condimentum ligula eget lorem eleifend, ac euismod est aliquam. Integer ac interdum metus, at egestas risus. Curabitur consectetur sem ut nulla malesuada commodo. Vivamus mauris ipsum, tempus dignissim tortor eget, auctor vehicula nulla. Pellentesque cursus enim ac lorem malesuada hendrerit. Fusce ut sem elit. Curabitur convallis nibh nec ligula blandit, quis rhoncus mauris porttitor. Vestibulum sit amet turpis ac nisl dictum volutpat nec vel massa. Vivamus ornare leo orci, id convallis quam dignissim et. Phasellus sit amet faucibus felis. Nulla sollicitudin est in ipsum gravida, eget pharetra est luctus. Class sed."
			;
		msg.Fecha = boost::posix_time::microsec_clock::universal_time();
		gateway->Publish("BasicSampleContracts.IMessageBasic,BasicSampleContracts", &msg);
	}
	gateway->Close();
	delete (gateway);
}
