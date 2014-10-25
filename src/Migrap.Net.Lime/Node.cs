using System;
using System.Text.RegularExpressions;

namespace Migrap.Net.Lime {
    public class Node(string name, string domain, string instance) : Identity, IEquatable<Node> {
        public override string Name { get; } = name;
        public override string Domain { get; } = domain;
        public string Instance { get; } = instance;

        public override string ToString() {
            if(string.IsNullOrWhiteSpace(Instance)) {
                return string.Format("{0}@{1}", Name, Domain);
            } else {
                return string.Format("{0}@{1}/{2}", Name, Domain, Instance);
            }
        }

        public override int GetHashCode() {
            return ToString().ToLower().GetHashCode();
        }

        public bool Equals(Node other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }
            if(ReferenceEquals(this, other)) {
                return true;
            }

            return string.Equals(this, other, StringComparison.InvariantCultureIgnoreCase);
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

            return Equals((Node)obj);
        }

        public static Node Parse(string s) {
            var match = Regex.Match(s, "^(?:([^\"&'/:<>@]{1,1023})@)?([^/@]{1,1023})(?:/(.{1,1023}))?$");

            if(match.Success) {
                return new Node(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);
            }

            return null;
        }

        public static bool TryParse(string s, out Node value) {
            try {
                value = Parse(s);
                return true;
            } catch {
                value = null;
                return false;
            }
        }

        public static implicit operator string (Node value) {
            return value.ToString();
        }

        public static implicit operator Node(string value) {
            return Node.Parse(value);
        }
    }
}