using System;
using System.Collections.Generic;
using System.IO;
using static dotSpace.Objects.Network.Encoders.Binary.Configurations;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    /// <summary>
    /// Provides functionality to serialize and deserialize .Net objects to and from binary code for network communication.
    /// </summary>
    public sealed class BinarySerializer
    {
        #region Fields
        internal static readonly Byte // Primitive TAGs
            BOOL = 0b01100010, // UTF8 for 'b'
            INT = 0b01101001, // UTF8 for 'i'
            DOUBLE = 0b01100100, // UTF8 for 'd'
            CHAR = 0b01100011, // UTF8 for 'c'
            STRING = 0b01110011, // UTF8 for 's'
            ENUM = 0b01100101; // UTF8 for 'e'
        internal static readonly Byte // TAGs
            CLASS = 0b01010100, // UTF8 for 'T'
            OBJECT = 0b01001111, // UTF8 for 'O'
            CLASSPARAMETER = 0b01010000, // UTF8 for 'P'
            FIELD = 0b01000110, // UTF8 for 'F'
            ARRAY = 0b01000001, // UTF8 for 'A'
            ARRAYLENGTH = 0b01001100, // UTF8 for 'L'
            COLLECTION = 0b01000011; // UTF8 for 'C'
        
        /// <summary>
        /// The current number of bits preferred to describe the length of strings.
        /// </summary>
        public LengthBits Length { get; set; } = LengthBits._16bit;

        /// <summary>
        /// The current preferred character encoding to be used for characters.
        /// </summary>
        public CharEncodings CharEncoding { get; set; } = CharEncodings.Unicode;

        private Serialization serialization = new Serialization();
        private Deserialization deserialization = new Deserialization();
        #endregion

        /// <summary>
        /// Serializes the given object into a byte array.
        /// </summary>
        /// <param name="obj">The .Net object to serialize.</param>
        /// <returns>A byte array describing everything necessary to reconstruct the given .Net object.</returns>
        public byte[] Serialize(Object obj)
        {
            using (MemoryStream output = new MemoryStream())
            {
                Serialize(obj, output);
                return output.ToArray();
            }
        }
        /// <summary>
        /// Serializes the given object into a byte array and writes it into the given stream.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="stream">The stream to write the object to.</param>
        public void Serialize(Object obj, Stream stream)
        {
            Configurations config = new Configurations(Length, CharEncoding);
            stream.WriteByte(config.ToByte());
            serialization.NewObjectSerialization(obj, stream, config);
        }

        /// <summary>
        /// Deserializes the given byte array into a .Net object.
        /// </summary>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <returns>The .Net object representation of the given byte array.</returns>
        public Object Deserialize(byte[] bytes) => Deserialize(null, bytes);
        /// <summary>
        /// Deserializes the given the bytes read from the given stream into a .Net object.
        /// </summary>
        /// <param name="stream">The stream to read data from.</param>
        /// <returns>The .Net object representation of the bytes contained in the given stream.</returns>
        public Object Deserialize(Stream stream) => Deserialize(null, stream);

        /// <summary>
        /// Deserializes the given byte array into a .Net object.
        /// </summary>
        /// <param name="type">The expected object type used as reference when deserializing.</param>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <returns>The .Net object representation of the given byte array.</returns>
        public Object Deserialize(Type type, byte[] bytes)
        {
            MemoryStream input = new MemoryStream(bytes);
            return Deserialize(type, input);
        }
        /// <summary>
        /// Deserializes the given the bytes read from the given stream into a .Net object.
        /// </summary>
        /// <param name="type">The expected object type used as reference when deserializing.</param>
        /// <param name="stream">The stream to read data from.</param>
        /// <returns>The .Net object representation of the bytes contained in the given stream.</returns>
        public Object Deserialize(Type type, Stream stream)
        {
            int configurations = stream.ReadByte();
            if (configurations == -1) throw new IOException("Stream contains no data.");
            Configurations config = new Configurations(Convert.ToByte(configurations));
            Object obj = deserialization.NewObjectDeserialization(type, stream, config);
            return obj;
        }
    }
}
