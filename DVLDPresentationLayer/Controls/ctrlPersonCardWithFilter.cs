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

namespace DVLDPresentationLayer.User_Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public int PersonID { get; private set; }
        private DataTable dtItems;

        frmAddEditPerson AddPerson;

        public ctrlPersonCardWithFilter()
        {

            InitializeComponent();

        }

        public ctrlPersonCardWithFilter(int PersonID)
        {

            InitializeComponent();
            ctrlPersonCard1.Refresh(PersonID);

        }

        //Fill filter's combobox with filters's names
        private void LoadFilters()
        {

            Utils.Filtering.FillFilters(dtItems, cbFilters);
            cbFilters.Items.Remove("DateOfBirth");

            cbFilters.SelectedIndex = 0;

        }

        //Check filter and apply it on people's data (it can be None, or IsActive...)
        public void ApplyFilter(string filterName, string value)
        {

            if (!Utils.Filtering.FilterDataTable(filterName, value, dtItems))
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dtItems.DefaultView.Count > 0)
                PersonID = Convert.ToInt32(dtItems.DefaultView[0]["PersonID"]);
            else
                MessageBox.Show("No matching person found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {

            dtItems = Person.GetPeopleMainInfo();
            LoadFilters();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

            if (dtItems.DefaultView.Count == 1)
                ctrlPersonCard1.Refresh((int)dtItems.DefaultView[0]["PersonID"]);

        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() != "PersonID")
                return;

            Utils.UI.StopEnteringCharacters(e);

        }

        public void RefreshPerson(int PersonID)
        {

            this.PersonID = PersonID;

            Person person = Person.FindPerson(PersonID);

            if(person != null)
                ctrlPersonCard1.Refresh(person.PersonID);

        }

        public void RefreshPerson()
        {

            Person person = Person.FindPerson(AddPerson.person.PersonID);

            if (person != null)
                ctrlPersonCard1.Refresh(person.PersonID);

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {

            AddPerson = new frmAddEditPerson();

            if (AddPerson.person.PersonID != -1)
                PersonID = AddPerson.person.PersonID;

            AddPerson.OnSaveEventHandler += () =>
            {

                PersonID = AddPerson.person.PersonID;
                RefreshPerson(PersonID);

            };

            AddPerson.ShowDialog();

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

            tbValue.Text = string.Empty;

        }

    }

}
