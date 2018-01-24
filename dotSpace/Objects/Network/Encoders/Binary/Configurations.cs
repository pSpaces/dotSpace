using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using System;
using System.IO;
using static dotSpace.Objects.Network.Encoders.Binary.Configurations;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    public sealed class Configurations
    {
        /// <summary>
        /// Enumerators for the possible number of bits to describe the length of strings.
        /// Note: 8bit should only be used in special space saving scenarios.
        /// </summary>
        public enum LengthBits
        {
            _8bit, // Max length 127, 8bit (signed)
            _16bit, // Max length 32767, 16bit (signed)
            _32bit // Max length 2147483647, 32bit (signed)
        }

        /// <summary>
        /// Enumerators for the possible character encodings to be used for characters.
        /// </summary>
        public enum CharEncodings
        {
            UTF8 = 0, UTF16 = 1, Unicode = 1, UTF32 = 2
        }

        /// <summary>
        /// The length enum contained in this configuration.
        /// </summary>
        public LengthBits LengthConfig { get; }
        /// <summary>
        /// The character encoding enum contained in this configuration.
        /// </summary>
        public CharEncodings CharEncoding { get; }

        /// <summary>
        /// Initialize a configuration object based on the given byte configuration.
        /// </summary>
        /// <param name="configByte"></param>
        public Configurations(byte configByte)
        {
            if ((configByte & 0b00000001) == 0b00000001)
                CharEncoding = CharEncodings.UTF8;
            else if ((configByte & 0b00000010) == 0b00000010)
                CharEncoding = CharEncodings.UTF16;
            else
                CharEncoding = CharEncodings.UTF32;
            if ((configByte & 0b00001100) == 0b00000100)
                LengthConfig = LengthBits._8bit;
            else if ((configByte & 0b00001100) == 0b00001000)
                LengthConfig = LengthBits._16bit;
            else
                LengthConfig = LengthBits._32bit;
        }

        /// <summary>
        /// Initialize a configuration object based on the given preferred configuration enums.
        /// </summary>
        /// <param name="lengthConfig">The preferred length enum.</param>
        /// <param name="charEncoding">The preferred character encoding enum.</param>
        public Configurations(LengthBits lengthConfig, CharEncodings charEncoding)
        {
            this.LengthConfig = lengthConfig;
            this.CharEncoding = charEncoding;
        }

        /// <summary>
        /// Convert the configuration object into its byte representation.
        /// </summary>
        /// <returns>The byte representing this configuration.</returns>
        public byte ToByte()
        {
            int settings = 0b00000000;
            switch (CharEncoding)
            {
                case CharEncodings.UTF8:
                    settings = settings | 0b00000001;
                    break;
                case CharEncodings.Unicode:
                    settings = settings | 0b00000010;
                    break;
                case CharEncodings.UTF32:
                    settings = settings | 0b00000011;
                    break;
            }
            switch (LengthConfig)
            {
                case LengthBits._8bit:
                    settings = settings | 0b00000100;
                    break;
                case LengthBits._16bit:
                    settings = settings | 0b00001000;
                    break;
                case LengthBits._32bit:
                    settings = settings | 0b00001100;
                    break;
            }
            return Convert.ToByte(settings);
        }
    }

    internal static class LengthHelper
    {
        internal static byte[] ToBytes(this LengthBits length, int count)
        {
            switch (length)
            {
                case LengthBits._8bit:
                    return new byte[] { Convert.ToByte(count) };
                case LengthBits._16bit:
                    return TypeConverter.GetBytes(Convert.ToInt16(count));
                case LengthBits._32bit:
                    return TypeConverter.GetBytes(count);
                default:
                    return new byte[] { Convert.ToByte(count) };
            }
        }

        internal static int ToLength(this LengthBits lengthConfig, Stream stream)
        {
            byte[] bytes;
            switch (lengthConfig)
            {
                case LengthBits._8bit:
                    return stream.ReadByte();
                case LengthBits._16bit:
                    bytes = new byte[2];
                    stream.Read(bytes, 0, bytes.Length);
                    return TypeConverter.ToInt16(bytes);
                case LengthBits._32bit:
                    bytes = new byte[4];
                    stream.Read(bytes, 0, bytes.Length);
                    return TypeConverter.ToInt32(bytes);
                default:
                    return stream.ReadByte();
            }
        }
    }
}
