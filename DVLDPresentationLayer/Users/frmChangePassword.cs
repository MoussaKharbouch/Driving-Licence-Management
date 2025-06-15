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

namespace DVLDPresentationLayer.Users
{

    public partial class frmChangePassword : Form
    {


        private User user;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmChangePassword(int UserID)
        {

            InitializeComponent();

            user = User.FindUser(UserID);

            if (user == null)
            {

                MessageBox.Show("This user is inavalaible!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
            else
                ctrlUserInfoCard1.ShowInformation(user);

        }

        private bool ValidateInformation()
        {

            bool isPasswordConfirmed = tbConfirmPassword.Text == tbNewPassword.Text;

            if (tbCurrentPassword.Text != user.Password)
            {

                MessageBox.Show("Invalid Password!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (string.IsNullOrEmpty(tbCurrentPassword.Text) || string.IsNullOrEmpty(tbConfirmPassword.Text))
            {

                MessageBox.Show("Some fields are not completed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (!isPasswordConfirmed)
            {

                MessageBox.Show("Password and confirmation do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;

        }

        private bool SaveItem(User User)
        {

            if (!ValidateInformation())
            {

                return false;

            }

            user.Password = tbNewPassword.Text;

            if (!user.Save())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void CheckEmptyFields(object sender, CancelEventArgs e)
        {

            TextBox tbSender = (TextBox)sender;

            Utils.UI.ShowErrorProvider(tbSender.Tag == null && string.IsNullOrEmpty(tbSender.Text), "You have to complete this field!", (Control)tbSender, epEmptyFields);


        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            CheckEmptyFields(sender, e);
            Utils.UI.ShowErrorProvider(tbCurrentPassword.Text != user.Password, "Invalid password!", tbCurrentPassword, epCurrentPassword);

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            CheckEmptyFields(sender, e);
            Utils.UI.ShowErrorProvider(tbConfirmPassword.Text != tbNewPassword.Text, "Mismatch between password and confirmation!", tbConfirmPassword, epConfirmPassword);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveItem(user);

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

    }

}
