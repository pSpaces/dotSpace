using System;
using System.Collections.Generic;

namespace dotSpace.Objects
{
    public class JsonTypeConverter
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private static Dictionary<string, Type> unboxedTypes;
        private static Dictionary<Type, string> boxedTypes;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        static JsonTypeConverter()
        {
            unboxedTypes = new Dictionary<string, Type>();
            boxedTypes = new Dictionary<Type, string>();
            AddType("string", typeof(string));
            AddType("int", typeof(int));
            AddType("float", typeof(float));
            AddType("double", typeof(double));
            AddType("boolean", typeof(bool));
            AddType("decimal", typeof(decimal));
            AddType("long", typeof(long));
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public static object[] Box(object[] values)
        {
            for (int idx = 0; idx < values.Length; idx++)
            {
                if (values[idx] is Type)
                {
                    values[idx] = BoxType((Type)values[idx]);
                }
            }
            return values;
        }

        public static object[] Unbox(object[] values)
        {
            for (int idx = 0; idx < values.Length; idx++)
            {
                if (values[idx] is Binding)
                {
                    values[idx] = UnboxType((Binding)values[idx]);
                }
            }
            return values;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private static void AddType(string unboxedType, Type boxedType)
        {
            unboxedTypes.Add(unboxedType, boxedType);
            boxedTypes.Add(boxedType, unboxedType);
        }

        private static Binding BoxType(Type type)
        {
            if (boxedTypes.ContainsKey(type))
            {
                return new Binding(boxedTypes[type]);
            }
            throw new Exception("Attempting to box unsupported type");
        }

        private static Type UnboxType(Binding type)
        {
            if (unboxedTypes.ContainsKey(type.TypeName))
            {
                return unboxedTypes[type.TypeName];
            }
            throw new Exception("Attempting to unbox unsupported type");
        } 

        #endregion
    }
}
