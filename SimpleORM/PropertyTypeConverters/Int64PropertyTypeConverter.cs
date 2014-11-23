using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class Int64BytePropertyTypeConverter : PropertyTypeConverter<long, byte>
    {
        protected override Func<long, byte> ConverterFromT1ToT2
        {
            get { return v => (byte)v; }
        }

        protected override Func<byte, long> ConverterFromT2ToT1
        {
            get { return v => (long)v; }
        }
    }

    public class Int64Int16PropertyTypeConverter : PropertyTypeConverter<long, short>
    {
        protected override Func<long, short> ConverterFromT1ToT2
        {
            get { return v => (short)v; }
        }

        protected override Func<short, long> ConverterFromT2ToT1
        {
            get { return v => (long)v; }
        }
    }

    public class Int64Int32PropertyTypeConverter : PropertyTypeConverter<long, int>
    {
        protected override Func<long, int> ConverterFromT1ToT2
        {
            get { return v => (int)v; }
        }

        protected override Func<int, long> ConverterFromT2ToT1
        {
            get { return v => (long)v; }
        }
    }
}