/*
 * InBoundConnectionPoint.h
 *
 *  Created on: 22/02/2012
 *      Author: sergio
 */

#ifndef INBOUNDCONNECTIONPOINT_H_
#define INBOUNDCONNECTIONPOINT_H_

#include "ConnectException.h"
#include "ConnectionPoint.h"
#include "../core/Startable.h"
#include "../global.h"
#include <boost/thread.hpp>

namespace HermEsb
{
	namespace Channels
	{
		/**
		 * Clase que define un punto de conexion de entrada
		 */
		class HERMESB_API InBoundConnectionPoint: public ConnectionPoint, public HermEsb::Core::Startable
		{
			public:
				DELEGATE(void (InBoundConnectionPoint& sender, void* message, int messageLen))MessageReceivedHandler;

			protected:
				MessageReceivedHandler _onMessageReceived;
			
			public:
				/**
				 * Crea una instancia de InBoundConnection
				 * @param reconnectionTimer Temporizador de reconexion
				 */
				InBoundConnectionPoint(IReconnectionTimer* reconnectionTimer, int maxReconnections = INFINITE_RECONNECTIONS);
				virtual ~InBoundConnectionPoint();

				/**
				 * Callback de llamada cuando llega un mensaje, el metodo devuelve el puntero al buffer
				 * que contiene el mensaje recibido y el tamaño del buffer.
				 * Es responsabilidad del cliente eliminar el buffer mediante free.
				 * @param sender Punto de conexión que ha recibido el mensaje
				 * @param message Puntero al buffer con el mensaje recibido
				 * @param messageLength Tamaño del buffer
				 */
				EVENT(MessageReceivedHandler, _onMessageReceived, OnMessageReceived);

			protected:

				/**
				* Implementa el comportamiento necesario para arracar la instancia,
				* devuelve true o false en funcion de si ha arrancado o no.
				*/
				virtual bool OnStart();

				/**
				* Se lanza cuando se ha terminado el Stop
				*/
				virtual void OnTerminateStop();
				
				/**
				* Procesa la cola de mensajes y controla que no se produzca ninguna perdida de conexion
				*/
				void Proccess();

				/**
				* Metodo que se ejecuta al inicio de la operacion de cerrar
				*/
				virtual void BeforeClose();

				/**
				 * Metodo que lee el mensaje del punto de conexion
				 * @param destBuffer Buffer de destino 
				 */
				virtual int ListenMessage(void** destBuffer) throw (ConnectException) = 0;

				void InvokeOnMessageReceived(void* message, int messageLen);

				boost::thread *threadListen;

		};
	} /* namespace Channels */
} /* namespace HermEsb */
#endif /* INBOUNDCONNECTIONPOINT_H_ */
