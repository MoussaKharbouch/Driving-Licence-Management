using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer.Controls
{

    public partial class ctrlDrivingLicenseInfo : UserControl
    {

        public clsLicense DrivingLicense { get; private set; }

        public ctrlDrivingLicenseInfo()
        {
            InitializeComponent();
        }

        public ctrlDrivingLicenseInfo(int DrivingLicenseID = -1)
        {

            InitializeComponent();

            Refresh(DrivingLicenseID);
            
        }

        //Show person's image in picturebox
        public void RefreshImage()
        {

            clsPerson person = DrivingLicense.Driver.Person;
            //Show image if person has image
            if (person.ImagePath != string.Empty)
            {

                if (File.Exists(person.ImagePath))
                {

                    if (pbProfileImage.Image != null)
                        pbProfileImage.Image.Dispose();

                    using (var fs = new FileStream(person.ImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbProfileImage.Image = Image.FromStream(fs);
                    }

                }

            }

            //Show gender if person does not have an image
            if (person.ImagePath == string.Empty)
            {

                switch (person.Gender)
                {

                    case clsPerson.enGender.Male:
                        pbProfileImage.Image = Properties.Resources.Male_512;
                        pbGender.Image = Properties.Resources.Man_32;
                        break;
                    case clsPerson.enGender.Female:
                        pbProfileImage.Image = Properties.Resources.Female_512;
                        pbGender.Image = Properties.Resources.Woman_32;
                        break;
                    default:
                        break;

                }

            }

        }

        public void LoadDrivingLicense(int DrivingLicenseID)
        {

            DrivingLicense = clsLicense.FindLicense(DrivingLicenseID);

        }

        public void ShowItem(clsLicense DrivingLicense)
        {

            if (DrivingLicense == null)
                return;

            RefreshImage();
            lblClass.Text = DrivingLicense.LicenseClass.ClassName;
            lblFullName.Text = DrivingLicense.Driver.Person.FullName();
            lblLicenseID.Text = DrivingLicense.LicenseID.ToString();
            lblNationalNo.Text = DrivingLicense.Driver.Person.NationalNo;
            lblGender.Text = (DrivingLicense.Driver.Person.Gender == clsPerson.enGender.Male ? "Male" : "Female");
            lblIssueDate.Text = DrivingLicense.IssueDate.ToShortDateString();
            lblIssueReason.Text = DrivingLicense.IssueReasonText;
            lblNotes.Text = DrivingLicense.Notes;
            lblIsActive.Text = (DrivingLicense.IsActive ? "Yes" : "No");
            lblDateOfBirth.Text = DrivingLicense.Driver.Person.DateOfBirth.ToShortDateString();
            lblDriverID.Text = DrivingLicense.DriverID.ToString();
            lblExpirationDate.Text = DrivingLicense.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = clsDetainedLicense.IsDetained(DrivingLicense.LicenseID) ? "Yes" : "No";

        }

        public void RefreshInformation()
        {

            if (DrivingLicense == null)
            {

                MessageBox.Show("Driving License data not found on refresh!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            ShowItem(DrivingLicense);

        }

        public void Refresh(int DrivingLicenseID)
        {

            if (DrivingLicenseID != -1)
            {

                LoadDrivingLicense(DrivingLicenseID);
                RefreshInformation();

            }

        }

        private void ctrlDrivingLicenseCard_Load(object sender, EventArgs e)
        {

            if(DrivingLicense != null)
                RefreshInformation();

        }

    }

}
