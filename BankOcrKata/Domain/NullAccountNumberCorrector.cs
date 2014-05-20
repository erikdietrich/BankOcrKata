using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Domain
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
