using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime {
    public partial class Notification : Envelope {
        public Notification() : base(Guid.Empty) {
        }

        public Event Event { get; set; }

        public Reason Reason { get; set; }
    }
}
