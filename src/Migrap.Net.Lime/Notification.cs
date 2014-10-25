using System;

namespace Migrap.Net.Lime {
    public partial class Notification() : Envelope(Guid.Empty) {
        public Event Event { get; set; }

        public Reason Reason { get; set; }        
    }
}
