using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime {
    public class Command(Guid id) : Envelope(id) {
        public Method Method { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }

        public object Resource { get; set; }

        public string Status { get; set; }

        public Reason Reason { get; set; }
    }
}
