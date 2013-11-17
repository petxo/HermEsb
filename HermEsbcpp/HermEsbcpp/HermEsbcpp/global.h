#ifndef GLOBAL_DEFINITIONS
#define GLOBAL_DEFINITIONS

#define GLOG_NO_ABBREVIATED_SEVERITIES

#include <glog/logging.h>

#ifdef HERMESBCPP_EXPORTS
    #define HERMESB_API __declspec(dllexport)
#else
    #define HERMESB_API __declspec(dllimport)
#endif

#define BOOST_ALL_DYN_LINK

#endif