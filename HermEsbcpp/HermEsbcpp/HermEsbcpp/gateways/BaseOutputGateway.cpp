#include "BaseOutputGateway.h"

namespace HermEsb
{
    namespace Gateways
    {
		BaseOutputGateway::BaseOutputGateway(OutBoundConnectionPoint *outBoundConnectionPoint)
		{
			_outBoundConnectionPoint = outBoundConnectionPoint;

			EVENT_BIND2(connection, outBoundConnectionPoint->ConnectionError, &BaseOutputGateway::ErrorConnection);
			EVENT_BIND4(connection, outBoundConnectionPoint->OnSendError, &BaseOutputGateway::SendError);
		}

		BaseOutputGateway::~BaseOutputGateway()
		{
			_outBoundConnectionPoint->Close();
			delete _outBoundConnectionPoint;
		}

		void BaseOutputGateway::Connect()
		{
			_outBoundConnectionPoint->Connect();
		}

		void BaseOutputGateway::Close()
		{
			_outBoundConnectionPoint->Close();
		}

		void BaseOutputGateway::ErrorConnection(ConnectionPoint& sender, ConnectException& exception)
        {
            this->_connectionError(*this, exception);
        }

		void BaseOutputGateway::SendError(ConnectionPoint& sender, ConnectException& exception, const void* message, int messageLen)
		{
			this->_sendError(*this, exception, message, messageLen);
		}

	}
}