
#include "InputGateway.h"
#include "BaseGateway.h"

namespace HermEsb
{
	namespace Gateways
	{
		InputGateway::InputGateway(Identification* identification, InBoundConnectionPoint *inBoundConnectionPoint, bool useCompression) : 
			BaseGateway(identification, inBoundConnectionPoint, useCompression), Startable()
		{

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
			return true;
		}

		/**
		* Implementa el comportamiento necesario para deterner la instancia,
		* devuelve true o false en funcion de si ha podido deternese o no.
		*/
		bool InputGateway::OnStop()
		{
			return true;
		}

	}
}