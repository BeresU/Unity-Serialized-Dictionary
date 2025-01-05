using System;
using SerializedFieldDictionary.Runtime;
using UnityEngine;

namespace SerializedFieldDictionary.Tests
{
    public class DictionaryTest : MonoBehaviour
    {
        [SerializeField] private SerializedFieldDictionary<string, Person> _dictionary;
        [Serializable]
        public class Person
        {
            public string FirstName;
            public string LastName;
            public int Age;

            public override string ToString()
            {
                return $"{FirstName} {LastName}, {Age}";
            }
        }

        private void Awake()
        {
            foreach (var dictionaryKey in _dictionary.Keys)
            {
                Debug.Log($"Dictionary Key: {dictionaryKey}, value: {_dictionary[dictionaryKey]}");
            }
        }
    }
}
