using Migrap.Net.Lime;
using Migrap.Net.Lime.Serialization;
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

        static void Test<T>(T value) where T : Envelope {
            var bson = new Migrap.Net.Lime.Serialization.JsonSerializer();
            var buffer = new byte[8096 * 8];
            bson.Serialize(value as Envelope, buffer, 8096, 8096);

            var envelope = bson.Deserialize<T>(buffer, 8096, 8096);
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
                Content = new Cute {
                    Text = "I am the one who knowcks!",
                    Thread = 2
                }
            };
            message.Metadata.Add("Localhost", "10.0.1.1");

            return message;
        }
    }

    public class Cute {
        public string Text { get; set; }
        public int Thread { get; set; }
    }
}