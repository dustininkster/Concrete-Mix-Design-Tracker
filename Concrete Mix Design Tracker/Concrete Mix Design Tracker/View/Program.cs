﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engineering_Database_View;

namespace Concrete_Mix_Design_Tracker
{
    static class Program
    {
        public static MainForm main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main = new MainForm();
            Application.Run(main);
        }
    }
}
