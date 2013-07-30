using System;

namespace HermEsb.Core.Communication.EndPoints
{
    public class EndPointInfo : IEndPoint
    {
        public EndPointInfo(Uri uri, TransportType transport)
        {
            Transport = transport;
            Uri = uri;
        }

        public void Dispose()
        {
            
        }

        public Uri Uri { get; private set; }
        public TransportType Transport { get; private set; }
    }
}