/*
 * InBoundConnectionPoint.cpp
 *
 *  Created on: 22/02/2012
 *      Author: sergio
 */

#include "InBoundConnectionPoint.h"

namespace HermEsb
{
    namespace Channels
    {
        InBoundConnectionPoint::InBoundConnectionPoint(
                IReconnectionTimer* reconnectionTimer) :
                ConnectionPoint(reconnectionTimer)
        {

        }

        InBoundConnectionPoint::~InBoundConnectionPoint()
        {

        }

        void InBoundConnectionPoint::BeginReceive()
        {
            //TODO: Crear un hilo para iniciar la recepcion de mensajes
        }

        void InBoundConnectionPoint::EndReceive()
        {
            //TODO: Parar el hilo de recepcion de mensajes
        }
    } /* namespace Channels */
} /* namespace HermEsb */
