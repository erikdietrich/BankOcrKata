using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    public class NullAccountNumberCorrector : IAccountNumberCorrector
    {
        public string FormatPossibleCorrections(PrintedAccountNumber target)
        {
            return target.DisplayValue;
        }

        public IEnumerable<PrintedAccountNumber> GetPossibleCorrections(PrintedAccountNumber target)
        {
            yield return target;
        }
    }
}
