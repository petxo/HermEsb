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
        void ErrorConnection(ConnectionPoint& sender,
                ConnectException& exception)
        {
            errorConnectionCalled = true;
        }


        FakeHandler(ConnectionPointFake* test)
        {
            EVENT_BIND2(connection, test->ConnectionError, &FakeHandler::ErrorConnection);
            this->errorConnectionCalled = false;
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

    //test->ConnectionPoint::OnConnectionError = ErrorConnection;
    test->OnSendMessageError = SendMessageError;

    char* buffer = "Message";

    ASSERT_NO_THROW(test->Send(buffer, sizeof(buffer)));
    ASSERT_FALSE(_sendMessageErrorCalled);

    delete (test);
}

TEST(ConnectionPointTest, SendMessageFalied)
{
    _sendMessageErrorCalled = false;
    ConnectionPointFake* test = new ConnectionPointFake(12, 3);

    //test->ConnectionPoint::OnConnectionError = ErrorConnection;
    test->OnSendMessageError = SendMessageError;

    char* buffer = "Message";

    ASSERT_NO_THROW(test->Send(buffer, sizeof(buffer)));
    ASSERT_TRUE(_sendMessageErrorCalled);
    ASSERT_EQ(buffer, _message);

    delete (test);
}

