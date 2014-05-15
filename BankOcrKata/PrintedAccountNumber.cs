using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    public class PrintedAccountNumber
    {
        public string DisplayValue { get; private set; }

        public PrintedAccountNumber(string[] accountNumberLines)
        {
            if (accountNumberLines.Length != 3)
                throw new BadAccountNumberFormatException();

            var printDigits = ParseAccountNumberIntoDigits(accountNumberLines);
            DisplayValue = String.Join(string.Empty, printDigits.Select(pd => pd.IntegerValue));
        }

        private static List<PrintedDigit> ParseAccountNumberIntoDigits(string[] accountNumberLines)
        {
            return Enumerable.Range(0, 9).Select(digitNumber => new PrintedDigit(Enumerable.Range(0, 3).Select(row => accountNumberLines[row].Substring(digitNumber * 3, 3)).ToArray())).ToList();
        }
    }
}
