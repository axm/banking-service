using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    [DebuggerDisplay("('{AccountInterestAlgorithmId}', {Version})")]
    public class AccountInterestAlgorithmSpecifier
    {
        public readonly AccountInterestAlgorithmGuid AccountInterestAlgorithmId;
        public readonly uint Version;

        public AccountInterestAlgorithmSpecifier(AccountInterestAlgorithmGuid accountInterestAlgorithmId, uint version)
        {
            AccountInterestAlgorithmId = accountInterestAlgorithmId;
            Version = version;
        }

        public AccountInterestAlgorithmSpecifier(string specifier)
        {
            var pair = specifier.Split(':');
            AccountInterestAlgorithmId = new AccountInterestAlgorithmGuid(pair[0]);
            Version = uint.Parse(pair[1]);
        }
    }
}
