/*
 * LogarithmicReconnectionTimer.h
 *
 *  Created on: 15/02/2012
 *      Author: sergio
 */

#ifndef LOGARITHMICRECONNECTIONTIMER_H_
#define LOGARITHMICRECONNECTIONTIMER_H_

#include "IReconnectionTimer.h"
#include "../../global.h"

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {
            /**
             * Define un temporizador de reconexion logaritmico.
             */
            class HERMESB_API LogarithmicReconnectionTimer: public IReconnectionTimer
            {
                public:
                    LogarithmicReconnectionTimer();
                    virtual ~LogarithmicReconnectionTimer();
                    void Wait();
                    void Wait(float waiting);
                    void Reset();

                private:
                    float _seed;

            };

        } /* namespace Timers */
    } /* namespace Core */
} /* namespace HermEsb */
#endif /* LOGARITHMICRECONNECTIONTIMER_H_ */
