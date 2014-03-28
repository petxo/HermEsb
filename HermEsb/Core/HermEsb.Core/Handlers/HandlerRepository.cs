using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HermEsb.Core.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class HandlerRepository : IHandlerRepository
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<Type>> _handlerList;

        private readonly ConcurrentDictionary<Type, ConcurrentBag<Type>> _workList;

        private AutoResetEvent _monitor = new AutoResetEvent(true);


        /// <summary>
        /// Initializes a new instance of the <see cref="HandlerRepository"/> class.
        /// </summary>
        internal HandlerRepository()
        {
            _handlerList = new ConcurrentDictionary<Type, ConcurrentBag<Type>>();
            _workList = new ConcurrentDictionary<Type, ConcurrentBag<Type>>();
        }

        /// <summary>
        /// Adds the handler.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="handlerType">Type of the handler.</param>
        public void AddHandler(Type messageType, Type handlerType)
        {
            ConcurrentBag<Type> list;
            if (_handlerList.TryGetValue(messageType, out list))
            {
                list.Add(handlerType);
            }
            else
            {
                list = new ConcurrentBag<Type> { handlerType };
                _handlerList.GetOrAdd(messageType, list);
            }
        }

        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        public IEnumerable<Type> GetHandlersByMessage(Type messageType)
        {
            ConcurrentBag<Type> list;
            if (!_workList.TryGetValue(messageType, out list))
            {
                AddTypeToWorkList(messageType);

                _workList.TryGetValue(messageType, out list);
            }
            return list?? new ConcurrentBag<Type>();
        }

        /// <summary>
        /// Gets the type of the messages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetMessageTypes()
        {
            return _handlerList.Keys;
        }

        /// <summary>
        /// Adds the type to work list.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        private void AddTypeToWorkList(Type messageType)
        {
            ConcurrentBag<Type> listout;
            if (!_workList.TryGetValue(messageType, out listout))
            {
                foreach (var pair in _handlerList)
                {
                    if (pair.Key.IsAssignableFrom(messageType))
                    {
                        var list = new ConcurrentBag<Type>();
                        list = _workList.GetOrAdd(messageType, list);
                        Parallel.ForEach(pair.Value, t =>
                                                         {
                                                             _monitor.WaitOne();
                                                             if (!list.Contains(t))
                                                             {
                                                                 list.Add(t);
                                                             }
                                                             _monitor.Set();
                                                         });
                    }
                }
            }
        }
    }
}