using BankOcrKata.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata
{
    public class AccountNumberCorrector : IAccountNumberCorrector
    {
        public IEnumerable<PrintedAccountNumber> GetPossibleCorrections(PrintedAccountNumber target)
        {
            var accountNumbersFromAddingUnderscore = GetAccountNumbersBySwapping(target.RawLines, ' ', '_');
            var accountNumbersFromRemovingUnderscore = GetAccountNumbersBySwapping(target.RawLines, '_', ' ');
            var accountNumbersForAddingPipe = GetAccountNumbersBySwapping(target.RawLines, ' ', '|');
            var accountNumbersForRemovingPipe = GetAccountNumbersBySwapping(target.RawLines, '|', ' ');

            return accountNumbersFromAddingUnderscore.Union(accountNumbersFromRemovingUnderscore).Union(accountNumbersForAddingPipe).Union(accountNumbersForRemovingPipe);
        }

        public string FormatPossibleCorrections(PrintedAccountNumber target)
        {
            var corrections = GetPossibleCorrections(target);
            if(corrections.Count() == 0)
                return target.DisplayValue;
            else if(corrections.Count() == 1)
                return corrections.First().DisplayValue;

            return string.Format("{0} AMB [{1}]", target.DisplayValue.Substring(0, PrintedAccountNumber.AccountNumberWidth), string.Join(", ", corrections.Select(c => string.Format("'{0}'", c.DisplayValue))));
        }

        private static IEnumerable<PrintedAccountNumber> GetAccountNumbersBySwapping(string[] rawLines, char characterToGetRidOf, char characterToTry)
        {
            var possibleAlternativesForLineOneSwap = GetProspectsByReplacingLine(rawLines, 0, characterToGetRidOf, characterToTry);
            var possibleAlternativesForLineTwoSwap = GetProspectsByReplacingLine(rawLines, 1, characterToGetRidOf, characterToTry);
            var possibleAlternativesForLineThreeSwap = GetProspectsByReplacingLine(rawLines, 2, characterToGetRidOf, characterToTry);

            return (possibleAlternativesForLineOneSwap.Union(possibleAlternativesForLineTwoSwap).Union(possibleAlternativesForLineThreeSwap)).Where(p => p.IsValid);
        }

        private static IEnumerable<PrintedAccountNumber> GetProspectsByReplacingLine(string[] rawLines, int lineNumber, char characterToGetRidOf, char characterToTry)
        {
            var accountNumberLines = new string[rawLines.Length];
            Array.Copy(rawLines, accountNumberLines, rawLines.Length);

            var alternatives = GetAlternativeStringsForLine(rawLines[lineNumber], characterToGetRidOf, characterToTry);
            foreach (var alternative in alternatives)
            {
                accountNumberLines[lineNumber] = alternative;
                yield return new PrintedAccountNumber(accountNumberLines);
            }
        }

        private static IEnumerable<string> GetAlternativeStringsForLine(string line, char characterToGetRidOf, char newCharacter)
        {
            foreach (int columnIndex in line.ToCharArray().Select((c, i) => c == characterToGetRidOf ? i : -1).Where(i => i >= 0))
                yield return line.Remove(columnIndex, 1).Insert(columnIndex, newCharacter.ToString());
        }
    }
}
