#ifndef GATEWAYS_BASEOUTPUTGATEWAY_H_
#define GATEWAYS_BASEOUTPUTGATEWAY_H_

#include "../global.h"
#include "../channels/ConnectException.h"
#include "../channels/ConnectionPoint.h"
#include "../messages/MessageBus.h"

using namespace HermEsb::Channels;
using namespace HermEsb::Messages;

namespace HermEsb
{
	namespace Gateways
	{
		/**
		 * Clase que implementa un punto de salida, que gestiona el multithreading
		 */
		class HERMESB_API BaseGateway
		{
		public:
			DELEGATE(void(BaseGateway& sender, ConnectException& exception))ConnectionErrorHandler;
			
		protected:
			ConnectionErrorHandler _connectionError;
			

		public:
			/**
			* Crea una instancia OutputGateway, contiene un puntero a un punto de conexion
			* de salida, que será eliminado cuando se destruya el objeto.
			* @param outBoundConnectionPoint Puntero a un punto de conexion de salida.
			*/
			BaseGateway(Identification* identification, ConnectionPoint *connectionPoint, bool useCompression);

			/**
			* Destructor
			*/
			~BaseGateway();

			/**
			* Abre la conexion con el punto de salida
			*/
			void Connect();

			/**
			* Cierra la conexion con el punto de salida
			*/
			void Close();

			EventConnection connection;

			EVENT(ConnectionErrorHandler, _connectionError, OnConnectionError);

			


		protected:
			ConnectionPoint* _connectionPoint;
			
			void ErrorConnection(ConnectionPoint& sender, ConnectException& exception);

			Identification* _identification;

			bool _useCompression;
		};
	}
}


#endif