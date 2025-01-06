using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SerializedDictionary.Runtime
{
    [System.Serializable]
    public class SerializedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValue<TKey, TValue>> keyValues;
        
        [Tooltip("Default value when using GetValueOrDefault method")]
        [SerializeField] private TValue defaultValue;

        private Dictionary<TKey, TValue> Dictionary { get; } = new Dictionary<TKey, TValue>();

        public ICollection<TKey> Keys => Dictionary.Keys;
        public ICollection<TValue> Values => Dictionary.Values;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            foreach (var keyValue in keyValues.Where(keyValue => !Dictionary.ContainsKey(keyValue.Key)))
            {
                Dictionary.Add(keyValue.Key, keyValue.Value);
            }
        }

        public TValue GetValueOrDefault(TKey key) => Dictionary.TryGetValue(key, out var value) ? value : defaultValue;

        public void Add(KeyValuePair<TKey, TValue> item)
        {
#if UNITY_EDITOR
            keyValues.Add(new KeyValue<TKey, TValue>(item.Key, item.Value));
#endif
            Dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
# if UNITY_EDITOR
            keyValues.Clear();
#endif
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => Dictionary.ContainsKey(item.Key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var entries = Dictionary.Select(kvp => kvp).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = entries[i];
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
#if UNITY_EDITOR
            var itemToRemove = keyValues.FirstOrDefault(kvp => kvp.Key.Equals(item.Key));

            keyValues.Remove(itemToRemove);
#endif
            return Dictionary.Remove(item.Key);
        }

        public int Count => Dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
#if UNITY_EDITOR
            keyValues.Add(new KeyValue<TKey, TValue>(key, value));
#endif
            Dictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

        public bool Remove(TKey key)
        {
#if UNITY_EDITOR
            var itemToRemove = keyValues.FirstOrDefault(kvp => kvp.Key.Equals(key));

            keyValues.Remove(itemToRemove);
#endif
            return Dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }
    }
}