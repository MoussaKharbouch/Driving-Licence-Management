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

        public enum enMode { Add, Edit }
        public enMode Mode { get; private set; }

        User user;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public delegate void OnSaveReturningUserID(int UserID);
        public event OnSaveReturningUserID OnSaveReturningUserIDEventHandler;

        public frmAddEditUser()
        {

            InitializeComponent();

        }

        public frmAddEditUser(int UserID)
        {

            InitializeComponent();

            user = User.FindUser(UserID);

            if (user != null)
                ctrlPersonCardWithFilter1.RefreshPerson(user.PersonID);
            else
                MessageBox.Show("This user is inavailable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            

        }

        private void ShowInformation(User user)
        {

            if (user == null)
                MessageBox.Show("This user is inavailable!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ctrlPersonCardWithFilter1.RefreshPerson(user.PersonID);

            tbUsername.Text = user.Username;
            tbPassword.Text = user.Password;
            tbConfirmPassword.Text = user.Password;

        }

        private void FillUser()
        {

            user.PersonID = ctrlPersonCardWithFilter1.PersonID;

            user.Username = tbUsername.Text;
            user.Password = tbPassword.Text;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            tpLoginInfo.Show();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            FillUser();

            if (!user.Save())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            if (OnSaveReturningUserIDEventHandler != null)
                OnSaveReturningUserIDEventHandler(user.PersonID);

        }

    }

}
