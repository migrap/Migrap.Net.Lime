using Migrap.Net.Lime;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            Test(Message());
            Test(Notification());
            Test(Command());       
        }

        static void Test<T>(T value) {
            var settings = Settings();
            var json = JsonConvert.SerializeObject(value, settings);

            value = JsonConvert.DeserializeObject<T>(json, settings);

            Console.WriteLine(json);
        }

        static Command Command() {
            var command = new Command(Guid.NewGuid())
            {
                From = "jesse@breakingbad.com/home",
                Method = "set",
                Uri = "/presence",
                Type = "application/vnd.lime.presence+json",
                Resource = new {
                    Status = "available",
                    Message = "Yo 148"
                }
            };

            return command;
        }

        static Notification Notification() {
            var notification = new Notification
            {
                From = "skyler@breakingbad.com/bedroom",
                To = "heisenberg@breakingbad.com/bedroom",
                Event = "received"
            };

            return notification;
        }

        static Message Message() {
            var message = new Message(Guid.NewGuid())
            {
                From = "heisenberg@breakingbad.com/bedroom",
                To = "skyler/bedroom",
                Type = "application/vnd.lime.threadedtext+json",
                Content = new {
                    Text = "I am the one who knowcks!",
                    Thread = 2
                }
            };
            message.Metadata.Add("Localhost", "10.0.1.1");

            return message;
        }
            

        static JsonSerializerSettings Settings() {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new LimeCamelCasePropertyNamesContractResolver(),                
            };

            settings.Converters.Add(new NodeConverter());
            settings.Converters.Add(new EventConverter());
            settings.Converters.Add(new MethodConverter());

            return settings;
        }
    }
    internal class LimeCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if(typeof(Envelope).IsAssignableFrom(member.DeclaringType) && property.PropertyType == typeof(Guid)) {
                property.ShouldSerialize = x => (x as Envelope).Id != Guid.Empty;
                
            }

            if(typeof(Envelope).IsAssignableFrom(member.DeclaringType) && property.PropertyType == typeof(IDictionary<string, string>)) {
                property.ShouldSerialize = x => (x as Envelope).Metadata.Any();
            }

            return property;
        }
    }

    public static partial class Extensions {
        public static bool Any<TSource>(this IEnumerable<TSource> source) {
            return null != source && Enumerable.Any(source);
        }
    }


    public class NodeConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Node).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.String) {
                return Node.Parse((string)reader.Value);
            }
            throw new InvalidOperationException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue((string)(value as Node));
        }
    }

    public class EventConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Event).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.String) {
                return new Event((string)reader.Value);
            }
            throw new InvalidOperationException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue((string)(value as Event));
        }
    }

    public class MethodConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Method).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.String) {
                return new Method((string)reader.Value);
            }
            throw new InvalidOperationException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue((string)(value as Method));
        }
    }
}
