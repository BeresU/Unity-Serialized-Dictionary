using UnityEngine;

namespace SerializedDictionary.Runtime
{
    [System.Serializable]
    internal struct KeyValue<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;

        public TKey Key => key;
        public TValue Value => value;

        public KeyValue(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}