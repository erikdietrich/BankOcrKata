using BankOcrKata.DataAccess;
using BankOcrKata.Exceptions;
using BankOcrKata.Repositories;
using BankOcrKata.Types;
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
            if (!AreCommandLineParamtersValid(args))
                DieWithMessage("Invalid usage.  Usage: BankOcrKata.exe {filename} {/c -- optional parameter to auto-correct when applicable}");

            var corrector = BuildCorrector(args);

            WriteAccountNumbersInFileToConsole(args[0], corrector);
        }

        private static bool AreCommandLineParamtersValid(string[] args)
        {
            return args.Length == 1 || (args.Length == 2 && args[1] == "/c");
        }

        private static IAccountNumberCorrector BuildCorrector(string[] args)
        {
            if (args.Length != 2 || args[1] != "/c")
                return new NullAccountNumberCorrector();

            return new AccountNumberCorrector();
        }

        private static void WriteAccountNumbersInFileToConsole(string filePath, IAccountNumberCorrector corrector)
        {
            try
            {
                RetrieveAccountNumbers(filePath).ForEach(accountNumber => WriteAccountNumber(accountNumber, corrector));
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

        private static void WriteAccountNumber(PrintedAccountNumber accountNumber, IAccountNumberCorrector corrector)
        {
            var formattedAccountNumber = corrector.FormatPossibleCorrections(accountNumber);
            Console.WriteLine(formattedAccountNumber);
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
