using org.dotspace.io.tools.exceptions;
using System;
using System.Collections.Generic;

namespace org.dotspace.io.tools
{
    public class ClassRegistry
    {
        private static TwoWayDictionary<Type, String> registry = new TwoWayDictionary<Type, String>();
        private static HashSet<Type> primitives = new HashSet<Type>()
        {
            typeof(Boolean),
            typeof(Byte),
            typeof(SByte),
            typeof(Decimal),
            typeof(Double),
            typeof(Single),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Char),
            typeof(String) 
        };

        public static String Get(Type key)
        {
            if (registry.ContainsKey(key))
            {
                return registry[key];
            }
            else
                throw new ClassDictionaryException("Key is missing. No key found for type '"+ key.ToString() +"'.");
        }
        public static Type Get(String value)
        {
            if (registry.ContainsValue(value))
            {
                return registry[value];
            }
            else
                throw new ClassDictionaryException("Key is missing. No key found for type '" + value.ToString() + "'.");
        }

        public static void Register(Type key, String value) => Add(key, value);
        public static void Add(Type key, String value)
        {
            if (primitives.Contains(key))
                throw new ClassDictionaryException("Cannot add primitive type to the Class Dictionary. '" + key + "' is considered a primitive type.");
            registry.Add(key, value);
        }
        public static void AddAll(ClassEntry[] array)
        {
            foreach (ClassEntry entry in array)
                ClassRegistry.Add(entry.Key, entry.Value);
        }

        public static bool TryGetValue(Type key, out String value) => registry.TryGetValue(key, out value);
        public static bool TryGetValue(String key, out Type value) => registry.TryGetValue(key, out value);
        
        public static void Remove(Type key) => registry.Remove(key);
        public static void Remove(String value) => registry.Remove(value);

        public static bool IsPrimitive(Type type) => primitives.Contains(type) || type.IsEnum;

        public static void Clear() => registry.Clear();
        public static bool ContainsKey(Type key) => registry.ContainsKey(key);
        public static bool ContainsValue(String value) => registry.ContainsValue(value);
        public static ICollection<Type> Keys => registry.Keys;
        public static ICollection<String> Values => registry.Values;
        public static IEnumerator<KeyValuePair<Type, String>> GetEnumerator() => registry.GetEnumerator();
        public static int Count => registry.Keys.Count;
    }
}