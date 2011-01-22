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
            WeakReference _ordRef = null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();

            _ordRef = new WeakReference(form._customers[0]);
            Application.Run(form);

            form.Dispose();
            form = null;
            GC.Collect();
            Console.WriteLine("El objeto referenciado sigue vivo: {0}", _ordRef.IsAlive);

        }
    }
}