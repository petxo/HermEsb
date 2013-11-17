#include "Startable.h"

namespace HermEsb
{
	namespace Core
	{
		Startable::Startable()
		{
			_running = false;
		}

		Startable::~Startable()
		{
		}

		void Startable::Start()
		{
			if (_running) return;

			if(OnStart())
			{
				_running = true;
			}
		}

		void Startable::Stop()
		{
			if(!_running) return;
			if(OnStop())
			{
				_running = false;
			}
		}

		bool Startable::OnStart()
		{
			return true;
		}

		bool Startable::OnStop()
		{
			return true;
		}

		bool Startable::IsRunning()
		{
			return _running;
		}
	}
}