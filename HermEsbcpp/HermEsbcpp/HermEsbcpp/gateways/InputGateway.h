#ifndef GATEWAYS_INPUTGATEWAY_H_
#define GATEWAYS_INPUTGATEWAY_H_

#include "../global.h"
#include "BaseGateway.h"
#include "../messages/MessageBus.h"
#include "../core/Startable.h"
#include "../messages/IMessage.h"
#include "../channels/InBoundConnectionPoint.h"

using namespace HermEsb::Channels;
using namespace HermEsb::Messages;
using namespace HermEsb::Core;

namespace HermEsb
{
	namespace Gateways
	{
		/**
		* Clase que implementa una conexion de entrada
		*/
		class HERMESB_API InputGateway : public BaseGateway, public Startable
		{
		public:
			DELEGATE(void(InputGateway& sender, ConnectException& exception, const void* message, int messageLen))RecevieErrorHandler;
		protected:
			RecevieErrorHandler _recevieError;

		public:
			/**
			* Crea una instacia del punto de entrada
			* @param identification Identificacion del punto de entrada
			* @param inBoundConnectionPoint Conexion del punto de entrada
			* @param useCompression Determina si se debe usar la compresion
			*/
			InputGateway(Identification* identification, InBoundConnectionPoint *inBoundConnectionPoint, bool useCompression);

			/**
			* Destructor del punto de entrada
			*/
			~InputGateway();

		protected:
			/**
			* Implementa el comportamiento necesario para arracar la instancia,
			* devuelve true o false en funcion de si ha arrancado o no.
			*/
			virtual bool OnStart();

			/**
			* Implementa el comportamiento necesario para deterner la instancia,
			* devuelve true o false en funcion de si ha podido deternese o no.
			*/
			virtual bool OnStop();

			/**

			*/
			void ReceivedMessage(InBoundConnectionPoint& sender, void* message, int messageLen);
		};

	}
}

#endif