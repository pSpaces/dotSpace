using dotSpace.Objects.Network.Encoders.Binary.Exceptions;
using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static dotSpace.Objects.Network.Encoders.Binary.BinarySerializer;
using static dotSpace.Objects.Network.Encoders.Binary.Configurations;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class Deserialization
    {
        private static readonly HashSet<byte> binaryprimitives = new HashSet<byte>() // Used for checking if byte is a Primitive TAG.
        {
            INT, DOUBLE, BOOL, CHAR, STRING, ENUM
        };
        private static readonly Dictionary<Type, PrimitiveTypes> primitives = new Dictionary<Type, PrimitiveTypes>()
        {
            { typeof(Boolean), PrimitiveTypes.Boolean },
            { typeof(Byte), PrimitiveTypes.Byte },
            { typeof(SByte), PrimitiveTypes.SByte },
            { typeof(Decimal), PrimitiveTypes.Decimal },
            { typeof(Double), PrimitiveTypes.Double },
            { typeof(Single), PrimitiveTypes.Single },
            { typeof(Int16), PrimitiveTypes.Int16 },
            { typeof(UInt16), PrimitiveTypes.UInt16 },
            { typeof(Int32), PrimitiveTypes.Int32 },
            { typeof(UInt32), PrimitiveTypes.UInt32 },
            { typeof(Int64), PrimitiveTypes.Int64 },
            { typeof(UInt64), PrimitiveTypes.UInt64 },
            { typeof(Char), PrimitiveTypes.Char },
            { typeof(String), PrimitiveTypes.String },
            { typeof(Enum), PrimitiveTypes.Enum }
        };

        internal Object NewObjectDeserialization(Type expectedType, Stream stream, Configurations config)
        {
            int LastRead = stream.ReadByte(); // New TAG.
            if (LastRead.Equals(CLASS))
            {
                expectedType = ReadClass(expectedType, stream, config);
                LastRead = stream.ReadByte(); // New TAG.
                if (LastRead.Equals(CLASS))
                    return expectedType;
            }
            if (binaryprimitives.Contains(Convert.ToByte(LastRead)))
                return ReadPrimitive(expectedType, stream, config);
            else if (LastRead.Equals(ARRAY))
                return ReadArray(expectedType, stream, config);
            else if (LastRead.Equals(COLLECTION))
                return ReadCollection(expectedType, stream, config);
            else if (LastRead.Equals(OBJECT))
                return ReadObject(expectedType, stream, config);
            else
                throw new Exception("Error. Invalid stream.");
        }

        private Type ReadClass(Type expectedType, Stream stream, Configurations config)
        {
            Type result = null;
            String className = TypeConverter.ToString(Read(stream, config), config.CharEncoding);
            if (ClassRegistry.ContainsValue(className))
                result = ClassRegistry.Get(className);
            if (result == null)
                result = Type.GetType(className, false, true);
            if (result == null)
                result = expectedType; // Nothing else works, try and parse data into the expected type given.
            return result;
        }

        private object ReadArray(Type expectedType, Stream stream, Configurations config)
        {
            LinkedList<int> arrayLengthsTemp = new LinkedList<int>();
            while (stream.ReadByte().Equals(ARRAYLENGTH)) // While reading Array TAGs, record the lengths of the array dimension. Array dimensions is ended by a zero.
                arrayLengthsTemp.AddLast(config.LengthConfig.ToLength(stream)); // Array length.
            int[] arrayLengths = new int[arrayLengthsTemp.Count];
            arrayLengthsTemp.CopyTo(arrayLengths, 0);

            Array newArray = Array.CreateInstance(expectedType.GetElementType(), arrayLengths);
            int[] indices = new int[newArray.Rank];
            do
            {
                newArray.SetValue(NewObjectDeserialization(expectedType.GetElementType(), stream, config), indices); // Iterate through array, all dimensions and all lengths.
            } while (NextIndices(ref indices, newArray));
            return newArray;
        }

        private object ReadCollection(Type expectedType, Stream stream, Configurations config)
        {
            // IList
            // IDictionary
            // Else ICollection
            int count = config.LengthConfig.ToLength(stream); // Collection count.
            dynamic collection = Activator.CreateInstance(expectedType);
            for (int i = 0; i < count; i++) // Reconstruct collection through iteration.
            {
                dynamic newObject = NewObjectDeserialization(expectedType.GetGenericArguments()[0], stream, config);
                collection.Add(newObject);
            }
            return collection;
        }

        private object ReadObject(Type expectedType, Stream stream, Configurations config)
        {
            Object obj = Activator.CreateInstance(expectedType);
            int count = config.LengthConfig.ToLength(stream); // Field count.
            for (int i = 0; i < count; i++)
            {
                if (!Convert.ToByte(stream.ReadByte()).Equals(FIELD)) // Field TAG.
                    throw new Exception("Not a field.");
                String fieldName = TypeConverter.ToString(Read(stream, config), config.CharEncoding);
                FieldInfo field = expectedType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                    field.SetValue(obj, NewObjectDeserialization(field.FieldType, stream, config)); // Field data deserializing.
                else
                    SkipObject(stream, config); // No matching field, skip deserialization of this field.
            }
            return obj;
        }

        private Object ReadPrimitive(Type type, Stream stream, Configurations config)
        {
            if (type.IsEnum)
            {
                String str = TypeConverter.ToString(Read(stream, config), config.CharEncoding);
                foreach (Enum e in type.GetEnumValues())
                {
                    if (e.ToString().Equals(str))
                        return e;
                }
                throw new ParseException("No matching enum found for '" + str + "'.");
            }
            else
            {
                switch (primitives[type])
                {
                    case PrimitiveTypes.Boolean:
                        return TypeConverter.ToBoolean(Read(stream, config, true));
                    case PrimitiveTypes.SByte:
                        return Read(stream, config, true)[0];
                    case PrimitiveTypes.Byte:
                        return Read(stream, config, true)[0];
                    case PrimitiveTypes.Int16:
                        return TypeConverter.ToInt16(Read(stream, config, true));
                    case PrimitiveTypes.Int32:
                        return TypeConverter.ToInt32(Read(stream, config, true));
                    case PrimitiveTypes.Int64:
                        return TypeConverter.ToInt64(Read(stream, config, true));
                    case PrimitiveTypes.UInt16:
                        return TypeConverter.ToUInt16(Read(stream, config, true));
                    case PrimitiveTypes.UInt32:
                        return TypeConverter.ToUInt32(Read(stream, config, true));
                    case PrimitiveTypes.UInt64:
                        return TypeConverter.ToUInt64(Read(stream, config, true));
                    case PrimitiveTypes.Single:
                        return TypeConverter.ToSingle(Read(stream, config, true));
                    case PrimitiveTypes.Double:
                        return TypeConverter.ToDouble(Read(stream, config, true));
                    case PrimitiveTypes.Decimal:
                        return TypeConverter.ToDecimal(Read(stream, config, true));
                    case PrimitiveTypes.Char:
                        return TypeConverter.ToChar(Read(stream, config, true), config.CharEncoding);
                    case PrimitiveTypes.String:
                        return TypeConverter.ToString(Read(stream, config), config.CharEncoding);
                    default:
                        throw new ParseException(type.ToString() + " is not a primitive.");
                }
            }
        }

        private byte[] Read(Stream stream, Configurations config, bool singleByteLength = false)
        {
            int length;
            if (singleByteLength)
                length = stream.ReadByte();
            else
            {
                byte[] lengthbytes;
                switch (config.LengthConfig)
                {
                    case LengthBits._8bit:
                        length = stream.ReadByte();
                        break;
                    case LengthBits._16bit:
                        lengthbytes = new byte[2];
                        stream.Read(lengthbytes, 0, lengthbytes.Length);
                        length = TypeConverter.ToInt16(lengthbytes);
                        break;
                    case LengthBits._32bit:
                        lengthbytes = new byte[4];
                        stream.Read(lengthbytes, 0, lengthbytes.Length);
                        length = TypeConverter.ToInt32(lengthbytes);
                        break;
                    default:
                        length = stream.ReadByte();
                        break;
                }
            }
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        private void SkipObject(Stream stream, Configurations config)
        {
            Skip(stream, config); // Start skipping field.
            void Skip(Stream skipstream, Configurations skipconfig)
            {
                int LastRead = stream.ReadByte();
                if (LastRead.Equals(CLASS))
                {
                    SkipClass(skipstream, skipconfig);
                    LastRead = stream.ReadByte();
                    if (LastRead.Equals(CLASS))
                        return;
                }
                if (LastRead.Equals(ARRAY))
                    SkipArray(skipstream, skipconfig);
                else if (LastRead.Equals(COLLECTION))
                    SkipCollection(skipstream, skipconfig);
                else if (LastRead.Equals(OBJECT))
                    SkipObject(skipstream, skipconfig);
                else if (binaryprimitives.Contains(Convert.ToByte(LastRead)))
                    SkipPrimitive(Convert.ToByte(LastRead), skipstream, skipconfig);
            }
            void SkipClass(Stream skipstream, Configurations skipconfig)
            {
                Read(stream, config);
            }
            void SkipArray(Stream skipstream, Configurations skipconfig)
            {
                int length = 0;
                if (stream.ReadByte().Equals(ARRAYLENGTH))
                    length = config.LengthConfig.ToLength(skipstream);
                while (stream.ReadByte().Equals(ARRAYLENGTH)) // While reading Array TAGs, record the lengths of the array dimension.
                    length = length * config.LengthConfig.ToLength(skipstream); // Array length.
                for (int i = 0; i < length; i++)
                    Skip(skipstream, skipconfig);
            }
            void SkipCollection(Stream skipstream, Configurations skipconfig)
            {
                int count = stream.ReadByte(); // Collection count.
                for (int i = 0; i < count; i++)
                    Skip(skipstream, skipconfig);
            }
            void SkipObject(Stream skipstream, Configurations skipconfig)
            {
                int count = stream.ReadByte(); // Field count.
                for (int i = 0; i < count; i++)
                {
                    stream.ReadByte(); // Field TAG.
                    Read(stream, config);
                    Skip(skipstream, skipconfig);
                }
            }
            void SkipPrimitive(byte tag, Stream skipstream, Configurations skipconfig)
            {
                if (tag.Equals(ENUM) || tag.Equals(STRING))
                    Read(stream, config);
                else
                    Read(stream, config, true);
            }
        }

        private bool NextIndices(ref int[] currentIndices, Array array)
        {
            for (int i = currentIndices.Length-1; 0 <= i; i--)
            {
                currentIndices[i]++;
                if (currentIndices[i] < array.GetLength(i))
                    return true;
                else
                    currentIndices[i] = 0;
            }
            return false;
        }
    }
}
