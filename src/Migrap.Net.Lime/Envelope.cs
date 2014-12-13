using System;
using System.Collections.Generic;

namespace Migrap.Net.Lime {
    public abstract partial class Envelope {

        public Envelope(Guid id) {
            Id = id;
        }

        public Guid Id { get; }

        public Node From { get; set; }

        public Node Delegate { get; set; } //  per procurationem

        public Node To { get; set; }

        public IDictionary<string,string> Metadata { get; } = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
    }
}