using System;
using NUnit.Framework;
using SimpleORM.Impl.Mappings.Xml.Exceptions;
using SimpleORM.Impl.Mappings.Xml.Utils;

namespace SimpleORM.Impl.Mappings.Xml.Test
{
    [TestFixture]
    public class ParseTypeUnitTest
    {
        [Test]
        [ExpectedException(typeof(ParseTypeException))]
        public void ResolveWrongType()
        {
            Assert.AreEqual(TypeUtils.ParseType("wrong-type"), typeof(bool));
        }

        [Test]
        public void ResolveBooleanType()
        {
            Assert.AreEqual(TypeUtils.ParseType("bool"), typeof(bool));
        }

        [Test]
        public void ResolveNullableBooleanType()
        {
            Assert.AreEqual(TypeUtils.ParseType("bool?"), typeof(bool?));
        }

        [Test]
        public void ResolveStringType()
        {
            Assert.AreEqual(TypeUtils.ParseType("string"), typeof(string));
        }

        [Test]
        [ExpectedException(typeof(ParseTypeException))]
        public void ResolveNullableStringType()
        {
            Assert.AreEqual(TypeUtils.ParseType("string?"), typeof(string));
        }

        [Test]
        public void ResolveTypeType()
        {
            Assert.AreEqual(TypeUtils.ParseType("type"), typeof(Type));
        }

        [Test]
        [ExpectedException(typeof(ParseTypeException))]
        public void ResolveNullableTypeType()
        {
            Assert.AreEqual(TypeUtils.ParseType("type?"), typeof(Type));
        }

        [Test]
        public void ResolveDateTimeType()
        {
            Assert.AreEqual(TypeUtils.ParseType("date-time"), typeof(DateTime));
        }

        [Test]
        public void ResolveByteType()
        {
            Assert.AreEqual(TypeUtils.ParseType("byte"), typeof(byte));
        }

        [Test]
        public void ResolveNullableByteType()
        {
            Assert.AreEqual(TypeUtils.ParseType("byte?"), typeof(byte?));
        }

        [Test]
        public void ResolveShortType()
        {
            Assert.AreEqual(TypeUtils.ParseType("short"), typeof(short));
        }

        [Test]
        public void ResolveNullableShortType()
        {
            Assert.AreEqual(TypeUtils.ParseType("short?"), typeof(short?));
        }

        [Test]
        public void ResolveIntType()
        {
            Assert.AreEqual(TypeUtils.ParseType("int"), typeof(int));
        }

        [Test]
        public void ResolveNullableIntType()
        {
            Assert.AreEqual(TypeUtils.ParseType("int?"), typeof(int?));
        }

        [Test]
        public void ResolveLongType()
        {
            Assert.AreEqual(TypeUtils.ParseType("long"), typeof(long));
        }

        [Test]
        public void ResolveNullableLongType()
        {
            Assert.AreEqual(TypeUtils.ParseType("long?"), typeof(long?));
        }

        [Test]
        public void ResolveFloatType()
        {
            Assert.AreEqual(TypeUtils.ParseType("float"), typeof(float));
        }

        [Test]
        public void ResolveNullableFloatType()
        {
            Assert.AreEqual(TypeUtils.ParseType("float?"), typeof(float?));
        } 
        
        [Test]
        public void ResolveDoubleType()
        {
            Assert.AreEqual(TypeUtils.ParseType("double"), typeof(double));
        }

        [Test]
        public void ResolveNullableDoubleType()
        {
            Assert.AreEqual(TypeUtils.ParseType("double?"), typeof(double?));
        }

        [Test]
        public void ResolveDecimalType()
        {
            Assert.AreEqual(TypeUtils.ParseType("decimal"), typeof(decimal));
        }

        [Test]
        public void ResolveNullableDecimalType()
        {
            Assert.AreEqual(TypeUtils.ParseType("decimal?"), typeof(decimal?));
        }

        [Test]
        public void ResolveGuidType()
        {
            Assert.AreEqual(TypeUtils.ParseType("guid"), typeof(Guid));
        }

        [Test]
        public void ResolveNullableGuidType()
        {
            Assert.AreEqual(TypeUtils.ParseType("guid?"), typeof(Guid?));
        }
    }
}
