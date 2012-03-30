using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ObjectBindingSourceDemo
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
            MainForm form = new MainForm();

            //Note: 
            // If turning on break on exception for ArgumentNullException in the debugger:
            // http://connect.microsoft.com/VisualStudio/feedback/details/552261/bindingsource-object-string-constructor-throws-argumentnullexception-when-object-is-a-source-with-data-in-it
            Application.Run(form);

            form.Dispose();
            form = null;
            GC.Collect();
        }
    }
}