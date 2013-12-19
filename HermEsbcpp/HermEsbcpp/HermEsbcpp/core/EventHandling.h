/*
 * EVENTHANDLING.h
 *
 *  Created on: 30/10/2012
 *      Author: sergio
 */

#ifndef EVENTHANDLING_H_
#define EVENTHANDLING_H_

#include <boost/signals2.hpp>

#define EVENT(delegateType, eventHandler, event) EventConnection event( \
                const delegateType::slot_type& subscriber) {  \
        return eventHandler.connect(subscriber); \
}

#define DELEGATE(fs) typedef boost::signals2::signal<fs>
#define EVENT_BIND(eventConnection, event, eventHanlderFunction) eventConnection = event(boost::bind(eventHanlderFunction, this));
#define EVENT_BIND1(eventConnection, event, eventHanlderFunction) eventConnection = event(boost::bind(eventHanlderFunction, this, _1));
#define EVENT_BIND2(eventConnection, event, eventHanlderFunction) eventConnection = event(boost::bind(eventHanlderFunction, this, _1, _2));
#define EVENT_BIND3(eventConnection, event, eventHanlderFunction) eventConnection = event(boost::bind(eventHanlderFunction, this, _1, _2, _3));
#define EVENT_BIND4(eventConnection, event, eventHanlderFunction) eventConnection = event(boost::bind(eventHanlderFunction, this, _1, _2, _3, _4));
#define EVENT_UNBIND(eventConnection) eventConnection.disconnect();
typedef boost::signals2::connection EventConnection;


#endif /* EVENTHANDLING_H_ */
