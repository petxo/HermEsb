/*
 * LogarithmicReconnectionTimer.cpp
 *
 *  Created on: 15/02/2012
 *      Author: sergio
 */

#include "LogarithmicReconnectionTimer.h"
#ifdef _WIN32
#include <Windows.h>
#endif
#include <math.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {

            LogarithmicReconnectionTimer::LogarithmicReconnectionTimer()
            {
                _seed = 1.0;
            }

            LogarithmicReconnectionTimer::~LogarithmicReconnectionTimer()
            {
            }

            void LogarithmicReconnectionTimer::Wait()
            {
                Wait(_seed);
                _seed++;
            }

            void LogarithmicReconnectionTimer::Wait(float waiting)
            {
#ifdef _WIN32
                Sleep((DWORD)log(waiting));
#else
				sleep(log(waiting));
#endif
            }

            void LogarithmicReconnectionTimer::Reset()
            {
                _seed = 1.0;
            }

        } /* namespace Timers */
    } /* namespace Core */
} /* namespace HermEsb */
