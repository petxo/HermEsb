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
				IReconnectionTimer* reconnectionTimer, int maxReconnections) :
				ConnectionPoint(reconnectionTimer, maxReconnections)
		{
			threadListen = NULL;
		}

		InBoundConnectionPoint::~InBoundConnectionPoint()
		{

		}

		bool InBoundConnectionPoint::OnStart()
		{
			if(threadListen == NULL)
			{
				threadListen = new boost::thread(boost::bind(&InBoundConnectionPoint::Proccess, this));
			}
			return true;
		}

		void InBoundConnectionPoint::OnTerminateStop()
		{
			if(threadListen != NULL)
			{
				threadListen->join();
				threadListen = NULL;
			}
		}
		
		void InBoundConnectionPoint::BeforeClose()
		{
			this->Stop();
		}

		void InBoundConnectionPoint::Proccess()
		{
			while (this->IsRunning()) 
			{
				try
				{
					void* message;
					int messageLen = ListenMessage(&message);
					if (messageLen > 0)
					{
						InvokeOnMessageReceived(message, messageLen);
						free(message);
					}
				}
				catch (ConnectException& connException)
				{
					//Cerrar la conexion y volver a conectar
					if(!this->IsRunning())
						break;

					this->ClosePoint();
					try
					{
						if(this->IsRunning())
							this->Reconnect();
					} catch (ConnectException& exception)
					{
						//TODO: Gestion de errores en la recepcion de mensajes
						//this->InvokeOnSendMessageError(exception, message);
						break;
					}
				}

			}
		}

		void InBoundConnectionPoint::InvokeOnMessageReceived(void* message, int messageLen)
		{
			this->_onMessageReceived(*this, message, messageLen);
		}
	} /* namespace Channels */
} /* namespace HermEsb */
