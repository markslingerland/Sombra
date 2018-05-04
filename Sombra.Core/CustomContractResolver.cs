using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sombra.Core
{
    public class CustomContractResolver<T> : DefaultContractResolver
        where T : Attribute
    {
        private readonly Type _attributeToSerialize;

        public CustomContractResolver()
        {
            _attributeToSerialize = typeof(T);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var list = type.GetProperties()
                .Where(x => Attribute.IsDefined(x, _attributeToSerialize))
                .Select(p => new JsonProperty()
                {
                    PropertyName = p.Name,
                    PropertyType = p.PropertyType,
                    Readable = true,
                    Writable = true,
                    ValueProvider = CreateMemberValueProvider(p)
                }).ToList();

            return list;
        }
    }
}
