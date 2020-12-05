using System.Collections;
using System.Collections.Generic;

namespace FluentCollections
{
    /// <summary>
    /// Represents a C# list with possibility of modification it inside foreach loop without "Collection was modified" exception.
    /// But it's needed to invoke Save method.
    /// </summary>
    /// <typeparam name="T">The type of elements in the modifiable list.</typeparam>
    public class ModifiableList<T> : IEnumerable<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly List<T> _inserted = new List<T>();
        private readonly List<T> _deleted = new List<T>();

        public int Count => _list.Count + _inserted.Count - _deleted.Count;

        public void Add(T item)
        {
            if (_deleted.Contains(item))
            {
                _deleted.Remove(item);
            }
            else
            {
                _inserted.Add(item);
            }
        }

        public void Remove(T key)
        {
            if (_inserted.Contains(key))
            {
                _inserted.Remove(key);
            }
            else
            {
                _deleted.Add(key);
            }
        }

        public bool Contains(T key)
        {
            return _list.Contains(key);
        }

        public void Save()
        {
            foreach (var value in _deleted)
            {
                _list.Remove(value);
            }

            foreach (var value in _inserted)
            {
                _list.Add(value);
            }

            _deleted.Clear();
            _inserted.Clear();
        }

        public void Clear()
        {
            _list.Clear();
            _inserted.Clear();
            _deleted.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
