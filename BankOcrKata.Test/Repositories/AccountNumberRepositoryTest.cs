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

        [TestInitialize]
        public void BeforeEachTest()
        {
            File = Mock.Create<IFile>();
            File.Arrange(f => f.ExistsOnDisk()).Returns(true);
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
                ExtendedAssert.Throws<InvalidAccountNumberFileException>(() => new AccountNumberRepository(File));
            }
        }

        [TestClass]
        public class GetAccountNumbers : AccountNumberRepositoryTest
        {
            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Account_Number_Of_All_Ones_For_Four_Line_File_Of_All_Ones()
            {
                var fileLines = new string[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                    string.Empty
                };

                File.Arrange(f => f.ReadAllLines()).Returns(fileLines);

                var repository = new AccountNumberRepository(File);

                var accountNumbers = repository.GetAccountNumbers();

                Assert.AreEqual<string>("111111111", accountNumbers.First().DisplayValue);
            }

            [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
            public void Returns_Two_Account_Numbers_For_8_Lines_With_Two_Numbers()
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

                var repository = new AccountNumberRepository(File);

                var accountNumbers = repository.GetAccountNumbers();

                Assert.AreEqual<string>("111111111", accountNumbers.First().DisplayValue);
            }
        }
    }
}
