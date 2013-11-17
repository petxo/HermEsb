/*
 * OutBoundConnectionPoint.cpp
 *
 *  Created on: 22/02/2012
 *      Author: sergio
 */

#include "OutBoundConnectionPoint.h"

namespace HermEsb
{
    namespace Channels
    {
        OutBoundConnectionPoint::OutBoundConnectionPoint(
                IReconnectionTimer* reconnectionTimer) :
                ConnectionPoint(reconnectionTimer)
        {

        }

        OutBoundConnectionPoint::~OutBoundConnectionPoint()
        {

        }

        void OutBoundConnectionPoint::Send(const void* message, int messageLen)
        {
            while (true)
            {
                try
                {
                    this->SendMessage(message, messageLen);
                    return;
                } catch (ConnectException& connException)
                {
                    //Cerrar la conexion y volver a conectar
                    this->ClosePoint();
                    try
                    {
                        this->Reconnect();
                    } catch (ConnectException& exception)
                    {
                        this->InvokeOnSendMessageError(exception, message);
                        break;
                    }

                }
            }
        }

        void OutBoundConnectionPoint::InvokeOnSendMessageError(
                ConnectException& exception, const void* message)
        {
            if (this->OnSendMessageError != NULL)
                this->OnSendMessageError(*this, exception, message);
        }
    } /* namespace Channels */
} /* namespace HermEsb */
