/*
 * ConnectException.h
 *
 *  Created on: 13/02/2012
 *      Author: sergio
 */

#ifndef CHANNELS_CONNECTEXCEPTION_H_
#define CHANNELS_CONNECTEXCEPTION_H_

#include <string>            // For string
#include <exception>         // For exception class
#include <stdlib.h>
#include <string.h>
#include "../global.h"

using namespace std;

namespace HermEsb
{
	namespace Channels
	{
		/**
		 * Define una excepcion en la conexion
		 */
		class HERMESB_API ConnectException : public exception
		{
			public:
				/**
				 * Crea una instancia de ConnectionException
				 * @param message Mensaje de error
				 * @param inclSysMsg Indicador de si se debe incluir el error de sistema
				 */
				ConnectException(const string &message,
						bool inclSysMsg = false) throw ();
				virtual ~ConnectException() throw ();

				/**
				 * Devuelve el mensaje de error de la excepcion
				 */
				const char *what() const throw ();

			private:
				string userMessage;
		};
	} /* namespace Channels */
} /* namespace HermEsb */
#endif /* CONNECTEXCEPTION_H_ */
