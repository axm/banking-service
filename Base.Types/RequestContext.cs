using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    [DataContract]
    public class RequestContext
    {
        [DataMember]
        public CorrelationGuid CorrelationId { get; private set; }
        [DataMember]
        private IDictionary<string, object> Data { get; set; }

        public RequestContext(CorrelationGuid correlationId) : this(correlationId, new Dictionary<string, object>())
        {
        }

        public RequestContext(CorrelationGuid correlationId, IDictionary<string, object> data)
        {
            CorrelationId = correlationId;
            Data = data;
        }
    }
}
