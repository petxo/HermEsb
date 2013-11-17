/*
 * IReconnectionTimer.h
 *
 *  Created on: 15/02/2012
 *      Author: sergio
 */

#ifndef IRECONNECTIONTIMER_H_
#define IRECONNECTIONTIMER_H_

#include "../../global.h"

namespace HermEsb
{
    namespace Core
    {
        namespace Timers
        {
            /**
             * Interfaz del temporizador para la reconexiones
             */
			class HERMESB_API IReconnectionTimer
            {
                public:
                    virtual ~IReconnectionTimer()
                    {

                    }

                    /**
                     * Se espera una vuelta del ciclo antes de la reconexion
                     */
                    virtual void Wait() = 0;

                    /**
                     * Se espera un numero de segundos especificado.
                     * @param waiting Segundos de espera
                     */
                    virtual void Wait(int waiting) = 0;

                    /**
                     * Reincia el temporiador
                     */
                    virtual void Reset() = 0;
            };
        }/* namespace Timers */
    } /* namespace Core */
} /* namespace HermEsb */
#endif /* IRECONNECTIONTIMER_H_ */
