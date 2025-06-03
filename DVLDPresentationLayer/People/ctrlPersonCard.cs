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

namespace DVLDPresentationLayer.People
{

    public partial class ctrlPersonCard : UserControl
    {

        private Person person = null;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPerson(int PersonID)
        {

            person = Person.FindPerson(PersonID);

        }

        public void ShowImage()
        {

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

            if (person.ImagePath == string.Empty)
            {

                switch (person.Gender)
                {

                    case Person.enGender.Male:
                        pbProfileImage.Image = Properties.Resources.Male_512;
                        break;
                    case Person.enGender.Female:
                        pbProfileImage.Image = Properties.Resources.Female_512;
                        break;
                    default:
                        break;

                }

            }

        }

        private void ClearInformations()
        {

            ShowImage();
            lblPersonID.Text = person.PersonID.ToString();
            lblName.Text = person.FullName();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = (person.Gender == Person.enGender.Male ? "Male" : "Female");
            lblEmail.Text = person.Email;
            lblAddress.Text = person.Address;
            lblDateOfBirth.Text = person.DateOfBirth.ToString();
            lblPhone.Text = person.Phone;
            lblCountry.Text = Country.FindCountry(person.NationalityCountryID).CountryName;

        }

        public void RefreshInformations()
        {

            if (person == null)
                return;

            LoadPerson(person.PersonID);

            ShowImage();
            lblPersonID.Text = person.PersonID.ToString();
            lblName.Text = person.FullName();
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = (person.Gender == Person.enGender.Male ? "Male" : "Female");
            lblEmail.Text = person.Email;
            lblAddress.Text = person.Address;
            lblDateOfBirth.Text = person.DateOfBirth.ToString();
            lblPhone.Text = person.Phone;
            lblCountry.Text = Country.FindCountry(person.NationalityCountryID).CountryName;

            if (person.Gender == Person.enGender.Male)
                pbGender.Image = Properties.Resources.Man_32;
            else
                pbGender.Image = Properties.Resources.Woman_32;

        }

        public void Initialize(int PersonID)
        {

            LoadPerson(PersonID);
            RefreshInformations();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (person != null)
            {

                if (person.PersonID != -1)
                {

                    frmAddEditPerson EditPerson = new frmAddEditPerson(person.PersonID);
                    EditPerson.OnSaveEventHandler += RefreshInformations;

                    EditPerson.ShowDialog();

                }

            }
            else
            {

                MessageBox.Show("This person is not availabel!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

            RefreshInformations();

        }

    }

}
