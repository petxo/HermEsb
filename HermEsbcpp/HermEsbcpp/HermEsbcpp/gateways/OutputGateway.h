#ifndef GATEWAYS_OUTPTGATEWAY_H_
#define GATEWAYS_OUTPTGATEWAY_H_

#include "../global.h"
#include "../channels/OutBoundConnectionPoint.h"
#include "../channels/ConnectException.h"
#include "BaseGateway.h"
#include "../messages/IMessage.h"
#include "../messages/MessageBus.h"

using namespace HermEsb::Channels;
using namespace HermEsb::Messages;
using namespace std;


namespace HermEsb
{
	namespace Gateways
	{
		/**
		 * Clase que implementa un punto de salida, que gestiona el multithreading
		 */
		class HERMESB_API OutputGateway : public BaseGateway
		{
		public:
			DELEGATE(void(OutputGateway& sender, ConnectException& exception, const void* message, int messageLen))SendErrorHandler;
		protected:
			SendErrorHandler _sendError;

		public:
			/**
			* Crea una instancia de una punto de salida
			* @param identification Identificacion del punto
			* @param outBoundConnectionPoint Punto de conexion de salida
			* @param useCompression Determina si se debe usar compresion o no
			*/
			OutputGateway(Identification* identification, OutBoundConnectionPoint *outBoundConnectionPoint, bool useCompression);
			
			/**
			* Destructor de una conexion de salida
			*/
			~OutputGateway();
			
			/**
			* Publica un mensaje en el punto de salida con la clave especificada
			* @param key Clave de publicacion
			* @param message Mensaje de salida
			* @param priority Prioridad de publicacion
			*/
			void Publish(string key, IMessage* message, int priority=0);

			/**
			* Evento que se lanza cuando se produce un error al enviar el mensaje
			*/
			EVENT(SendErrorHandler, _sendError, OnSendError);

		protected:
			void SendError(ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen);

		};
	}
}

#endif