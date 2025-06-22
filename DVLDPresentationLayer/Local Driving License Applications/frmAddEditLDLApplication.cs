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

namespace DVLDPresentationLayer.Local_Driving_License_Applications
{
    public partial class frmAddEditLDLApplication : Form
    {

        public enum enStatus { New = 1, Canceled = 2, Completed = 3 }

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
        private clsApplication Application;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmAddEditLDLApplication()
        {

            InitializeComponent();

            LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            Application = new clsApplication();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            tabControl1.SelectTab(1);

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

        private void frmAddEditLDLApplication_Load(object sender, EventArgs e)
        {

            foreach (DataRow row in clsLicenseClass.GetLicenseClasses().Rows)
            {

                cbLicenseClass.Items.Add(row["ClassName"]);

            }

            cbLicenseClass.SelectedIndex = 0;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = ((int)clsApplicationType.FindApplicationType(1).ApplicationFees).ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void FillLDLApplication()
        {

            if (LocalDrivingLicenseApplication != null)
            {

                LocalDrivingLicenseApplication.ApplicationID = Application.ApplicationID;

                clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClass(cbLicenseClass.SelectedItem.ToString());

                if(LicenseClass != null)
                    LocalDrivingLicenseApplication.LicenseClassID = LicenseClass.LicenseClassID;
                else
                    MessageBox.Show("Invalid License Class!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        
        private void FillApplication()
        {

            if (Application != null)
            {

                Application.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = 1;
                Application.ApplicationStatus = clsApplication.enStatus.New;
                Application.LastStatusDate = DateTime.Now;

                clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(1);

                if (ApplicationType != null)
                    Application.PaidFees = ApplicationType.ApplicationFees;

                if (Global.user != null)
                    Application.CreatedByUserID = Global.user.UserID;
                else
                    MessageBox.Show("No user is logged in. You cannot do this action!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private bool ValidateInformation()
        {

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {

                MessageBox.Show("No person is selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            clsLicenseClass LicenseClass = clsLicenseClass.FindLicenseClass(cbLicenseClass.SelectedItem.ToString());

            if (LicenseClass != null)
            {

                if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveLocalLicenseInSameClass(ctrlPersonCardWithFilter1.PersonID, LicenseClass.LicenseClassID, 1))
                {

                    MessageBox.Show("This Person is already Have a new local driving license application with same license class. Please select another license class!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }

            }
            else
                MessageBox.Show("Invalid License Class!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return true;

        }

        private bool SaveItem(clsLocalDrivingLicenseApplication LDLApplication)
        {

            if (!ValidateInformation())
                return false;

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {

                MessageBox.Show("No person is selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            FillApplication();
            Application.Save();

            FillLDLApplication();

            if (!LDLApplication.Save())
            {

                MessageBox.Show("Data has not been saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            MessageBox.Show("Data has been saved successfully.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!SaveItem(LocalDrivingLicenseApplication))
                return;

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

    }

}
