using System;
using System.Collections.Generic;

namespace FluentCollections
{
    /// <summary>
    /// Represents a C# queue with possibility of removing by predicate.
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    /// </summary>
    public class RichQueue<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();

        public int Count => _list.Count;

        public T First
        {
            get
            {
                var node = _list.First;
                return node == null ? default : node.Value;
            }
        }

        public void Enqueue(T item)
        {
            _list.AddLast(item);
        }

        public void Enqueue(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public T Dequeue()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var item = _list.First;
            _list.RemoveFirst();

            return item.Value;
        }

        public IEnumerable<T> Remove(Predicate<T> predicate)
        {
            if (_list.Count == 0)
            {
                yield break;
            }

            var current = _list.First;
            while (current != null)
            {
                var next = current.Next;
                var item = current.Value;
                if (predicate(item))
                {
                    _list.Remove(current);
                    yield return item;
                }

                current = next;
            }
        }
    }
}
