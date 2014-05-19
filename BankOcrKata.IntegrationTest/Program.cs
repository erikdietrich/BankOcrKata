using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOcrKata.IntegrationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyNormalScenarios();
            VerifyErrorScenarios();
        }

        private static void VerifyNormalScenarios()
        {
            VerifyFileOrThrow("allones.txt", "111111111 ERR\r\n");
            VerifyFileOrThrow("alltwos.txt", "222222222 ERR\r\n");
            VerifyFileOrThrow("TwoRows.txt", "111111111 ERR\r\n222222222 ERR\r\n");
            VerifyFileOrThrow("SomeMiscellaneousNumbers.txt", "024685555 ERR\r\n123456789\r\n");
            VerifyFileOrThrow("ZerosThroughNines.txt", GenerateZeroesThroughNines());
            VerifyFileOrThrow("NormalWorkload.txt", string.Join(string.Empty, Enumerable.Repeat(GenerateZeroesThroughNines(), 50)));
            VerifyFileOrThrow("10xWorkload.txt", string.Join(string.Empty, Enumerable.Repeat(GenerateZeroesThroughNines(), 500)));
            VerifyFileOrThrow("OneIllegible.txt", "11111111? ILL\r\n");
            VerifyFileOrThrow("UserStoryThreeExample.txt", "457508000\r\n664371495 ERR\r\n86110??36 ILL\r\n");
            VerifyFileOrThrow("SecondUserStory3Example.txt", "000000051\r\n49006771? ILL\r\n1234?678? ILL\r\n");

            VerifyFileWithCorrectionOrThrow("BasicCorrections.txt", "711111111\r\n777777177\r\n200800000\r\n333393333\r\n");
            VerifyFileWithCorrectionOrThrow("AllCorrections.txt", "711111111\r\n777777177\r\n200800000\r\n333393333\r\n888888888 AMB ['888888880', '888886888', '888888988']\r\n555555555 AMB ['559555555', '555655555']\r\n666666666 AMB ['686666666', '666566666']\r\n999999999 AMB ['899999999', '993999999', '999959999']\r\n490067715 AMB ['490867715', '490067115', '490067719']\r\n123456789\r\n000000051\r\n490867715\r\n");
        }

        private static void VerifyErrorScenarios()
        {
            Verify_That_Wrong_Number_Of_Arguments_Results_In_Usage_Statement();
            VerifyFileOrThrow("OneColumnTooWide.txt", "Invalid account number file format.\r\n");
            VerifyFileOrThrow("WrongRowCount.txt", "Invalid account number file format.\r\n");
            VerifyFileOrThrow("WrongCharacterCountOnFourthLine.txt", "Invalid account number file format.\r\n");
            VerifyFileOrThrow("AccountNumberWithIllegalCharacter.txt", "Invalid account number file format.\r\n");
        }

        public static void Verify_That_Wrong_Number_Of_Arguments_Results_In_Usage_Statement()
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "BankOcrKata.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            ExecuteProcessAndCheckForExpectedOutput(startInfo, "Invalid usage.  Usage: BankOcrKata.exe {filename} {/c -- optional parameter to auto-correct when applicable}\r\n");
        }

        private static void VerifyFileWithCorrectionOrThrow(string filename, string expectedOutput)
        {
            VerifyFileOrThrow(filename, expectedOutput, " /c");
        }

        private static void VerifyFileOrThrow(string fileName, string expectedOutput, string autoCorrect = "")
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "BankOcrKata.exe",
                Arguments = fileName + autoCorrect,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            try
            {
                ExecuteProcessAndCheckForExpectedOutput(startInfo, expectedOutput);
            }
            catch (InvalidOperationException ioe)
            {
                throw new InvalidOperationException(string.Format("Did not work for file {0}", fileName), ioe);
            }
        }

        private static void ExecuteProcessAndCheckForExpectedOutput(ProcessStartInfo startInfo, string expectedOutput)
        {
            using (var ocrDataProcessor = new Process() { StartInfo = startInfo })
            {
                ocrDataProcessor.Start();
                var output = ocrDataProcessor.StandardOutput.ReadToEnd();
                if (output != expectedOutput)
                    throw new InvalidOperationException();

                ocrDataProcessor.WaitForExit();
            }
        }
        private static string GenerateZeroesThroughNines()
        {
            return string.Join(string.Empty, Enumerable.Range(0, 10).Select(i => GenerateRepeatingIntegerLine(i)));
        }
        private static string GenerateRepeatingIntegerLine(int digit)
        {
            var numbers = string.Join(string.Empty, Enumerable.Repeat(digit.ToString(), PrintedAccountNumber.AccountNumberWidth));
            return string.Format("{0}{1}\r\n", numbers, digit != 0 ? " ERR" : string.Empty);
        }
    }
}
