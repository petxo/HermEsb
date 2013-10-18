using System;
using System.Collections.Generic;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages.Control;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// 
    /// </summary>
    public class Subscriptor : IDisposable
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        public Identification Service { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        public IList<SubscriptionKey> SubcriptionTypes { get; set; }

        /// <summary>
        /// Gets or sets the input gateway.
        /// </summary>
        /// <value>The input gateway.</value>
        public IOutputGateway<byte[]> ServiceInputGateway { get; set; }

        /// <summary>
        /// Gets or sets the input control queue.
        /// </summary>
        /// <value>The input control queue.</value>
        public IOutputGateway<IControlMessage> ServiceInputControlGateway { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriptor"/> class.
        /// </summary>
        public Subscriptor()
        {
            SubcriptionTypes = new List<SubscriptionKey>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServiceInputGateway.Dispose();
                ServiceInputControlGateway.Dispose();
            }
        }
    }
}