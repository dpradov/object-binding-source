using System;
using System.Collections.Generic;
using System.Windows.Forms;

static class Program
{
    static public MainForm FrmMain; 

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        FrmMain = new MainForm();

        Application.Run(FrmMain);

        FrmMain.Dispose();
        FrmMain = null;
        GC.Collect();
    }
}
