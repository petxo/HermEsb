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

using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;


TEST(RabbitOutBoundConnectionPointTest, ConnectionPointSuccess)
{
    RabbitOutBoundConnectionPoint* test = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());

    ASSERT_NO_THROW(test->Connect());

	test->Close();
    delete (test);
}

TEST(RabbitOutBoundConnectionPointTest, SendMessageSuccess)
{
    RabbitOutBoundConnectionPoint* test = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());

    ASSERT_NO_THROW(test->Connect());
	void* message = malloc(512);
	memset(message,66,512);
	ASSERT_NO_THROW(test->Send(message, 512));

	test->Close();
    delete (test);
}

TEST(RabbitOutBoundConnectionPointTest, MassiveSendMessageSuccess)
{
    RabbitOutBoundConnectionPoint* test = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());

    ASSERT_NO_THROW(test->Connect());
	void* message = malloc(512);
	memset(message,66,512);
	for(int i = 0; i < 1000; i++)
	{
		ASSERT_NO_THROW(test->Send(message, 512));
	}
	test->Close();
    delete (test);
}