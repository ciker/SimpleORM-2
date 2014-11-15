using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleORM.Mappings;

namespace SimpleORM.Impl.Mappings
{
    sealed class XmlViewMapping : IViewMapping
    {
        public string Name { get; private set; }

        public string Schema { get; private set; }

        public Type Type { get; private set; }

        public IList<IPropertyMapping> Properties { get; private set; }

        public IDiscriminatorColumn Discriminator { get; private set; }

        public IList<ISubClassMapping> SubClasses { get; private set; }
    }

    sealed class XmlViewPropertyMapping : IViewPropertyMapping
    {
        public ITypeMapping TypeMapping { get; private set; }

        public string Name { get; private set; }

        public MemberInfo Member { get; private set; }
    }
}