/*
 * ConnectionPointFake.h
 *
 *  Created on: 14/02/2012
 *      Author: sergio
 */

#ifndef CONNECTIONPOINTFAKE_H_
#define CONNECTIONPOINTFAKE_H_
#include <hermesb.h>

using namespace HermEsb::Channels;

namespace HermEsbTest
{
    namespace Channels
    {
        namespace Connections
        {
            class ConnectionPointFake : public OutBoundConnectionPoint
            {
                public:
                    ConnectionPointFake(int reintentos, int reintentosEnvios);
                    virtual ~ConnectionPointFake();

                protected:
                    virtual void ConnectPoint() throw(ConnectException);
                    virtual void SendMessage(const void* message, int messageLen);
                    virtual void ClosePoint();

                private:
                    int _reintentos;
                    int _numConnections;
                    int _reintentosEnvios;
                    int _numreintentosEnvios;

            };
        }
    }
}

#endif /* CONNECTIONPOINTFAKE_H_ */
