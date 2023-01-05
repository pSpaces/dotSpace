using System;
using System.Collections.Generic;
using System.Text.Json;

namespace dotSpace.Objects.Json
{
    /// <summary>
    /// Provdes functionaly to convert .NET objects into json strings and back. 
    /// Furthermore, it supports language independent typeconversion by boxing and unboxing of values through the use of a
    /// type map.
    /// </summary>
    internal class TypeConverter
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private static Dictionary<string, Type> unboxedTypes;
        private static Dictionary<Type, string> boxedTypes;

        private static Dictionary<string,string> typeToString;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the PatternValue class.
        /// </summary>
        static TypeConverter()
        {
            unboxedTypes = new Dictionary<string, Type>();
            boxedTypes = new Dictionary<Type, string>();
            typeToString = new Dictionary<string, string>();
            typeToString.Add("int32", "int");
            typeToString.Add("int64", "long");

            AddType("string", typeof(string));
            AddType("int", typeof(int));
            AddType("float", typeof(float));
            AddType("double", typeof(double));
            AddType("boolean", typeof(bool));
            AddType("decimal", typeof(decimal));
            AddType("long", typeof(long));
            AddType("enum", typeof(Enum));
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public static object[] Box(object[] values)
        {
            if (values == null)
                return null;
            object[] newValues = new object[values.Length];
            for (int idx = 0; idx < values.Length; idx++)
            {
                if (values[idx] is Type)
                {
                    newValues[idx] = BoxType((Type)values[idx]);
                }
                else
                {
                    newValues[idx] = BoxType(values[idx].GetType(), values[idx]);
                }
            }
            return newValues;
        }

        public static object[] Unbox(object[] values)
        {
            if (values == null)
                return null;

            List<object> unboxedValues = new List<object>();

            foreach (object val in values)
            {
                // This is only used if the JSON Serializer fails to correctly deserialize.
                if (val is JsonElement)
                {
                    var value = (JsonElement)val;
                    
                    if (value.TryGetProperty("Value", out JsonElement _))
                    {
                        unboxedValues.Add(UnboxType(value.GetProperty("TypeName").GetString(), value.GetProperty("Value")));
                    }
                    else
                    {
                        unboxedValues.Add(UnboxType(value.GetProperty("TypeName").GetString()));
                    }
                }
                // As it sometimes manages to do it correctly.
                else
                {
                    if(val is Type)
                    {
                        unboxedValues.Add(UnboxType(((Type)val).Name.ToLower()));
                    }
                    else
                    {
                        unboxedValues.Add(UnboxType(val.GetType().Name.ToLower(), val));
                    }
                }
            }
            return unboxedValues.ToArray();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods


        private static void AddType(string unboxedType, Type boxedType)
        {
            unboxedTypes.Add(unboxedType, boxedType);
            boxedTypes.Add(boxedType, unboxedType);
        }

        private static PatternBinding BoxType(Type type)
        {
            if (boxedTypes.ContainsKey(type))
            {
                return new PatternBinding(boxedTypes[type]);
            }
            throw new Exception("Attempting to box unsupported type");
        }

        private static Type UnboxType(string typename)
        {
            if (typeToString.ContainsKey(typename)) typename = typeToString[typename];

            if (unboxedTypes.ContainsKey(typename))
            {
                return unboxedTypes[typename];
            }
            throw new Exception("Attempting to unbox unsupported type");
        }

        private static PatternValue BoxType(Type type, object value)
        {
            if (boxedTypes.ContainsKey(type))
            {
                return new PatternValue(boxedTypes[type], value);
            }
            throw new Exception("Attempting to box unsupported type");
        }

        private static object UnboxType(string typename, JsonElement value)
        {
            if (typeToString.ContainsKey(typename)) typename = typeToString[typename];

            if (unboxedTypes.ContainsKey(typename))
            {
                return JsonSerializer.Deserialize(value, unboxedTypes[typename]);
            }
            throw new Exception("Attempting to unbox unsupported type");
        }

        private static object UnboxType(string typename, object value)
        {
            if(typeToString.ContainsKey(typename)) typename = typeToString[typename];

            if (unboxedTypes.ContainsKey(typename))
            {
                return Convert.ChangeType(value, unboxedTypes[typename]);
            }
            throw new Exception("Attempting to unbox unsupported type");
        }


        #endregion
    }
}
