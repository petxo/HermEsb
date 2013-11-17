#include <gtest/gtest.h>
#include <hermesb.h>

using namespace HermEsb::Core;

class NotStart : public Startable
{
protected:
	bool OnStart()
	{
		return false;
	}
};

class NonStop : public Startable
{
protected:
	bool OnStop()
	{
		return false;
	}
};

TEST(StartableTest, ArrancarUnaInstanciaParada)
{
	Startable * startable = new Startable();
	
	startable->Start();

	EXPECT_TRUE(startable->IsRunning());

	delete startable;
}

TEST(StartableTest, ArrancarUnaInstanciaQueNoArranca)
{
	Startable * startable = (Startable *) new NotStart();
	
	startable->Start();

	EXPECT_FALSE(startable->IsRunning());

	delete startable;
}

TEST(StartableTest, DeternerUnaInstanciaArrancada)
{
	Startable * startable = new Startable();
	
	startable->Start();
	startable->Stop();
	EXPECT_FALSE(startable->IsRunning());

	delete startable;
}

TEST(StartableTest, DeternerUnaInstanciaQueNoSeDetiene)
{
	Startable * startable = (Startable * )new NonStop();
	
	startable->Start();
	startable->Stop();
	EXPECT_TRUE(startable->IsRunning());

	delete startable;
}