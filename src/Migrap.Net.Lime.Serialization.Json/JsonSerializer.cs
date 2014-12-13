using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Migrap.Net.Lime.Serialization.Converters;
using Newtonsoft.Json.Bson;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Migrap.Net.Lime.Serialization {
    public class JsonSerializer : ISerializer {
        private JsonSerializerSettings _settings;

        public JsonSerializer() {
            _settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new JsonContractResolver(),
            };
            _settings.Converters.Add(new NodeConverter());
            _settings.Converters.Add(new EventConverter());
            _settings.Converters.Add(new MethodConverter());
        }

        private Envelope Deserialize(string value, JsonSerializerSettings settings) {
            var jobject = (JObject)JsonConvert.DeserializeObject(value, settings);
            if(jobject.Property("content") != null) {
                return jobject.ToObject<Message>();
            }
            if(jobject.Property("event") != null) {
                return jobject.ToObject<Notification>();
            }
            if(jobject.Property("method") != null) {
                return jobject.ToObject<Command>();
            }
            if(jobject.Property("state") != null) {
                return jobject.ToObject<Session>();
            }
            throw new ArgumentException("value is not a valid envelope");
        }

        public Envelope Deserialize(byte[] buffer, int offset, int count) {
            return Deserialize(buffer, offset, count, _settings);
        }

        private Envelope Deserialize(byte[] buffer, int offset, int count, JsonSerializerSettings settings) {
            return Deserialize(Encoding.UTF8.GetString(buffer, offset, count), settings);
        }

        private T Deserialize<T>(byte[] buffer, int offset, int count, JsonSerializerSettings settings) where T : Envelope {
            return (T)Deserialize(buffer, offset, count, settings);
        }        

        public T Deserialize<T>(byte[] buffer, int offset, int count) where T : Envelope {
            return (T)Deserialize<T>(buffer, offset, count, _settings);
        }

        private int Serialize(object value, byte[] buffer, int offset, int count, JsonSerializerSettings settings) {
            var json = JsonConvert.SerializeObject(value, settings);
            return Encoding.UTF8.GetBytes(json, 0, json.Length, buffer, offset);
        }

        private IEnumerable<ArraySegment<byte>> Serialize(object value, JsonSerializerSettings settings, Func<ArraySegment<byte>> segments) {
            var s = JsonConvert.SerializeObject(value, settings);
            var length = Encoding.UTF8.GetByteCount(s);

            for(var i = 0; i < length;) {
                var segment = segments();
                var count = Math.Min(length, segment.Count);
                var encoded = Encoding.UTF8.GetBytes(s, i, count, segment.Array, segment.Offset);
                i += encoded;
                yield return segment;
            }
        }

        public void Serialize(object value, byte[] buffer, int offset, int count) {
            Serialize(value, buffer, offset, count, _settings);
        }

        public string Serialize(Envelope value, JsonSerializerSettings settings) {
            //MemoryStream ms = new MemoryStream();
            //JsonSerializer serializer = new JsonSerializer();
 
            //// serialize product to BSON
            //BsonWriter writer = new BsonWriter(ms);
            //serializer.Serialize(writer, p);
            return JsonConvert.SerializeObject(value, settings);
        }

        void ISerializer.Serialize(Envelope value, byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }

        Envelope ISerializer.Deserialize(string value) {
            throw new NotImplementedException();
        }
    }
}