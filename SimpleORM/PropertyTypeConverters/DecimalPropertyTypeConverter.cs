using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class DecimalBytePropertyTypeConverter : PropertyTypeConverter<decimal, byte>
    {
        protected override Func<decimal, byte> ConverterFromT1ToT2
        {
            get { return v => (byte) v; }
        }

        protected override Func<byte, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }

    public class DecimalInt16PropertyTypeConverter : PropertyTypeConverter<decimal, short>
    {
        protected override Func<decimal, short> ConverterFromT1ToT2
        {
            get { return v => (short)v; }
        }

        protected override Func<short, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }

    public class DecimalInt32PropertyTypeConverter : PropertyTypeConverter<decimal, int>
    {
        protected override Func<decimal, int> ConverterFromT1ToT2
        {
            get { return v => (int)v; }
        }

        protected override Func<int, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }

    public class DecimalInt64PropertyTypeConverter : PropertyTypeConverter<decimal, long>
    {
        protected override Func<decimal, long> ConverterFromT1ToT2
        {
            get { return v => (long)v; }
        }

        protected override Func<long, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }

    public class DecimalFloatPropertyTypeConverter : PropertyTypeConverter<decimal, float>
    {
        protected override Func<decimal, float> ConverterFromT1ToT2
        {
            get { return v => (float)v; }
        }

        protected override Func<float, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }

    public class DecimalDoublePropertyTypeConverter : PropertyTypeConverter<decimal, double>
    {
        protected override Func<decimal, double> ConverterFromT1ToT2
        {
            get { return v => (double)v; }
        }

        protected override Func<double, decimal> ConverterFromT2ToT1
        {
            get { return v => (decimal)v; }
        }
    }
}