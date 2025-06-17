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

        public clsPerson person { get; set; }
        string ImagePath = string.Empty;

        public bool succeeded { get; set; }

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        //Constructor for add person mode
        public frmAddEditPerson()
        {

            Mode = enMode.Add;
            person = new clsPerson();

            InitializeComponent();

        }

        //Constructor for update person mode
        public frmAddEditPerson(int PersonID)
        {

            Mode = enMode.Edit;
            person = clsPerson.FindPerson(PersonID);

            InitializeComponent();

        }

        //Show person's image in picturebox
        public void RefreshImage(string ImagePath, clsPerson.enGender Gender)
        {

            //If image is not empty it shows it
            Utils.ImageHandling.ShowLocalImage(ImagePath, pbProfileImage);

            //If image is empty it shows the gender of person
            if (ImagePath == string.Empty)
            {

                switch (Gender)
                {

                    case clsPerson.enGender.Male:
                        pbProfileImage.Image = Properties.Resources.Male_512;
                        break;
                    case clsPerson.enGender.Female:
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

            DataTable dtCountriesNames = clsCountry.GetCountries();

            foreach(DataRow drCountry in dtCountriesNames.Rows)
            {

                cbCountry.Items.Add(drCountry["CountryName"].ToString());

            }

        }

        //This function choose the right gender radio button in form
        private void ShowGender(clsPerson.enGender gender)
        {

            switch (gender)
            {

                case clsPerson.enGender.Male:
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                    break;
                case clsPerson.enGender.Female:
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
        private void ShowInformation(clsPerson person)
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

            person.Gender = (rbMale.Checked ? clsPerson.enGender.Male : clsPerson.enGender.Female);

            person.NationalityCountryID = cbCountry.SelectedIndex + 1;

            person.ImagePath = ImagePath;

        }

        //Convert form mode to update mode and edit it (show information...)
        private void InitializeUpdatePersonWindow(clsPerson person)
        {

            Mode = enMode.Edit;

            clsPerson newPerson = clsPerson.FindPerson(person.PersonID);

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
            ImagePath = person.ImagePath;

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

            bool isProfileImageDeleted = (ImagePath == string.Empty && person.ImagePath != string.Empty);
            bool doesPersonEditProfileImage = (ImagePath != string.Empty && person.ImagePath != string.Empty && ImagePath != person.ImagePath && Mode == enMode.Edit);

            if (isProfileImageDeleted || doesPersonEditProfileImage)
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

            RefreshImage(ImagePath, (rbMale.Checked ? clsPerson.enGender.Male : clsPerson.enGender.Female));

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
                RefreshImage(ImagePath, (rbMale.Checked ? clsPerson.enGender.Male : clsPerson.enGender.Female));

            }

        }

        //Close form
        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private bool AreColumnsCompleted()
        {

            return (tbFirstName.Text != string.Empty && tbLastName.Text != string.Empty &&
                    tbAddress.Text != string.Empty && dtpBirthDate.Value != null && tbPhone.Text != string.Empty);

        }

        public bool SaveItem(clsPerson person)
        {

            if (Mode == enMode.Edit)
                DeleteUnusedImage();

            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
            FillPerson(Utils.ImageHandling.CopyImage(ImagePath, newFileName));

            if (person.Save())
            {

                MessageBox.Show("Data has been saved succeesfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                succeeded = true;

            }
            else
            {
                
                MessageBox.Show("Data has not been saved succeesfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                succeeded = false;

            }
                
            return succeeded;

        }

        private void ChangeMode()
        {

            if (Mode == enMode.Add)
            {

                Mode = enMode.Edit;
                lblMode.Text = "Update Person";
                lblPersonID.Text = person.PersonID.ToString();

            }
            else
                this.Close();

        }

        //Save person's information in database
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!AreColumnsCompleted())
            {

                MessageBox.Show("Some fields are not completed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            if (SaveItem(person))
                ChangeMode();

        }

        //Check if NationalNo exists
        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {

            CheckEmptyFields(sender, e);

            if(!string.IsNullOrEmpty(tbNationalNo.Text))
                Utils.UI.ShowErrorProvider(clsPerson.DoesNationalNoExist(tbNationalNo.Text) && !string.IsNullOrEmpty(tbNationalNo.Text) && Mode == enMode.Add, "This national number already exists!", (Control)tbNationalNo, epNationalNo, e);

        }

        //Check if email is valid
        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            Utils.UI.ValidateEmail(tbEmail, epEmail, e);

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

        private void frmAddEditPerson_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (succeeded == false)
                return;

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}
