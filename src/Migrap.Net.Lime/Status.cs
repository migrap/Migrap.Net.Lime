﻿using System;
using System.Diagnostics;

namespace Migrap.Net.Lime {
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public sealed class Status(string value) : IEquatable<Status> {
        private readonly string _value = value;

        public static implicit operator string (Status value) {
            return value._value;
        }

        public static implicit operator Status(string value) {
            return new Status(value);
        }

        private string DebuggerDisplay() {
            return _value;
        }

        public override int GetHashCode() {
            return _value.ToLower().GetHashCode();
        }

        public bool Equals(Status other) {
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

            return Equals((Status)obj);
        }
    }
}