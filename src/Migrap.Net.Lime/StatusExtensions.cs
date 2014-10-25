using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime {

    public static class StatusExtensions {
        private static readonly Status _success = new Status("success");
        private static readonly Status _failure = new Status("failure");

        public static Status Success(this Command command) {
            return _success;
        }

        public static Status Failure(this Command command) {
            return _failure;
        }
    }
}