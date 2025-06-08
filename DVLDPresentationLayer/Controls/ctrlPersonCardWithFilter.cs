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
        private DataTable dtPeople;

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

            cbFilter.Items.Add("None");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("FirstName");
            cbFilter.Items.Add("SecondName");
            cbFilter.Items.Add("ThirdName");
            cbFilter.Items.Add("LastName");
            cbFilter.Items.Add("Gender");
            cbFilter.Items.Add("Nationality");
            cbFilter.Items.Add("Phone");
            cbFilter.Items.Add("Email");

            cbFilter.SelectedIndex = 0;

        }

        //Apply normal filter (not special situation)
        private void ApplyFilter(string filterName, string value, DataTable dtData)
        {

            Type columnType = dtData.Columns[filterName].DataType;

            if (columnType == typeof(int))
            {

                int numericValue;

                if (int.TryParse(value, out numericValue))
                {

                    dtData.DefaultView.RowFilter = string.Format("{0} = {1}", filterName, value.Replace("'", "''"));

                }
                else
                {

                    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtData.DefaultView.RowFilter = string.Empty;

                }

            }
            if (columnType == typeof(string))
            {

                dtData.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterName, value.Replace("'", "''"));

            }

        }

        //Check filter and apply it on people's data (it can be None, or IsActive...)
        private void CheckFilter(string filterName, string value, DataTable dtData)
        {

            if (dtData == null)
                return;

            if (filterName == "None")
                dtData.DefaultView.RowFilter = "";
            else
            {

                if (string.IsNullOrWhiteSpace(value))
                    dtData.DefaultView.RowFilter = string.Empty;

                else if (!dtData.Columns.Contains(filterName))
                    MessageBox.Show("This filter is invalid!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (dtData.Columns.Contains(filterName) && value != string.Empty)
                    ApplyFilter(filterName, value, dtData);

            }

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {

            dtPeople = Person.GetPeople();
            LoadFilters();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            CheckFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, dtPeople);

            if (dtPeople.DefaultView.Count == 1)
                ctrlPersonCard1.Refresh((int)dtPeople.Rows[0]["PersonID"]);

        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.SelectedItem.ToString() != "PersonID")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }

        public void RefreshPerson(int PersonID)
        {

            this.PersonID = PersonID;

            Person person = Person.FindPerson(PersonID);

            if(person != null)
                ctrlPersonCard1.Refresh(person.PersonID);

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {

            frmAddEditPerson AddPerson = new frmAddEditPerson();
            AddPerson.OnSaveReturningPersonIDEventHandler += RefreshPerson;

            AddPerson.ShowDialog();

        }

    }

}
