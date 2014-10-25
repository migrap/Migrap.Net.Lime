using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime {
    public class Metadata : IEnumerable<KeyValuePair<string,string>> {
        private MultiValueDictionary<string, string> _metadata = new MultiValueDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public Metadata Add(string name, string value) {
            if(true == _metadata.TryGetValue(name, out var values) && values.Contains(value)) {
                return this;
            }
            _metadata.Add(name, value);            
            return this;
        }

        public int Count {
            get { return _metadata.Sum(x => x.Value.Count); }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
            return _metadata.Select((k, v) => new KeyValuePair<string, string>(k, v)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    internal static partial class MultiValueDictionaryExtensions {
        public static IEnumerable<TResult> Select<TKey, TValue, TResult>(this MultiValueDictionary<TKey, TValue> source, Func<TKey, TValue, TResult> selector) {
            return source.SelectMany(x => x.Value, (k, v) => selector(k.Key, v));
        }
    }
}
