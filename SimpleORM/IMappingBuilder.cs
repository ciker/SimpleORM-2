using System;
using System.Xml.Linq;
using SimpleORM.Mappings;

namespace SimpleORM
{
    public interface IMappingBuilder
    {
        void Configure(XElement configuration);
        
        bool TryGet(Type type, out IMapping mapping);
    }
}
