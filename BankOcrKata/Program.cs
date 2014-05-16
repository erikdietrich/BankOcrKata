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
        static void Main(string[] args)
        {
            if (args.Length != 1)
                DieWithMessage("Invalid usage.  Usage: BankOcrKata.exe {filename}");

            try
            {
                var accountNumbers = RetrieveAccountNumbers(args[0]);
                accountNumbers.ForEach(an => Console.WriteLine(an.DisplayValue));
            }
            catch (BadAccountNumberFormatException)
            {
                DieWithMessage("Invalid account number file format.");
            }
            catch (InvalidAccountNumberFileException)
            {
                DieWithMessage("Invalid account number file format.");
            }
        }

        private static List<PrintedAccountNumber> RetrieveAccountNumbers(string filePath)
        {
            var repository = new AccountNumberRepository(new BasicFile(filePath));
            return repository.GetAccountNumbers().ToList();
        }

        private static void DieWithMessage(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(1);
        }
    }
}
