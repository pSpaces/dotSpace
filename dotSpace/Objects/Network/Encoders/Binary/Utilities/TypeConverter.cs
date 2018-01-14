using System;
using System.Text;
using static dotSpace.Objects.Network.Encoders.Binary.BinarySerializer;

namespace dotSpace.Objects.Network.Encoders.Binary.Utilities
{
    public static class TypeConverter
    {
        public static byte[] GetBytes(int v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static int ToInt32(byte[] v) => ToInt32(v, 0);
        public static int ToInt32(byte[] v, int i)
        {
            return BitConverter.ToInt32(v, i);
        }

        public static byte[] GetBytes(uint v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static uint ToUInt32(byte[] v) => ToUInt32(v, 0);
        public static uint ToUInt32(byte[] v, int i)
        {
            return BitConverter.ToUInt32(v, 0);
        }

        public static byte[] GetBytes(short v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static short ToInt16(byte[] v) => ToInt16(v, 0);
        public static short ToInt16(byte[] v, int i)
        {
            return BitConverter.ToInt16(v, 0);
        }

        public static byte[] GetBytes(ushort v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static ushort ToUInt16(byte[] v) => ToUInt16(v, 0);
        public static ushort ToUInt16(byte[] v, int i)
        {
            return BitConverter.ToUInt16(v, 0);
        }

        public static byte[] GetBytes(long v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static long ToInt64(byte[] v) => ToInt64(v, 0);
        public static long ToInt64(byte[] v, int i)
        {
            return BitConverter.ToInt64(v, 0);
        }

        public static byte[] GetBytes(ulong v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static ulong ToUInt64(byte[] v) => ToUInt64(v, 0);
        public static ulong ToUInt64(byte[] v, int i)
        {
            return BitConverter.ToUInt64(v, 0);
        }

        public static byte[] GetBytes(bool v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static bool ToBoolean(byte[] v) => ToBoolean(v, 0);
        public static bool ToBoolean(byte[] v, int i)
        {
            return BitConverter.ToBoolean(v, 0);
        }

        public static byte[] GetBytes(char v, CharEncoding e)
        {
            switch (e)
            {
                case CharEncoding.UTF8:
                    return new UTF8Encoding().GetBytes(new char[] { v });
                case CharEncoding.Unicode:
                    return new UnicodeEncoding().GetBytes(new char[] { v });
                case CharEncoding.UTF32:
                    return new UTF32Encoding().GetBytes(new char[] { v });
                default:
                    return new UnicodeEncoding().GetBytes(new char[] { v });
            }
        }

        public static char ToChar(byte[] v, CharEncoding e)
        {
            switch (e)
            {
                case CharEncoding.UTF8:
                    return new UTF8Encoding().GetChars(v)[0];
                case CharEncoding.Unicode:
                    return new UnicodeEncoding().GetChars(v)[0];
                case CharEncoding.UTF32:
                    return new UTF32Encoding().GetChars(v)[0];
                default:
                    return new UnicodeEncoding().GetChars(v)[0];
            }
        }

        public static byte[] GetBytes(string v, CharEncoding e)
        {
            switch (e)
            {
                case CharEncoding.UTF8:
                    return new UTF8Encoding().GetBytes(v);
                case CharEncoding.Unicode:
                    return new UnicodeEncoding().GetBytes(v);
                case CharEncoding.UTF32:
                    return new UTF32Encoding().GetBytes(v);
                default:
                    return new UnicodeEncoding().GetBytes(v);
            }
        }

        public static string ToString(byte[] v, CharEncoding e)
        {
            switch (e)
            {
                case CharEncoding.UTF8:
                    return new UTF8Encoding().GetString(v);
                case CharEncoding.Unicode:
                    return new UnicodeEncoding().GetString(v);
                case CharEncoding.UTF32:
                    return new UTF32Encoding().GetString(v);
                default:
                    return new UnicodeEncoding().GetString(v);
            }
        }

        public static byte[] GetBytes(float v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static float ToSingle(byte[] v) => ToSingle(v, 0);
        public static float ToSingle(byte[] v, int i)
        {
            return BitConverter.ToSingle(v, 0);
        }

        public static byte[] GetBytes(double v)
        {
            byte[] result = BitConverter.GetBytes(v);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        public static double ToDouble(byte[] v) => ToDouble(v, 0);
        public static double ToDouble(byte[] v, int i)
        {
            return BitConverter.ToDouble(v, 0);
        }

        public static byte[] GetBytes(decimal v)
        {
            int[] dec = Decimal.GetBits(v);
            byte[] result = new byte[dec.Length * 4];
            int index = 0;
            foreach (int i in dec)
            {
                byte[] j = GetBytes(i);
                foreach (byte d in j)
                {
                    result[index++] = d;
                }
            }
            return result;
        }

        public static decimal ToDecimal(byte[] v) => ToDecimal(v, 0);
        public static decimal ToDecimal(byte[] v, int i)
        {
            int[] dec = new int[4];
            for (int index = 0; index < dec.Length; index++)
                dec[index] = ToInt32(v, i + index * 4);
            return new Decimal(dec);
        }
    }
}
