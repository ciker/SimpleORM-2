using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class SingleBytePropertyTypeConverter : PropertyTypeConverter<float, byte>
    {
        protected override Func<float, byte> ConverterFromT1ToT2
        {
            get { return v => (byte)v; }
        }

        protected override Func<byte, float> ConverterFromT2ToT1
        {
            get { return v => (float)v; }
        }
    }

    public class SingleInt16PropertyTypeConverter : PropertyTypeConverter<float, short>
    {
        protected override Func<float, short> ConverterFromT1ToT2
        {
            get { return v => (short)v; }
        }

        protected override Func<short, float> ConverterFromT2ToT1
        {
            get { return v => (float)v; }
        }
    }

    public class SingleInt32PropertyTypeConverter : PropertyTypeConverter<float, int>
    {
        protected override Func<float, int> ConverterFromT1ToT2
        {
            get { return v => (int)v; }
        }

        protected override Func<int, float> ConverterFromT2ToT1
        {
            get { return v => (float)v; }
        }
    }

    public class SingleInt64PropertyTypeConverter : PropertyTypeConverter<float, long>
    {
        protected override Func<float, long> ConverterFromT1ToT2
        {
            get { return v => (long)v; }
        }

        protected override Func<long, float> ConverterFromT2ToT1
        {
            get { return v => (float)v; }
        }
    }
}