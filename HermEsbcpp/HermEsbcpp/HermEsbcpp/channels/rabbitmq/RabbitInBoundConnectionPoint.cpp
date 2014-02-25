#include "RabbitInBoundConnectionPoint.h"
#ifdef WIN32
#include <WinSock.h>
#else
#include <sys/time.h>
#endif

namespace HermEsb
{
	namespace Channels
	{
		namespace Rabbit
		{
			RabbitInBoundConnectionPoint::RabbitInBoundConnectionPoint(char* server, int port, char* exchange, char* routingKey, char* user, char* password, char* queue, IReconnectionTimer* reconnectionTimer):
				InBoundConnectionPoint(reconnectionTimer), BaseRabbitConnectionPoint(server, port, user, password)
			{
				_exchange = exchange;
				_routingKey = routingKey;
				_exchangetype = "direct";
				_queue = queue;
			}
			
			RabbitInBoundConnectionPoint::~RabbitInBoundConnectionPoint()
			{
			}

			
			void RabbitInBoundConnectionPoint::ConnectPoint() throw (ConnectException)
			{
				ConnectRabbit();

				amqp_exchange_declare(_conn, 1, amqp_cstring_bytes(_exchange), amqp_cstring_bytes(_exchangetype),
						0, 1, amqp_empty_table);
				GetRabbitError("Declaracion Exchange");

				amqp_queue_declare(_conn, 1, amqp_cstring_bytes(_queue), 0, 1, 0, 0, amqp_empty_table);
				GetRabbitError("Declaracion Queue");

				amqp_queue_bind(_conn, 1, amqp_cstring_bytes(_queue), amqp_cstring_bytes(_exchange), amqp_cstring_bytes(_routingKey),
				  amqp_empty_table);
				GetRabbitError("Binding Queue");

				amqp_basic_consume(_conn, 1, amqp_cstring_bytes(_queue), amqp_empty_bytes, 0, 0, 0, amqp_empty_table);
				GetRabbitError("Binding Queue");
			}

			void RabbitInBoundConnectionPoint::ClosePoint()
			{
				CloseRabbit();
			}

			int RabbitInBoundConnectionPoint::ListenMessage(void** destBuffer) throw (ConnectException)
			{
				amqp_rpc_reply_t res;
				amqp_envelope_t envelope;

				amqp_maybe_release_buffers(_conn);

				struct timeval timeout;
				timeout.tv_sec = 5;
				timeout.tv_usec = 0;

				res = amqp_consume_message(_conn, &envelope, &timeout, 0);
				  
				if (AMQP_RESPONSE_NORMAL == res.reply_type) 
				{
					int messageLen = (int) envelope.message.body.len;
					void* message = malloc(messageLen); 
					memcpy(message, envelope.message.body.bytes, messageLen);
					amqp_basic_ack(_conn, 1, envelope.delivery_tag, false);
					GetRabbitError("Ack Error");
					amqp_destroy_envelope(&envelope);
					*destBuffer = message;
					return messageLen;
				}
				else
				{
					if(res.library_error != AMQP_STATUS_TIMEOUT)
					{
						LOG(ERROR) << "Error al leer de Rabbit: " << amqp_error_string2(res.library_error);
						throw ConnectException("Error al leer de Rabbit", true);
					}
				}

				return 0;
			}
		}
	}
}