using System.Collections;
using System.Collections.Generic;

namespace dotSpace.Objects.Network.Binary
{
    public class TwoWayDictionary<T1, T2> : IDictionary<T1, T2>
    {
        private Dictionary<T1, T2> dictionary = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> rDictionary = new Dictionary<T2, T1>();
        private bool isReadOnly = false;

        public TwoWayDictionary(){}

        public T2 this[T1 key] { get => dictionary[key]; set { dictionary[key] = value; rDictionary[value] = key; } }
        public T1 this[T2 key] { get => rDictionary[key]; set { rDictionary[key] = value; dictionary[value] = key; } }

        public ICollection<T1> Keys => dictionary.Keys;

        public ICollection<T2> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly => isReadOnly;

        public void Add(T1 key, T2 value)
        {
            dictionary.Add(key, value);
            rDictionary.Add(value, key);
        }

        public void Add(KeyValuePair<T1, T2> item)
        {
            dictionary.Add(item.Key, item.Value);
            rDictionary.Add(item.Value, item.Key);
        }

        public void Clear()
        {
            dictionary.Clear();
            rDictionary.Clear();
        }

        public bool Contains(KeyValuePair<T1, T2> item)
        {
            return dictionary.ContainsKey(item.Key) && dictionary[item.Key].Equals(item.Value);
        }

        public bool Contains(KeyValuePair<T2, T1> item)
        {
            return rDictionary.ContainsKey(item.Key) && rDictionary[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(T1 key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool ContainsValue(T2 value)
        {
            return rDictionary.ContainsKey(value);
        }

        public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        {
            Dictionary<T1,T2>.Enumerator entries = dictionary.GetEnumerator();
            while (array.Length > arrayIndex && entries.MoveNext())
            {
                array[arrayIndex] = entries.Current;
                arrayIndex++;
            }
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public bool Remove(T1 key)
        {
            return dictionary.ContainsKey(key) && rDictionary.Remove(dictionary[key]) && dictionary.Remove(key);
        }
        public bool Remove(T2 value)
        {
            return rDictionary.ContainsKey(value) && dictionary.Remove(rDictionary[value]) && rDictionary.Remove(value);
        }

        public bool Remove(KeyValuePair<T1, T2> item)
        {
            return dictionary.ContainsKey(item.Key) && dictionary[item.Key].Equals(item.Value) && rDictionary.Remove(dictionary[item.Key]) && dictionary.Remove(item.Key);
        }

        public bool TryGetValue(T1 key, out T2 value)
        {
            return dictionary.TryGetValue(key, out value);
        }
        public bool TryGetValue(T2 key, out T1 value)
        {
            return rDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
