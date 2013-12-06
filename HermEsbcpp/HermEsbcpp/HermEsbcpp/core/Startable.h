#ifndef STATARTABLE_H_
#define STATARTABLE_H_
#include "../global.h"

namespace HermEsb
{
	namespace Core
	{
		/**
		* Define el comportamiento de una clase que se puede arrancar y parar
		*/
		class HERMESB_API Startable
		{
		public:
			/**
			* Crea una instncia de Startable
			*/
			Startable();

			~Startable();

			/**
			* Arranca la instancia
			*/
			void Start();

			/**
			* Detiene la instancia
			*/
			void Stop();

			/**
			* Indica si la instancia esta corriendo
			*/
			bool IsRunning();

		protected:
			/**
			* Implementa el comportamiento necesario para arracar la instancia,
			* devuelve true o false en funcion de si ha arrancado o no.
			*/
			virtual bool OnStart();

			/**
			* Implementa el comportamiento necesario para deterner la instancia,
			* devuelve true o false en funcion de si ha podido deternese o no.
			*/
			virtual bool OnStop();

			/**
			* Se lanza cuando se ha terminado el Stop
			*/
			virtual void OnTerminateStop();

			/**
			* Se lanza cuando se ha terminado el Start
			*/
			virtual void OnTerminateStart();

			bool _running;
		};

	}
}

#endif