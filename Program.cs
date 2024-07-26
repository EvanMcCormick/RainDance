using Microsoft.Extensions.Logging;
using RainDance.Services.Logging;
using System;
using System.Windows.Forms;

namespace Raindance
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           

            // Initialize the main form
            var mainForm = new Form1();

            // Run the application
            Application.Run(mainForm);
        }
    }
}
