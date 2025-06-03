using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer.People
{

    public partial class frmAddEditPerson : Form
    {

        enum enMode { Add, Edit }
        enMode Mode;

        Person person;
        string ImagePath = string.Empty;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmAddEditPerson()
        {

            Mode = enMode.Add;
            person = new Person();

            InitializeComponent();

        }

        public frmAddEditPerson(int PersonID)
        {

            Mode = enMode.Edit;
            person = Person.FindPerson(PersonID);

            InitializeComponent();

        }

        public void RefreshImage(string ImagePath, Person.enGender Gender)
        {

            if (ImagePath != string.Empty)
            {

                if (File.Exists(ImagePath))
                {

                    if (pbProfileImage.Image != null)
                        pbProfileImage.Image.Dispose();

                    using (var fs = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbProfileImage.Image = Image.FromStream(fs);
                    }

                }

            }

            if (ImagePath == string.Empty)
            {

                switch (Gender)
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

        private void FillCountries()
        {

            DataTable dtCountriesNames = Country.GetCountriesNames();

            foreach(DataRow drCountry in dtCountriesNames.Rows)
            {

                cbCountry.Items.Add(drCountry["CountryName"].ToString());

            }

        }

        private void ShowGender(Person.enGender gender)
        {

            switch (gender)
            {

                case Person.enGender.Male:
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                    break;
                case Person.enGender.Female:
                    rbMale.Checked = false;
                    rbFemale.Checked = true;
                    break;
                default:
                    break;

            }

        }

        private void ShowInformations(Person person)
        {

            lblPersonID.Text = person.PersonID.ToString();

            tbFirstName.Text = person.FirstName;
            tbSecondName.Text = person.SecondName;
            tbThirdName.Text = person.ThirdName;
            tbLastName.Text = person.LastName;

            tbNationalNo.Text = person.NationalNo;
            ShowGender(person.Gender);

            tbEmail.Text = person.Email;
            tbAddress.Text = person.Address;
            tbPhone.Text = person.Phone;

            FillCountries();
            cbCountry.SelectedIndex = person.NationalityCountryID - 1;

            if (person.DateOfBirth <= dtpBirthDate.MaxDate)
                dtpBirthDate.Value = person.DateOfBirth;
            else
            {

                dtpBirthDate.Value = dtpBirthDate.MaxDate;
                MessageBox.Show("Birth date of this person is invalid (less than 18 years)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            if (person.ImagePath != string.Empty)
                ImagePath = person.ImagePath;

            RefreshImage(person.ImagePath, person.Gender);

        }

        private void FillPerson(string ImagePath)
        {

            person.FirstName = tbFirstName.Text.Trim();
            person.SecondName = tbSecondName.Text.Trim();
            person.ThirdName = tbThirdName.Text.Trim();
            person.LastName = tbLastName.Text.Trim();

            person.NationalNo = tbNationalNo.Text.Trim();
            person.Email = tbEmail.Text.Trim();
            person.Address = tbAddress.Text.Trim();
            person.Phone = tbPhone.Text.Trim();

            person.DateOfBirth = dtpBirthDate.Value;

            person.Gender = (rbMale.Checked ? Person.enGender.Male : Person.enGender.Female);

            person.NationalityCountryID = cbCountry.SelectedIndex + 1;

            person.ImagePath = ImagePath;

        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {

            dtpBirthDate.MaxDate = DateTime.Today.AddYears(-18);
            dtpBirthDate.Value = DateTime.Today.AddYears(-18);

            switch(Mode)
            {

                case enMode.Edit:
                    ShowInformations((person != null) ? person : new Person());
                    lblMode.Text = "Update Person";
                    break;

                case enMode.Add:
                    FillCountries();
                    cbCountry.SelectedIndex = 2;
                    lblMode.Text = "Add New Person";
                    break;

                default:
                    break;

            }

        }

        private void rbGender_CheckedChanged(object sender, EventArgs e)
        {

            RefreshImage(ImagePath, (rbMale.Checked ? Person.enGender.Male : Person.enGender.Female));

        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (ofdImage.ShowDialog() == DialogResult.OK)
                ImagePath = ofdImage.FileName;

            if (ImagePath != string.Empty)
            {

                if (File.Exists(ImagePath))
                {

                    if (pbProfileImage.Image != null)
                        pbProfileImage.Image.Dispose();

                    pbProfileImage.Image = Image.FromFile(ImagePath);

                }

            }

        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (ImagePath != string.Empty)
            {

                ImagePath = string.Empty;
                RefreshImage(ImagePath, (rbMale.Checked ? Person.enGender.Male : Person.enGender.Female));

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void CopyImage(string oldImagePath, string copyImageFolder, string copyImagePath)
        {

            if (File.Exists(oldImagePath))
            {

                if (!Directory.Exists(copyImageFolder))
                    Directory.CreateDirectory(copyImageFolder);

                if (!File.Exists(copyImagePath))
                    File.Copy(oldImagePath, copyImagePath);

            }

        }

        private void DeleteImage(string ImagePath)
        {

            if (File.Exists(ImagePath))
                File.Delete(ImagePath);

        }

        private bool AreAllColumnCompleted()
        {

            return (tbFirstName.Text != string.Empty && tbLastName.Text != string.Empty &&
                    tbNationalNo.Text != string.Empty && tbEmail.Text != string.Empty &&
                    tbAddress.Text != string.Empty && tbPhone.Text != string.Empty);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!AreAllColumnCompleted())
            {

                MessageBox.Show("Some fields are not completed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if ((ImagePath == string.Empty && person.ImagePath != string.Empty) || (ImagePath != string.Empty && person.ImagePath != string.Empty && ImagePath != person.ImagePath && Mode == enMode.Edit))
            {

                if (pictureBox1.Image != null)
                {

                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;

                }

                DeleteImage(person.ImagePath);

            }
                
            if (ImagePath != string.Empty && File.Exists(ImagePath))
            {

                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
                string destPath = Path.Combine(Properties.Settings.Default.ImageFolderPath, newFileName);
                CopyImage(ImagePath, Properties.Settings.Default.ImageFolderPath, destPath);
                FillPerson(destPath);

            }
            else
            {

                FillPerson(string.Empty);

            }

            person.Save();

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            this.Close();

        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (Person.DoesNationalNoExist(tbNationalNo.Text))
            {

                e.Cancel = true;
                epNationalNo.SetError(tbNationalNo, "This national number already exist!");

            }
            else
            {

                e.Cancel = false;
                epNationalNo.SetError(tbNationalNo, "");

            }

        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            if (!tbEmail.Text.EndsWith("gmail.com"))
            {

                e.Cancel = true;
                epNationalNo.SetError(tbEmail, "Invalid email format!");

            }
            else
            {

                e.Cancel = false;
                epNationalNo.SetError(tbEmail, "");

            }

        }

    }

}
