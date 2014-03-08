#define BOOST_ALL_DYN_LINK

#include <stdio.h>
#include <string.h>
#include <gtest/gtest.h>
#include <hermesb.h>
#ifdef WIN32
#include <Windows.h>
#endif

using namespace HermEsb::Channels::Rabbit;
using namespace HermEsb::Core::Timers;

class FakeRabbitInBoundHandler
{
	public:
		EventConnection connection;
		bool _receivedMessage;
		int _messageCount;

		void ReceivedMessage(HermEsb::Channels::InBoundConnectionPoint& sender, void* message, int messageLen)
		{
			_receivedMessage = true;
			_messageCount++;
			//char* msg = (char*)malloc(messageLen + 1);
			//memset(msg, 0, messageLen + 1);
			//memcpy(msg, message, messageLen);
			//printf((char*) msg);
		}


		FakeRabbitInBoundHandler(HermEsb::Channels::InBoundConnectionPoint* test)
		{
			this->_receivedMessage = false;
			_messageCount = 0;
			EVENT_BIND3(connection, test->OnMessageReceived, &FakeRabbitInBoundHandler::ReceivedMessage);
		}
};

TEST(RabbitInBoundConnectionPointTest, ReceivedMessageSuccess)
{
	RabbitInBoundConnectionPoint* test = new RabbitInBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", "QueueTestCpp", new InstantReconnectionTimer());
	RabbitOutBoundConnectionPoint* output = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());

	ASSERT_NO_THROW(test->Connect());
	ASSERT_NO_THROW(test->Start());
	FakeRabbitInBoundHandler *handler = new FakeRabbitInBoundHandler(test);
#ifdef WIN32
	Sleep(1000);
#else
	sleep(1000);
#endif


	output->Connect();

	void* message = malloc(512);
	memset(message,66,512);
	output->Send(message, 512, 0);

	while(!handler->_receivedMessage)
	{
#ifdef WIN32
		Sleep(1000);
#else
		sleep(1000);
#endif

	}

	ASSERT_TRUE(handler->_receivedMessage);
	test->Stop();
	test->Close();
	output->Close();
	delete (test);
	delete (output);
}

TEST(RabbitInBoundConnectionPointTest, ReceivedMessageMassiveSuccess)
{
	RabbitInBoundConnectionPoint* test = new RabbitInBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", "QueueTestCpp", new InstantReconnectionTimer());
	RabbitOutBoundConnectionPoint* output = new RabbitOutBoundConnectionPoint("localhost", 5672, "HermesbCppExch", "TestKey","guest", "guest", new InstantReconnectionTimer());

	ASSERT_NO_THROW(test->Connect());
	ASSERT_NO_THROW(test->Start());
	FakeRabbitInBoundHandler *handler = new FakeRabbitInBoundHandler(test);
#ifdef WIN32
	Sleep(1000);
#else
	sleep(1000);
#endif

	output->Connect();

	void* message = malloc(512);
	memset(message,66,512);
	for(int i=0; i<100000; i++)
		output->Send(message, 512, 0);

	while(handler->_messageCount < 100000)
	{
#ifdef WIN32
		Sleep(1000);
#else
		sleep(1000);
#endif
	}

	ASSERT_TRUE(handler->_receivedMessage);
	test->Stop();
	test->Close();
	output->Close();
	delete (test);
	delete (output);
}
