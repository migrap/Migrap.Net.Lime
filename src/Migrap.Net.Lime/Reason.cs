using System;
using System.Diagnostics;

namespace Migrap.Net.Lime {
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public partial class Reason(int code, string description) : IEquatable<Reason> {
        public int Code { get; } = code;

        public string Description { get; } = description;

        private string DebuggerDisplay() {
            return string.Format("{0} (Code {1})", Description, Code);
        }

        public override int GetHashCode() {
            return Description.ToLower().GetHashCode() ^ Code;
        }

        public bool Equals(Reason other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }
            if(ReferenceEquals(this, other)) {
                return true;
            }

            return Code == other.Code && string.Equals(Description, other.Description, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }
            if(ReferenceEquals(this, obj)) {
                return true;
            }
            if(obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((Reason)obj);
        }
    }
}