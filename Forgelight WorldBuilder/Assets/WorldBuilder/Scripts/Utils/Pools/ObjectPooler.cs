namespace WorldBuilder.Utils.Pools
{
    using System;
    using System.Collections.Generic;

    public class ObjectPooler
    {
        private Dictionary<Type, ObjectPool> objectPools = new Dictionary<Type, ObjectPool>();

        public T Create<T>() where T : new() => GetPool<T>().Create();
        public void Dispose<T>(T instance) where T : new() => GetPool<T>().Dispose(instance);

        private ObjectPool<T> GetPool<T>() where T : new()
        {
            ObjectPool pool;
            Type type = typeof(T);

            if (!objectPools.TryGetValue(type, out pool))
            {
                pool = new ObjectPool<T>();
                objectPools[type] = pool;
            }

            return (ObjectPool<T>)pool;
        }
    }
}