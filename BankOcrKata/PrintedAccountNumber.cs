﻿using BankOcrKata.Exceptions;
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

        private readonly IEnumerable<PrintedDigit> _printDigits;

        public string DisplayValue { get { return String.Join(string.Empty, _printDigits.Select(pd => pd.IntegerValue)); } }

        public bool IsValid  { get { return IsChecksumSatisfied(_printDigits); } }

        public PrintedAccountNumber(string[] accountNumberLines)
        {
            ValidateOrThrow(accountNumberLines);

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
            string row = accountNumberLines[rowIndex];
            return row.Substring(digitOffset * PrintedDigit.DigitWidth, PrintedDigit.DigitWidth);
        }

        private static void ValidateOrThrow(string[] accountNumberLines)
        {
            if (accountNumberLines.Length != 3 || 
                accountNumberLines.Any(line => line.Length != AccountNumberWidth * PrintedDigit.DigitWidth))
                throw new BadAccountNumberFormatException();
        }

        private static bool IsChecksumSatisfied(IEnumerable<PrintedDigit> printDigits)
        {
            var collection = printDigits.Reverse().ToArray();

            int sum = 0;
            for (int index = 0; index < AccountNumberWidth; index++)
                sum += collection[index].IntegerValue * (index + 1);

            return sum % 11 == 0;
        }
    }
}
