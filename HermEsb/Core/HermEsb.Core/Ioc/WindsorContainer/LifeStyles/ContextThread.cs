using System;
using System.Collections.Generic;

namespace HermEsb.Core.Ioc.WindsorContainer.LifeStyles
{
    /// <summary>
    /// </summary>
    public class ContextThread : IContext
    {
        /// <summary>
        /// </summary>
        private readonly IDictionary<string, object> _components;

        /// <summary>
        ///     Occurs when [ended].
        /// </summary>
        public event EventHandler Ended;

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContextThread" /> class.
        /// </summary>
        public ContextThread()
        {
            _components = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the _components.
        /// </summary>
        /// <value>The _components.</value>
        public IDictionary<string, object> Components
        {
            get { return _components; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Inits this instance.
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            _components.Clear();
        }

        /// <summary>
        ///     Called when [ended].
        /// </summary>
        protected void OnEnded()
        {
            if (Ended != null)
            {
                Ended(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            Ended = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                OnEnded();
            }
        }

        #endregion
    }
}