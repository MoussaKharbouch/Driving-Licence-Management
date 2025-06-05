using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{

    public partial class frmLogin : Form
    {

        public User user;

        public frmLogin()
        {

            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            user = User.FindUser(tbUsername.Text, tbPassword.Text);

            if (user != null && user.IsActive)
            {

                //Make DialogResult eqauls ok so we can check if user logged in successfully
                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            else if (user == null)
                MessageBox.Show("Invalid Username or Password!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!user.IsActive)
                MessageBox.Show("This user account is inactive. Please contact your administrator", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        //Check if textboxes has data
        private void CheckField(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {

                e.Cancel = true;
                errorProvider.SetError(((TextBox)sender), "You have to complete this field!");

            }
            else
            {

                e.Cancel = false;
                errorProvider.SetError(((TextBox)sender), "");

            }

        }

    }

}
