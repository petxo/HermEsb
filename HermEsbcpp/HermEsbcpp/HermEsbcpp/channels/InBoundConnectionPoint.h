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
#include "../global.h"

namespace HermEsb
{
    namespace Channels
    {
        /**
         * Clase que define un punto de conexion de entrada
         */
		class HERMESB_API InBoundConnectionPoint: public ConnectionPoint
        {
            public:

                /**
                 * Crea una instancia de InBoundConnection
                 * @param reconnectionTimer Temporizador de reconexion
                 */
                InBoundConnectionPoint(IReconnectionTimer* reconnectionTimer);
                virtual ~InBoundConnectionPoint();

                /**
                 * Fuerza el comienzo de la repecion de mensajes
                 */
                void BeginReceive();

                /**
                 * Detiene la recepción de mensajes
                 */
                void EndReceive();

                /**
                 * Callback de llamada cuando llega un mensaje, el metodo devuelve el puntero al buffer
                 * que contiene el mensaje recibido y el tamaño del buffer.
                 * Es responsabilidad del cliente eliminar el buffer mediante free.
                 * @param sender Punto de conexión que ha recibido el mensaje
                 * @param message Puntero al buffer con el mensaje recibido
                 * @param messageLength Tamaño del buffer
                 */
                void (*OnMessageReceived)(InBoundConnectionPoint& sender,
                        const void* message, int messageLength);

            protected:
                /**
                 * Inicio de la recepcion de mensaje
                 */
                virtual void StartReceive() = 0;

                /**
                 * Detencion de la recepcion de los mensajes
                 */
                virtual void StopReceive() = 0;

                //TODO: Aqui tiene que devolver algo
                /**
                 * Metodo que pone al punto de conexion en modo de escucha
                 */
                virtual void ListenMessage() = 0;

        };
    } /* namespace Channels */
} /* namespace HermEsb */
#endif /* INBOUNDCONNECTIONPOINT_H_ */
