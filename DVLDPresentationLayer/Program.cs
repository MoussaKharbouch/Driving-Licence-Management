﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
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

            //This is the right start (application starts from login from)         

            frmLogin Login;
            frmMain Main = new frmMain(null);

            do
            {

                Global.user = null;

                Login = new frmLogin();
                DialogResult succeeded = Login.ShowDialog();

                if (succeeded == DialogResult.OK)
                {

                    Main = new frmMain(Login.user);
                    Main.ShowDialog();

                }
                else if (succeeded == DialogResult.Cancel)
                    break;

                } while ((Main.DialogResult == DialogResult.Retry));

            //If i want run main from to test features
            //Application.Run(new frmMain(null));

        }

    }

}
