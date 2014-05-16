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
            RunOnFile("allones.txt", "111111111");
        }

        private static void RunOnFile(string fileName, string expectedOutput)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "BankOcrKata.exe",
                Arguments = fileName,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (Process ocrDataProcessor = new Process() { StartInfo = startInfo })
            {
                ocrDataProcessor.Start();
                var output = ocrDataProcessor.StandardOutput.ReadToEnd();
                if (output != expectedOutput)
                    throw new InvalidOperationException(string.Format("Output was not a match for file {0}", fileName));

                ocrDataProcessor.WaitForExit();
            }
        }
    }
}
