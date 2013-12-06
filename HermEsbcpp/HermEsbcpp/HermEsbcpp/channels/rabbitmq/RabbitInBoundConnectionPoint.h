#ifndef CHANNELS_RABBIT_RABBITINBOUNDCONNECTIONPOINT
#define CHANNELS_RABBIT_RABBITINBOUNDCONNECTIONPOINT

#include "../InBoundConnectionPoint.h"
#include "BaseRabbitConnectionPoint.h"
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
			 * Clase que implementa un punto de conexion de entrada 
			 * a RabbitMq
			 */
			class HERMESB_API  RabbitInBoundConnectionPoint : public InBoundConnectionPoint, public BaseRabbitConnectionPoint
			{
			public:
				/**
                 * Crea una instancia OutBoundConnectionPoint
                 * @param reconnectionTimer Temporizador de reconexion
                 */
                RabbitInBoundConnectionPoint(char* server, int port, char* exchange, char* routingKey, char* user, char* password, char* queue, IReconnectionTimer* reconnectionTimer);
                virtual ~RabbitInBoundConnectionPoint();

			protected:

                /**
                 * Metodo abstracto que establece la conexion fisica con el host
                 * @exception ConnectException Se lanza en el caso que no sea posible la conexion con el host
                 */
                virtual void ConnectPoint() throw (ConnectException);

                /**
                 * Metodo abstracto que cierra la conexion fisica con el host
                 */
                virtual void ClosePoint();
				
                /**
                 * Metodo que pone al punto de conexion en modo de escucha
                 */
				virtual	int ListenMessage(void** destBuffer) throw (ConnectException);

			private:
				char* _exchange;
				char* _routingKey;
				char* _exchangetype;
				char* _queue;
			};
		}
	}
}

#endif