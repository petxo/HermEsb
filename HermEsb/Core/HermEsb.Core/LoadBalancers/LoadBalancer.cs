using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HermEsb.Core.LoadBalancers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class RoundRobinLoadBalancer<TObject> : ILoadBalancer<TObject>
    {
        private readonly List<TObject> _balancingList;
        private SpinLock _spinLock;
        int _currentIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoundRobinLoadBalancer&lt;TObject&gt;"/> class.
        /// </summary>
        public RoundRobinLoadBalancer()
        {
            _balancingList = new List<TObject>();
            _spinLock = new SpinLock();
        }

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public void Add(TObject obj)
        {
            var lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                _balancingList.Add(obj);
                Reset();
                _spinLock.Exit();
            }
        }

        /// <summary>
        /// Removes the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public void Remove(TObject obj)
        {
            var lockTaken = false;
            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                _balancingList.Remove(obj);
                Reset();
                _spinLock.Exit();
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public TObject Next()
        {
            var lockTaken = false;
            var obj = default(TObject);

            _spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                if (_balancingList.Count != 0)
                {
                    _currentIndex = _currentIndex < _balancingList.Count - 1 ? _currentIndex + 1 : 0;

                    obj = _balancingList[_currentIndex];
                }

                _spinLock.Exit();
            }
            return obj;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        public void Reset()
        {
            bool lockTaken = false;
            if (_spinLock.IsHeldByCurrentThread)
            {
                _currentIndex = -1;
            }
            else
            {
                _spinLock.Enter(ref lockTaken);
                if (lockTaken)
                {
                    _currentIndex = -1;
                    _spinLock.Exit();
                }
            }
        }

        /// <summary>
        /// Existses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public bool Exists(TObject obj)
        {
            return _balancingList.Any(o => o.Equals(obj));
        }

        public void Dispose()
        {

        }

    }
}