using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HermEsb.Core.Messages
{
    /// <summary>
    ///     Header for message bus
    /// </summary>
    [Serializable]
    public class MessageHeader : ICloneable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageHeader" /> class.
        /// </summary>
        public MessageHeader()
        {
            IdentificationService = new Identification();
            CreatedAt = DateTime.UtcNow;
            CallContext = new Session();
            CallStack = new Stack<CallerContext>();
            MessageId = Guid.NewGuid();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageHeader" /> class.
        /// </summary>
        /// <param name="identificationService">The identification service.</param>
        /// <param name="callContext">The call context.</param>
        /// <param name="callStack">The call stack.</param>
        /// <param name="createdAt">The created at.</param>
        private MessageHeader(Identification identificationService, Session callContext, Stack<CallerContext> callStack,
                              DateTime createdAt)
        {
            IdentificationService = identificationService;
            CallContext = callContext;
            CallStack = callStack;
            CreatedAt = createdAt;
            MessageId = Guid.NewGuid();
        }

        /// <summary>
        ///     Gets or sets the identification service.
        /// </summary>
        /// <value>The identification service.</value>

        public Identification IdentificationService { get; set; }


        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>
        /// The message id.
        /// </value>

        public Guid MessageId { get; set; }

        /// <summary>
        ///     Gets or sets the type of the body.
        /// </summary>
        /// <value>The type of the body.</value>

        public string BodyType { get; set; }

        /// <summary>
        ///     Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>

        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets the encoding code page.
        /// </summary>
        /// <value>The encoding code page.</value>

        public int EncodingCodePage { get; set; }

        /// <summary>
        ///     Gets or sets the error reinjections.
        /// </summary>
        /// <value>The error reinjections.</value>

        public int ReinjectionNumber { get; set; }

        /// <summary>
        ///     Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>

        public int Priority { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>

        public MessageBusType Type { get; set; }

        /// <summary>
        ///     Gets or sets the call context.
        /// </summary>
        /// <value>The call context.</value>

        public Session CallContext { get; set; }

        /// <summary>
        ///     Gets or sets the call stack.
        /// </summary>
        /// <value>The call stack.</value>

        public Stack<CallerContext> CallStack { get; set; }

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            var messageHeader = new MessageHeader(IdentificationService,
                                                  (Session) CallContext.Clone(),
                                                  new Stack<CallerContext>(CallStack.Reverse()),
                                                  CreatedAt)
                {
                    BodyType = BodyType,
                    EncodingCodePage = EncodingCodePage,
                    Priority = Priority,
                    ReinjectionNumber = ReinjectionNumber,
                    Type = Type,
                };

            return messageHeader;
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(MessageHeader other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.IdentificationService, IdentificationService)
                   && Equals(other.BodyType, BodyType)
                   && other.CreatedAt.Equals(CreatedAt)
                   && other.EncodingCodePage == EncodingCodePage
                   && other.ReinjectionNumber == ReinjectionNumber
                   && other.Priority == Priority
                   && Equals(other.Type, Type);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="System.Object" /> to compare with this instance.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MessageHeader)) return false;
            return Equals((MessageHeader) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (IdentificationService != null ? IdentificationService.GetHashCode() : 0);
                result = (result*397) ^ (BodyType != null ? BodyType.GetHashCode() : 0);
                result = (result*397) ^ CreatedAt.GetHashCode();
                result = (result*397) ^ EncodingCodePage;
                result = (result*397) ^ ReinjectionNumber;
                result = (result*397) ^ Priority;
                result = (result*397) ^ Type.GetHashCode();
                return result;
            }
        }
    }
}