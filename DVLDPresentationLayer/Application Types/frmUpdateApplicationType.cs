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

namespace DVLDPresentationLayer.ApplicationTypes
{

    public partial class frmUpdateApplicationType : Form
    {

        public ApplicationType ApplicationType { get; private set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {

            InitializeComponent();
            ShowInformation(ApplicationTypeID);

        }

        private void ShowInformation(int ApplicationTypeID)
        {

            ApplicationType = ApplicationType.FindApplicationType(ApplicationTypeID);

            if (ApplicationType == null)
            {

                MessageBox.Show("This application type is inavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblApplicationTypeID.Text = ApplicationTypeID.ToString();

            tbTitle.Text = ApplicationType.ApplicationTypeTitle;
            tbFees.Text = ApplicationType.ApplicationFees.ToString();

        }

        private void CheckEmptyFields(object sender, CancelEventArgs e)
        {

            TextBox tbSender = (TextBox)sender;

            Utils.UI.ShowErrorProvider(tbSender.Tag == null && string.IsNullOrEmpty(tbSender.Text), "You have to complete this field!", (Control)tbSender, epEmptyFields, e);

        }

        private bool ValidateInformation()
        {

            if (tbTitle.Text == string.Empty || tbFees.Text == string.Empty)
            {

                MessageBox.Show("Some fields are not completed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            if (ApplicationType.DoesApplicationTypeTitleExist(tbTitle.Text) && tbTitle.Text != ApplicationType.ApplicationTypeTitle)
            {

                MessageBox.Show("This title is already used!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;

        }

        private bool SaveItem(ApplicationType ApplicationType)
        {

            if (!ValidateInformation())
                return false;

            FillApplicationType();

            if (!ApplicationType.Update())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void FillApplicationType()
        {

            ApplicationType.ApplicationTypeTitle = tbTitle.Text;
            ApplicationType.ApplicationFees = Convert.ToDecimal(tbFees.Text);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveItem(ApplicationType);

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
