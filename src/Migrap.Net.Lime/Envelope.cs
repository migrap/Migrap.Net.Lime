using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime {
    public abstract partial class Envelope {
        public Envelope() : this(Guid.NewGuid()) {
        }

        protected Envelope(Guid id) {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}