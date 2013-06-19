using HermEsb.Core.Gateways;
using HermEsb.Core.Messages;

namespace HermEsb.Core.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorHandlingControllerFactory
    {
        /// <summary>
        /// Initializes the <see cref="ErrorHandlingControllerFactory"/> class.
        /// </summary>
        static ErrorHandlingControllerFactory()
        {
            NullController = new NullErrorHandlingController();
        }

        /// <summary>
        /// Creates the specified output gateway.
        /// </summary>
        /// <param name="outputGateway">The output gateway.</param>
        /// <param name="identification">The identification.</param>
        /// <returns></returns>
        public static IErrorHandlingController Create(IOutputGateway<IMessage> outputGateway, Identification identification)
        {
            return new ErrorHandlingController(outputGateway, identification);
        }


        /// <summary>
        /// Gets or sets the null controller.
        /// </summary>
        /// <value>The null controller.</value>
        public static IErrorHandlingController NullController { get; private set; }
    }
}