using System.Diagnostics;

namespace Migrap.Net.Lime {
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public sealed class Event(string value) {
        private readonly string _value = value;

        public static implicit operator string(Event value) {
            return value._value;
        }

        public static implicit operator Event(string value) {
            return new Event(value);
        }

        private string DebuggerDisplay() {
            return _value;
        }
    }
}
