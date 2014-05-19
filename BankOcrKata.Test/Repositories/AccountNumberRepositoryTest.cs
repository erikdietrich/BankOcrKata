using BankOcrKata.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using System.Linq;
using System.IO;
using BankOcrKata.Exceptions;
using System.Collections.Generic;
using BankOcrKata.Repositories;

namespace BankOcrKata.Test.Repositories
{
    [TestClass]
    public class AccountNumberRepositoryTest
    {
        private IFile File { get; set; }

        private static readonly string[] FileLinesForAllOnes = new string[]
        {
            "                           ", 
            "  |  |  |  |  |  |  |  |  |", 
            "  |  |  |  |  |  |  |  |  |", 
            "                           " 
        };

        private AccountNumberRepository Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            File = Mock.Create<IFile>();
            File.Arrange(f => f.ExistsOnDisk()).Returns(true);
            File.Arrange(f => f.ReadAllLines()).Returns(FileLinesForAllOnes);

            Target = new AccountNumberRepository(File);
        }

        [TestClass]
        public class Constructor : AccountNumberRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_If_File_Exists_Returns_False()
            {
                File.Arrange(f => f.ExistsOnDisk()).Returns(false);

                ExtendedAssert.Throws<FileNotFoundException>(() => new AccountNumberRepository(File));
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_When_File_Has_One_Line()
            {
                File.Arrange(f => f.ReadAllLines()).Returns(new string[] { "                           " });
                ExtendedAssert.Throws<InvalidAccountNumberFileException>(() => new AccountNumberRepository(File));
            }
        }

        [TestClass]
        public class GetAccountNumbers : AccountNumberRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Account_Number_Of_All_Ones_And_Err_For_Four_Line_File_Of_All_Ones()
            {
                var accountNumbers = Target.GetAccountNumbers();

                Assert.AreEqual<string>("111111111 ERR", accountNumbers.First().DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Two_Account_Numbers_For_8_Lines_With_Two_Numbers_With_Output_For_Second_Matching()
            {
                var fileLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                    "                           ",
                    " _  _  _  _  _  _  _  _  _ ",
                    " _| _| _| _| _| _| _| _| _|",
                    "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
                    "                           "
                };

                File.Arrange(f => f.ReadAllLines()).Returns(fileLines);

                Target = new AccountNumberRepository(File);

                var accountNumbers = Target.GetAccountNumbers();

                Assert.AreEqual<string>("222222222 ERR", accountNumbers.Skip(1).First().DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Throws_Exception_When_Spacing_Line_Is_Wrong_Length()
            {
                var fileLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                    "                          ",
                    " _  _  _  _  _  _  _  _  _ ",
                    " _| _| _| _| _| _| _| _| _|",
                    "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
                    "                           "
                };

                File.Arrange(f => f.ReadAllLines()).Returns(fileLines);

                ExtendedAssert.Throws<InvalidAccountNumberFileException>(() => Target = new AccountNumberRepository(File));
            }
        }
    }
}
