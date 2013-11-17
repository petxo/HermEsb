#ifndef PERIODICALRECONNECTIONTIMER
#define PERIODICALRECONNECTIONTIMER
#include "IReconnectionTimer.h"
#include "../../global.h"

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {
			class HERMESB_API PeriodicalReconnectionTimer: public IReconnectionTimer
            {
                public:
                    PeriodicalReconnectionTimer(float periodicalTime);
                    virtual ~PeriodicalReconnectionTimer();
                    void Wait();
                    void Wait(float waiting);
                    void Reset();

                private:
                    float _periodicalTime;

            };
		}
	}
}
#endif