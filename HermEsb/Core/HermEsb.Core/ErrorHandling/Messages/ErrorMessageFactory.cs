using System;

namespace HermEsb.Core.ErrorHandling.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorMessageFactory
    {
        /// <summary>
        /// Creates the error router message from event args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static ErrorRouterMessage CreateErrorRouterMessageFromEventArgs(ErrorOnRouterEventHandlerArgs args)
        {
            return new ErrorRouterMessage
            {
                Message = args.Message,
                Exception = CreateExceptionMessage(args.Exception)
            };
        }

        /// <summary>
        /// Creates the error handler message from event args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static ErrorHandlerMessage CreateErrorHandlerMessageFromEventArgs(ErrorOnHandlersEventHandlerArgs args)
        {
            return new ErrorHandlerMessage
            {
                Message = args.Message,
                Header = args.Header,
                Exception = CreateExceptionMessage(args.Exception),
                HandlerType = args.HandlerType.FullName,
                MessageBus = args.MessageBus
            };
        }

        private static ExceptionMessage CreateExceptionMessage(Exception exception)
        {
            return exception != null ? new ExceptionMessage()
                                           {
                                               Message = exception.Message,
                                               StackTrace = exception.StackTrace,
                                               InnerException = CreateExceptionMessage(exception.InnerException)
                                           } : null;

        }
    }
}