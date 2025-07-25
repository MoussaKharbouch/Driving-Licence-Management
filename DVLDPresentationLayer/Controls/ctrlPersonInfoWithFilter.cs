﻿using System;
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

            PersonID = -1;

        }

        public ctrlPersonCardWithFilter(int PersonID)
        {

            InitializeComponent();
            ctrlPersonCard1.Refresh(PersonID);

        }

        //Fill filter's combobox with filters's names
        private void LoadFilters()
        {

            cbFilters.Items.Add("PersonID");
            cbFilters.Items.Add("NationalNo");

            cbFilters.SelectedIndex = 0;

        }

        
        public void ApplyFilter(string filterName, string value)
        {

            if (!Utils.Filtering.FilterDataTable(filterName, value, dtItems))
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (dtItems.DefaultView.Count == 1)
                PersonID = Convert.ToInt32(dtItems.DefaultView[0]["PersonID"]);
            else if (dtItems.DefaultView.Count != 1)
                MessageBox.Show("No matching person found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void Initialize()
        {

            dtItems = clsPerson.GetPeopleMainInfo();
            LoadFilters();

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {

            Initialize();

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

            clsPerson person = clsPerson.FindPerson(PersonID);

            if(person != null)
                ctrlPersonCard1.Refresh(person.PersonID);

        }

        public void RefreshPerson()
        {

            clsPerson person = clsPerson.FindPerson(AddPerson.person.PersonID);

            if (person != null)
                ctrlPersonCard1.Refresh(person.PersonID);

        }

        public void Filter(string FilterName, string Value)
        {

            ApplyFilter(FilterName, Value);

            cbFilters.SelectedItem = FilterName;
            cbFilters.Enabled = false;

            tbValue.Text = Value;
            tbValue.Enabled = false;

            RefreshPerson(PersonID);

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
