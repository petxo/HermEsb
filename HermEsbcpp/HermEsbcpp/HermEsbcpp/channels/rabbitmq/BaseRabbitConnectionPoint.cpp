
#include "BaseRabbitConnectionPoint.h"

namespace HermEsb
{
	namespace Channels
    {
		namespace Rabbit
		{
			BaseRabbitConnectionPoint::BaseRabbitConnectionPoint(char* server, int port, char* user, char* password)
			{
				_server = server;
				_port = port;
				_user = user;
				_password = password;
			}

            BaseRabbitConnectionPoint::~BaseRabbitConnectionPoint()
			{
			}

			void BaseRabbitConnectionPoint::ConnectRabbit() throw (ConnectException)
			{
								int status;
				amqp_rpc_reply_t reply;

				_conn = amqp_new_connection();

				_socket = amqp_tcp_socket_new(_conn);
				if (!_socket) {
					throw new ConnectException("Error creando Socket");
				}

				status = amqp_socket_open(_socket, _server, _port);
				if (status) {
					throw new ConnectException("Error abriendo Socket amqp");
				}

				amqp_login(_conn, "/", 0, 131072, 0, AMQP_SASL_METHOD_PLAIN, _user, _password);
				amqp_channel_open(_conn, 1);
				GetRabbitError("Login");
			}

			void BaseRabbitConnectionPoint::CloseRabbit()
			{
				amqp_channel_close(_conn, 1, AMQP_REPLY_SUCCESS);
				amqp_connection_close(_conn, AMQP_REPLY_SUCCESS);
				amqp_destroy_connection(_conn);
			}

			void BaseRabbitConnectionPoint::GetRabbitError(char const *context)
			{
				amqp_rpc_reply_t reply = amqp_get_rpc_reply(_conn);
				if (reply.reply_type != AMQP_RESPONSE_NORMAL)
				{
					LOG(ERROR) << "Error de login rabbit:" << reply.reply_type;
					throw new ConnectException("Error de login");
				}

				switch (reply.reply_type) {
					case AMQP_RESPONSE_NORMAL:
						return;

					case AMQP_RESPONSE_NONE:
						LOG(ERROR) << context << ": missing RPC reply type!\n";
						break;

					case AMQP_RESPONSE_LIBRARY_EXCEPTION:
						LOG(ERROR) << context << ": " << amqp_error_string2(reply.library_error);
						break;

					case AMQP_RESPONSE_SERVER_EXCEPTION:
						switch (reply.reply.id) {
							case AMQP_CONNECTION_CLOSE_METHOD: {
								amqp_connection_close_t *m = (amqp_connection_close_t *) reply.reply.decoded;
								LOG(ERROR) << context << ": server connection error " << m->reply_code 
											<<", message: " << (char *) m->reply_text.bytes;
								break;
							}
							case AMQP_CHANNEL_CLOSE_METHOD: {
								amqp_channel_close_t *m = (amqp_channel_close_t *) reply.reply.decoded;
								LOG(ERROR) << context << ": server channel error " << m->reply_code 
											<<", message: " << (char *) m->reply_text.bytes;
								break;
							}
							default:
								LOG(ERROR) << context << ": unknown server error, method id " << reply.reply.id;
								break;
						}
					break;
				}

				throw new ConnectException("Connection Error");
			}
		}
	}
}