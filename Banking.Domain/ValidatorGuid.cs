using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain
{
    public class ValidatorGuid : TypedGuid
    {
        public ValidatorGuid() : this(Guid.NewGuid())
        {
        }

        public ValidatorGuid(Guid id) : base(id)
        {
        }
    }
}
