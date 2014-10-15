
#include "InputGateway.h"
#include "BaseGateway.h"
#include <boost\thread.hpp>
#include <boost\thread\latch.hpp>
#include <boost\interprocess\sync\interprocess_semaphore.hpp>
#include "../global.h"

namespace HermEsb
{
	namespace Gateways
	{
		InputGateway::InputGateway(Identification* identification, InBoundConnectionPoint *inBoundConnectionPoint, bool useCompression) : 
			BaseGateway(identification, inBoundConnectionPoint, useCompression), Startable()
		{
			EVENT_BIND3(connection, inBoundConnectionPoint->OnMessageReceived, &InputGateway::ReceivedMessage);
		}
		InputGateway::~InputGateway()
		{
		}

		/**
		* Implementa el comportamiento necesario para arracar la instancia,
		* devuelve true o false en funcion de si ha arrancado o no.
		*/
		bool InputGateway::OnStart()
		{
			((InBoundConnectionPoint *)this->_connectionPoint)->Start();
			return true;
		}

		/**
		* Implementa el comportamiento necesario para deterner la instancia,
		* devuelve true o false en funcion de si ha podido deternese o no.
		*/
		bool InputGateway::OnStop()
		{
			((InBoundConnectionPoint *)this->_connectionPoint)->Stop();
			return true;
		}

		void InputGateway::ReceivedMessage(InBoundConnectionPoint& sender, void* message, int messageLen)
		{
			//TODO: Leer el mensaje del bus y lanzar un hilo con un nuevo evento

		}

	}
}