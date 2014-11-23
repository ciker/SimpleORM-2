using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class DoubleBytePropertyTypeConverter : PropertyTypeConverter<double, byte>
    {
        protected override Func<double, byte> ConverterFromT1ToT2
        {
            get { return v => (byte)v; }
        }

        protected override Func<byte, double> ConverterFromT2ToT1
        {
            get { return v => (double)v; }
        }
    }

    public class DoubleInt16PropertyTypeConverter : PropertyTypeConverter<double, short>
    {
        protected override Func<double, short> ConverterFromT1ToT2
        {
            get { return v => (short)v; }
        }

        protected override Func<short, double> ConverterFromT2ToT1
        {
            get { return v => (double)v; }
        }
    }

    public class DoubleInt32PropertyTypeConverter : PropertyTypeConverter<double, int>
    {
        protected override Func<double, int> ConverterFromT1ToT2
        {
            get { return v => (int)v; }
        }

        protected override Func<int, double> ConverterFromT2ToT1
        {
            get { return v => (double)v; }
        }
    }

    public class DoubleInt64PropertyTypeConverter : PropertyTypeConverter<double, long>
    {
        protected override Func<double, long> ConverterFromT1ToT2
        {
            get { return v => (long)v; }
        }

        protected override Func<long, double> ConverterFromT2ToT1
        {
            get { return v => (double)v; }
        }
    }

    public class DoubleFloatPropertyTypeConverter : PropertyTypeConverter<double, float>
    {
        protected override Func<double, float> ConverterFromT1ToT2
        {
            get { return v => (float)v; }
        }

        protected override Func<float, double> ConverterFromT2ToT1
        {
            get { return v => (double)v; }
        }
    }
}
