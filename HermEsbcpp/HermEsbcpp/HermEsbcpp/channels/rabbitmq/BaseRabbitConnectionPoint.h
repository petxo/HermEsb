#ifndef CHANNELS_RABBIT_BASERABBITCONNECTIONPOINT
#define CHANNELS_RABBIT_BASERABBITCONNECTIONPOINT

#include "../../global.h"
#include "../ConnectException.h"
#include <amqp_tcp_socket.h>
#include <amqp.h>
#include <amqp_framing.h>

namespace HermEsb
{
	namespace Channels
    {
		namespace Rabbit
		{
			/**
			 * Clase que implementa un punto de conexion de salida 
			 * a RabbitMq
			 */
			class HERMESB_API  BaseRabbitConnectionPoint 
			{
			public:
				/**
                 * Crea una instancia OutBoundConnectionPoint
                 * @param reconnectionTimer Temporizador de reconexion
                 */
                BaseRabbitConnectionPoint(char* server, int port, char* user, char* password);
                virtual ~BaseRabbitConnectionPoint();

			protected:

                /**
                 * Metodo abstracto que establece la conexion fisica con el host
                 * @exception ConnectException Se lanza en el caso que no sea posible la conexion con el host
                 */
                void ConnectRabbit() throw (ConnectException);

                /**
                 * Metodo abstracto que cierra la conexion fisica con el host
                 */
                void CloseRabbit();


				amqp_connection_state_t _conn;
				amqp_socket_t *_socket;
				char* _server;
				int _port;
				char* _user;
				char* _password;

				void GetRabbitError(char const *context);
			};
		}
	}
}

#endif