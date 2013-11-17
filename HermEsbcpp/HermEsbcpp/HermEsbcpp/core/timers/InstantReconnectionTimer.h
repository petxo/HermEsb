/*
 * InstantReconnectionTimer.h
 *
 *  Created on: 15/02/2012
 *      Author: sergio
 */

#ifndef INSTANTRECONNECTIONTIMER_H_
#define INSTANTRECONNECTIONTIMER_H_
#include "IReconnectionTimer.h"
#include "../../global.h"

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {
            /**
             * Define un temporizador de reconexion instantaneo, no hay espera
             * entre un ciclo y el siguiente.
             */
			class HERMESB_API InstantReconnectionTimer: public IReconnectionTimer
            {
                public:
                    InstantReconnectionTimer();
                    virtual ~InstantReconnectionTimer();

                    void Wait();
                    void Wait(int waiting);
                    void Reset();
            };

        } /* namespace Timers */
    } /* namespace Core */
} /* namespace HermEsb */
#endif /* INSTANTRECONNECTIONTIMER_H_ */
