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

        public enum enMode { Add, Edit }
        public enMode Mode { get; private set; }

        Person person;
        string ImagePath = string.Empty;

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public delegate void OnSaveReturningPersonID(int PersonID);
        public event OnSaveReturningPersonID OnSaveReturningPersonIDEventHandler;

        //Constructor for add person mode
        public frmAddEditPerson()
        {

            Mode = enMode.Add;
            person = new Person();

            InitializeComponent();

        }

        //Constructor for update person mode
        public frmAddEditPerson(int PersonID)
        {

            Mode = enMode.Edit;
            person = Person.FindPerson(PersonID);

            InitializeComponent();

        }

        //Show person's image in picturebox
        public void RefreshImage(string ImagePath, Person.enGender Gender)
        {

            //If image is not empty it shows it

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

            //If image is empty it shows the gender of person

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

        //Fill countries (names only) in combobox from database
        private void FillCountries()
        {

            DataTable dtCountriesNames = Country.GetCountries();

            foreach(DataRow drCountry in dtCountriesNames.Rows)
            {

                cbCountry.Items.Add(drCountry["CountryName"].ToString());

            }

        }

        //This function choose the right gender radio button in form
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
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                    break;

            }

        }

        //Show person's information on form
        private void ShowInformation(Person person)
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

            //Choose the country in combobox
            if (person.NationalityCountryID > 0 && cbCountry.Items.Count >= person.NationalityCountryID)
                cbCountry.SelectedIndex = person.NationalityCountryID - 1;

            //Select person's date of birth if he is bigger than 18 years, and shows error if he is smaller than 18 years
            if (person.DateOfBirth <= dtpBirthDate.MaxDate)
                dtpBirthDate.Value = person.DateOfBirth;
            else
            {

                dtpBirthDate.Value = dtpBirthDate.MaxDate;
                MessageBox.Show("Birth date of this person is invalid (less than 18 years)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            RefreshImage(person.ImagePath, person.Gender);

        }

        //Fill person object from user's inputs (controls)
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

        //Convert form mode to update mode and edit it (show information...)
        private void InitializeUpdatePersonWindow(Person person)
        {

            Mode = enMode.Edit;

            Person newPerson = Person.FindPerson(person.PersonID);

            if (newPerson != null)
                ShowInformation(newPerson);
            else
                MessageBox.Show("This person is not available!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblMode.Text = "Update Person";

        }

        //Convert form mode to add mode and edit it
        private void InitializeAddPersonWindow()
        {

            //Choose Algeria as default country
            int algeriaIndex = cbCountry.FindString("Algeria");
            if (algeriaIndex != -1)
                cbCountry.SelectedIndex = algeriaIndex;

            lblMode.Text = "Add New Person";

        }

        //Load event
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {

            //Make sure the date in datetimepicker is more than 18 years before current date
            dtpBirthDate.MaxDate = DateTime.Today.AddYears(-18);
            dtpBirthDate.Value = DateTime.Today.AddYears(-18);

            FillCountries();

            switch(Mode)
            {

                case enMode.Edit:
                    InitializeUpdatePersonWindow(person);
                    break;

                case enMode.Add:
                    InitializeAddPersonWindow();
                    break;

                default:
                    break;

            }

        }

        //Delete person's image from images's folder if he deleted it or replaced it
        private void DeleteUnusedImage()
        {

            if ((ImagePath == string.Empty && person.ImagePath != string.Empty) || (ImagePath != string.Empty && person.ImagePath != string.Empty && ImagePath != person.ImagePath && Mode == enMode.Edit))
            {

                if (pictureBox1.Image != null)
                {

                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;

                }

                if (File.Exists(ImagePath))
                    File.Delete(ImagePath);

            }

        }

        //Replace gender image if radiobutton changed and person has not an image
        private void rbGender_CheckedChanged(object sender, EventArgs e)
        {

            RefreshImage(ImagePath, (rbMale.Checked ? Person.enGender.Male : Person.enGender.Female));

        }

        //Show openfiledialog to set image
        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (ofdImage.ShowDialog() == DialogResult.OK)
                ImagePath = ofdImage.FileName;

            RefreshImage(ImagePath, person.Gender);

        }

        //Remove person's image
        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (ImagePath != string.Empty)
            {

                ImagePath = string.Empty;
                RefreshImage(ImagePath, (rbMale.Checked ? Person.enGender.Male : Person.enGender.Female));

            }

        }

        //To copy person's image from originl path to images's folder
        private string CopyPersonImage()
        {

            if (ImagePath != string.Empty && File.Exists(ImagePath))
            {

                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
                string destPath = Path.Combine(Properties.Settings.Default.ImageFolderPath, newFileName);

                //Copy image if exists
                if (File.Exists(ImagePath))
                {

                    if (!Directory.Exists(Properties.Settings.Default.ImageFolderPath))
                        Directory.CreateDirectory(Properties.Settings.Default.ImageFolderPath);

                    if (!File.Exists(destPath))
                        File.Copy(ImagePath, destPath);

                }

                return destPath;

            }

            return string.Empty;

        }

        //Close form
        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        //Save person's information in database
        private void btnSave_Click(object sender, EventArgs e)
        {

            if(Mode == enMode.Edit)
                DeleteUnusedImage();

            FillPerson(CopyPersonImage());

            if (!person.Save())
            {

                MessageBox.Show("Data has not been saved succeesfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            MessageBox.Show("Data has been saved succeesfully", "Secceeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (Mode == enMode.Add)
            {

                Mode = enMode.Edit;
                lblMode.Text = "Update Person";
                lblPersonID.Text = person.PersonID.ToString();

                return;

            }

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

            if (OnSaveReturningPersonIDEventHandler != null)
                OnSaveReturningPersonIDEventHandler(person.PersonID);

            this.Close();

        }

        //Check if NationalNo exists
        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(tbNationalNo.Text))
            {

                e.Cancel = true;
                epEmail.SetError(tbNationalNo, "You have to complete this field!");

            }
            else if (Person.DoesNationalNoExist(tbNationalNo.Text) && !string.IsNullOrEmpty(tbNationalNo.Text))
            {

                e.Cancel = true;
                epEmail.SetError(tbNationalNo, "This national number is already used!");

            }
            else
            {

                e.Cancel = false;
                epEmail.SetError(tbNationalNo, "");

            }

        }

        //Check if email is valid
        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            if (!tbEmail.Text.EndsWith("@gmail.com") && !string.IsNullOrEmpty(tbEmail.Text))
            {

                e.Cancel = true;
                epEmail.SetError(tbEmail, "Invalid email format!");

            }
            else
            {

                e.Cancel = false;
                epEmail.SetError(tbEmail, "");

            }

        }

        //Make sure that no field is empty
        private void CheckEmptyFields(object sender, CancelEventArgs e)
        {

            TextBox tbSender = (TextBox)sender;

            if (tbSender.Tag == null && string.IsNullOrEmpty(tbSender.Text))
            {

                e.Cancel = true;
                epEmptyField.SetError(tbSender, "You have to complete this field!");

            }
            else
            {

                e.Cancel = false;
                epEmptyField.SetError(tbSender, "");

            }

        }

    }

}
