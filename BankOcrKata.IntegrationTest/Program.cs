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
            VerifyFileOrThrow("allones.txt", "111111111\r\n");
            VerifyFileOrThrow("alltwos.txt", "222222222\r\n");
            VerifyFileOrThrow("TwoRows.txt", "111111111\r\n222222222\r\n");
            VerifyFileOrThrow("SomeMiscellaneousNumbers.txt", "024685555\r\n123456789\r\n");
            VerifyFileOrThrow("ZerosThroughNines.txt", GenerateZeroesThroughNines());
            VerifyFileOrThrow("NormalWorkload.txt", string.Join(string.Empty, Enumerable.Repeat(GenerateZeroesThroughNines(), 50)));
            VerifyFileOrThrow("10xWorkload.txt", string.Join(string.Empty, Enumerable.Repeat(GenerateZeroesThroughNines(), 500)));
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

            ExecuteProcessAndCheckForExpectedOutput(startInfo, "Invalid usage.  Usage: BankOcrKata.exe {filename}\r\n");
        }

        private static void VerifyFileOrThrow(string fileName, string expectedOutput)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "BankOcrKata.exe",
                Arguments = fileName,
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
            string numbers = string.Join(string.Empty, Enumerable.Repeat(digit.ToString(), PrintedAccountNumber.AccountNumberWidth));
            return string.Format("{0}\r\n", numbers);
        }
    }
}
