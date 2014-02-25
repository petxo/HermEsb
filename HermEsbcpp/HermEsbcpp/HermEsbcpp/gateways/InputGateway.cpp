
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
	}
}