using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    [DebuggerDisplay("('{CreditInterestAlgorithmId}', {Version})")]
    public class CreditInterestAlgorithmSpecifier
    {
        public readonly CreditInterestAlgorithmGuid CreditInterestAlgorithmId;
        public readonly uint Version;

        public CreditInterestAlgorithmSpecifier(CreditInterestAlgorithmGuid creditInterestAlgorithmId, uint version)
        {
            CreditInterestAlgorithmId = creditInterestAlgorithmId;
            Version = version;
        }
    }
}
