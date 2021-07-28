using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentCollections
{
    /// <summary>
    /// Represents a C# dictionary with possibility of modification it inside foreach loop without "Collection was modified" exception.
    /// But it's needed to invoke Save method.
    /// </summary>
    public class ModifiableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private class ValueHolder
        {
            public TValue Value;
            public bool IsRemoved;
        }

        private readonly Dictionary<TKey, ValueHolder> _dictionary;

        public ModifiableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, ValueHolder>(capacity);
        }

        public ModifiableDictionary()
        {
            _dictionary = new Dictionary<TKey, ValueHolder>();
        }

        public bool IsReadOnly { get; } = false;

        public int Count => _dictionary.Count(x => !x.Value.IsRemoved);

        public ICollection<TKey> Keys =>
            (ICollection<TKey>)_dictionary
                .Where(x => !x.Value.IsRemoved)
                .Select(x => x.Key);

        public ICollection<TValue> Values =>
            (ICollection<TValue>)_dictionary
                .Where(x => !x.Value.IsRemoved)
                .Select(x => x.Value.Value);

        public TValue this[TKey key]
        {
            get => TryGetValue(key, out var value) ? value : default;
            set => Add(key, value, false);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            Add(key, value, true);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(TKey key)
        {
            if (_dictionary.TryGetValue(key, out var value))
            {
                value.IsRemoved = true;
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dictionary.TryGetValue(key, out var item) && !item.IsRemoved)
            {
                value = item.Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.TryGetValue(item.Key, out var value) && !value.IsRemoved && value.Value.Equals(item.Value);
        }

        public bool ContainsKey(TKey key) => _dictionary.TryGetValue(key, out var value) && !value.IsRemoved;

        public void Clear() => _dictionary.Clear();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var pair in _dictionary)
            {
                if (pair.Value.IsRemoved)
                {
                    continue;
                }

                yield return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int index = 0;
            foreach (var pair in _dictionary)
            {
                if (pair.Value.IsRemoved)
                {
                    continue;
                }

                array[arrayIndex + index] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
                index++;
            }
        }

        public void Save()
        {
            // todo
        }

        private void Add(TKey key, TValue value, bool throwExceptionIfDuplicated)
        {
            if (_dictionary.TryGetValue(key, out var item))
            {
                if (!item.IsRemoved && throwExceptionIfDuplicated)
                {
                    throw new InvalidOperationException($"Item of key {key} already exists");
                }

                item.Value = value;
                item.IsRemoved = false;
            }
            else
            {
                _dictionary[key] = new ValueHolder { Value = value };
            }
        }
    }
}
