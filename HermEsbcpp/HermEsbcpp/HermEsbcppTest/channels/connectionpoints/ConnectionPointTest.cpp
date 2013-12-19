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
#include "ConnectionPointFake.h"

using namespace HermEsb::Channels;
using namespace HermEsbTest::Channels::Connections;

bool _errorConnectionCalled = false;
bool _sendMessageErrorCalled = false;
const void* _message = NULL;

void SendMessageError(OutBoundConnectionPoint& sender,
        ConnectException& exception, const void* message)
{
    _sendMessageErrorCalled = true;
    _message = message;
}

class FakeHandler
{
    public:
        EventConnection connection;
        bool errorConnectionCalled;
		bool sendErrorCalled;

        void ErrorConnection(ConnectionPoint& sender,
                ConnectException& exception)
        {
            errorConnectionCalled = true;
        }

		void SendError(ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen)
		{
			sendErrorCalled = true;
		}

        FakeHandler(ConnectionPointFake* test)
        {
            EVENT_BIND2(connection, test->ConnectionError, &FakeHandler::ErrorConnection);
			EVENT_BIND4(connection, test->OnSendError, &FakeHandler::SendError);
            this->errorConnectionCalled = false;
			this->sendErrorCalled = false;
        }
};

TEST(ConnectionPointTest, ConnectToPointFailed)
{
    ConnectionPointFake* test = new ConnectionPointFake(12, 0);
    FakeHandler *handler = new FakeHandler(test);

    ASSERT_NO_THROW(test->Connect());
    ASSERT_TRUE(handler->errorConnectionCalled);

    delete (test);
}

TEST(ConnectionPointTest, ConnectToPointSuccess)
{
    ConnectionPointFake* test = new ConnectionPointFake(3, 0);
    FakeHandler *handler = new FakeHandler(test);

    ASSERT_NO_THROW(test->Connect());
    ASSERT_FALSE(handler->errorConnectionCalled);

    delete (test);
}

TEST(ConnectionPointTest, SendMessageSuccess)
{
    _sendMessageErrorCalled = false;
    ConnectionPointFake* test = new ConnectionPointFake(3, 3);
	FakeHandler *handler = new FakeHandler(test);
    //test->ConnectionPoint::OnConnectionError = ErrorConnection;
    

    char* buffer = "Message";

    ASSERT_NO_THROW(test->Send(buffer, sizeof(buffer)));
	ASSERT_FALSE(handler->sendErrorCalled);

    delete (test);
}

TEST(ConnectionPointTest, SendMessageFailed)
{
    _sendMessageErrorCalled = false;
    ConnectionPointFake* test = new ConnectionPointFake(12, 3);
	FakeHandler *handler = new FakeHandler(test);
    
    char* buffer = "Message";

    ASSERT_NO_THROW(test->Send(buffer, sizeof(buffer)));
	ASSERT_TRUE(handler->sendErrorCalled);

    delete (test);
}

