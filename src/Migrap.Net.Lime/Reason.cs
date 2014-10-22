using System.Diagnostics;

namespace Migrap.Net.Lime {
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public partial class Reason(int code, string description) {
        public int Code { get; } = code;

        public string Description { get; } = description;

        private string DebuggerDisplay() {
            return string.Format("{0} (Code {1})", Description, Code);
        }
    }
}