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

        private Person person;

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPerson(int PersonID)
        {

            person = Person.FindPerson(PersonID);

        }

        //Show person's image in picturebox
        public void RefreshImage()
        {

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

                    case Person.enGender.Male:
                        pbProfileImage.Image = Properties.Resources.Male_512;
                        pbGender.Image = Properties.Resources.Man_32;
                        break;
                    case Person.enGender.Female:
                        pbProfileImage.Image = Properties.Resources.Female_512;
                        pbGender.Image = Properties.Resources.Woman_32;
                        break;
                    default:
                        break;

                }

            }

        }

        private void ShowInformation(Person person)
        {

            if (person == null)
                return;

            RefreshImage();
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

        public void RefreshInformation()
        {

            person = Person.FindPerson(person.PersonID);
            ShowInformation(person);

        }

        public void Initialize(int PersonID)
        {

            LoadPerson(PersonID);
            RefreshInformation();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (person != null)
            {

                frmAddEditPerson EditPerson = new frmAddEditPerson(person.PersonID);
                EditPerson.OnSaveEventHandler += RefreshInformation;

                EditPerson.ShowDialog();

            }
            else
            {

                MessageBox.Show("This person is not availabel!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

            RefreshInformation();

        }

    }

}
