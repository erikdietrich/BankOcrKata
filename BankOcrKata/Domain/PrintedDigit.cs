﻿using BankOcrKata.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Domain
{
    public class PrintedDigit
    {
        public const int DigitWidth = 3;
        public const int DigitHeight = 3;

        public static readonly string[][] PrintedIntegerDefinitions = new string[][]
        {
            new string[] {
                " _ ",
                "| |",
                "|_|"
            },
            new string[] { 
                "   ", 
                "  |",  
                "  |" 
            },
            new string[] { 
                " _ ",
                " _|",  
                "|_ " 
            },
            new string[] { 
                " _ ",
                " _|", 
                " _|" 
            },
            new string[] {
                "   ",                   
                "|_|",
                "  |"
            },
            new string[] {
                " _ ",
                "|_ ",
                " _|",
            },
            new string[] {
                " _ ",
                "|_ ",
                "|_|"
            },
            new string[] {
                " _ ",
                "  |",
                "  |",
            },
            new string[] {
                " _ ",
                "|_|",
                "|_|"
            },
            new string[] {
                " _ ",
                "|_|",
                " _|"
            }
        };

        public const string AllowedCharacters = " _|";

        public int IntegerValue { get; private set; }

        public bool IsLegible { get { return IntegerValue != -1; } }

        public PrintedDigit(IList<string> inputRows)
        {
            ValidateOrThrow(inputRows);

            IntegerValue = MatchInputRowsToIntegerValue(inputRows);
        }

        private static int MatchInputRowsToIntegerValue(IList<string> inputRows)
        {
            var possibleDigits = Enumerable.Range(0, 10);
            var possibleDigitsAsNullables = possibleDigits.Select(i => new int?(i));
            var digitThatMatchesOrNull = possibleDigitsAsNullables.FirstOrDefault(nullableDigit => MatchesDefinedMatrixFor(nullableDigit.Value, inputRows));

            return digitThatMatchesOrNull ?? -1;
        }

        private static bool MatchesDefinedMatrixFor(int digitToTry, IList<string> inputRows)
        {
            var actualMatrixForDigitToTry = PrintedIntegerDefinitions[digitToTry];
            var rowIndeces = Enumerable.Range(0, actualMatrixForDigitToTry.Length);

            return rowIndeces.All(i => actualMatrixForDigitToTry[i] == inputRows[i]);
        }

        private static void ValidateOrThrow(IList<string> inputRows)
        {
            if (inputRows == null)
                throw new ArgumentNullException("inputRows");
            if (inputRows.Count != DigitHeight || inputRows.Any(row => !IsLegal(row)))
                throw new BadDigitFormatException();
        }

        private static bool IsLegal(string inputRow)
        {
            return inputRow.All(c => AllowedCharacters.Contains(c)) && inputRow.Length == DigitWidth;
        }
    }
}
