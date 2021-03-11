using System;
using System.Threading;
using System.Windows.Forms;

namespace Caffeine
{
    static class Program
    {
        private static Mutex mutex;
        public const string WindowTitle = "Caffeine";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            mutex = new Mutex(true, "eb06a900-686a-45a0-b2ee-30b8a8a0981a", out createdNew); // Allow only one instance to run
            if (!createdNew)
            {
                MessageBox.Show("Caffeine is already running!", WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Caffeine());
            }
        }
    }
}
