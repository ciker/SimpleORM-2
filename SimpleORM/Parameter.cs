using System;

namespace SimpleORM
{
    public class Parameter
    {
        public static Parameter Next
        {
            get { return new Parameter(); }
        }

        internal string Name;
        internal int Index;

        internal Parameter()
        {
        }

        public static implicit operator Parameter(string x)
        {
            return null;
        }

        public static implicit operator Parameter(DateTime x)
        {
            return null;
        }

        public static implicit operator Parameter(bool x)
        {
            return null;
        }

        public static implicit operator Parameter(int x)
        {
            return null;
        }

        public static implicit operator Parameter(long x)
        {
            return null;
        }

        public static implicit operator Parameter(float x)
        {
            return null;
        }

        public static implicit operator Parameter(DateTime? x)
        {
            return null;
        }

        public static implicit operator Parameter(bool? x)
        {
            return null;
        }

        public static implicit operator Parameter(int? x)
        {
            return null;
        }

        public static implicit operator Parameter(long? x)
        {
            return null;
        }

        public static implicit operator Parameter(float? x)
        {
            return null;
        }

        public static bool operator ==(Parameter p1, Parameter p2)
        {
            return true;
        }

        public static bool operator !=(Parameter p1, Parameter p2)
        {
            return true;
        }

        public static bool operator >(Parameter p1, Parameter p2)
        {
            return true;
        }

        public static bool operator <(Parameter p1, Parameter p2)
        {
            return true;
        }
    }
}
