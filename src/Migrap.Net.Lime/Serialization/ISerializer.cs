using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrap.Net.Lime.Serialization {
    public interface ISerializer {
        /// <summary>
        /// Serialize an envelope
        /// to a string
        /// </summary>
        /// <typeparam name="TEnvelope"></typeparam>
        /// <param name="envelope"></param>
        /// <returns></returns>
        void Serialize(Envelope value, byte[] buffer, int offset, int count);

        /// <summary>
        /// Deserialize an envelope
        /// from a string
        /// </summary>
        /// <typeparam name="TEnvelope"></typeparam>
        /// <param name="envelopeString"></param>
        /// <returns></returns>
        Envelope Deserialize(string value);
    }
}
