using Migrap.Net.Lime;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            var message = Message();
            var settings = Settings();
            var json = JsonConvert.SerializeObject(message, settings);

            message = JsonConvert.DeserializeObject<Message>(json, settings);

            Console.WriteLine(json);
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
                ContractResolver = new CamelCasePropertyNamesContractResolver(),                
            };

            settings.Converters.Add(new NodeConverter());

            return settings;
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
            writer.WriteValue((value as Node).ToString());
        }
    }
}
