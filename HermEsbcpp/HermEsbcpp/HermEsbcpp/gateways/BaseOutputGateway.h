#ifndef GATEWAYS_BASEOUTPUTGATEWAY_H_
#define GATEWAYS_BASEOUTPUTGATEWAY_H_

#include "../global.h"
#include "../channels/OutBoundConnectionPoint.h"

using namespace HermEsb::Channels;

namespace HermEsb
{
    namespace Gateways
    {
		/**
         * Clase que implementa un punto de salida, que gestiona el multithreading
         */
		class HERMESB_API BaseOutputGateway
		{
		public:
            DELEGATE(void (BaseOutputGateway& sender, ConnectException& exception))ConnectionErrorHandler;
			DELEGATE(void (BaseOutputGateway& sender, ConnectException& exception, const void* message, int messageLen))SendErrorHandler;
            
		protected:
            ConnectionErrorHandler _connectionError;
			SendErrorHandler _sendError;

		public:
			/**
            * Crea una instancia OutputGateway, contiene un puntero a un punto de conexion
			* de salida, que será eliminado cuando se destruya el objeto.
            * @param outBoundConnectionPoint Puntero a un punto de conexion de salida.
            */
			BaseOutputGateway(OutBoundConnectionPoint *outBoundConnectionPoint);

			/**
            * Destructor
            */
			~BaseOutputGateway();

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

			EVENT(SendErrorHandler, _sendError, OnSendError);


		protected:
			OutBoundConnectionPoint* _outBoundConnectionPoint;
			
			void ErrorConnection(ConnectionPoint& sender, ConnectException& exception);
			void SendError(ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen);

		};
	}
}


#endif