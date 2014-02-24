/*
 * ConnectionPoint.h
 *
 *  Created on: 03/02/2012
 *      Author: sergio
 */

#ifndef CONNECTIONPOINT_H_
#define CONNECTIONPOINT_H_

#include <stdio.h>
#include "ConnectException.h"
#include "../core/timers/IReconnectionTimer.h"
#include "../core/EventHandling.h"
#include "../global.h"

using namespace std;

namespace HermEsb
{
    using namespace Core::Timers;

    namespace Channels
    {
		#define INFINITE_RECONNECTIONS -1

        /**
         * Clase abstracta que define un punto de conexion con un host, y que gestiona
         * las reconexiones mediante un temporizador
         */
		class HERMESB_API ConnectionPoint
        {
            public:
                DELEGATE(void (ConnectionPoint& sender, ConnectException& exception))ConnectionErrorHandler;
            
			protected:
                ConnectionErrorHandler _connectionError;

            public:

                /**
                 * Crea una instancia de un punto de conexion, y estable el temporizador
                 * de la reconexion
                 * @param reconnectionTimer Temporizador de la reconexion
				 * @param maxReconnections Numero maximo de reconexiones
                 */
                ConnectionPoint(IReconnectionTimer* reconnectionTimer, int maxReconnections = INFINITE_RECONNECTIONS);

                virtual ~ConnectionPoint();

                /**
                 * Establece la conexion con el host, en el caso de no establecerse
                 * se procede a la reconexion hasta que el temporizador llegue a su limite de intentos
                 * de reconexion
                 */
                void Connect();

                /**
                 * Indica si el punto de conexion esta conectado con el host
                 */
                bool IsConnected();

                /**
                 * Cierra la conexion con el host
                 */
                void Close();

                EVENT(ConnectionErrorHandler, _connectionError, ConnectionError);

			protected:
				/**
				* Metodo que se ejecuta al inicio de la operacion de cerrar
				*/
				virtual void BeforeClose();

                /**
                 * Metodo abstracto que establece la conexion fisica con el host
                 * @exception ConnectException Se lanza en el caso que no sea posible la conexion con el host
                 */
                virtual void ConnectPoint() throw (ConnectException) = 0;

                /**
                 * Metodo abstracto que cierra la conexion fisica con el host
                 */
                virtual void ClosePoint() = 0;

                /**
                 * Metodo que controla la reconexion con el host, la reconexion se produce mientras
                 * no se pueda establecer la conexion y hasta que se llegue al limite que
                 * estable el temporizador.
                 */
                void Reconnect() throw (ConnectException);

            private:
                bool _isConnected;
                IReconnectionTimer* _reconnectionTimer;
                int _reconnectionNumber;
                int _maxReconnections;
                void InvokeOnConnectionError(ConnectException& exception);

            };
   } /* namespace Channels */
} /* namespace HermEsb */
#endif /* CONNECTIONPOINT_H_ */
