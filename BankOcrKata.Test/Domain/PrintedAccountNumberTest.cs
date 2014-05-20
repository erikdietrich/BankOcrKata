using BankOcrKata.Domain;
using BankOcrKata.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Test.Domain
{
    [TestClass]
    public class PrintedAccountNumberTest
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_BadAccountNumberFormatException_When_Input_Has_Only_Two_Lines()
            {
                var accountNumberLines = new string[] 
                {
                      "    _  _     _  _  _  _  _ ",
                      "  | _| _||_||_ |_   ||_||_|",
                };
                ExtendedAssert.Throws<BadAccountNumberFormatException>(() => new PrintedAccountNumber(accountNumberLines));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Bad_AccountNumberFormatException_When_A_Line_Has_28_Characters()
            {
                var accountNumberLines = new string[] 
                {
                      "    _  _     _  _  _  _  _ ",
                      "  | _| _||_||_ |_   ||_||_| ",
                      "  ||_  _|  | _||_|  ||_| _|",
                };

                ExtendedAssert.Throws<BadAccountNumberFormatException>(() => new PrintedAccountNumber(accountNumberLines));
            }
        }

        [TestClass]
        public class DisplayValue
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_123456789_When_Passed_That_Three_Line_Number()
            {
                var accountNumberLines = new string[] 
                {
                      "    _  _     _  _  _  _  _ ",
                      "  | _| _||_||_ |_   ||_||_|",
                      "  ||_  _|  | _||_|  ||_| _|",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.AreEqual<string>("123456789", accountNumber.DisplayValue.Substring(0, PrintedAccountNumber.AccountNumberWidth));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_1111111111_When_Passed_That_Three_Line_Number()
            {
                var accountNumberLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.AreEqual<string>("111111111", accountNumber.DisplayValue.Substring(0, PrintedAccountNumber.AccountNumberWidth));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Initializes_To_111111111QuestionMark_When_Passed_All_Ones_With_An_Illegible()
            {
                var accountNumberLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |   ",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.AreEqual<string>("11111111?", accountNumber.DisplayValue.Substring(0, PrintedAccountNumber.AccountNumberWidth));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Adds_Space_And_ILL_For_8_1s_And_One_Illegible()
            {
                var accountNumberLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |   ",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.AreEqual<string>("11111111? ILL", accountNumber.DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Adds_Space_And_ERR_For_All_Ones()
            {
                var accountNumberLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.AreEqual<string>("111111111 ERR", accountNumber.DisplayValue);
            }

        }

        [TestClass]
        public class IsValid
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Is_True_For_All_Zeroes()
            {
                var accountNumberLines = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "| || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsTrue(accountNumber.IsValid);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Is_False_For_All_Zeroes_And_A_One()
            {
                var accountNumberLines = new string[]
                {
                    " _  _  _  _  _  _  _  _    ",
                    "| || || || || || || || |  |",
                    "|_||_||_||_||_||_||_||_|  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsFalse(accountNumber.IsValid);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void IsTrue_For_All_Ones_With_A_Zero()
            {
                var accountNumberLines = new string[]
                {
                    "                         _ ",
                    "  |  |  |  |  |  |  |  || |",
                    "  |  |  |  |  |  |  |  ||_|",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsTrue(accountNumber.IsValid);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Is_True_For_Zeroes_Then_Five_One()
            {
                var accountNumberLines = new string[]
                {
                    " _  _  _  _  _  _  _  _    ",
                    "| || || || || || || ||_   |",
                    "|_||_||_||_||_||_||_| _|  |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsTrue(accountNumber.IsValid);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Is_False_If_There_Are_Any_Illegible_Characters()
            {
                var linesForErrorCondition = new string[]
                {
                    " _  _  _  _ __  _  _  _  _ ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForErrorCondition);

                Assert.IsFalse(accountNumber.IsValid);
            }
        }

        [TestClass]
        public class IsLegible
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_True_For_All_Zeroes()
            {
                var accountNumberLines = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "| || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsTrue(accountNumber.IsLegible);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_False_For_8_Zeros_And_1_Garbage_Character()
            {
                var accountNumberLines = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "| || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_|| |",
                };

                var accountNumber = new PrintedAccountNumber(accountNumberLines);

                Assert.IsFalse(accountNumber.IsLegible);
            }
        }
    }
}
