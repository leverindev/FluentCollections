using System.Collections.Generic;

namespace FluentCollections
{
    /// <summary>
    /// Represents a priority queue.
    /// <typeparam name="T">The type of elements in the priority queue.</typeparam>
    /// </summary>
    public class PriorityQueue<T>
    {
        private readonly List<T> _items;
        private readonly IComparer<T> _comparer;

        public PriorityQueue(IComparer<T> comparer = null, int capacity = 0)
        {
            _items = capacity == 0 ? new List<T>() : new List<T>(capacity);
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count => _items.Count;

        public T Dequeue()
        {
            T result = _items[0];
            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            if (_items.Count > 1)
            {
                Heapify(0);
            }

            return result;
        }

        public T Peek()
        {
            return _items[0];
        }

        public void Add(T value)
        {
            _items.Add(value);
            int i = _items.Count - 1;
            int parentIndex = (i - 1) / 2;

            while (i > 0 && _comparer.Compare(_items[parentIndex], _items[i]) > 0)
            {
                Swap(i, parentIndex);
                i = parentIndex;
                parentIndex = (i - 1) / 2;
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        private void Heapify(int index)
        {
            for (; ; )
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int bestChildIndex = index;

                if (leftChildIndex < _items.Count && _comparer.Compare(_items[leftChildIndex], _items[bestChildIndex]) < 0)
                {
                    bestChildIndex = leftChildIndex;
                }

                if (rightChildIndex < _items.Count && _comparer.Compare(_items[rightChildIndex], _items[bestChildIndex]) < 0)
                {
                    bestChildIndex = rightChildIndex;
                }

                if (bestChildIndex == index)
                {
                    break;
                }

                Swap(index, bestChildIndex);
                index = bestChildIndex;
            }
        }

        private void Swap(int i1, int i2)
        {
            T temp = _items[i1];
            _items[i1] = _items[i2];
            _items[i2] = temp;
        }
    }
}
