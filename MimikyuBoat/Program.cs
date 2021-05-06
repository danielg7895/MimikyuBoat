using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Shizui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            try
            {
            } catch (Exception e)
            {
                File.AppendAllText("log.txt", "StackTrace: " + e.StackTrace + Environment.NewLine);
                File.AppendAllText("log.txt", "Message: " + e.Message + Environment.NewLine);
                File.AppendAllText("log.txt", "Source: " + e.Source + Environment.NewLine);
                File.AppendAllText("log.txt", "InnerException: " + e.InnerException + Environment.NewLine);
                File.AppendAllText("log.txt", "Data: " + e.Data + Environment.NewLine);
                MessageBox.Show("Ocurrio un error, guardado en el log.");
            }
        }

        
    }
}
