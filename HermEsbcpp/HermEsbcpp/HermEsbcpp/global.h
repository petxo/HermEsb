#ifndef GLOBAL_DEFINITIONS
#define GLOBAL_DEFINITIONS

#define GLOG_NO_ABBREVIATED_SEVERITIES

#include <glog/logging.h>


#define BOOST_ALL_DYN_LINK

#ifndef WIN32
	#include <inttypes.h>
	#define HERMESB_API
#else
	#ifdef HERMESBCPP_EXPORTS
		#define HERMESB_API __declspec(dllexport)
	#else
		#define HERMESB_API __declspec(dllimport)
	#endif

	typedef __int64 int64_t;
#endif


#endif
