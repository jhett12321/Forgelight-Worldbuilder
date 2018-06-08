namespace WorldBuilder.Utils.Pools
{
    using System;
    using System.Collections.Generic;
    using Zenject;

    public class ObjectPooler
    {
        [Inject] private AssetManager assetManager;

        private Dictionary<Type, ObjectPool> objectPools = new Dictionary<Type, ObjectPool>();

        public T Create<T>() where T : new() => GetPool<T>().Create();

        public void Dispose<T>(T instance) where T : new()
        {
            IPoolDisposable resetable = instance as IPoolDisposable;
            resetable?.Dispose(assetManager);

            GetPool<T>().ReturnToPool(instance);
        }

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