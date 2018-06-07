namespace WorldBuilder.Utils.Pools
{
    using System.Collections.Concurrent;
    using System.Threading;

    public interface IPoolResetable
    {
        void Reset();
    }

    public abstract class ObjectPool
    {
        public int ActiveInstances;
        public abstract int TotalObjects { get; }
        public abstract int Capacity { get; set; }
    }

    /// <summary>
    /// A simple generic object pool. Thread Safe.
    /// </summary>
    /// <typeparam name="T">The type of object pool.</typeparam>
    public class ObjectPool<T> : ObjectPool where T : new()
    {
        private int capacity;
        private bool capacityDefined;

        public ConcurrentStack<T> PooledItems = new ConcurrentStack<T>();

        public override int TotalObjects => ActiveInstances + PooledItems.Count;
        public override int Capacity
        {
            get { return capacity; }
            set
            {
                capacityDefined = true;
                capacity        = value;
            }
        }

        /// <param name="startAmount">The number of instances to prefill the pool.</param>
        public ObjectPool(int startAmount)
        {
            InitializePool(startAmount);
        }

        /// <param name="startAmount">The number of instances to prefill the pool.</param>
        /// <param name="capacity">The capacity of the pool.</param>
        public ObjectPool(int startAmount, int capacity)
        {
            Capacity = capacity;
            InitializePool(startAmount);
        }

        public ObjectPool() {}

        private void InitializePool(int startAmount)
        {
            for (int i = 0; i < startAmount; i++)
            {
                PooledItems.Push(new T());
            }
        }

        /// <summary>
        /// Finds an existing pooled object, or creates a new one if none are available
        /// </summary>
        /// <returns>A pooled object, or the default value if no pooled objects are available, and the pool has reached capacity.</returns>
        public T Create()
        {
            T pooledItem;
            PooledItems.TryPop(out pooledItem);

            if (pooledItem == null)
            {
                if (capacityDefined && capacity <= TotalObjects)
                {
                    return default(T);
                }

                pooledItem = new T();
            }

            Interlocked.Increment(ref ActiveInstances);
            return pooledItem;
        }

        /// <summary>
        /// Returns an existing active instance back to the pool.
        /// </summary>
        /// <param name="instance"></param>
        public void Dispose(T instance)
        {
            IPoolResetable resetable = instance as IPoolResetable;
            resetable?.Reset();

            PooledItems.Push(instance);
            Interlocked.Decrement(ref ActiveInstances);
        }
    }
}