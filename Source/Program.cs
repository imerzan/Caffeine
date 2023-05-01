using System;
using System.Threading;
using System.Windows.Forms;

namespace Caffeine
{
    static class Program
    {
        private static readonly Mutex _mutex;

        static Program()
        {
            // Allow only one program instance to run
            _mutex = new Mutex(true, "EB06A900-686A-45A0-B2EE-30B8A8A0981A", out bool createdNew);
            if (!createdNew)
                throw new ApplicationException("Caffeine is already running!");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
