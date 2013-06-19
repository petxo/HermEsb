using System;
using System.IO;
using log4net;
using log4net.Config;

namespace HermEsb.Logging.L4N
{
    /// <summary>
    /// 
    /// </summary>
    public class Log4NetWrapper : ILogger
    {

        private readonly ILog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetWrapper"/> class.
        /// </summary>
        public Log4NetWrapper()
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(Log4NetWrapper));
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetWrapper"/> class.
        /// </summary>
        /// <param name="pathConfig">The path config.</param>
        public Log4NetWrapper(string pathConfig)
        {
            var fileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathConfig));
            XmlConfigurator.Configure(fileInfo);
            _log = LogManager.GetLogger(typeof (Log4NetWrapper));
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            _log.Debug(message);
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            _log.Warn(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warn(string message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            _log.Error(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
    }
}