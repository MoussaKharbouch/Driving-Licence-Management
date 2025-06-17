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

namespace DVLDPresentationLayer.Test_Types
{

    public partial class frmUpdateTestType : Form
    {

        public clsTestType TestType { get; private set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmUpdateTestType(int TestTypeID)
        {

            InitializeComponent();
            ShowInformation(TestTypeID);

        }

        private void ShowInformation(int TestTypeID)
        {

            TestType = clsTestType.FindTestType(TestTypeID);

            if (TestType == null)
            {

                MessageBox.Show("This test type is inavailable!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            lblTestTypeID.Text = TestTypeID.ToString();

            tbTitle.Text = TestType.TestTypeTitle;
            tbDescription.Text = TestType.TestTypeDescription;
            tbFees.Text = TestType.TestTypeFees.ToString();

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

            if (clsTestType.DoesTestTypeTitleExist(tbTitle.Text) && tbTitle.Text != TestType.TestTypeTitle)
            {

                MessageBox.Show("This title is already used!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;

        }

        private bool SaveItem(clsTestType TestType)
        {

            if (!ValidateInformation())
                return false;

            FillTestType();

            if (!TestType.Update())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void FillTestType()
        {

            TestType.TestTypeTitle = tbTitle.Text;
            TestType.TestTypeDescription = tbDescription.Text;
            TestType.TestTypeFees = Convert.ToDecimal(tbFees.Text);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveItem(TestType);

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
