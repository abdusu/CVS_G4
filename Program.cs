using System.Diagnostics;
namespace CVS_G4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            foreach (var process in Process.GetProcessesByName("CVS_G4"))
            {
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    try { process.Kill(); } catch { }
                }
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new addStudent());
        }
    }
}