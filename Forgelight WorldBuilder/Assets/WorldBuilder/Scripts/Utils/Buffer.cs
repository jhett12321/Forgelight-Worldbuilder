namespace WorldBuilder.Utils
{
    using System.Collections;
    using System.Collections.Generic;

    public class Buffer<T> : IEnumerable<T>
    {
        public T[] Data;
        public int Count { get; private set; }

        public T this[int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }

        public Buffer(int initialCapacity = 1)
        {
            Data = new T[initialCapacity];
        }

        public void PrepareBuffer(int count)
        {
            if (count > Data.Length)
            {
                Data = new T[count];
            }

            Count = count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return Data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}