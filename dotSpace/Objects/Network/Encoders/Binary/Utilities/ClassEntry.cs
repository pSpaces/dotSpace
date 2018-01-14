using System;

namespace dotSpace.Objects.Network.Encoders.Binary.Utilities
{
    public class ClassEntry
    {
        public Type Key { get; set; }
        public String Value { get; set; }

        public ClassEntry(Type key, String value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
