using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.Types
{
    public interface IFile
    {
        bool ExistsOnDisk();
        string[] ReadAllLines();
    }
}
