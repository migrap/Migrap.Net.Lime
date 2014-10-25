
namespace Migrap.Net.Lime {
    public abstract class Identity {
        public abstract string Name { get; }
        public abstract string Domain { get; }

        public override string ToString() {
            return string.Format("{0}@{1}", Name, Domain);
        }

        public static implicit operator string(Identity value) {
            return value.ToString();
        }
    }
}
