using System;
using System.Collections.Generic;
using HermEsb.Core.ErrorHandling.Messages;
using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorHandlingController : IErrorHandlingController
    {
        private readonly IOutputGateway<IMessage> _outputGateway;
        private readonly Identification _identification;

        private readonly IList<IAgentErrorHandling> _agentErrorHandlings;

        private readonly IList<IRouterErrorHandling> _routerErrorHandlings;


        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingController"/> class.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="identification">The identification.</param>
        internal ErrorHandlingController(IOutputGateway<IMessage> outputGateway, Identification identification)
        {
            _outputGateway = outputGateway;
            _identification = identification;
            _agentErrorHandlings = new List<IAgentErrorHandling>();
            _routerErrorHandlings = new List<IRouterErrorHandling>();
        }

        /// <summary>
        /// Adds the agent error handling.
        /// </summary>
        /// <param name="agent">The _agent.</param>
        public void AddAgentErrorHandling(IAgentErrorHandling agent)
        {
            _agentErrorHandlings.Add(agent);
            agent.OnErrorHandler += OnErrorHandler;
        }

        /// <summary>
        /// Adds the router error handling.
        /// </summary>
        /// <param name="router">The _router.</param>
        public void AddRouterErrorHandling(IRouterErrorHandling router)
        {
            _routerErrorHandlings.Add(router);
            router.OnRouterError += OnRouterError;
        }

        private void OnRouterError(object sender, ErrorOnRouterEventHandlerArgs args)
        {
            var errorRouterMessage = ErrorMessageFactory.CreateErrorRouterMessageFromEventArgs(args);
            errorRouterMessage.ServiceId = _identification;
            _outputGateway.Send(errorRouterMessage);
        }

        private void OnErrorHandler(object sender, ErrorOnHandlersEventHandlerArgs<byte[]> args)
        {
            var message = ErrorMessageFactory.CreateErrorHandlerMessageFromEventArgs(args);
            message.ServiceId = _identification;
            _outputGateway.Send(message);
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
                _outputGateway.Dispose();

                foreach (var routerErrorHandling in _routerErrorHandlings)
                {
                    routerErrorHandling.OnRouterError -= OnRouterError;
                }

                foreach (var agentErrorHandling in _agentErrorHandlings)
                {
                    agentErrorHandling.OnErrorHandler -= OnErrorHandler;
                }
            }
        }
    }
}