using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Test
{
    [TestClass]
    public class AccountNumberCorrectorTest
    {
        private AccountNumberCorrector Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Target = new AccountNumberCorrector();   
        }

        [TestClass]
        public class GetPossibleCorrections : AccountNumberCorrectorTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_711111111_For_111111111()
            {
                var linesForAllOnes = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForAllOnes);

                var firstMatch = Target.GetPossibleCorrections(accountNumber).First();

                Assert.AreEqual<string>("711111111", firstMatch.DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_777777177_For_777777777()
            {
                var linesForAllOnes = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForAllOnes);

                var firstMatch = Target.GetPossibleCorrections(accountNumber).First();

                Assert.AreEqual<string>("777777177", firstMatch.DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_333393333_For_333333333()
            {
                var linesForAllThrees = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    " _| _| _| _| _| _| _| _| _|",
                    " _| _| _| _| _| _| _| _| _|"
                };

                var accountNumber = new PrintedAccountNumber(linesForAllThrees);

                var firstMatch = Target.GetPossibleCorrections(accountNumber).First();

                Assert.AreEqual<string>("333393333", firstMatch.DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Finds_888886888_For_88888888()
            {
                var linesForAllThrees = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "|_||_||_||_||_||_||_||_||_|",
                    "|_||_||_||_||_||_||_||_||_|"
                };

                var accountNumber = new PrintedAccountNumber(linesForAllThrees);

                var matches = Target.GetPossibleCorrections(accountNumber);

                Assert.IsTrue(matches.Any(m => m.DisplayValue == "888886888"));
            }
        }

        [TestClass]
        public class FormatPossibleCorrections : AccountNumberCorrectorTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Display_Of_Original_When_Formatting_Not_Possible()
            {
                var garbage = new string[] 
                {
                    "                           ",
                    "                           ",
                    "                           "
                };

                var accountNumber = new PrintedAccountNumber(garbage);

                var output = Target.FormatPossibleCorrections(accountNumber);

                Assert.AreEqual<string>("????????? ILL", output);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Correct_AccountNumber_When_Only_One_Match_Is_Possible()
            {
                var linesForAllOnes = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                };

                var accountNumber = new PrintedAccountNumber(linesForAllOnes);

                var output = Target.FormatPossibleCorrections(accountNumber);

                Assert.AreEqual<string>("711111111", output);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Original_With_AMB_Designation_For_Mutliple_Possibles()
            {
                var linesForAllThrees = new string[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "|_||_||_||_||_||_||_||_||_|",
                    "|_||_||_||_||_||_||_||_||_|"
                };

                var accountNumber = new PrintedAccountNumber(linesForAllThrees);

                var output = Target.FormatPossibleCorrections(accountNumber);

                Assert.AreEqual<string>("888888888 AMB ['888888880', '888886888', '888888988']", output);
            }
        }
    }
}
