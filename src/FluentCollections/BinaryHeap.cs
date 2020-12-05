using System.Collections.Generic;

namespace FluentCollections
{
    /// <summary>
    /// Represents a binary heap. It's a common way of implementing priority queue.
    /// For more information follow the link: https://en.wikipedia.org/wiki/Binary_heap
    /// <typeparam name="T">The type of elements in the binary heap.</typeparam>
    /// </summary>
    public class BinaryHeap<T>
    {
        private readonly List<T> _heap;
        private readonly IComparer<T> _comparer;

        public BinaryHeap(IComparer<T> comparer = null, int capacity = 0)
        {
            _heap = capacity == 0 ? new List<T>() : new List<T>(capacity);
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public int Count => _heap.Count;

        public T Dequeue()
        {
            T result = _heap[0];
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            if (_heap.Count > 1)
            {
                Heapify(0);
            }

            return result;
        }

        public T Peek()
        {
            return _heap[0];
        }

        public void Add(T value)
        {
            _heap.Add(value);
            int i = _heap.Count - 1;
            int parentIndex = (i - 1) / 2;

            while (i > 0 && _comparer.Compare(_heap[parentIndex], _heap[i]) > 0)
            {
                Swap(i, parentIndex);
                i = parentIndex;
                parentIndex = (i - 1) / 2;
            }
        }

        public void Clear()
        {
            _heap.Clear();
        }

        private void Heapify(int index)
        {
            for (; ; )
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int bestChildIndex = index;

                if (leftChildIndex < _heap.Count && _comparer.Compare(_heap[leftChildIndex], _heap[bestChildIndex]) < 0)
                {
                    bestChildIndex = leftChildIndex;
                }

                if (rightChildIndex < _heap.Count && _comparer.Compare(_heap[rightChildIndex], _heap[bestChildIndex]) < 0)
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
            T temp = _heap[i1];
            _heap[i1] = _heap[i2];
            _heap[i2] = temp;
        }
    }
}
