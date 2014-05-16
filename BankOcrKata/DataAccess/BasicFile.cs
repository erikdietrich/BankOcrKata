using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.DataAccess
{
    public class BasicFile : IFile
    {
        private readonly string _path;

        public BasicFile(string path)
        {
            if(path == null)
                throw new ArgumentNullException("path");
            _path = path;
        }

        public bool ExistsOnDisk()
        {
            return File.Exists(_path);
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines(_path);
        }
    }
}
