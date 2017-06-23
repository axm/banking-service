using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Base.Types
{
    public class SortCode
    {
        public readonly string Code;

        public SortCode(string code)
        {
            if (!Regex.IsMatch(code, "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
            {
                throw new ArgumentException();
            }

            Code = code;
        }

        public override string ToString() => Code;
    }
}
