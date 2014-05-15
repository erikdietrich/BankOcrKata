using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    public class PrintedAccountNumber
    {
        public string DisplayValue { get; private set; }

        public PrintedAccountNumber(string[] accountNumberLines)
        {
            if (accountNumberLines.Length != 3)
                throw new BadAccountNumberFormatException();

            var printDigits = new List<PrintedDigit>();
            for (int index = 0; index < 9; index++)
            {
                var lines = new string[]
                {
                    accountNumberLines[0].Substring(index * 3, 3),
                    accountNumberLines[1].Substring(index * 3, 3),
                    accountNumberLines[2].Substring(index * 3, 3)
                };
                printDigits.Add(new PrintedDigit(lines));
            }
            DisplayValue = String.Join(string.Empty, printDigits.Select(pd => pd.IntegerValue));
        }
    }
}
