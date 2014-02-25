#include "BaseGateway.h"

namespace HermEsb
{
	namespace Gateways
	{
		BaseGateway::BaseGateway(Identification* identification, ConnectionPoint *connectionPoint, bool useCompression)
		{
			_connectionPoint = connectionPoint;
			_identification = identification;
			_useCompression = useCompression;

			EVENT_BIND2(connection, connectionPoint->ConnectionError, &BaseGateway::ErrorConnection);
		}

		BaseGateway::~BaseGateway()
		{
			_connectionPoint->Close();
			EVENT_UNBIND(connection);
			delete _connectionPoint;
		}

		void BaseGateway::Connect()
		{
			_connectionPoint->Connect();
		}

		void BaseGateway::Close()
		{
			_connectionPoint->Close();
		}

		void BaseGateway::ErrorConnection(ConnectionPoint& sender, ConnectException& exception)
		{
			this->_connectionError(*this, exception);
		}
	}
}