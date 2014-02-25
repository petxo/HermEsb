#ifndef MESSAGES_MESSAGEBUSFACTORY_H_
#define MESSAGES_MESSAGEBUSFACTORY_H_
#include "../global.h"
#include <stdio.h>
#include <stdlib.h>
#include "MessageBus.h"
#include <boost/date_time/posix_time/posix_time.hpp>

using namespace std;
using namespace boost::posix_time;

namespace HermEsb
{
	namespace Messages
	{
		class HERMESB_API MessageBusFactory
		{
		public:
			static MessageBus* CreateMessageBus(Identification* identification, string key, string body);
			static int CreateRouterMessage(MessageBus* messageBus, void** messageBuffer , bool useCompression);
		};

	}
}



#endif
