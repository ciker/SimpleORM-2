using System;
using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM
{
    public interface IMappingBuilder
    {
        void Configure(XElement configuration);

        void RegisterMapping(IMapping mapping);

        TMapping GetMapping<TMapping>(Type type) where TMapping : IMapping;
    }
}
