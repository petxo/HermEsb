#ifndef GATEWAYS_OUTPTGATEWAY_H_
#define GATEWAYS_OUTPTGATEWAY_H_

#include "../global.h"
#include "../channels/OutBoundConnectionPoint.h"
#include "BaseOutputGateway.h"
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
		class HERMESB_API OutputGateway : public BaseOutputGateway
		{
		public:
			OutputGateway(Identification* identification, OutBoundConnectionPoint *outBoundConnectionPoint, bool useCompression);
			~OutputGateway();
			void Publish(string key, IMessage* message, int priority=0);
		private:
			Identification* _identification;
			bool _useCompression;
		};
	}
}

#endif