using System;
using System.Collections;
using System.ComponentModel;
using System.Messaging;
using System.Runtime.Remoting;

#if !MONO
namespace HermEsb.Core.Communication.Channels.Msmq
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageQueueDecorator : IMessageQueue
    {
        private readonly MessageQueue _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueDecorator"/> class.
        /// </summary>
        /// <param name="queue">The queue.</param>
        public MessageQueueDecorator(MessageQueue queue)
        {
            _queue = queue;
        }

        /// <summary>
        /// Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/></PermissionSet>
        public object GetLifetimeService()
        {
            return _queue.GetLifetimeService();
        }

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/> property.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/></PermissionSet>
        public object InitializeLifetimeService()
        {
            return _queue.InitializeLifetimeService();
        }

        /// <summary>
        /// Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <returns>
        /// Information required to generate a proxy.
        /// </returns>
        /// <param name="requestedType">The <see cref="T:System.Type"/> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef"/> will reference. </param><exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object. </exception><exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure"/></PermissionSet>
        public ObjRef CreateObjRef(Type requestedType)
        {
            return _queue.CreateObjRef(requestedType);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        public void Dispose()
        {
            _queue.Dispose();
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.ComponentModel.ISite"/> of the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.ISite"/> associated with the <see cref="T:System.ComponentModel.Component"/>, or null if the <see cref="T:System.ComponentModel.Component"/> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer"/>, the <see cref="T:System.ComponentModel.Component"/> does not have an <see cref="T:System.ComponentModel.ISite"/> associated with it, or the <see cref="T:System.ComponentModel.Component"/> is removed from its <see cref="T:System.ComponentModel.IContainer"/>.
        /// </returns>
        public ISite Site
        {
            get { return _queue.Site; }
            set { _queue.Site = value; }
        }

        /// <summary>
        /// Gets the <see cref="T:System.ComponentModel.IContainer"/> that contains the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.IContainer"/> that contains the <see cref="T:System.ComponentModel.Component"/>, if any, or null if the <see cref="T:System.ComponentModel.Component"/> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer"/>.
        /// </returns>
        public IContainer Container
        {
            get { return _queue.Container; }
        }

        /// <summary>
        /// Occurs when [disposed].
        /// </summary>
        public event EventHandler Disposed
        {
            add { _queue.Disposed += value; }
            remove { _queue.Disposed -= value; }
        }

        /// <summary>
        /// Initiates an asynchronous peek operation that has no time-out. The operation is not complete until a message becomes available in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginPeek()
        {
            return _queue.BeginPeek();
        }

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginPeek(TimeSpan timeout)
        {
            return _queue.BeginPeek(timeout);
        }

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginPeek(TimeSpan timeout, object stateObject)
        {
            return _queue.BeginPeek(timeout, stateObject);
        }

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that will receive the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginPeek(TimeSpan timeout, object stateObject, AsyncCallback callback)
        {
            return _queue.BeginPeek(timeout, stateObject, callback);
        }

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and that uses a specified cursor, a specified peek action, and a specified state object. The state object provides associated information throughout the lifetime of the operation. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="action">One of the <see cref="T:System.Messaging.PeekAction"/> values. Indicates whether to peek at the current message in the queue, or the next message.</param><param name="state">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that receives the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentOutOfRangeException">A value other than PeekAction.Current or PeekAction.Next was specified for the <paramref name="action"/> parameter.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public IAsyncResult BeginPeek(TimeSpan timeout, Cursor cursor, PeekAction action, object state, AsyncCallback callback)
        {
            return _queue.BeginPeek(timeout, cursor, action, state, callback);
        }

        /// <summary>
        /// Initiates an asynchronous receive operation that has no time-out. The operation is not complete until a message becomes available in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginReceive()
        {
            return _queue.BeginReceive();
        }

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly because it represents a negative number. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginReceive(TimeSpan timeout)
        {
            return _queue.BeginReceive(timeout);
        }

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginReceive(TimeSpan timeout, object stateObject)
        {
            return _queue.BeginReceive(timeout, stateObject);
        }

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that will receive the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public IAsyncResult BeginReceive(TimeSpan timeout, object stateObject, AsyncCallback callback)
        {
            return _queue.BeginReceive(timeout, stateObject, callback);
        }

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and uses a specified cursor and a specified state object. The state object provides associated information throughout the lifetime of the operation. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="state">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that receives the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public IAsyncResult BeginReceive(TimeSpan timeout, Cursor cursor, object state, AsyncCallback callback)
        {
            return _queue.BeginReceive(timeout, cursor, state, callback);
        }

        /// <summary>
        /// Frees all resources allocated by the <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        public void Close()
        {
            _queue.Close();
        }

        /// <summary>
        /// Creates a new <see cref="T:System.Messaging.Cursor"/> for the current message queue.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Messaging.Cursor"/> for the current message queue. This cursor is used to maintain a specific location in the queue when reading the queue's messages.
        /// </returns>
        public Cursor CreateCursor()
        {
            return _queue.CreateCursor();
        }

        /// <summary>
        /// Completes the specified asynchronous peek operation.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> associated with the completed asynchronous operation.
        /// </returns>
        /// <param name="asyncResult">The <see cref="T:System.IAsyncResult"/> that identifies the asynchronous peek operation to finish and from which to retrieve an end result. </param><exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The syntax of the <paramref name="asyncResult"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public Message EndPeek(IAsyncResult asyncResult)
        {
            return _queue.EndPeek(asyncResult);
        }

        /// <summary>
        /// Completes the specified asynchronous receive operation.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> associated with the completed asynchronous operation.
        /// </returns>
        /// <param name="asyncResult">The <see cref="T:System.IAsyncResult"/> that identifies the asynchronous receive operation to finish and from which to retrieve an end result. </param><exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The syntax of the <paramref name="asyncResult"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public Message EndReceive(IAsyncResult asyncResult)
        {
            return _queue.EndReceive(asyncResult);
        }

        /// <summary>
        /// Returns all the messages that are in the queue.
        /// </summary>
        /// <returns>
        /// An array of type <see cref="T:System.Messaging.Message"/> that represents all the messages in the queue, in the same order as they appear in the Message Queuing queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message[] GetAllMessages()
        {
            return _queue.GetAllMessages();
        }

        /// <summary>
        /// Enumerates the messages in a queue. <see cref="M:System.Messaging.MessageQueue.GetEnumerator"/> is deprecated. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator2"/> should be used instead.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.IEnumerator"/> that provides a dynamic connection to the messages in the queue.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public IEnumerator GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        /// <summary>
        /// Creates an enumerator object for all the messages in the queue. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator"/> is deprecated. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator2"/> should be used instead.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessageEnumerator"/> holding the messages that are contained in the queue.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public MessageEnumerator GetMessageEnumerator()
        {
            return _queue.GetMessageEnumerator();
        }

        /// <summary>
        /// Creates an enumerator object for all the messages in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessageEnumerator"/> holding the messages that are contained in the queue.
        /// </returns>
        public MessageEnumerator GetMessageEnumerator2()
        {
            return _queue.GetMessageEnumerator2();
        }

        /// <summary>
        /// Returns without removing (peeks) the first message in the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> that represents the first message in the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Peek()
        {
            return _queue.Peek();
        }

        /// <summary>
        /// Returns without removing (peeks) the first message in the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available or the specified time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> that represents the first message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the maximum time to wait for the queue to contain a message. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Peek(TimeSpan timeout)
        {
            return _queue.Peek(timeout);
        }

        /// <summary>
        /// Returns without removing (peeks) the current or next message in the queue, using the specified cursor. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available or the specified time-out occurs.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that represents a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the maximum time to wait for the queue to contain a message. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="action">One of the <see cref="T:System.Messaging.PeekAction"/> values. Indicates whether to peek at the current message in the queue, or the next message.</param><exception cref="T:System.ArgumentOutOfRangeException">A value other than PeekAction.Current or PeekAction.Next was specified for the <paramref name="action"/> parameter.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public Message Peek(TimeSpan timeout, Cursor cursor, PeekAction action)
        {
            return _queue.Peek(timeout, cursor, action);
        }

        /// <summary>
        /// Peeks the message whose message identifier matches the <paramref name="id"/> parameter.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to peek. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">No message with the specified <paramref name="id"/> exists. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message PeekById(string id)
        {
            return _queue.PeekById(id);
        }

        /// <summary>
        /// Peeks the message whose message identifier matches the <paramref name="id"/> parameter. Waits until the message appears in the queue or a time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to peek. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> does not exist in the queue and did not arrive before the period specified by the <paramref name="timeout"/> parameter expired. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message PeekById(string id, TimeSpan timeout)
        {
            return _queue.PeekById(id, timeout);
        }

        /// <summary>
        /// Peeks the message that matches the given correlation identifier and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to peek. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message PeekByCorrelationId(string correlationId)
        {
            return _queue.PeekByCorrelationId(correlationId);
        }

        /// <summary>
        /// Peeks the message that matches the given correlation identifier and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to peek. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message PeekByCorrelationId(string correlationId, TimeSpan timeout)
        {
            return _queue.PeekByCorrelationId(correlationId, timeout);
        }

        /// <summary>
        /// Deletes all the messages contained in the queue.
        /// </summary>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void Purge()
        {
            _queue.Purge();
        }

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive()
        {
            return _queue.Receive();
        }

        /// <summary>
        /// Receives the first message available in the transactional queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method.-or- The queue is non-transactional. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive(MessageQueueTransaction transaction)
        {
            return _queue.Receive(transaction);
        }

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive(MessageQueueTransactionType transactionType)
        {
            return _queue.Receive(transactionType);
        }

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/> and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive(TimeSpan timeout)
        {
            return _queue.Receive(timeout);
        }

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method </exception>
        public Message Receive(TimeSpan timeout, Cursor cursor)
        {
            return _queue.Receive(timeout, cursor);
        }

        /// <summary>
        /// Receives the first message available in the transactional queue referenced by the <see cref="T:System.Messaging.MessageQueue"/> and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive(TimeSpan timeout, MessageQueueTransaction transaction)
        {
            return _queue.Receive(timeout, transaction);
        }

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message Receive(TimeSpan timeout, MessageQueueTransactionType transactionType)
        {
            return _queue.Receive(timeout, transactionType);
        }

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.-or-The <paramref name="transaction"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception>
        public Message Receive(TimeSpan timeout, Cursor cursor, MessageQueueTransaction transaction)
        {
            return _queue.Receive(timeout, cursor, transaction);
        }

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values that describes the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception>
        public Message Receive(TimeSpan timeout, Cursor cursor, MessageQueueTransactionType transactionType)
        {
            return _queue.Receive(timeout, cursor, transactionType);
        }

        /// <summary>
        /// Receives the message that matches the given identifier from a non-transactional queue and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id)
        {
            return _queue.ReceiveById(id);
        }

        /// <summary>
        /// Receives the message that matches the given identifier (from a transactional queue) and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id, MessageQueueTransaction transaction)
        {
            return _queue.ReceiveById(id, transaction);
        }

        /// <summary>
        /// Receives the message that matches the given identifier and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id, MessageQueueTransactionType transactionType)
        {
            return _queue.ReceiveById(id, transactionType);
        }

        /// <summary>
        /// Receives the message that matches the given identifier (from a non-transactional queue) and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id, TimeSpan timeout)
        {
            return _queue.ReceiveById(id, timeout);
        }

        /// <summary>
        /// Receives the message that matches the given identifier (from a transactional queue) and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id, TimeSpan timeout, MessageQueueTransaction transaction)
        {
            return _queue.ReceiveById(id, timeout, transaction);
        }

        /// <summary>
        /// Receives the message that matches the given identifier and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveById(string id, TimeSpan timeout, MessageQueueTransactionType transactionType)
        {
            return _queue.ReceiveById(id, timeout, transactionType);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a non-transactional queue) and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId)
        {
            return _queue.ReceiveByCorrelationId(correlationId);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a transactional queue) and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId, MessageQueueTransaction transaction)
        {
            return _queue.ReceiveByCorrelationId(correlationId, transaction);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId, MessageQueueTransactionType transactionType)
        {
            return _queue.ReceiveByCorrelationId(correlationId, transactionType);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a non-transactional queue) and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout)
        {
            return _queue.ReceiveByCorrelationId(correlationId, timeout);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a transactional queue) and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout, MessageQueueTransaction transaction)
        {
            return _queue.ReceiveByCorrelationId(correlationId, timeout, transaction);
        }

        /// <summary>
        /// Receives the message that matches the given correlation identifier and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout, MessageQueueTransactionType transactionType)
        {
            return _queue.ReceiveByCorrelationId(correlationId, timeout, transactionType);
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives the message that matches the given lookup identifier from a non-transactional queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.LookupId"/> property matches the <paramref name="lookupId"/> parameter passed in.
        /// </returns>
        /// <param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public Message ReceiveByLookupId(long lookupId)
        {
            return _queue.ReceiveByLookupId(lookupId);
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives a specific message from the queue, using the specified transaction context. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="action"/> and <paramref name="lookupId"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Receives the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Next: Receives the message following the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Previous: Receives the message preceding the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.First: Receives the first message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Receives the last message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive, or 0. 0 is used when accessing the first or last message in the queue. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message.</param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.-or- The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members.</exception>
        public Message ReceiveByLookupId(MessageLookupAction action, long lookupId, MessageQueueTransactionType transactionType)
        {
            return _queue.ReceiveByLookupId(action, lookupId, transactionType);
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives a specific message from a transactional queue. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="lookupId"/> and <paramref name="action"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Receives the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Next: Receives the message following the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Previous: Receives the message preceding the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.First: Receives the first message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Receives the last message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive, or 0. 0 is used when accessing the first or last message in the queue. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object.</param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method.-or- The queue is non-transactional.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.</exception>
        public Message ReceiveByLookupId(MessageLookupAction action, long lookupId, MessageQueueTransaction transaction)
        {
            return _queue.ReceiveByLookupId(action, lookupId, transaction);
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Peeks at the message that matches the given lookup identifier from a non-transactional queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.LookupId"/> property matches the <paramref name="lookupId"/> parameter passed in.
        /// </returns>
        /// <param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to peek at. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        public Message PeekByLookupId(long lookupId)
        {
            return _queue.PeekByLookupId(lookupId);
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Peeks at a specific message from the queue. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="action"/> and <paramref name="lookupId"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Peeks at the message specified by <paramref name="lookupId"/>.MessageLookupAction.Next: Peeks at the message following the message specified by <paramref name="lookupId"/>.MessageLookupAction.Previous: Peeks at the message preceding the message specified by <paramref name="lookupId"/>.MessageLookupAction.First: Peeks at the first message in the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Peeks at the last message in the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to peek at, or 0. 0 is used when accessing the first or last message in the queue. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.</exception>
        public Message PeekByLookupId(MessageLookupAction action, long lookupId)
        {
            return _queue.PeekByLookupId(action, lookupId);
        }

        /// <summary>
        /// Refreshes the properties presented by the <see cref="T:System.Messaging.MessageQueue"/> to reflect the current state of the resource.
        /// </summary>
        public void Refresh()
        {
            _queue.Refresh();
        }

        /// <summary>
        /// Sends an object to non-transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj)
        {
            _queue.Send(obj);
        }

        /// <summary>
        /// Sends an object to the transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- The Message Queuing application indicated an incorrect transaction use.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj, MessageQueueTransaction transaction)
        {
            _queue.Send(obj, transaction);
        }

        /// <summary>
        /// Sends an object to the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj, MessageQueueTransactionType transactionType)
        {
            _queue.Send(obj, transactionType);
        }

        /// <summary>
        /// Sends an object to the non-transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj, string label)
        {
            _queue.Send(obj, label);
        }

        /// <summary>
        /// Sends an object to the transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- The Message Queuing application indicated an incorrect transaction usage.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj, string label, MessageQueueTransaction transaction)
        {
            _queue.Send(obj, label, transaction);
        }

        /// <summary>
        /// Sends an object to the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueTransaction">The Message Queuing application indicated an incorrect transaction usage. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Send(object obj, string label, MessageQueueTransactionType transactionType)
        {
            _queue.Send(obj, label, transactionType);
        }

        /// <summary>
        /// Resets the permission list to the operating system's default values. Removes any queue permissions you have appended to the default list.
        /// </summary>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void ResetPermissions()
        {
            _queue.ResetPermissions();
        }

        /// <summary>
        /// Gives a computer, group, or user the specified access rights.
        /// </summary>
        /// <param name="user">The individual, group, or computer that gets additional rights to the queue. </param><param name="rights">A <see cref="T:System.Messaging.MessageQueueAccessRights"/> that indicates the set of rights to the queue that Message Queuing assigns to the <paramref name="user"/> passed in. </param><exception cref="T:System.ArgumentException">The <paramref name="user"/> is null. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void SetPermissions(string user, MessageQueueAccessRights rights)
        {
            _queue.SetPermissions(user, rights);
        }

        /// <summary>
        /// Gives a computer, group, or user the specified access rights, with the specified access control type (allow, deny, revoke, or set).
        /// </summary>
        /// <param name="user">The individual, group, or computer that gets additional rights to the queue. </param><param name="rights">A <see cref="T:System.Messaging.MessageQueueAccessRights"/> that indicates the set of rights to the queue that Message Queuing assigns to the <paramref name="user"/> passed in. </param><param name="entryType">A <see cref="T:System.Messaging.AccessControlEntryType"/> that specifies whether to grant, deny, or revoke the permissions specified by the <paramref name="rights"/> parameter. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void SetPermissions(string user, MessageQueueAccessRights rights, AccessControlEntryType entryType)
        {
            _queue.SetPermissions(user, rights, entryType);
        }

        /// <summary>
        /// Assigns access rights to the queue based on the contents of an access control entry.
        /// </summary>
        /// <param name="ace">A <see cref="T:System.Messaging.MessageQueueAccessControlEntry"/> that specifies a user, an access type, and a permission type. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void SetPermissions(MessageQueueAccessControlEntry ace)
        {
            _queue.SetPermissions(ace);
        }

        /// <summary>
        /// Assigns access rights to the queue based on the contents of an access control list.
        /// </summary>
        /// <param name="dacl">A <see cref="T:System.Messaging.AccessControlList"/> that contains one or more access control entries that specify the trustees and the permissions to grant. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public void SetPermissions(AccessControlList dacl)
        {
            _queue.SetPermissions(dacl);
        }

        /// <summary>
        /// Gets a value that indicates the access mode for the queue.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Messaging.QueueAccessMode"/> values.
        /// </returns>
        public QueueAccessMode AccessMode
        {
            get { return _queue.AccessMode; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the queue accepts only authenticated messages.
        /// </summary>
        /// <returns>
        /// true if the queue accepts only authenticated messages; otherwise, false. The default is false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public bool Authenticate
        {
            get { return _queue.Authenticate; }
            set { _queue.Authenticate = value; }
        }

        /// <summary>
        /// Gets or sets the base priority Message Queuing uses to route a public queue's messages over the network.
        /// </summary>
        /// <returns>
        /// The single base priority for all messages sent to the (public) queue. The default is zero (0).
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The base priority was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public short BasePriority
        {
            get { return _queue.BasePriority; }
            set { _queue.BasePriority = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="T:System.Messaging.MessageQueue"/> can be read.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Messaging.MessageQueue"/> exists and the application can read from it; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public bool CanRead
        {
            get { return _queue.CanRead; }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="T:System.Messaging.MessageQueue"/> can be written to.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Messaging.MessageQueue"/> exists and the application can write to it; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public bool CanWrite
        {
            get { return _queue.CanWrite; }
        }

        /// <summary>
        /// Gets or sets the queue category.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Guid"/> that represents the queue category (Message Queuing type identifier), which allows an application to categorize its queues. The default is Guid.empty.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The queue category was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public Guid Category
        {
            get { return _queue.Category; }
            set { _queue.Category = value; }
        }

        /// <summary>
        /// Gets the time and date that the queue was created in Message Queuing.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> that represents the date and time at which the queue was created.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public DateTime CreateTime
        {
            get { return _queue.CreateTime; }
        }

        /// <summary>
        /// Gets or sets the message property values to be used by default when the application sends messages to the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.DefaultPropertiesToSend"/> that contains the default Message Queuing message property values used when the application sends objects other than <see cref="T:System.Messaging.Message"/> instances to the queue.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The default properties could not be set for the queue, possibly because one of the properties is not valid. </exception>
        public DefaultPropertiesToSend DefaultPropertiesToSend
        {
            get { return _queue.DefaultPropertiesToSend; }
            set { _queue.DefaultPropertiesToSend = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="T:System.Messaging.MessageQueue"/> has exclusive access to receive messages from the Message Queuing queue.
        /// </summary>
        /// <returns>
        /// true if this <see cref="T:System.Messaging.MessageQueue"/> has exclusive rights to receive messages from the queue; otherwise, false. The default is false.
        /// </returns>
        public bool DenySharedReceive
        {
            get { return _queue.DenySharedReceive; }
            set { _queue.DenySharedReceive = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the queue accepts only non-private (non-encrypted) messages.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Messaging.EncryptionRequired"/> values. The default is None.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public EncryptionRequired EncryptionRequired
        {
            get { return _queue.EncryptionRequired; }
            set { _queue.EncryptionRequired = value; }
        }

        /// <summary>
        /// Gets the unique queue name that Message Queuing generated at the time of the queue's creation.
        /// </summary>
        /// <returns>
        /// The name for the queue, which is unique on the network.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> is not set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public string FormatName
        {
            get { return _queue.FormatName; }
        }

        /// <summary>
        /// Gets or sets the formatter used to serialize an object into or deserialize an object from the body of a message read from or written to the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.IMessageFormatter"/> that produces a stream to be written to or read from the message body. The default is <see cref="T:System.Messaging.XmlMessageFormatter"/>.
        /// </returns>
        public IMessageFormatter Formatter
        {
            get { return _queue.Formatter; }
            set { _queue.Formatter = value; }
        }

        /// <summary>
        /// Gets the unique Message Queuing identifier of the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="P:System.Messaging.MessageQueue.Id"/> that represents the message identifier generated by the Message Queuing application.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public Guid Id
        {
            get { return _queue.Id; }
        }

        /// <summary>
        /// Gets or sets the queue description.
        /// </summary>
        /// <returns>
        /// The label for the message queue. The default is an empty string ("").
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The label was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public string Label
        {
            get { return _queue.Label; }
            set { _queue.Label = value; }
        }

        /// <summary>
        /// Gets the last time the properties of a queue were modified.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> that indicates when the queue properties were last modified.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public DateTime LastModifyTime
        {
            get { return _queue.LastModifyTime; }
        }

        /// <summary>
        /// Gets or sets the name of the computer where the Message Queuing queue is located.
        /// </summary>
        /// <returns>
        /// The name of the computer where the queue is located. The Message Queuing default is ".", the local computer.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Messaging.MessageQueue.MachineName"/> is null. </exception><exception cref="T:System.ArgumentException">The name of the computer is not valid, possibly because the syntax is incorrect. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public string MachineName
        {
            get { return _queue.MachineName; }
            set { _queue.MachineName = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the journal queue.
        /// </summary>
        /// <returns>
        /// The maximum size, in kilobytes, of the journal queue. The Message Queuing default specifies that no limit exists.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The maximum journal queue size was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public long MaximumJournalSize
        {
            get { return _queue.MaximumJournalSize; }
            set { _queue.MaximumJournalSize = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the queue.
        /// </summary>
        /// <returns>
        /// The maximum size, in kilobytes, of the queue. The Message Queuing default specifies that no limit exists.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The maximum queue size contains a negative value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public long MaximumQueueSize
        {
            get { return _queue.MaximumQueueSize; }
            set { _queue.MaximumQueueSize = value; }
        }

        /// <summary>
        /// Gets or sets the property filter for receiving or peeking messages.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessagePropertyFilter"/> used by the queue to filter the set of properties it receives or peeks for each message.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The filter is null. </exception>
        public MessagePropertyFilter MessageReadPropertyFilter
        {
            get { return _queue.MessageReadPropertyFilter; }
            set { _queue.MessageReadPropertyFilter = value; }
        }

        /// <summary>
        /// Introduced in MSMQ 3.0. Gets or sets the multicast address associated with the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that contains a valid multicast address (in the form shown below) or null, which indicates that the queue is not associated with a multicast address.�Copy Code&lt;address&gt;:&lt;port&gt;
        /// </returns>
        /// <exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception>
        public string MulticastAddress
        {
            get { return _queue.MulticastAddress; }
            set { _queue.MulticastAddress = value; }
        }

        /// <summary>
        /// Gets or sets the queue's path. Setting the <see cref="P:System.Messaging.MessageQueue.Path"/> causes the <see cref="T:System.Messaging.MessageQueue"/> to point to a new queue.
        /// </summary>
        /// <returns>
        /// The queue that is referenced by the <see cref="T:System.Messaging.MessageQueue"/>. The default depends on which <see cref="M:System.Messaging.MessageQueue.#ctor"/> constructor you use; it is either null or is specified by the constructor's <paramref name="path"/> parameter.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The path is not valid, possibly because the syntax is not valid. </exception>
        public string Path
        {
            get { return _queue.Path; }
            set { _queue.Path = value; }
        }

        /// <summary>
        /// Gets or sets the friendly name that identifies the queue.
        /// </summary>
        /// <returns>
        /// The name that identifies the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The value cannot be null.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The queue name is null. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public string QueueName
        {
            get { return _queue.QueueName; }
            set { _queue.QueueName = value; }
        }

        /// <summary>
        /// Gets the native handle used to read messages from the message queue.
        /// </summary>
        /// <returns>
        /// A handle to the native queue object that you use for peeking and receiving messages from the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public IntPtr ReadHandle
        {
            get { return _queue.ReadHandle; }
        }

        /// <summary>
        /// Gets or sets the object that marshals the event-handler call resulting from a <see cref="E:System.Messaging.MessageQueue.ReceiveCompleted"/> or <see cref="E:System.Messaging.MessageQueue.PeekCompleted"/> event.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.ISynchronizeInvoke"/>, which represents the object that marshals the event-handler call resulting from a <see cref="E:System.Messaging.MessageQueue.ReceiveCompleted"/> or <see cref="E:System.Messaging.MessageQueue.PeekCompleted"/> event. The default is null.
        /// </returns>
        public ISynchronizeInvoke SynchronizingObject
        {
            get { return _queue.SynchronizingObject; }
            set { _queue.SynchronizingObject = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the queue accepts only transactions.
        /// </summary>
        /// <returns>
        /// true if the queue accepts only messages sent as part of a transaction; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public bool Transactional
        {
            get { return _queue.Transactional; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether received messages are copied to the journal queue.
        /// </summary>
        /// <returns>
        /// true if messages received from the queue are copied to its journal queue; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public bool UseJournalQueue
        {
            get { return _queue.UseJournalQueue; }
            set { _queue.UseJournalQueue = value; }
        }

        /// <summary>
        /// Gets the native handle used to send messages to the message queue.
        /// </summary>
        /// <returns>
        /// A handle to the native queue object that you use for sending messages to the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">The message queue is not available for writing. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        public IntPtr WriteHandle
        {
            get { return _queue.WriteHandle; }
        }

        public event PeekCompletedEventHandler PeekCompleted
        {
            add { _queue.PeekCompleted += value; }
            remove { _queue.PeekCompleted -= value; }
        }

        public event ReceiveCompletedEventHandler ReceiveCompleted
        {
            add { _queue.ReceiveCompleted += value; }
            remove { _queue.ReceiveCompleted -= value; }
        }
    }
}
#endif