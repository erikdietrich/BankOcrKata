using BankOcrKata.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Types
{
    public interface IAccountNumberCorrector
    {
        string FormatPossibleCorrections(PrintedAccountNumber target);
        IEnumerable<PrintedAccountNumber> GetPossibleCorrections(PrintedAccountNumber target);
    }
}
