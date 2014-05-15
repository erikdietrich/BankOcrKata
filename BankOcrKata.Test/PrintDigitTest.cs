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
            public void Returns_4_WHen_Passed_Characters_For_4()
            {
                
            }
        }
    }

    public class PrintedDigit
    {
        public static readonly string[][] PrintedIntegerDefinitions = new string[][]
        {
            new string[]{ "", "", ""},
            new string[]{ 
                "   ", 
                "  |",  
                "  |" 
            },
            new string[] { 
                " _ ",
                " _|",  
                "|_ " 
            },
            new string[]{ 
                " _ ",
                " _|", 
                " _|" 
            }
        };

        public int IntegerValue { get; private set; }

        public PrintedDigit(IList<string> lines)
        {
            for (int index = 1; index < 4; index++)
            {
                if (MatchesEntryInDefinitions(index, lines))
                    IntegerValue = index;
            }
        }
        private bool MatchesEntryInDefinitions(int integerToTry, IList<string> lines)
        {
            return PrintedIntegerDefinitions[integerToTry][0] == lines[0]
                && PrintedIntegerDefinitions[integerToTry][1] == lines[1]
                && PrintedIntegerDefinitions[integerToTry][2] == lines[2];
        }
    }
}
