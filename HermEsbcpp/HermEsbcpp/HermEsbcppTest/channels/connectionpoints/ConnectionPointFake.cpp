/*
 * ConnectionPointFake.cpp
 *
 *  Created on: 14/02/2012
 *      Author: sergio
 */

#include "ConnectionPointFake.h"
using namespace HermEsb::Core::Timers;

namespace HermEsbTest
{
    namespace Channels
    {
        namespace Connections
        {

            ConnectionPointFake::ConnectionPointFake(int reintentos, int reintentosEnvios) :
                        OutBoundConnectionPoint(new InstantReconnectionTimer(), 10)
            {
                this->_reintentos = reintentos;
                this->_numConnections = 0;
                this->_reintentosEnvios = reintentosEnvios;
                this->_numreintentosEnvios = 0;
            }

            ConnectionPointFake::~ConnectionPointFake()
            {
            }

            void ConnectionPointFake::ConnectPoint() throw (ConnectException)
            {
                this->_numConnections++;
                if (this->_numConnections < this->_reintentos)
                    throw ConnectException("Test de Error");
            }

            void ConnectionPointFake::SendMessage(const void* message, int messageLen, int priority)
            {
                this->_numreintentosEnvios++;
                if (this->_numreintentosEnvios < this->_reintentosEnvios)
                    throw ConnectException("Test de Error");
            }

            void ConnectionPointFake::ClosePoint()
            {

            }

        }
    }
}
