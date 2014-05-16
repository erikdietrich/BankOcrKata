using BankOcrKata.DataAccess;
using BankOcrKata.Exceptions;
using BankOcrKata.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    class Program
    {
        private const string DefaultFormatErrorMessage = "Invalid account number file format.";

        static void Main(string[] args)
        {
            if (args.Length != 1)
                DieWithMessage("Invalid usage.  Usage: BankOcrKata.exe {filename}");

            WriteAccountNumbersInFileToConsole(args[0]);
        }

        private static void WriteAccountNumbersInFileToConsole(string filePath)
        {
            try
            {
                RetrieveAccountNumbers(filePath).ForEach(accountNumber => Console.WriteLine(accountNumber.DisplayValue));
            }
            catch (BadAccountNumberFormatException)
            {
                DieWithMessage();
            }
            catch (InvalidAccountNumberFileException)
            {
                DieWithMessage();
            }
            catch (BadDigitFormatException)
            {
                DieWithMessage();
            }
        }

        private static List<PrintedAccountNumber> RetrieveAccountNumbers(string filePath)
        {
            var repository = new AccountNumberRepository(new BasicFile(filePath));
            return repository.GetAccountNumbers().ToList();
        }

        private static void DieWithMessage(string errorMessage = DefaultFormatErrorMessage)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(1);
        }
    }
}
