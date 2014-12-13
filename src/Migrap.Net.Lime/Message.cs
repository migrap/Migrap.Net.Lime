using System;

namespace Migrap.Net.Lime {
    public class Message : Envelope {
        public Message(Guid id) : base(id) { }
        public string Type { get; set; }
        public object Content { get; set; }
    }
}
