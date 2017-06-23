using System;

namespace Base.Types
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
