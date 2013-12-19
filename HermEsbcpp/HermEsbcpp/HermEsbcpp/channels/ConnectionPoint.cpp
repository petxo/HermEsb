/*
 * ConnectionPoint.cpp
 *
 *  Created on: 03/02/2012
 *      Author: sergio
 */

#include "ConnectionPoint.h"
#include <stdio.h>
#include "../global.h"

using namespace std;

namespace HermEsb
{
    namespace Channels
    {
        ConnectionPoint::ConnectionPoint(IReconnectionTimer* reconnectionTimer, int maxReconnections)
        {
            this->_maxReconnections = maxReconnections;
            this->_reconnectionNumber = 0;
            this->_reconnectionTimer = reconnectionTimer;
            this->_isConnected = false;
        }

        ConnectionPoint::~ConnectionPoint()
        {
            this->_connectionError.disconnect_all_slots();
            delete (this->_reconnectionTimer);
        }

        void ConnectionPoint::Connect()
        {
            try
            {
                this->Reconnect();
            } catch (ConnectException& connException)
            {
                this->InvokeOnConnectionError(connException);
            }
        }

        void ConnectionPoint::Reconnect() throw (ConnectException)
        {
            //TODO: Bloquear para multithreading
            this->_reconnectionNumber = 0;
            this->_reconnectionTimer->Reset();

            _isConnected = false;
            while (true)
            {
                try
                {
                    this->ConnectPoint();
                    _isConnected = true;
                    return;
                } catch (ConnectException& connException)
                {
                    if ((this->_maxReconnections != INFINITE_RECONNECTIONS) && (this->_reconnectionNumber >= this->_maxReconnections))
                    {
                        LOG(INFO)<< "Error Connect to Point, se ha alcanzado el limite de reconexiones "
                        << this->_maxReconnections;
                        throw;
                    }
                    ++this->_reconnectionNumber;
                    DLOG(INFO)
                    << "Error Connect to Point, reconnection number "
                    << this->_reconnectionNumber;
                    this->_reconnectionTimer->Wait();
                    //TODO: ¿Hay que hacer un delete de la excepcion?

                }
            }
        }

        bool ConnectionPoint::IsConnected()
        {
            return this->_isConnected;
        }

        void ConnectionPoint::Close()
        {
            if (this->_isConnected)
			{
				this->BeforeClose();
                this->ClosePoint();
				_isConnected = false;
			}
        }

		void ConnectionPoint::BeforeClose()
		{
		}

        void ConnectionPoint::InvokeOnConnectionError(
                ConnectException& exception)
        {
            this->_connectionError(*this, exception);
        }
    } /* namespace Channels */
} /* namespace HermEsb */
