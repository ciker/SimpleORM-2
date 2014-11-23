using System;

namespace SimpleORM.PropertyTypeConverters
{
    public class YesNoPropertyTypeConverter : PropertyTypeConverter<string, bool>
    {
        protected override Func<string, bool> ConverterFromT1ToT2
        {
            get { return v => v == "Y"; }
        }

        protected override Func<bool, string> ConverterFromT2ToT1
        {
            get { return v => v ? "Y" : "N"; }
        }
    }

    public class LowerYesNoPropertyTypeConverter : PropertyTypeConverter<string, bool>
    {
        protected override Func<string, bool> ConverterFromT1ToT2
        {
            get { return v => v == "y"; }
        }

        protected override Func<bool, string> ConverterFromT2ToT1
        {
            get { return v => v ? "y" : "n"; }
        }
    }
}