using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    public class PrintedAccountNumber
    {
        public const int AccountNumberWidth = 9;

        public string DisplayValue { get; private set; }

        public PrintedAccountNumber(string[] accountNumberLines)
        {
            if (accountNumberLines.Length != 3)
                throw new BadAccountNumberFormatException();

            var printDigits = ParseAccountNumberIntoDigits(accountNumberLines);
            DisplayValue = String.Join(string.Empty, printDigits.Select(pd => pd.IntegerValue));
        }

        private static IEnumerable<PrintedDigit> ParseAccountNumberIntoDigits(string[] accountNumberLines)
        {
            var accountNumberDigitOffsets = Enumerable.Range(0, AccountNumberWidth);
            return accountNumberDigitOffsets.Select(digitNumber => BuildDigitFor(digitNumber, accountNumberLines));
        }

        private static PrintedDigit BuildDigitFor(int digitOffset, string[] accountNumberLines)
        {
            var rowIndeces = Enumerable.Range(0, PrintedDigit.DigitHeight);
            var digitRows = rowIndeces.Select(rowIndex => BuildRowFor(rowIndex, digitOffset, accountNumberLines));
            return new PrintedDigit(digitRows.ToArray());
        }

        private static string BuildRowFor(int rowIndex, int digitOffset, string[] accountNumberLines)
        {
            return accountNumberLines[rowIndex].Substring(digitOffset * PrintedDigit.DigitWidth, PrintedDigit.DigitWidth);
        }
    }
}
