using BankOcrKata.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Domain
{
    public class PrintedAccountNumber
    {
        public const int AccountNumberWidth = 9;

        private readonly IEnumerable<PrintedDigit> _printDigits;

        public string[] RawLines
        {
            get;
            private set;
        }

        public string DisplayValue 
        {
            get
            {
                var rawAccountNumber = String.Join(string.Empty, _printDigits.Select(pd => pd.IntegerValue));
                return FormatForSpecialCasesIfNeeded(rawAccountNumber);
            } 
        }

        public bool IsValid  { get { return IsLegible && IsChecksumSatisfied(_printDigits); } }

        public bool IsLegible { get { return _printDigits.All(pd => pd.IsLegible);  } }

        public PrintedAccountNumber(string[] accountNumberLines)
        {
            ValidateOrThrow(accountNumberLines);
            RawLines = accountNumberLines;
            _printDigits = ParseAccountNumberIntoDigits(accountNumberLines);
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
            var row = accountNumberLines[rowIndex];
            return row.Substring(digitOffset * PrintedDigit.DigitWidth, PrintedDigit.DigitWidth);
        }

        private static void ValidateOrThrow(string[] accountNumberLines)
        {
            if (accountNumberLines.Length != PrintedDigit.DigitHeight || 
                accountNumberLines.Any(line => line.Length != AccountNumberWidth * PrintedDigit.DigitWidth))
                throw new BadAccountNumberFormatException();
        }

        private static bool IsChecksumSatisfied(IEnumerable<PrintedDigit> printDigits)
        {
            var collection = printDigits.Reverse().ToArray();

            var sum = 0;
            for (var index = 0; index < AccountNumberWidth; index++)
                sum += collection[index].IntegerValue * (index + 1);

            return sum % 11 == 0;
        }

        private string FormatForSpecialCasesIfNeeded(string rawAccountNumber)
        {
            if (rawAccountNumber.Contains("-1"))
            {
                rawAccountNumber = rawAccountNumber.Replace("-1", "?");
                rawAccountNumber = rawAccountNumber + " ILL";
            }
            else if (!IsValid)
                rawAccountNumber = rawAccountNumber + " ERR";

            return rawAccountNumber;
        }
    }
}
