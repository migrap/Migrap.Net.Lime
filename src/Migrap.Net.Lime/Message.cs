using System;

namespace Migrap.Net.Lime {
    public class Message(Guid id) : Envelope(id) { 
        public string Type { get; set; }
        public object Content { get; set; }
    }
}
