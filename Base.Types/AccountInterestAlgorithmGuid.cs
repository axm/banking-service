using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public class AccountInterestAlgorithmGuid : TypedGuid
    {
        public AccountInterestAlgorithmGuid()
        {
        }

        public AccountInterestAlgorithmGuid(string guid) : this(new Guid(guid))
        {
        }

        public AccountInterestAlgorithmGuid(Guid guid): base(guid)
        {
        }
    }
}
