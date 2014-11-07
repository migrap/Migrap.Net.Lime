using Newtonsoft.Json;
using System;

namespace Migrap.Net.Lime.Serialization.Converters {
    public class EventConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Event).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, global::Newtonsoft.Json.JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.String) {
                return new Event((string)reader.Value);
            }
            throw new InvalidOperationException();
        }

        public override void WriteJson(JsonWriter writer, object value, global::Newtonsoft.Json.JsonSerializer serializer) {
            writer.WriteValue((string)(value as Event));
        }
    }
}
