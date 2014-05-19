using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Types
{
    public interface IAccountNumberCorrector
    {
        string FormatPossibleCorrections(PrintedAccountNumber target);
        IEnumerable<PrintedAccountNumber> GetPossibleCorrections(PrintedAccountNumber target);
    }
}
