/*
 * ConnectException.cpp
 *
 *  Created on: 13/02/2012
 *      Author: sergio
 */

#include "ConnectException.h"
#include <errno.h>

namespace HermEsb
{
    namespace Channels
    {
        ConnectException::ConnectException(const string &message,
                bool inclSysMsg) throw () :
                userMessage(message)
        {
            if (inclSysMsg)
            {
                userMessage.append(": ");
                userMessage.append(strerror(errno));
            }
        }

        ConnectException::~ConnectException() throw ()
        {

        }

        const char *ConnectException::what() const throw ()
        {
            return userMessage.c_str();
        }
    } /* namespace Channels */
} /* namespace HermEsb */
