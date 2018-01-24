using dotSpace.Objects.Network.Encoders.Binary.Exceptions;
using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static dotSpace.Objects.Network.Encoders.Binary.BinarySerializer;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    internal sealed class Serialization
    {
        private static readonly HashSet<Type> primitives = new HashSet<Type>() // Can be replaced by isPrimitive() or isValueType()?
        {
            { typeof(Boolean) },
            { typeof(Byte) },
            { typeof(SByte) },
            { typeof(Decimal) },
            { typeof(Double) },
            { typeof(Single) },
            { typeof(Int16) },
            { typeof(UInt16) },
            { typeof(Int32) },
            { typeof(UInt32) },
            { typeof(Int64) },
            { typeof(UInt64) },
            { typeof(Char) },
            { typeof(String) },
            { typeof(Enum) }
        };

        internal void NewObjectSerialization(Object obj, Stream stream, Configurations config)
        {
            if (WriteClass(obj, stream, config))
                stream.WriteByte(CLASS);
            else if (primitives.Contains(obj.GetType()) || obj.GetType().IsEnum)
                WritePrimitive(obj, stream, config);
            else if (obj.GetType().IsArray)
                WriteArray(obj, stream, config);
            else if (typeof(ICollection).IsInstanceOfType(obj))
                WriteCollection(obj, stream, config);
            else
                WriteObject(obj, stream, config);
        }

        private bool WriteClass(Object obj, Stream stream, Configurations config)
        {
            bool isAType = typeof(Type).IsInstanceOfType(obj);
            if (isAType)
                Write(CLASS, TypeConverter.GetBytes(((Type)obj).AssemblyQualifiedName, config.CharEncoding), stream, config);
            else
                Write(CLASS, TypeConverter.GetBytes(obj.GetType().AssemblyQualifiedName, config.CharEncoding), stream, config);
            return isAType;
        }

        private void WriteArray(Object obj, Stream stream, Configurations config)
        {
            stream.WriteByte(ARRAY); // Array TAG.
            Array array = (Array)obj;
            for (int i = 0; i < obj.GetType().GetArrayRank(); i++)
                WriteCount(ARRAYLENGTH, array.GetLength(i), stream, config);
            stream.WriteByte(0);
            foreach (object ele in array)
                NewObjectSerialization(ele, stream, config); // Iterate through array, all dimensions and all lengths.
        }

        private void WriteCollection(Object obj, Stream stream, Configurations config)
        {
            // IList
            // IDictionary
            // Else ICollection
            ICollection array = (ICollection)obj;
            WriteCount(COLLECTION, array.Count, stream, config);
            foreach (object element in array)
                NewObjectSerialization(element, stream, config); // Iterate through collection.
        }

        private void WriteObject(Object obj, Stream stream, Configurations config)
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            LinkedList<FieldInfo> fieldslist = new LinkedList<FieldInfo>();
            foreach (FieldInfo field in fields) // Sort out fields with the NotSerializable modifier and fields with null values.
                if (!field.IsNotSerialized && field.GetValue(obj) != null)
                    fieldslist.AddLast(field);
            foreach (FieldInfo f in GetAllHiddenFields(obj, obj.GetType()))
                fieldslist.AddLast(f);
            WriteCount(OBJECT, fieldslist.Count, stream, config);
            foreach (FieldInfo field in fieldslist)
            {
                Write(FIELD, TypeConverter.GetBytes(field.Name, config.CharEncoding), stream, config);
                NewObjectSerialization(field.GetValue(obj), stream, config); // Continue by serializing fields.
            }
        }

        private void WritePrimitive(Object obj, Stream stream, Configurations config)
        {
            if (obj.GetType().IsEnum)
                Write(ENUM, TypeConverter.GetBytes(obj.ToString(), config.CharEncoding), stream, config);
            else
            {
                switch (obj)
                {
                    case Boolean b:
                        Write(BOOL, TypeConverter.GetBytes(b), stream, config, true);
                        break;
                    case SByte sb:
                        Write(INT, TypeConverter.GetBytes(sb), stream, config, true);
                        break;
                    case Byte b:
                        Write(INT, TypeConverter.GetBytes(b), stream, config, true);
                        break;
                    case Int16 i:
                        Write(INT, TypeConverter.GetBytes(i), stream, config, true);
                        break;
                    case Int32 i:
                        Write(INT, TypeConverter.GetBytes(i), stream, config, true);
                        break;
                    case Int64 i:
                        Write(INT, TypeConverter.GetBytes(i), stream, config, true);
                        break;
                    case UInt16 ui:
                        Write(INT, TypeConverter.GetBytes(ui), stream, config, true);
                        break;
                    case UInt32 ui:
                        Write(INT, TypeConverter.GetBytes(ui), stream, config, true);
                        break;
                    case UInt64 ui:
                        Write(INT, TypeConverter.GetBytes(ui), stream, config, true);
                        break;
                    case Single s:
                        Write(DOUBLE, TypeConverter.GetBytes(s), stream, config, true);
                        break;
                    case Double d:
                        Write(DOUBLE, TypeConverter.GetBytes(d), stream, config, true);
                        break;
                    case Decimal d:
                        Write(DOUBLE, TypeConverter.GetBytes(d), stream, config, true);
                        break;
                    case Char c:
                        Write(CHAR, TypeConverter.GetBytes(c, config.CharEncoding), stream, config, true);
                        break;
                    case String s:
                        Write(STRING, TypeConverter.GetBytes(s, config.CharEncoding), stream, config);
                        break;
                    default:
                        throw new ParseException(obj.GetType().ToString() + " is not a primitive.");
                }
            }
        }

        private void WriteCount(byte tag, int count, Stream stream, Configurations config)
        {
            stream.WriteByte(tag);
            byte[] bytes = config.LengthConfig.ToBytes(count);
            stream.Write(bytes, 0, bytes.Length);
        }

        private void Write(byte tag, byte[] bytes, Stream stream, Configurations config, bool singleByteLength = false)
        {
            byte[] length = singleByteLength ? new byte[] { Convert.ToByte(bytes.Length) } : config.LengthConfig.ToBytes(bytes.Length);
            stream.WriteByte(tag);
            stream.Write(length, 0, length.Length);
            stream.Write(bytes, 0, bytes.Length);
        }

        private FieldInfo[] GetAllHiddenFields(Object obj, Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<FieldInfo> fieldslist = new List<FieldInfo>();
            foreach (FieldInfo field in fields) // Sort out fields with the NotSerializable modifier and fields with null values.
                if (!field.IsNotSerialized && field.GetValue(obj) != null)
                    fieldslist.Add(field);
            return type.BaseType.Equals(typeof(Object)) ? fieldslist.ToArray() : fieldslist.ToArray().Concat(GetAllHiddenFields(obj, type.BaseType)).ToArray();
        }
    }
}
