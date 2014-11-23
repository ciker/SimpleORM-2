using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class Int16BytePropertyTypeConverter : PropertyTypeConverter<short, byte>
    {
        protected override Func<short, byte> ConverterFromT1ToT2
        {
            get { return v => (byte)v; }
        }

        protected override Func<byte, short> ConverterFromT2ToT1
        {
            get { return v => (short)v; }
        }
    }
}