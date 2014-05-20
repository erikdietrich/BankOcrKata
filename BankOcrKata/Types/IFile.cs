using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcrKata.Types
{
    public interface IFile
    {
        bool ExistsOnDisk();
        string[] ReadAllLines();
    }
}
