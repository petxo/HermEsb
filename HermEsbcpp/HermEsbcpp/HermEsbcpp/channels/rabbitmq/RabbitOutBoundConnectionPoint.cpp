#include "RabbitOutBoundConnectionPoint.h"
#include <amqp_tcp_socket.h>
#include <amqp.h>
#include <amqp_framing.h>

namespace HermEsb
{
	namespace Channels
    {
		namespace Rabbit
		{
			RabbitOutBoundConnectionPoint::RabbitOutBoundConnectionPoint(char* server, int port, char* exchange, char* routingKey, char* user, char* password, IReconnectionTimer* reconnectionTimer) :
				OutBoundConnectionPoint(reconnectionTimer), BaseRabbitConnectionPoint(server, port, user, password)
			{
				_exchange = exchange;
				_routingKey = routingKey;
				_exchangetype = "direct";
			}

			RabbitOutBoundConnectionPoint::~RabbitOutBoundConnectionPoint()
			{
			}

			void RabbitOutBoundConnectionPoint::ConnectPoint() throw (ConnectException)
			{
				ConnectRabbit();

				amqp_exchange_declare(_conn, 1, amqp_cstring_bytes(_exchange), amqp_cstring_bytes(_exchangetype),
                        0, 1, amqp_empty_table);
				GetRabbitError("Declaracion Exchange");
			}

			void RabbitOutBoundConnectionPoint::ClosePoint()
			{
				CloseRabbit();
			}

			void RabbitOutBoundConnectionPoint::SendMessage(const void* message, int messageLen, int priority)
			{
				amqp_basic_properties_t propertiesMessage;
				propertiesMessage._flags = AMQP_BASIC_DELIVERY_MODE_FLAG  | AMQP_BASIC_PRIORITY_FLAG;
				propertiesMessage.delivery_mode = 2; /* persistent delivery mode */
				propertiesMessage.priority = priority;

				amqp_bytes_t messageByte = amqp_bytes_malloc(messageLen);
				memcpy(messageByte.bytes, message, messageLen);

				amqp_basic_publish(_conn,  1,
									amqp_cstring_bytes(_exchange),
                                    amqp_cstring_bytes(_routingKey),
                                    0,
                                    0,
                                    &propertiesMessage,
                                    messageByte);
				amqp_bytes_free(messageByte);
				GetRabbitError("SendMessage");
                                   
			}
		}
	}
}