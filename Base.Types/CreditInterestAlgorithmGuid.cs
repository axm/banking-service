using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public class CreditInterestAlgorithmGuid : TypedGuid
    {
        public CreditInterestAlgorithmGuid()
        {
        }

        public CreditInterestAlgorithmGuid(string guid) : this(new Guid(guid))
        {
        }

        public CreditInterestAlgorithmGuid(Guid id) : base(id)
        {
        }
    }
}
