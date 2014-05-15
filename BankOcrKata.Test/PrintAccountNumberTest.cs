using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Test
{
    [TestClass]
    public class PrintAccountNumberTest
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

                Assert.AreEqual<string>("123456789", accountNumber.DisplayValue);
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

                Assert.AreEqual<string>("111111111", accountNumber.DisplayValue);
            }

        }
    }
}
