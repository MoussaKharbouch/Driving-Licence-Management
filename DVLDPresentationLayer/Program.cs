using System;
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
            /*frmLogin Login = new frmLogin();

            if (Login.ShowDialog() == DialogResult.OK)
            {

                Application.Run(new frmMain(Login.user));

            }*/

            //If i want run main from to test features
            Application.Run(new frmMain(new User()));

        }

    }

}
