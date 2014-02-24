#ifndef UTILS_TIME_UTILS_H
#define UTILS_TIME_UTILS_H
#include <stdio.h>
#include <stdlib.h>
#include <boost/date_time/posix_time/posix_time.hpp>


using namespace boost::posix_time;
using namespace std;
namespace HermEsb
{
	namespace Utils
	{
		namespace Time
		{
			class HERMESB_API IsoTime
			{
			public:
				static ptime from_iso_extended_string(const string& inDateString)
				{
					// If we get passed a zero length string, then return the "not a date" value. 
					if (inDateString.empty())
					{ 
						return not_a_date_time;
					}
         
					// Use the ISO extended input facet to interpret the string. 
					std::stringstream ss;
					time_input_facet* input_facet = new time_input_facet();
					input_facet->set_iso_extended_format();
					ss.imbue(std::locale(ss.getloc(), input_facet));
					ss.str(inDateString);
					ptime timeFromString;
					ss >> timeFromString;
         
					return timeFromString;
				}
			};

			class HERMESB_API TimeExtensions
			{
			public:
				static int64_t ticks_from_epoch(ptime t)
				{
					ptime myEpoch(boost::gregorian::date(1970,1,1));
					time_duration myTimeFromEpoch = t - myEpoch;
					return myTimeFromEpoch.ticks() * 10;
				}

				static int64_t ticks_from_mindate(ptime t)
				{
					int64_t ticksFromEpoch = ticks_from_epoch(t);
					return ticksFromEpoch + 621355968000000000;
				}
			};
		}
	}
}

#endif
