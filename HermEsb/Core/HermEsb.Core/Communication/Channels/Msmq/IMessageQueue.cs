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
    public interface IMessageQueue : IDisposable
    {
        /// <summary>
        /// Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/></PermissionSet>
        object GetLifetimeService();

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/> property.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/></PermissionSet>
        object InitializeLifetimeService();

        /// <summary>
        /// Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <returns>
        /// Information required to generate a proxy.
        /// </returns>
        /// <param name="requestedType">The <see cref="T:System.Type"/> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef"/> will reference. </param><exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object. </exception><exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure"/></PermissionSet>
        ObjRef CreateObjRef(Type requestedType);

        /// <summary>
        /// Gets or sets the <see cref="T:System.ComponentModel.ISite"/> of the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.ISite"/> associated with the <see cref="T:System.ComponentModel.Component"/>, or null if the <see cref="T:System.ComponentModel.Component"/> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer"/>, the <see cref="T:System.ComponentModel.Component"/> does not have an <see cref="T:System.ComponentModel.ISite"/> associated with it, or the <see cref="T:System.ComponentModel.Component"/> is removed from its <see cref="T:System.ComponentModel.IContainer"/>.
        /// </returns>
        ISite Site { get; set; }

        /// <summary>
        /// Gets the <see cref="T:System.ComponentModel.IContainer"/> that contains the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ComponentModel.IContainer"/> that contains the <see cref="T:System.ComponentModel.Component"/>, if any, or null if the <see cref="T:System.ComponentModel.Component"/> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer"/>.
        /// </returns>
        IContainer Container { get; }

        /// <summary>
        /// Gets a value that indicates the access mode for the queue.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Messaging.QueueAccessMode"/> values.
        /// </returns>
        QueueAccessMode AccessMode { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether the queue accepts only authenticated messages.
        /// </summary>
        /// <returns>
        /// true if the queue accepts only authenticated messages; otherwise, false. The default is false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        bool Authenticate { get; set; }

        /// <summary>
        /// Gets or sets the base priority Message Queuing uses to route a public queue's messages over the network.
        /// </summary>
        /// <returns>
        /// The single base priority for all messages sent to the (public) queue. The default is zero (0).
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The base priority was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        short BasePriority { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="T:System.Messaging.MessageQueue"/> can be read.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Messaging.MessageQueue"/> exists and the application can read from it; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        bool CanRead { get; }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="T:System.Messaging.MessageQueue"/> can be written to.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Messaging.MessageQueue"/> exists and the application can write to it; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        bool CanWrite { get; }

        /// <summary>
        /// Gets or sets the queue category.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Guid"/> that represents the queue category (Message Queuing type identifier), which allows an application to categorize its queues. The default is Guid.empty.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The queue category was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        Guid Category { get; set; }

        /// <summary>
        /// Gets the time and date that the queue was created in Message Queuing.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> that represents the date and time at which the queue was created.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        DateTime CreateTime { get; }

        /// <summary>
        /// Gets or sets the message property values to be used by default when the application sends messages to the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.DefaultPropertiesToSend"/> that contains the default Message Queuing message property values used when the application sends objects other than <see cref="T:System.Messaging.Message"/> instances to the queue.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The default properties could not be set for the queue, possibly because one of the properties is not valid. </exception>
        DefaultPropertiesToSend DefaultPropertiesToSend { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="T:System.Messaging.MessageQueue"/> has exclusive access to receive messages from the Message Queuing queue.
        /// </summary>
        /// <returns>
        /// true if this <see cref="T:System.Messaging.MessageQueue"/> has exclusive rights to receive messages from the queue; otherwise, false. The default is false.
        /// </returns>
        bool DenySharedReceive { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the queue accepts only non-private (non-encrypted) messages.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Messaging.EncryptionRequired"/> values. The default is None.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        EncryptionRequired EncryptionRequired { get; set; }

        /// <summary>
        /// Gets the unique queue name that Message Queuing generated at the time of the queue's creation.
        /// </summary>
        /// <returns>
        /// The name for the queue, which is unique on the network.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> is not set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        string FormatName { get; }

        /// <summary>
        /// Gets or sets the formatter used to serialize an object into or deserialize an object from the body of a message read from or written to the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.IMessageFormatter"/> that produces a stream to be written to or read from the message body. The default is <see cref="T:System.Messaging.XmlMessageFormatter"/>.
        /// </returns>
        IMessageFormatter Formatter { get; set; }

        /// <summary>
        /// Gets the unique Message Queuing identifier of the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="P:System.Messaging.MessageQueue.Id"/> that represents the message identifier generated by the Message Queuing application.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the queue description.
        /// </summary>
        /// <returns>
        /// The label for the message queue. The default is an empty string ("").
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The label was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        string Label { get; set; }

        /// <summary>
        /// Gets the last time the properties of a queue were modified.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> that indicates when the queue properties were last modified.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        DateTime LastModifyTime { get; }

        /// <summary>
        /// Gets or sets the name of the computer where the Message Queuing queue is located.
        /// </summary>
        /// <returns>
        /// The name of the computer where the queue is located. The Message Queuing default is ".", the local computer.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Messaging.MessageQueue.MachineName"/> is null. </exception><exception cref="T:System.ArgumentException">The name of the computer is not valid, possibly because the syntax is incorrect. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the journal queue.
        /// </summary>
        /// <returns>
        /// The maximum size, in kilobytes, of the journal queue. The Message Queuing default specifies that no limit exists.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The maximum journal queue size was set to an invalid value. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        long MaximumJournalSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the queue.
        /// </summary>
        /// <value>The maximum size of the queue.</value>
        long MaximumQueueSize { get; set; }

        /// <summary>
        /// Gets or sets the property filter for receiving or peeking messages.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessagePropertyFilter"/> used by the queue to filter the set of properties it receives or peeks for each message.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The filter is null. </exception>
        MessagePropertyFilter MessageReadPropertyFilter { get; set; }

        /// <summary>
        /// Introduced in MSMQ 3.0. Gets or sets the multicast address associated with the queue.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that contains a valid multicast address (in the form shown below) or null, which indicates that the queue is not associated with a multicast address. Copy Code&lt;address&gt;:&lt;port&gt;
        /// </returns>
        /// <exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception>
        string MulticastAddress { get; set; }

        /// <summary>
        /// Gets or sets the queue's path. Setting the <see cref="P:System.Messaging.MessageQueue.Path"/> causes the <see cref="T:System.Messaging.MessageQueue"/> to point to a new queue.
        /// </summary>
        /// <returns>
        /// The queue that is referenced by the <see cref="T:System.Messaging.MessageQueue"/>. The default depends on which <see cref="M:System.Messaging.MessageQueue.#ctor"/> constructor you use; it is either null or is specified by the constructor's <paramref name="path"/> parameter.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The path is not valid, possibly because the syntax is not valid. </exception>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the friendly name that identifies the queue.
        /// </summary>
        /// <returns>
        /// The name that identifies the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The value cannot be null.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The queue name is null. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        string QueueName { get; set; }

        /// <summary>
        /// Gets the native handle used to read messages from the message queue.
        /// </summary>
        /// <returns>
        /// A handle to the native queue object that you use for peeking and receiving messages from the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        IntPtr ReadHandle { get; }

        /// <summary>
        /// Gets or sets the object that marshals the event-handler call resulting from a <see cref="E:System.Messaging.MessageQueue.ReceiveCompleted"/> or <see cref="E:System.Messaging.MessageQueue.PeekCompleted"/> event.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.ISynchronizeInvoke"/>, which represents the object that marshals the event-handler call resulting from a <see cref="E:System.Messaging.MessageQueue.ReceiveCompleted"/> or <see cref="E:System.Messaging.MessageQueue.PeekCompleted"/> event. The default is null.
        /// </returns>
        ISynchronizeInvoke SynchronizingObject { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the queue accepts only transactions.
        /// </summary>
        /// <returns>
        /// true if the queue accepts only messages sent as part of a transaction; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        bool Transactional { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether received messages are copied to the journal queue.
        /// </summary>
        /// <returns>
        /// true if messages received from the queue are copied to its journal queue; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        bool UseJournalQueue { get; set; }

        /// <summary>
        /// Gets the native handle used to send messages to the message queue.
        /// </summary>
        /// <returns>
        /// A handle to the native queue object that you use for sending messages to the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">The message queue is not available for writing. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        IntPtr WriteHandle { get; }

        event EventHandler Disposed;

        /// <summary>
        /// Initiates an asynchronous peek operation that has no time-out. The operation is not complete until a message becomes available in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginPeek();

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginPeek(TimeSpan timeout);

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginPeek(TimeSpan timeout, object stateObject);

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that will receive the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginPeek(TimeSpan timeout, object stateObject, AsyncCallback callback);

        /// <summary>
        /// Initiates an asynchronous peek operation that has a specified time-out and that uses a specified cursor, a specified peek action, and a specified state object. The state object provides associated information throughout the lifetime of the operation. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="action">One of the <see cref="T:System.Messaging.PeekAction"/> values. Indicates whether to peek at the current message in the queue, or the next message.</param><param name="state">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that receives the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentOutOfRangeException">A value other than PeekAction.Current or PeekAction.Next was specified for the <paramref name="action"/> parameter.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        IAsyncResult BeginPeek(TimeSpan timeout, Cursor cursor, PeekAction action, object state, AsyncCallback callback);

        /// <summary>
        /// Initiates an asynchronous receive operation that has no time-out. The operation is not complete until a message becomes available in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginReceive();

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly because it represents a negative number. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginReceive(TimeSpan timeout);

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginReceive(TimeSpan timeout, object stateObject);

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and a specified state object, which provides associated information throughout the operation's lifetime. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="stateObject">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that will receive the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        IAsyncResult BeginReceive(TimeSpan timeout, object stateObject, AsyncCallback callback);

        /// <summary>
        /// Initiates an asynchronous receive operation that has a specified time-out and uses a specified cursor and a specified state object. The state object provides associated information throughout the lifetime of the operation. This overload receives notification, through a callback, of the identity of the event handler for the operation. The operation is not complete until either a message becomes available in the queue or the time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that identifies the posted asynchronous request.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the interval of time to wait for a message to become available. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="state">A state object, specified by the application, that contains information associated with the asynchronous operation. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> that receives the notification of the asynchronous operation completion. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        IAsyncResult BeginReceive(TimeSpan timeout, Cursor cursor, object state, AsyncCallback callback);

        /// <summary>
        /// Frees all resources allocated by the <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        void Close();

        /// <summary>
        /// Creates a new <see cref="T:System.Messaging.Cursor"/> for the current message queue.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Messaging.Cursor"/> for the current message queue. This cursor is used to maintain a specific location in the queue when reading the queue's messages.
        /// </returns>
        Cursor CreateCursor();

        /// <summary>
        /// Completes the specified asynchronous peek operation.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> associated with the completed asynchronous operation.
        /// </returns>
        /// <param name="asyncResult">The <see cref="T:System.IAsyncResult"/> that identifies the asynchronous peek operation to finish and from which to retrieve an end result. </param><exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The syntax of the <paramref name="asyncResult"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        Message EndPeek(IAsyncResult asyncResult);

        /// <summary>
        /// Completes the specified asynchronous receive operation.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> associated with the completed asynchronous operation.
        /// </returns>
        /// <param name="asyncResult">The <see cref="T:System.IAsyncResult"/> that identifies the asynchronous receive operation to finish and from which to retrieve an end result. </param><exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The syntax of the <paramref name="asyncResult"/> parameter is not valid. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        Message EndReceive(IAsyncResult asyncResult);

        /// <summary>
        /// Returns all the messages that are in the queue.
        /// </summary>
        /// <returns>
        /// An array of type <see cref="T:System.Messaging.Message"/> that represents all the messages in the queue, in the same order as they appear in the Message Queuing queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message[] GetAllMessages();

        /// <summary>
        /// Enumerates the messages in a queue. <see cref="M:System.Messaging.MessageQueue.GetEnumerator"/> is deprecated. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator2"/> should be used instead.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.IEnumerator"/> that provides a dynamic connection to the messages in the queue.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        IEnumerator GetEnumerator();

        /// <summary>
        /// Creates an enumerator object for all the messages in the queue. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator"/> is deprecated. <see cref="M:System.Messaging.MessageQueue.GetMessageEnumerator2"/> should be used instead.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessageEnumerator"/> holding the messages that are contained in the queue.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        MessageEnumerator GetMessageEnumerator();

        /// <summary>
        /// Creates an enumerator object for all the messages in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.MessageEnumerator"/> holding the messages that are contained in the queue.
        /// </returns>
        MessageEnumerator GetMessageEnumerator2();

        /// <summary>
        /// Returns without removing (peeks) the first message in the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> that represents the first message in the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Peek();

        /// <summary>
        /// Returns without removing (peeks) the first message in the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available or the specified time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> that represents the first message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the maximum time to wait for the queue to contain a message. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Peek(TimeSpan timeout);

        /// <summary>
        /// Returns without removing (peeks) the current or next message in the queue, using the specified cursor. The <see cref="M:System.Messaging.MessageQueue.Peek"/> method is synchronous, so it blocks the current thread until a message becomes available or the specified time-out occurs.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that represents a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the maximum time to wait for the queue to contain a message. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="action">One of the <see cref="T:System.Messaging.PeekAction"/> values. Indicates whether to peek at the current message in the queue, or the next message.</param><exception cref="T:System.ArgumentOutOfRangeException">A value other than PeekAction.Current or PeekAction.Next was specified for the <paramref name="action"/> parameter.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        Message Peek(TimeSpan timeout, Cursor cursor, PeekAction action);

        /// <summary>
        /// Peeks the message whose message identifier matches the <paramref name="id"/> parameter.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to peek. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">No message with the specified <paramref name="id"/> exists. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message PeekById(string id);

        /// <summary>
        /// Peeks the message whose message identifier matches the <paramref name="id"/> parameter. Waits until the message appears in the queue or a time-out occurs.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to peek. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> does not exist in the queue and did not arrive before the period specified by the <paramref name="timeout"/> parameter expired. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message PeekById(string id, TimeSpan timeout);

        /// <summary>
        /// Peeks the message that matches the given correlation identifier and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to peek. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message PeekByCorrelationId(string correlationId);

        /// <summary>
        /// Peeks the message that matches the given correlation identifier and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to peek. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message PeekByCorrelationId(string correlationId, TimeSpan timeout);

        /// <summary>
        /// Deletes all the messages contained in the queue.
        /// </summary>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void Purge();

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive();

        /// <summary>
        /// Receives the first message available in the transactional queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method.-or- The queue is non-transactional. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive(MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and blocks the current thread of execution until a message is available.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive(MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/> and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive(TimeSpan timeout);

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method </exception>
        Message Receive(TimeSpan timeout, Cursor cursor);

        /// <summary>
        /// Receives the first message available in the transactional queue referenced by the <see cref="T:System.Messaging.MessageQueue"/> and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive(TimeSpan timeout, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the first message available in the queue referenced by the <see cref="T:System.Messaging.MessageQueue"/>. This call is synchronous, and waits until either a message is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references the first message available in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message Receive(TimeSpan timeout, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.-or-The <paramref name="transaction"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception>
        Message Receive(TimeSpan timeout, Cursor cursor, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the current message in the queue, using a specified cursor. If no message is available, this method waits until either a message is available, or the time-out expires.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Messaging.Message"/> that references a message in the queue.
        /// </returns>
        /// <param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="cursor">A <see cref="T:System.Messaging.Cursor"/> that maintains a specific position in the message queue.</param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values that describes the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="cursor"/> parameter is null.</exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid. Possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">A message did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception>
        Message Receive(TimeSpan timeout, Cursor cursor, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the message that matches the given identifier from a non-transactional queue and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id);

        /// <summary>
        /// Receives the message that matches the given identifier (from a transactional queue) and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the message that matches the given identifier and immediately raises an exception if no message with the specified identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="id"/> could not be found. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the message that matches the given identifier (from a non-transactional queue) and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id, TimeSpan timeout);

        /// <summary>
        /// Receives the message that matches the given identifier (from a transactional queue) and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id, TimeSpan timeout, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the message that matches the given identifier and waits until either a message with the specified identifier is available in the queue or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.Id"/> property matches the <paramref name="id"/> parameter passed in.
        /// </returns>
        /// <param name="id">The <see cref="P:System.Messaging.Message.Id"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="id"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">A message with the specified <paramref name="id"/> did not arrive in the queue before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveById(string id, TimeSpan timeout, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a non-transactional queue) and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId);

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a transactional queue) and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the message that matches the given correlation identifier and immediately raises an exception if no message with the specified correlation identifier currently exists in the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a non-transactional queue) and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout);

        /// <summary>
        /// Receives the message that matches the given correlation identifier (from a transactional queue) and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- The queue is non-transactional.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout, MessageQueueTransaction transaction);

        /// <summary>
        /// Receives the message that matches the given correlation identifier and waits until either a message with the specified correlation identifier is available in the queue, or the time-out expires.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.CorrelationId"/> matches the <paramref name="correlationId"/> parameter passed in.
        /// </returns>
        /// <param name="correlationId">The <see cref="P:System.Messaging.Message.CorrelationId"/> of the message to receive. </param><param name="timeout">A <see cref="T:System.TimeSpan"/> that indicates the time to wait until a new message is available for inspection. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="correlationId"/> parameter is null. </exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="correlationId"/> could not be found. </exception><exception cref="T:System.ArgumentException">The value specified for the <paramref name="timeout"/> parameter is not valid, possibly <paramref name="timeout"/> is less than <see cref="F:System.TimeSpan.Zero"/> or greater than <see cref="F:System.Messaging.MessageQueue.InfiniteTimeout"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The message with the specified <paramref name="correlationId"/> does not exist in the queue and did not arrive before the time-out expired.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        Message ReceiveByCorrelationId(string correlationId, TimeSpan timeout, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives the message that matches the given lookup identifier from a non-transactional queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.LookupId"/> property matches the <paramref name="lookupId"/> parameter passed in.
        /// </returns>
        /// <param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        Message ReceiveByLookupId(long lookupId);

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives a specific message from the queue, using the specified transaction context. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="action"/> and <paramref name="lookupId"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Receives the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Next: Receives the message following the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Previous: Receives the message preceding the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.First: Receives the first message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Receives the last message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive, or 0. 0 is used when accessing the first or last message in the queue. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message.</param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.-or- The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members.</exception>
        Message ReceiveByLookupId(MessageLookupAction action, long lookupId, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Introduced in MSMQ 3.0. Receives a specific message from a transactional queue. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="lookupId"/> and <paramref name="action"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Receives the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Next: Receives the message following the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.Previous: Receives the message preceding the message specified by <paramref name="lookupId"/> and removes it from the queue.MessageLookupAction.First: Receives the first message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Receives the last message in the queue and removes it from the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to receive, or 0. 0 is used when accessing the first or last message in the queue. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object.</param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method.-or- The queue is non-transactional.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.</exception>
        Message ReceiveByLookupId(MessageLookupAction action, long lookupId, MessageQueueTransaction transaction);

        /// <summary>
        /// Introduced in MSMQ 3.0. Peeks at the message that matches the given lookup identifier from a non-transactional queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> whose <see cref="P:System.Messaging.Message.LookupId"/> property matches the <paramref name="lookupId"/> parameter passed in.
        /// </returns>
        /// <param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to peek at. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception>
        Message PeekByLookupId(long lookupId);

        /// <summary>
        /// Introduced in MSMQ 3.0. Peeks at a specific message from the queue. The message can be specified by a lookup identifier or by its position at the front or end of the queue.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Messaging.Message"/> specified by the <paramref name="action"/> and <paramref name="lookupId"/> parameters passed in.
        /// </returns>
        /// <param name="action">One of the <see cref="T:System.Messaging.MessageLookupAction"/> values, specifying how the message is read in the queue. Specify one of the following:MessageLookupAction.Current: Peeks at the message specified by <paramref name="lookupId"/>.MessageLookupAction.Next: Peeks at the message following the message specified by <paramref name="lookupId"/>.MessageLookupAction.Previous: Peeks at the message preceding the message specified by <paramref name="lookupId"/>.MessageLookupAction.First: Peeks at the first message in the queue. The <paramref name="lookupId"/> parameter must be set to 0.MessageLookupAction.Last: Peeks at the last message in the queue. The <paramref name="lookupId"/> parameter must be set to 0.</param><param name="lookupId">The <see cref="P:System.Messaging.Message.LookupId"/> of the message to peek at, or 0. 0 is used when accessing the first or last message in the queue. </param><exception cref="T:System.PlatformNotSupportedException">MSMQ 3.0 is not installed.</exception><exception cref="T:System.InvalidOperationException">The message with the specified <paramref name="lookupId"/> could not be found. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="action"/> parameter is not one of the <see cref="T:System.Messaging.MessageLookupAction"/> members.</exception>
        Message PeekByLookupId(MessageLookupAction action, long lookupId);

        /// <summary>
        /// Refreshes the properties presented by the <see cref="T:System.Messaging.MessageQueue"/> to reflect the current state of the resource.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Sends an object to non-transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj);

        /// <summary>
        /// Sends an object to the transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- The Message Queuing application indicated an incorrect transaction use.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj, MessageQueueTransaction transaction);

        /// <summary>
        /// Sends an object to the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/>.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Sends an object to the non-transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj, string label);

        /// <summary>
        /// Sends an object to the transactional queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><param name="transaction">The <see cref="T:System.Messaging.MessageQueueTransaction"/> object. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null.-or- The <paramref name="transaction"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- The Message Queuing application indicated an incorrect transaction usage.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj, string label, MessageQueueTransaction transaction);

        /// <summary>
        /// Sends an object to the queue referenced by this <see cref="T:System.Messaging.MessageQueue"/> and specifies a label for the message.
        /// </summary>
        /// <param name="obj">The object to send to the queue. </param><param name="label">The label of the message. </param><param name="transactionType">One of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> values, describing the type of transaction context to associate with the message. </param><exception cref="T:System.ArgumentNullException">The <paramref name="label"/> parameter is null. </exception><exception cref="T:System.Messaging.MessageQueueTransaction">The Message Queuing application indicated an incorrect transaction usage. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transactionType"/> parameter is not one of the <see cref="T:System.Messaging.MessageQueueTransactionType"/> members. </exception><exception cref="T:System.Messaging.MessageQueueException">The <see cref="P:System.Messaging.MessageQueue.Path"/> property has not been set.-or- An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        void Send(object obj, string label, MessageQueueTransactionType transactionType);

        /// <summary>
        /// Resets the permission list to the operating system's default values. Removes any queue permissions you have appended to the default list.
        /// </summary>
        /// <exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void ResetPermissions();

        /// <summary>
        /// Gives a computer, group, or user the specified access rights.
        /// </summary>
        /// <param name="user">The individual, group, or computer that gets additional rights to the queue. </param><param name="rights">A <see cref="T:System.Messaging.MessageQueueAccessRights"/> that indicates the set of rights to the queue that Message Queuing assigns to the <paramref name="user"/> passed in. </param><exception cref="T:System.ArgumentException">The <paramref name="user"/> is null. </exception><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void SetPermissions(string user, MessageQueueAccessRights rights);

        /// <summary>
        /// Gives a computer, group, or user the specified access rights, with the specified access control type (allow, deny, revoke, or set).
        /// </summary>
        /// <param name="user">The individual, group, or computer that gets additional rights to the queue. </param><param name="rights">A <see cref="T:System.Messaging.MessageQueueAccessRights"/> that indicates the set of rights to the queue that Message Queuing assigns to the <paramref name="user"/> passed in. </param><param name="entryType">A <see cref="T:System.Messaging.AccessControlEntryType"/> that specifies whether to grant, deny, or revoke the permissions specified by the <paramref name="rights"/> parameter. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void SetPermissions(string user, MessageQueueAccessRights rights, AccessControlEntryType entryType);

        /// <summary>
        /// Assigns access rights to the queue based on the contents of an access control entry.
        /// </summary>
        /// <param name="ace">A <see cref="T:System.Messaging.MessageQueueAccessControlEntry"/> that specifies a user, an access type, and a permission type. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void SetPermissions(MessageQueueAccessControlEntry ace);

        /// <summary>
        /// Assigns access rights to the queue based on the contents of an access control list.
        /// </summary>
        /// <param name="dacl">A <see cref="T:System.Messaging.AccessControlList"/> that contains one or more access control entries that specify the trustees and the permissions to grant. </param><exception cref="T:System.Messaging.MessageQueueException">An error occurred when accessing a Message Queuing method. </exception><PermissionSet><IPermission class="System.Messaging.MessageQueuePermission, System.Messaging, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true"/></PermissionSet>
        void SetPermissions(AccessControlList dacl);

        event PeekCompletedEventHandler PeekCompleted;
        event ReceiveCompletedEventHandler ReceiveCompleted;
    }
}
#endif
