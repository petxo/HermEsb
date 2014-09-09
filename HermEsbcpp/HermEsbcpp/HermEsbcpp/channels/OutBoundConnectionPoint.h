/*
 * OutBoundConnectionPoint.h
 *
 *  Created on: 22/02/2012
 *      Author: sergio
 */

#ifndef OUTBOUNDCONNECTIONPOINT_H_
#define OUTBOUNDCONNECTIONPOINT_H_

#include "ConnectException.h"
#include "ConnectionPoint.h"
#include "../global.h"
#include <boost/thread.hpp>

namespace HermEsb
{
	namespace Channels
	{
		/**
		 * Clase abstracta que implementa un punto de conexion de salida
		 * Es reliable....
		 */
		class HERMESB_API OutBoundConnectionPoint: public ConnectionPoint
		{
			public:
				DELEGATE(void (ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen))SendErrorHandler;
			
			protected:
				SendErrorHandler _sendError;

			public:
				/**
				 * Crea una instancia OutBoundConnectionPoint
				 * @param reconnectionTimer Temporizador de reconexion
				 */
				OutBoundConnectionPoint(IReconnectionTimer* reconnectionTimer, int maxReconnections = INFINITE_RECONNECTIONS);
				virtual ~OutBoundConnectionPoint();

				/**
				 * Envia un mensaje por medio del punto de conexion.
				 * En el caso de producirse un error al enviar, se produce una reconexion
				 * @param message Mensaje a enviar
				 * @param messageLen Longitud del mensaje a enviar
				 * @param priority Prioridad del mensaje a enviar
				 */
				void Send(const void* message, int messageLen, int priority=0);

				/**
				 * Callback de llamada cuando se produce un error durante el envío del
				 * mensaje
				 * @param sender ConnectionPoint que ha producido el error
				 * @param exception Excepcion que se ha producido
				 * @param message Mensaje que se estaba intentado enviar
				 */
				//void (*OnSendMessageError)(OutBoundConnectionPoint& sender,
				//        ConnectException& exception, const void* message);

				EVENT(SendErrorHandler, _sendError, OnSendError);

			protected:

				
				/**
				 * Envia un mensaje por medio del punto de conexión.
				 * @param message Mensaje a enviar
				 * @param messageLen Longitud del mensaje a enviar
				 * @param priority Prioridad del mensjae a enviar
				 */
				virtual void SendMessage(const void* message, int messageLen, int priority) = 0;

			private:
				boost::mutex _mutex;

				void InvokeOnSendMessageError(ConnectException& exception,
						const void* message, int messageLen);
		};
	} /* namespace Channels */
} /* namespace HermEsb */
#endif /* OUTBOUNDCONNECTIONPOINT_H_ */
