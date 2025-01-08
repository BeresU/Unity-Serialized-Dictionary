using System;
using System.Linq;
using SerializedDictionary.Runtime;
using UnityEngine;

namespace SerializedDictionary.Tests
{
    public class DictionaryTest : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<string, Person> dictionary;

        [Serializable]
        public class Person
        {
            public string firstName;
            public string lastName;
            public int age;

            public override string ToString()
            {
                return $"{firstName} {lastName}, {age}";
            }
        }

        private void Awake()
        {
            foreach (var dictionaryKey in dictionary.Keys)
            {
                Debug.Log($"Dictionary Key: {dictionaryKey}, value: {dictionary[dictionaryKey]}");
            }

            RemoveKeyTest();
        }

        private void RemoveKeyTest()
        {
            var firstKey = dictionary.Keys.FirstOrDefault();

            if (firstKey == null)
            {
                Debug.Log("No key to remove");
                return;
            }

            Debug.Log($"Removing key {firstKey}");

            dictionary.Remove(firstKey);
        }
    }
}