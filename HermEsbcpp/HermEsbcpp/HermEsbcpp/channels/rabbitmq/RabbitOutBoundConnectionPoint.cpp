
#include "RabbitOutBoundConnectionPoint.h"

namespace HermEsb
{
	namespace Channels
    {
		namespace Rabbit
		{
			RabbitOutBoundConnectionPoint::RabbitOutBoundConnectionPoint(IReconnectionTimer* reconnectionTimer) :
				OutBoundConnectionPoint(reconnectionTimer)
			{
			}

			RabbitOutBoundConnectionPoint::~RabbitOutBoundConnectionPoint()
			{
			}

			void RabbitOutBoundConnectionPoint::ConnectPoint() throw (ConnectException)
			{
			}

			void RabbitOutBoundConnectionPoint::ClosePoint()
			{
			}

			void RabbitOutBoundConnectionPoint::SendMessage(const void* message, int messageLen)
			{
			}
		}
	}
}