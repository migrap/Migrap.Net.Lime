using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Migrap.Net.Lime.Serialization {
    internal class JsonContractResolver  : CamelCasePropertyNamesContractResolver {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
            var property = base.CreateProperty(member, memberSerialization);

            if(typeof(Envelope).IsAssignableFrom(member.DeclaringType) && property.PropertyType == typeof(Guid)) {
                property.ShouldSerialize = x => (x as Envelope).Id != Guid.Empty;

            }

            if(typeof(Envelope).IsAssignableFrom(member.DeclaringType) && property.PropertyType == typeof(IDictionary<string, string>)) {
                property.ShouldSerialize = x => (x as Envelope).Metadata.Any();
            }

            return property;
        }
    }
}
