using HermEsb.Logging;
using System;
using System.Text;
using RabbitMQ.Client.Events;

namespace HermEsb.Core.Communication.Channels.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitWrapper : IRabbitWrapper
    {
        private readonly IConnectionProvider<IRabbitConnection> _connectionProvider;
        private readonly RabbitWrapperType _rabbitWrapperType;
        private IRabbitConnection _connection;

        private IQueueBasicConsumer _consumer;
        private ILogger _logger;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger
        {
            get { return _logger ?? new NullLogger(); }
            set { _logger = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitWrapper"/> class.
        /// </summary>
        /// <param name="connectionProvider">The connection provider.</param>
        /// <param name="rabbitWrapperType">Type of the rabbit wrapper.</param>
        internal RabbitWrapper(IConnectionProvider<IRabbitConnection> connectionProvider, RabbitWrapperType rabbitWrapperType)
        {
            _connectionProvider = connectionProvider;
            _rabbitWrapperType = rabbitWrapperType;
            _connection = _connectionProvider.Connect();
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            try
            {
                //Logger.Debug(string.Format("Iniciando Count de la cola {0}", QueueName));
                //using (var model = Connection.CreateModel())
                //{
                //    Logger.Debug("Model Creado");
                //    var basicGetResult = model.BasicGet(QueueName, false);

                //    if (basicGetResult != null)
                //    {
                //        model.BasicNack(basicGetResult.DeliveryTag, true, true);
                //        return (int)basicGetResult.MessageCount;
                //    }

                //    Logger.Debug("BasicGetResult es nulo");
                return -1;
                //}
            }
            catch (Exception exception)
            {
                Logger.Error("Error Count", exception);
                return -1;
            }
        }

        /// <summary>
        /// Purges the queue.
        /// </summary>
        public void Purge()
        {
            Logger.Debug("Purge Rabbit Queue");
            _connection.Channel.QueuePurge(_connection.QueueName);
        }

        /// <summary>
        /// Publishes the specified serialized message.
        /// </summary>
        /// <param name="serializedMessage">The serialized message.</param>
        /// <param name="priority">The priority.</param>
        public void Publish(string serializedMessage, int priority)
        {
            Publish(Encoding.UTF8.GetBytes(serializedMessage), priority);
        }

        /// <summary>
        /// Publishes the specified bytes message.
        /// </summary>
        /// <param name="bytesMessage">The bytes message.</param>
        /// <param name="priority">The priority.</param>
        public void Publish(byte[] bytesMessage, int priority)
        {
            while (true)
            {
                try
                {
                    var basicProperties = _connection.Channel.CreateBasicProperties();
                    basicProperties.Priority = (byte)priority;
                    basicProperties.SetPersistent(true);

                    _connection.Channel.BasicPublish(_connection.PublicationAddress, basicProperties, bytesMessage);
                    break;
                }
                catch (Exception exception)
                {
                    Logger.Error("Error Socket Rabbit intentando reconectar", exception);
                    Reconnect();
                }
            }
        }

        private void Reconnect()
        {
            try
            {
                _connection.Dispose();
            }
            catch (Exception)
            {
                Logger.Info("Error Dispose durante la reconexion, se procede a la reconexion");
            }
            
            _connection = _connectionProvider.Connect();
            CreateBasicConsumer();
        }

        /// <summary>
        /// Gets the basic consumer.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public IQueueBasicConsumer BasicConsumer
        {
            get { return _consumer; }
        }

        /// <summary>
        /// Basics the ack.
        /// </summary>
        /// <param name="deliveryTag">The delivery tag.</param>
        /// <param name="multiple">if set to <c>true</c> [multiple].</param>
        public void BasicAck(ulong deliveryTag, bool multiple)
        {
            while (true)
            {
                try
                {
                    _connection.Channel.BasicAck(deliveryTag, multiple);
                    break;
                }
                catch (Exception exception)
                {
                    Logger.Error("Error Socket Rabbit (AlreadyClosedException), intentando reconectar", exception);
                    Reconnect();
                }
            }

            
        }

        /// <summary>
        /// Dequeues the specified milliseconds timeout.
        /// </summary>
        /// <param name="millisecondsTimeout">The milliseconds timeout.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public bool Dequeue(int millisecondsTimeout, out BasicDeliverEventArgs result)
        {
            while (true)
            {
                try
                {
                    return BasicConsumer.Queue.Dequeue(millisecondsTimeout, out result); 
                }
                catch (Exception exception)
                {
                    Logger.Error("Error Socket Rabbit (AlreadyClosedException), intentando reconectar", exception);
                    Reconnect();
                }
            }
            
        }

        /// <summary>
        /// Creates the consumer.
        /// </summary>
        public void CreateBasicConsumer()
        {
            if (_rabbitWrapperType == RabbitWrapperType.Output)
            {
                return;
            }

            while (true)
            {
                try
                {
                    _consumer = BasicConsumerFactory.CreateQueueConsumer(_connection.Channel, _connection.QueueName);
                    break;
                }
                catch (Exception exception)
                {
                    Logger.Error("Error Socket Rabbit (AlreadyClosedException), intentando reconectar", exception);
                    Reconnect();
                }
            }
            
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
                _connection.Dispose();
            }
        }
    }
}