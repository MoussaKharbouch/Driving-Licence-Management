using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.People;

namespace DVLDPresentationLayer.Controls
{

    public partial class ctrlApplicationBasicInfo : UserControl
    {

        public clsApplication Application { get; private set; }

        public ctrlApplicationBasicInfo()
        {

            InitializeComponent();

        }

        public ctrlApplicationBasicInfo(int ApplicationID = -1)
        {

            InitializeComponent();

            Refresh(ApplicationID);
            
        }

        public void LoadApplication(int ApplicationID)
        {

            Application = clsApplication.FindApplication(ApplicationID);

        }

        public void ShowItem(clsApplication Application)
        {

            if (Application == null)
                return;

            lblID.Text = Application.ApplicationID.ToString();
            lblStatus.Text = Application.ApplicationStatus.ToString();
            lblFees.Text = Application.PaidFees.ToString();

            clsApplicationType ApplicationType = clsApplicationType.FindApplicationType(Application.ApplicationTypeID);

            if (ApplicationType != null)
                lblType.Text = ApplicationType.ApplicationTypeTitle;

            clsPerson Applicant = clsPerson.FindPerson(Application.ApplicantPersonID);

            if (Applicant != null)
                lblApplicant.Text = Applicant.FullName();

            lblDate.Text = Application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = Application.LastStatusDate.ToShortDateString();

            if (Global.user != null)
                lblCreatedBy.Text = Global.user.Username;

        }

        public void RefreshInformation()
        {

            if (Application == null)
            {

                MessageBox.Show("Application data not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            Application = clsApplication.FindApplication(Application.ApplicationID);

            if (Application == null)
            {

                MessageBox.Show("Application data not found on refresh!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ShowItem(Application);

        }

        public void Refresh(int ApplicationID)
        {

            if (ApplicationID != -1)
            {

                LoadApplication(ApplicationID);
                RefreshInformation();

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (Application != null)
            {

                frmShowPersonDetails ShowPersonDetails = new frmShowPersonDetails(Application.ApplicantPersonID);
                ShowPersonDetails.OnSaveEventHandler += RefreshInformation;

                ShowPersonDetails.ShowDialog();

            }
            else
            {

                MessageBox.Show("This Application is not available!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

            if(Application != null)
                RefreshInformation();

        }

    }

}
