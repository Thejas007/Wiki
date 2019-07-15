using System;

namespace ExitCodeCheck
{
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Debug\CertificateUploader.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
           
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    Console.WriteLine(exeProcess.ExitCode);
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
