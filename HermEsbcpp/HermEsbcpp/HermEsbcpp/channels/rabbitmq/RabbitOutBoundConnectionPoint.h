#ifndef CHANNELS_RABBIT_RABBITOUTBOUNDCONNECTIONPOINT
#define CHANNELS_RABBIT_RABBITOUTBOUNDCONNECTIONPOINT

#include "../OutBoundConnectionPoint.h"

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
			class HERMESB_API  RabbitOutBoundConnectionPoint : OutBoundConnectionPoint
			{
			public:
				/**
                 * Crea una instancia OutBoundConnectionPoint
                 * @param reconnectionTimer Temporizador de reconexion
                 */
                RabbitOutBoundConnectionPoint(IReconnectionTimer* reconnectionTimer);
                virtual ~RabbitOutBoundConnectionPoint();

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
                 * Envia un mensaje por medio del punto de conexión.
                 * @param message Mensaje a enviar
                 * @param messageLen Longitud del mensaje a enviar
                 */
                virtual void SendMessage(const void* message,
                        int messageLen);
			};
		}
	}
}

#endif