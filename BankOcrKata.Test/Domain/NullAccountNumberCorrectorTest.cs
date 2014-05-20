using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankOcrKata.Domain;

namespace BankOcrKata.Test.Domain
{
    [TestClass]
    public class NullAccountNumberCorrectorTest
    {
        [TestClass]
        public class GetPossibleCorrections : NullAccountNumberCorrectorTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Only_Passed_In_Value()
            {
                var linesForAllOnes = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForAllOnes);

                var corrector = new NullAccountNumberCorrector();

                var firstMatch = corrector.GetPossibleCorrections(accountNumber).First();

                Assert.AreEqual<string>("111111111 ERR", firstMatch.DisplayValue);
            }
        }

        [TestClass]
        public class FormatPossibleCorrections : NullAccountNumberCorrectorTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_DisplayValue_Of_Passed_In_AccountNumber()
            {
                var linesForAllOnes = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForAllOnes);

                var corrector = new NullAccountNumberCorrector();

                var output = corrector.FormatPossibleCorrections(accountNumber);

                Assert.AreEqual<string>("111111111 ERR", output);
            }
        }
    }
}
