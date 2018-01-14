using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using dotSpace.Objects.Network.Encoders.Binary.Exceptions;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    public class BinarySerializer
    {
        #region Fields
        private const Byte // Primitive TAGs
            BOOL = 0b01100010, // UTF8 for 'b'
            INT = 0b01101001, // UTF8 for 'i'
            DOUBLE = 0b01100100, // UTF8 for 'd'
            CHAR = 0b01100011, // UTF8 for 'c'
            STRING = 0b01110011, // UTF8 for 's'
            ENUM = 0b01100101; // UTF8 for 'e'
        private const Byte // TAGs
            CLASS = 0b01010100, // UTF8 for 'T'
            OBJECT = 0b01001111, // UTF8 for 'O'
            CLASSPARAMETER = 0b01010000, // UTF8 for 'P'
            FIELD = 0b01000110, // UTF8 for 'F'
            ARRAY = 0b01000001, // UTF8 for 'A'
            ARRAYLENGTH = 0b01001100, // UTF8 for 'L'
            COLLECTION = 0b01000011; // UTF8 for 'C'

        public enum Lengths
        {
            _8bit, // Max length 127, 8bit (signed)
            _16bit, // Max length 32767, 16bit (signed)
            _32bit // Max length 2147483647, 32bit (signed)
        }
        public Lengths Length { get; set; } = Lengths._16bit;

        public enum CharEncoding
        {
            UTF8 = 0, UTF16 = 1, Unicode = 1, UTF32 = 2
        }
        public CharEncoding CharEncoder { get; set; } = CharEncoding.Unicode;

        private enum PrimitiveTypes
        {
            Boolean, Byte, SByte, Decimal, Double, Single, Int16, UInt16, Int32, UInt32, Int64, UInt64, Char, String, Enum
        }
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
        private static readonly Dictionary<byte, Type> binaryprimitives = new Dictionary<byte, Type>() // Used for checking if byte is a Primitive TAG.
        {
            { INT, typeof(Int32) },
            { DOUBLE, typeof(Double) },
            { BOOL, typeof(Boolean) },
            { CHAR, typeof(Char) },
            { STRING, typeof(String) },
            { ENUM, typeof(Enum) }
        };
        
        public Stream stream;
        public int LastRead;
        #endregion

        public byte[] Serialize(Object obj)
        {
            using (MemoryStream output = new MemoryStream())
            {
                Serialize(obj, output);

                output.Flush();
                output.Position = 0;
                byte[] bytes = new byte[output.Length];
                output.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }
        public void Serialize(Object obj, Stream output)
        {
            stream = output;
            int settings = 0b00000000;
            switch (CharEncoder)
            {
                case CharEncoding.UTF8:
                    settings = settings | 0b00000001;
                    break;
                case CharEncoding.Unicode:
                    settings = settings | 0b00000010;
                    break;
                case CharEncoding.UTF32:
                    settings = settings | 0b00000011;
                    break;
            }
            switch (Length)
            {
                case Lengths._8bit:
                    settings = settings | 0b00000100;
                    break;
                case Lengths._16bit:
                    settings = settings | 0b00001000;
                    break;
                case Lengths._32bit:
                    settings = settings | 0b00001100;
                    break;
            }
            stream.WriteByte(Convert.ToByte(settings));
            NewObjectSerialization(obj);
        }

        public Object Deserialize(Type type, byte[] bytes)
        {
            MemoryStream input = new MemoryStream(bytes);
            return Deserialize(type, input);
        }
        public Object Deserialize(Type type, Stream input)
        {
            stream = input;
            CharEncoding tempEncoder = CharEncoder;
            Lengths tempLength = Length;
            int settings = stream.ReadByte();
            if ((settings & 0b00000011) != 0)
            {
                if ((settings & 0b00000001) == 0b00000001)
                    CharEncoder = CharEncoding.UTF8;
                else if ((settings & 0b00000010) == 0b00000010)
                    CharEncoder = CharEncoding.UTF16;
                else
                    CharEncoder = CharEncoding.UTF32;
            }
            if ((settings & 0b00001100) != 0)
            {
                if ((settings & 0b00001100) == 0b00000100)
                    Length = Lengths._8bit;
                else if ((settings & 0b00001100) == 0b00001000)
                    Length = Lengths._16bit;
                else
                    Length = Lengths._32bit;
            }
            Object obj = NewObjectDeserialization(type);
            CharEncoder = tempEncoder;
            Length = tempLength;
            return obj;
        }

        public void ClassSerialization(Type type)
        {
            if (primitives.ContainsKey(type))
            {
                stream.WriteByte(PrimitiveToTAG(primitives[type])); // Primitive TAG.
            }
            else if (type.IsArray)
            {
                stream.WriteByte(ARRAY); // Array TAG.
                stream.WriteByte(Convert.ToByte(type.GetArrayRank())); // Array dimensions.
                ClassSerialization(type.GetElementType());
            }
            else
            {
                stream.WriteByte(CLASS); // Class TAG.
                byte[] bytes = TypeConverter.GetBytes(type.AssemblyQualifiedName, CharEncoder);
                byte[] byteslength = LengthToBytes(bytes.Length);
                stream.Write(byteslength, 0, byteslength.Length); // Class full name data length.
                stream.Write(bytes, 0, bytes.Length); // Class full name.

                Type[] parametertypes = type.GetGenericArguments();
                if (type.ContainsGenericParameters)
                {
                    stream.WriteByte(CLASSPARAMETER); // Class parameter TAG.
                    stream.WriteByte(Convert.ToByte(parametertypes.Length)); // Number of class parameters
                    foreach (Type t in parametertypes)
                        ClassSerialization(t);
                }
            }
        }
        public Type ClassDeserialization(Type expectedType)
        {
            Type result = null;
            if (binaryprimitives.ContainsKey(Convert.ToByte(LastRead)))
            {
                if (primitives.ContainsKey(expectedType) && LastRead.Equals(PrimitiveToTAG(primitives[expectedType])))
                    result = expectedType;
                else
                    result = binaryprimitives[Convert.ToByte(LastRead)];
                LastRead = stream.ReadByte();
                return result;
            }
            else if (LastRead.Equals(ARRAY)) // Array TAG.
            {
                int arrayRank = stream.ReadByte(); // Array dimensions.
                LastRead = stream.ReadByte();
                result = ClassDeserialization(expectedType.GetElementType()).MakeArrayType(arrayRank);
                return result;
            }
            else if (LastRead.Equals(CLASS)) // Class TAG.
            {
                int fullLength = ReadLength(); // Class full name data length.
                byte[] fullnameBytes = new byte[fullLength];
                stream.Read(fullnameBytes, 0, fullnameBytes.Length); // Class full name.
                String className = TypeConverter.ToString(fullnameBytes, CharEncoder);
                if (ClassRegistry.ContainsValue(className))
                    result = ClassRegistry.Get(className);
                if (result == null)
                    result = Type.GetType(className, false, true);
                if (result == null)
                    result = expectedType; // Nothing else works, try and parse data into the expected type given.
                LastRead = stream.ReadByte();
                if (LastRead.Equals(CLASSPARAMETER)) // Class parameter TAG.
                {
                    int count = stream.ReadByte(); // Number of class parameters
                    LastRead = stream.ReadByte();
                    Type[] parametertypes = new Type[count];
                    for (int i = 0; i < count; i++)
                    {
                        parametertypes[i] = ClassDeserialization(null);
                    }
                    result = result.MakeGenericType(parametertypes);
                }
                return result;
            }
            else throw new Exception();
        }

        private void NewObjectSerialization(Object obj)
        {
            if (typeof(Type).IsInstanceOfType(obj))
            {
                ClassSerialization((Type)obj);
                stream.WriteByte(CLASS);
                return;
            }
            else
                ClassSerialization(obj.GetType());
            if (primitives.ContainsKey(obj.GetType()) || obj.GetType().IsEnum)
            {
                byte[] bytePrimitive = PrimitiveToBytes(obj); // Primitive TAG, Data length, Primitive data.
                stream.Write(bytePrimitive, 0, bytePrimitive.Length); // Primitive data. int, bool, string etc.
            }
            else if (obj.GetType().IsArray)
            {
                stream.WriteByte(ARRAY); // Array TAG.
                Array array = (Array)obj;
                for (int i = 0; i < obj.GetType().GetArrayRank(); i++)
                {
                    stream.WriteByte(ARRAYLENGTH); // Array length TAG.
                    byte[] bytes = LengthToBytes(array.GetLength(i));
                    stream.Write(bytes, 0, bytes.Length); // Array length data.
                }
                stream.WriteByte(0);

                int[] indices = new int[array.Rank];
                bool c = true;
                while (c)
                {
                    NewObjectSerialization(array.GetValue(indices)); // Iterate through array, all dimensions and all lengths.
                    c = false;
                    for (int i = 0; i < indices.Length; i++)
                    {
                        indices[i]++;
                        if (indices[i] < array.GetLength(i))
                        {
                            c = true;
                            break;
                        }
                        else
                            indices[i] = 0;
                    }
                }
            }
            else if (typeof(ICollection).IsInstanceOfType(obj))
            {
                // IList
                // IDictionary
                // Else ICollection
                ICollection array = (ICollection)obj;
                stream.WriteByte(COLLECTION); // Collection TAG.
                byte[] bytes = LengthToBytes(array.Count);
                stream.Write(bytes, 0, bytes.Length); // Collection count.
                foreach (object element in array)
                {
                    NewObjectSerialization(element); // Iterate through collection.
                }
            }
            else // REMEMBER POSSIBLE T CLASS ARGUMENT! Examine and Instantiate Generic Types with Reflection.
            {
                FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                LinkedList<FieldInfo> fieldslist = new LinkedList<FieldInfo>();
                foreach (FieldInfo field in fields) // Sort out fields with the NotSerializable modifier and fields with null values.
                    if (!field.IsNotSerialized && field.GetValue(obj) != null)
                        fieldslist.AddLast(field);
                var hiddenfields = GetAllHiddenFields(obj, obj.GetType());
                foreach (FieldInfo f in hiddenfields)
                    fieldslist.AddLast(f);
                stream.WriteByte(OBJECT); // Object TAG.
                byte[] bytes = LengthToBytes(fieldslist.Count);
                stream.Write(bytes, 0, bytes.Length); // Field count.
                foreach (FieldInfo field in fieldslist)
                {
                    stream.WriteByte(FIELD); // Field TAG.
                    byte[] fieldname = TypeConverter.GetBytes(field.Name, CharEncoder);
                    byte[] fieldnamelength = LengthToBytes(fieldname.Length);
                    stream.Write(fieldnamelength, 0, fieldnamelength.Length); // Field name data length.
                    stream.Write(fieldname, 0, fieldname.Length); // Field name data.

                    NewObjectSerialization(field.GetValue(obj)); // Continue by serializing fields.
                }
            }
        }
        private Object NewObjectDeserialization(Type expectedType)
        {
            LastRead = stream.ReadByte(); // New TAG.
            if (LastRead.Equals(CLASS) || LastRead.Equals(ARRAY) || binaryprimitives.ContainsKey(Convert.ToByte(LastRead)))
            {
                expectedType = ClassDeserialization(expectedType);
                if (LastRead.Equals(CLASS))
                    return expectedType;
            }
            if (binaryprimitives.ContainsKey(Convert.ToByte(LastRead))) // Read Primitive TAG. INT, BOOL, CHAR etc. Or an Enum is expected.
            {
                var result = ReadPrimitive(expectedType); // Primitive data length. // Primitive data. int, bool, string etc.
                return result;
            }
            else if (LastRead.Equals(ARRAY)) // Read Array TAG.
            {
                LinkedList<int> arrayLengthsTemp = new LinkedList<int>();
                while ((LastRead = stream.ReadByte()).Equals(ARRAYLENGTH)) // While reading Array TAGs, record the lengths of the array dimension.
                    arrayLengthsTemp.AddLast(ReadLength()); // Array length.
                int[] arrayLengths = new int[arrayLengthsTemp.Count];
                arrayLengthsTemp.CopyTo(arrayLengths, 0);
                
                Array newArray = Array.CreateInstance(expectedType.GetElementType(), arrayLengths);
                int[] indices = new int[newArray.Rank];
                bool c = true;
                while (c)
                {
                    newArray.SetValue(NewObjectDeserialization(expectedType.GetElementType()), indices); // Iterate through array, all dimensions and all lengths.
                    c = false;
                    for (int i = 0; i < indices.Length; i++)
                    {
                        indices[i]++;
                        if (indices[i] < newArray.GetLength(i))
                        {
                            c = true;
                            break;
                        }
                        else
                            indices[i] = 0;
                    }
                }
                return newArray;
            }
            else if (LastRead.Equals(COLLECTION)) // Read Collection TAG. NEED TO SUPPORT MORE COLLECTIONS! TODO.
            {

                // IList
                // IDictionary
                // Else ICollection
                int count = ReadLength(); // Collection count.
                dynamic collection = Activator.CreateInstance(expectedType);
                for (int i = 0; i < count; i++) // Reconstruct collection through iteration.
                {
                    dynamic newObject = NewObjectDeserialization(expectedType.GetGenericArguments()[0]);
                    collection.Add(newObject);
                }
                return collection;
            }
            else if (LastRead.Equals(OBJECT)) // Read Object TAG.
            {
                Object obj = Activator.CreateInstance(expectedType);
                int count = ReadLength(); // Field count.
                for (int i = 0; i < count; i++)
                {
                    LastRead = Convert.ToByte(stream.ReadByte()); // Field TAG.
                    if (!LastRead.Equals(FIELD))
                        throw new Exception("Not a field.");
                    int length = ReadLength(); // Field name data length.
                    byte[] bytes = new byte[length];
                    stream.Read(bytes, 0, length); // Field name data.
                    String fieldName = TypeConverter.ToString(bytes, CharEncoder);
                    FieldInfo field = expectedType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                        field.SetValue(obj, NewObjectDeserialization(field.FieldType)); // Field data deserializing.
                    else
                        SkipField(); // No matching field, skip deserialization of this field.
                }
                return obj;
            }
            else
                throw new Exception("Error. Invalid stream.");
        }

        #region Helper functions
        private int[] ReadArrayLengths()
        {
            LinkedList<int> arrayLengthsTemp = new LinkedList<int>();
            while (LastRead.Equals(ARRAY))
            {
                arrayLengthsTemp.AddLast(stream.ReadByte());
                LastRead = Convert.ToByte(stream.ReadByte());
            }

            int[] arrayLengths = new int[arrayLengthsTemp.Count];
            arrayLengthsTemp.CopyTo(arrayLengths, 0);
            return arrayLengths;
        }

        private void SkipField()
        {
            Skip(); // Start skipping field.
            void Skip()
            {
                LastRead = stream.ReadByte();
                if (LastRead.Equals(CLASS) || LastRead.Equals(ARRAY) || binaryprimitives.ContainsKey(Convert.ToByte(LastRead)))
                {
                    SkipClass();
                    if (LastRead.Equals(CLASS))
                        return;
                }
                if (LastRead.Equals(ARRAY))
                    SkipArray();
                else if (LastRead.Equals(COLLECTION))
                    SkipCollection();
                else if (LastRead.Equals(OBJECT))
                    SkipObject();
                else if (binaryprimitives.ContainsKey(Convert.ToByte(LastRead)))
                    SkipPrimitive();
            }
            void SkipClass()
            {
                if (binaryprimitives.ContainsKey(Convert.ToByte(LastRead)))
                {
                    LastRead = stream.ReadByte();
                }
                else if (LastRead.Equals(ARRAY)) // Array TAG.
                {
                    int arrayRank = stream.ReadByte(); // Array dimensions.
                    LastRead = stream.ReadByte();
                    SkipClass();
                }
                else if (LastRead.Equals(CLASS)) // Class TAG.
                {
                    int fullLength = ReadLength(); // Class full name data length.
                    byte[] fullnameBytes = new byte[fullLength];
                    stream.Read(fullnameBytes, 0, fullnameBytes.Length); // Class full name.
                    LastRead = stream.ReadByte();
                    if (LastRead.Equals(CLASSPARAMETER)) // Class parameter TAG.
                    {
                        int count = stream.ReadByte(); // Number of class parameters
                        LastRead = stream.ReadByte();
                        for (int i = 0; i < count; i++)
                        {
                            SkipClass();
                        }
                    }
                }
            }
            void SkipArray()
            {
                int length = 0;
                if ((LastRead = stream.ReadByte()).Equals(ARRAYLENGTH))
                    length = ReadLength();
                while ((LastRead = stream.ReadByte()).Equals(ARRAYLENGTH)) // While reading Array TAGs, record the lengths of the array dimension.
                    length = length * ReadLength(); // Array length.
                for (int i = 0; i < length; i++)
                {
                    Skip();
                }
            }
            void SkipCollection()
            {
                int count = stream.ReadByte(); // Collection count.
                for (int i = 0; i < count; i++)
                {
                    Skip();
                }
            }
            void SkipObject()
            {
                int count = stream.ReadByte(); // Field count.
                for (int i = 0; i < count; i++)
                {
                    stream.ReadByte(); // Field TAG.
                    int length = ReadLength(); // Length.
                    byte[] bytes = new byte[length];
                    stream.Read(bytes, 0, bytes.Length); // Field name.
                    Skip();
                }
            }
            void SkipPrimitive()
            {
                //int length = stream.ReadByte();
                //byte[] bytes = new byte[length];
                //stream.Read(bytes, 0, bytes.Length);
                int length;
                byte[] bytes;
                if (LastRead.Equals(ENUM) || LastRead.Equals(STRING))
                {
                    byte[] lengthbytes;
                    switch (Length) // Primitive data length.
                    {
                        case Lengths._8bit:
                            length = stream.ReadByte();
                            break;
                        case Lengths._16bit:
                            lengthbytes = new byte[2];
                            stream.Read(lengthbytes, 0, lengthbytes.Length);
                            length = TypeConverter.ToInt16(lengthbytes);
                            break;
                        case Lengths._32bit:
                            lengthbytes = new byte[4];
                            stream.Read(lengthbytes, 0, lengthbytes.Length);
                            length = TypeConverter.ToInt32(lengthbytes);
                            break;
                        default:
                            length = stream.ReadByte();
                            break;
                    }
                    bytes = new byte[length];
                    stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                }
                else
                {
                    length = stream.ReadByte(); // Primitive data length.
                    bytes = new byte[length];
                    stream.Read(bytes, 0, length); // Primitive data. int, bool etc.
                }
            }
        }

        private byte[] LengthToBytes(int length)
        {
            byte[] lengthbytes;
            switch (Length)
            {
                case Lengths._8bit:
                    lengthbytes = new byte[] { Convert.ToByte(length) };
                    break;
                case Lengths._16bit:
                    lengthbytes = TypeConverter.GetBytes(Convert.ToInt16(length));
                    break;
                case Lengths._32bit:
                    lengthbytes = TypeConverter.GetBytes(length);
                    break;
                default:
                    lengthbytes = new byte[] { Convert.ToByte(length) };
                    break;
            }
            return lengthbytes;
        }
        private int ReadLength()
        {
            byte[] lengthbytes;
            int length;
            switch (Length)
            {
                case Lengths._8bit:
                    length = stream.ReadByte();
                    break;
                case Lengths._16bit:
                    lengthbytes = new byte[2];
                    stream.Read(lengthbytes, 0, lengthbytes.Length);
                    length = TypeConverter.ToInt16(lengthbytes);
                    break;
                case Lengths._32bit:
                    lengthbytes = new byte[4];
                    stream.Read(lengthbytes, 0, lengthbytes.Length);
                    length = TypeConverter.ToInt32(lengthbytes);
                    break;
                default:
                    length = stream.ReadByte();
                    break;
            }
            return length;
        }

        private byte[] PrimitiveToBytes(Object obj)
        {
            byte type;
            byte[] length;
            byte[] data;
            if (obj.GetType().IsEnum)
            {
                data = TypeConverter.GetBytes(obj.ToString(), CharEncoder);
                length = LengthToBytes(data.Length);
                type = ENUM;
            }
            else
            {
                switch (obj)
                {
                    case Boolean b:
                        data = TypeConverter.GetBytes(b);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = BOOL;
                        break;
                    case SByte sb:
                        data = TypeConverter.GetBytes(sb);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case Byte b:
                        data = new byte[] { b };
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case Int16 i:
                        data = TypeConverter.GetBytes(i);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case Int32 i:
                        data = TypeConverter.GetBytes(i);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case Int64 i:
                        data = TypeConverter.GetBytes(i);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case UInt16 ui:
                        data = TypeConverter.GetBytes(ui);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case UInt32 ui:
                        data = TypeConverter.GetBytes(ui);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case UInt64 ui:
                        data = TypeConverter.GetBytes(ui);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = INT;
                        break;
                    case Single s:
                        data = TypeConverter.GetBytes(s);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = DOUBLE;
                        break;
                    case Double d:
                        data = TypeConverter.GetBytes(d);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = DOUBLE;
                        break;
                    case Decimal d:
                        data = TypeConverter.GetBytes(d);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = DOUBLE;
                        break;
                    case Char c:
                        data = TypeConverter.GetBytes(c);
                        length = new byte[] { Convert.ToByte(data.Length) };
                        type = CHAR;
                        break;
                    case String s:
                        data = TypeConverter.GetBytes(s, CharEncoder);
                        length = LengthToBytes(data.Length);
                        type = STRING;
                        break;
                    default:
                        throw new ParseException(obj.GetType().ToString() + " is not a primitive.");
                }
            }
            byte[] bytes = new byte[data.Length + length.Length + 1];
            bytes[0] = type;
            length.CopyTo(bytes, 1);
            data.CopyTo(bytes, length.Length + 1);
            return bytes;
        }
        private Object ReadPrimitive(Type type)
        {
            int length;
            byte[] bytes;
            if (type.IsEnum)
            {
                byte[] lengthbytes;
                switch (Length) // Primitive data length.
                {
                    case Lengths._8bit:
                        length = stream.ReadByte();
                        break;
                    case Lengths._16bit:
                        lengthbytes = new byte[2];
                        stream.Read(lengthbytes, 0, lengthbytes.Length);
                        length = TypeConverter.ToInt16(lengthbytes);
                        break;
                    case Lengths._32bit:
                        lengthbytes = new byte[4];
                        stream.Read(lengthbytes, 0, lengthbytes.Length);
                        length = TypeConverter.ToInt32(lengthbytes);
                        break;
                    default:
                        length = stream.ReadByte();
                        break;
                }
                bytes = new byte[length];
                stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                String str = TypeConverter.ToString(bytes, CharEncoder);
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
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToBoolean(bytes);
                    case PrimitiveTypes.SByte:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return bytes[0];
                    case PrimitiveTypes.Byte:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return bytes[0];
                    case PrimitiveTypes.Int16:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToInt16(bytes);
                    case PrimitiveTypes.Int32:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToInt32(bytes);
                    case PrimitiveTypes.Int64:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToInt64(bytes);
                    case PrimitiveTypes.UInt16:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToUInt16(bytes);
                    case PrimitiveTypes.UInt32:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToUInt32(bytes);
                    case PrimitiveTypes.UInt64:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToUInt64(bytes);
                    case PrimitiveTypes.Single:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToSingle(bytes);
                    case PrimitiveTypes.Double:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToDouble(bytes);
                    case PrimitiveTypes.Decimal:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToDecimal(bytes);
                    case PrimitiveTypes.Char:
                        length = stream.ReadByte(); // Primitive data length.
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToChar(bytes, CharEncoder);
                    case PrimitiveTypes.String:
                        byte[] lengthbytes;
                        switch (Length)
                        {
                            case Lengths._8bit:
                                length = stream.ReadByte();
                                break;
                            case Lengths._16bit:
                                lengthbytes = new byte[2];
                                stream.Read(lengthbytes, 0, lengthbytes.Length);
                                length = TypeConverter.ToInt16(lengthbytes);
                                break;
                            case Lengths._32bit:
                                lengthbytes = new byte[4];
                                stream.Read(lengthbytes, 0, lengthbytes.Length);
                                length = TypeConverter.ToInt32(lengthbytes);
                                break;
                            default:
                                length = stream.ReadByte();
                                break;
                        }
                        bytes = new byte[length];
                        stream.Read(bytes, 0, length); // Primitive data. int, bool, string etc.
                        return TypeConverter.ToString(bytes, CharEncoder);
                    default:
                        throw new ParseException(type.ToString() + " is not a primitive.");
                }
            }
        }

        private byte PrimitiveToTAG(PrimitiveTypes primitive)
        {
            switch (primitive)
            {
                case PrimitiveTypes.Boolean:
                    return BOOL;
                case PrimitiveTypes.Char:
                    return CHAR;
                case PrimitiveTypes.Decimal:
                case PrimitiveTypes.Double:
                case PrimitiveTypes.Single:
                    return DOUBLE;
                case PrimitiveTypes.Byte:
                case PrimitiveTypes.Int16:
                case PrimitiveTypes.Int32:
                case PrimitiveTypes.Int64:
                case PrimitiveTypes.SByte:
                case PrimitiveTypes.UInt16:
                case PrimitiveTypes.UInt32:
                case PrimitiveTypes.UInt64:
                    return INT;
                case PrimitiveTypes.String:
                    return STRING;
                case PrimitiveTypes.Enum:
                    return ENUM;
                default:
                    throw new Exception("Illegal Argument.");
            }
        }
        
        private FieldInfo[] GetAllHiddenFields(object obj, Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<FieldInfo> fieldslist = new List<FieldInfo>();
            foreach (FieldInfo field in fields) // Sort out fields with the NotSerializable modifier and fields with null values.
                if (!field.IsNotSerialized && field.GetValue(obj) != null)
                    fieldslist.Add(field);
            return type.BaseType.Equals(typeof(Object)) ? fieldslist.ToArray() : fieldslist.ToArray().Concat(GetAllHiddenFields(obj, type.BaseType)).ToArray();
        }
        #endregion
    }
}
