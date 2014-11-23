using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class TrueFalsePropertyTypeConverter : PropertyTypeConverter<string, bool>
    {
        protected override Func<string, bool> ConverterFromT1ToT2
        {
            get { return v => v == "T"; }
        }

        protected override Func<bool, string> ConverterFromT2ToT1
        {
            get { return v => v ? "T" : "F"; }
        }
    }

    public class TrueFalseLowerPropertyTypeConverter : PropertyTypeConverter<string, bool>
    {
        protected override Func<string, bool> ConverterFromT1ToT2
        {
            get { return v => v == "t"; }
        }

        protected override Func<bool, string> ConverterFromT2ToT1
        {
            get { return v => v ? "t" : "f"; }
        }
    }
}