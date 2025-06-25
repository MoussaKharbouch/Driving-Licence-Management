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

    public partial class frmAddEditUser : Form
    {

        //Fill user function
        public enum enMode { Add, Edit }
        public enMode Mode { get; private set; }

        public clsUser user { get; private set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmAddEditUser()
        {

            InitializeComponent();

            Mode = enMode.Add;

            user = new clsUser();

        }

        public frmAddEditUser(int UserID)
        {

            InitializeComponent();

            lblMode.Text = "Update User";

            Mode = enMode.Edit;

            user = clsUser.FindUser(UserID);
            ShowInformation(UserID);

        }

        private void ShowInformation(int UserID)
        {

            user = clsUser.FindUser(UserID);

            if (user == null)
            {

                MessageBox.Show("This user is unavailable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ctrlPersonCardWithFilter1.RefreshPerson(user.PersonID);

            lblUserID.Text = user.UserID.ToString();
            tbUsername.Text = user.Username;
            tbPassword.Text = user.Password;
            tbConfirmPassword.Text = user.Password;

            cbIsActive.Checked = user.IsActive;

        }

        private void FillUser()
        {

            if (user != null)
            {

                user.PersonID = ctrlPersonCardWithFilter1.PersonID;

                user.Username = tbUsername.Text;
                user.Password = tbPassword.Text;

                user.IsActive = cbIsActive.Checked;

            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (clsUser.DoesPersonUse(ctrlPersonCardWithFilter1.PersonID))
            {

                MessageBox.Show("This Person is already used. Please select another person!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            tabControl1.SelectTab(1);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void CheckEmptyFields(object sender, CancelEventArgs e)
        {

            TextBox tbSender = (TextBox)sender;

            Utils.UI.ShowErrorProvider(tbSender.Tag == null && string.IsNullOrEmpty(tbSender.Text), "You have to complete this field!", (Control)tbSender, epEmptyFields, e);
            

        }

        private bool ValidateInformation()
        {

            bool isPasswordConfirmed = tbConfirmPassword.Text == tbPassword.Text;

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {

                MessageBox.Show("No person is selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (Mode == enMode.Add)
            {

                if (clsUser.DoesUsernameExist(tbUsername.Text))
                {

                    MessageBox.Show("This username is already used!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                if (clsUser.DoesPersonUse(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("This Person is already used. Please select another person!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }

            }
            else
            {

                if (clsUser.DoesUsernameExist(tbUsername.Text) && !string.Equals(user.Username, tbUsername.Text, StringComparison.OrdinalIgnoreCase))
                {

                    MessageBox.Show("This username is already used!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                if (clsUser.DoesPersonUse(ctrlPersonCardWithFilter1.PersonID) && user.PersonID != ctrlPersonCardWithFilter1.PersonID)
                {

                    MessageBox.Show("This Person is already used. Please select another person!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }

            }
            
            if (tbUsername.Text == string.Empty || tbPassword.Text == string.Empty || tbConfirmPassword.Text == string.Empty)
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

        private void ChangeMode()
        {

            if (Mode == enMode.Add)
            {

                Mode = enMode.Edit;
                lblMode.Text = "Update User";
                lblUserID.Text = user.UserID.ToString();

            }
            else
                this.Close();

        }

        private bool SaveItem(clsUser User)
        {

            if (!ValidateInformation())
                return false;

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {

                MessageBox.Show("No person is selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            FillUser();

            if (!user.Save())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (SaveItem(user))
                ChangeMode();

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (ctrlPersonCardWithFilter1.PersonID != -1)
                e.Cancel = false;
            else
            {

                e.Cancel = true;
                MessageBox.Show("No person is selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            CheckEmptyFields(sender, e);

            bool isPasswordConfirmed = tbConfirmPassword.Text == tbPassword.Text;

            Utils.UI.ShowErrorProvider(!isPasswordConfirmed, "Mismatch between password and confirmation!", tbConfirmPassword, epConfirmPassword);

        }

        private void tbUsername_Validating(object sender, CancelEventArgs e)
        {

            CheckEmptyFields(sender, e);

            bool isUsernameUsed = false;

            if (Mode == enMode.Add)
                isUsernameUsed = clsUser.DoesUsernameExist(tbUsername.Text);
            else
                isUsernameUsed = clsUser.DoesUsernameExist(tbUsername.Text) &&
                                 !string.Equals(user.Username, tbUsername.Text, StringComparison.OrdinalIgnoreCase);

            Utils.UI.ShowErrorProvider(isUsernameUsed, "This username is already used!", tbUsername, epConfirmPassword);

        }

    }

}
