using BankOcrKata.Domain;
using BankOcrKata.Exceptions;
using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankOcrKata.Repositories
{
    public class AccountNumberRepository
    {
        private const int AccountNumberEntrySize = 4;
        private readonly string[] _fileLines;

        public AccountNumberRepository(IFile file)
        {
            if (!file.ExistsOnDisk())
                throw new FileNotFoundException();

            _fileLines = file.ReadAllLines();

            if (!AreFileLinesValid())
                throw new InvalidAccountNumberFileException();
        }

        public IEnumerable<PrintedAccountNumber> GetAccountNumbers()
        {
            for (var index = 0; index < _fileLines.Length; index += AccountNumberEntrySize)
                yield return new PrintedAccountNumber(_fileLines.Skip(index).Take(3).ToArray());
        }

        private bool AreFileLinesValid()
        {
            return _fileLines.Length > 0 && _fileLines.Length % AccountNumberEntrySize == 0 && 
                _fileLines.All(fl => fl.Length == PrintedAccountNumber.AccountNumberWidth * PrintedDigit.DigitWidth);
        }
    }
}
