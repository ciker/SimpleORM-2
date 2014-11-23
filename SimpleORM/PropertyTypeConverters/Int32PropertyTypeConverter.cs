using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class Int32BytePropertyTypeConverter : PropertyTypeConverter<int, byte>
    {
        protected override Func<int, byte> ConverterFromT1ToT2
        {
            get { return v => (byte)v; }
        }

        protected override Func<byte, int> ConverterFromT2ToT1
        {
            get { return v => (int)v; }
        }
    }

    public class Int32Int16PropertyTypeConverter : PropertyTypeConverter<int, short>
    {
        protected override Func<int, short> ConverterFromT1ToT2
        {
            get { return v => (short)v; }
        }

        protected override Func<short, int> ConverterFromT2ToT1
        {
            get { return v => (int)v; }
        }
    }
}