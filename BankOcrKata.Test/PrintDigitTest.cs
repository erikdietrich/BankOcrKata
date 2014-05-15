using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Test
{
    [TestClass]
    public class PrintedDigitTest
    {
        [TestClass]
        public class IntegerValue
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Negative_1_When_Passed_Charcters_Not_Matching_A_Number()
            {
                var linesForGarbage = new string[] { "   ", " _ ", "   " };

                var digit = new PrintedDigit(linesForGarbage);

                Assert.AreEqual<int>(-1, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_0_When_Passed_Characters_For_Zero()
            {
                var linesForPrintedZero = PrintedDigit.PrintedIntegerDefinitions[0];

                var digit = new PrintedDigit(linesForPrintedZero);

                Assert.AreEqual<int>(0, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_1_When_Passed_Characters_For_1()
            {
                var linesForPrintedOne = PrintedDigit.PrintedIntegerDefinitions[1];

                var digit = new PrintedDigit(linesForPrintedOne);

                Assert.AreEqual<int>(1, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_2_When_Passed_Characters_For_2()
            {
                var linesforPrintedTwo = PrintedDigit.PrintedIntegerDefinitions[2];

                var digit = new PrintedDigit(linesforPrintedTwo);

                Assert.AreEqual<int>(2, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_3_When_Passed_Characters_For_3()
            {
                var linesForPrintedThree = PrintedDigit.PrintedIntegerDefinitions[3];

                var digit = new PrintedDigit(linesForPrintedThree);

                Assert.AreEqual<int>(3, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_4_When_Passed_Characters_For_4()
            {
                var linesForPrintedFour = PrintedDigit.PrintedIntegerDefinitions[4];

                var digit = new PrintedDigit(linesForPrintedFour);

                Assert.AreEqual<int>(4, digit.IntegerValue);

            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_5_When_Passed_Characters_For_5()
            {
                var linesForPrintedFive = PrintedDigit.PrintedIntegerDefinitions[5];

                var digit = new PrintedDigit(linesForPrintedFive);

                Assert.AreEqual<int>(5, digit.IntegerValue);

            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_6_When_Passed_Characters_For_6()
            {
                var linesForPrintedSix = PrintedDigit.PrintedIntegerDefinitions[6];

                var digit = new PrintedDigit(linesForPrintedSix);

                Assert.AreEqual<int>(6, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_7_When_Passed_Characters_For_7()
            {
                var linesForPrintedSeven = PrintedDigit.PrintedIntegerDefinitions[7];

                var digit = new PrintedDigit(linesForPrintedSeven);

                Assert.AreEqual<int>(7, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_8_When_Passed_Characters_For_8()
            {
                var linesForPrintedEight = PrintedDigit.PrintedIntegerDefinitions[8];

                var digit = new PrintedDigit(linesForPrintedEight);

                Assert.AreEqual<int>(8, digit.IntegerValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_9_When_Passed_Characters_For_9()
            {
                var linesForPrintedNine = PrintedDigit.PrintedIntegerDefinitions[9];

                var digit = new PrintedDigit(linesForPrintedNine);

                Assert.AreEqual<int>(9, digit.IntegerValue);
            }
        }
    }

    public class PrintedDigit
    {
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

        public int IntegerValue { get; private set; }

        public PrintedDigit(IList<string> inputRows)
        {
            var possibleDigits = Enumerable.Range(0, 10);
            var possibleDigitsAsNullables = possibleDigits.Select(i => new int?(i));
            var digitThatMatchesOrNull = possibleDigitsAsNullables.FirstOrDefault(nullableDigit => MatchesDefinedMatrixFor(nullableDigit.Value, inputRows));
            IntegerValue = digitThatMatchesOrNull ?? -1;
        }

        private static bool MatchesDefinedMatrixFor(int digitToTry, IList<string> inputRows)
        {
            var actualMatrixForDigitToTry = PrintedIntegerDefinitions[digitToTry];
            var rowIndeces = Enumerable.Range(0, actualMatrixForDigitToTry.Length);
            return rowIndeces.All(i => actualMatrixForDigitToTry[i] == inputRows[i]);
        }
    }
}
