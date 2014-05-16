using BankOcrKata.Exceptions;
using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Repositories
{
    public class AccountNumberRepository
    {
        private readonly string[] _fileLines;

        public AccountNumberRepository(IFile file)
        {
            if (!file.ExistsOnDisk())
                throw new FileNotFoundException();

            _fileLines = file.ReadAllLines();

            if (_fileLines.Length != 4)
                throw new InvalidAccountNumberFileException();
        }

        public IEnumerable<PrintedAccountNumber> GetAccountNumbers()
        {
            for (int index = 0; index < _fileLines.Length; index += 4)
                yield return new PrintedAccountNumber(_fileLines.Skip(index).Take(3).ToArray());
        }
    }
}
