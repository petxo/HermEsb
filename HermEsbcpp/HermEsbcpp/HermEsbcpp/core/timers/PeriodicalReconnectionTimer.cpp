#include "PeriodicalReconnectionTimer.h"
#ifdef _WIN32
#include <Windows.h>
#endif

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {
			PeriodicalReconnectionTimer::PeriodicalReconnectionTimer(float _periodicalTime)
            {
                _periodicalTime = 5.0;
            }

            PeriodicalReconnectionTimer::~PeriodicalReconnectionTimer()
            {
            }

            void PeriodicalReconnectionTimer::Wait()
            {
                Wait(_periodicalTime);
            }

            void PeriodicalReconnectionTimer::Wait(float waiting)
            {
#ifdef _WIN32
                Sleep((DWORD)waiting);
#else
				sleep(waiting);
#endif
            }

            void PeriodicalReconnectionTimer::Reset()
            {
            }
		}
	}
}