using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using DVLDBusinessLayer;
using DVLDPresentationLayer.People;

namespace DVLDPresentationLayer
{

    public partial class frmManagePeople : Form
    {

        public frmManagePeople()
        {

            InitializeComponent();

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

            lblRecords.Text = dtData.DefaultView.Count.ToString();

        }

        private void LoadPeopleData()
        {

            DataTable dtPeople = Person.GetPeopleMainInfo();

            if (dtPeople.Rows.Count > 0)
                dgvPeople.DataSource = dtPeople;

            lblRecords.Text = dtPeople.Rows.Count.ToString();

        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {

            LoadFilters();
            LoadPeopleData();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            CheckFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvPeople.DataSource);
            dgvPeople.Refresh();

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            CheckFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvPeople.DataSource);

        }

        private void dgvPeople_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {

                dgvPeople.ClearSelection();
                dgvPeople.Rows[e.RowIndex].Selected = true;

                cmsPerson.Show(Cursor.Position);

            }

        }

        private void deletePerson(object sender, EventArgs e)
        {

            if (dgvPeople.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Are you sure you want delete this person?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                //Getting PersonID from row selected
                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                Person person = Person.FindPerson(PersonID);

                if (person == null)
                    return;

                try
                {

                    if (!Person.DeletePerson(person.PersonID))
                        MessageBox.Show("Data has not been saved succeesfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Data has been saved succeesfully", "Secceeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {

                    MessageBox.Show("Data has not been saved succeesfully. Person has data linked to it.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                //Delete person's image after delete it
                if (person.ImagePath != string.Empty)
                {

                    if (File.Exists(person.ImagePath))
                        File.Delete(person.ImagePath);

                }

                LoadPeopleData();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            frmShowDetails ShowDetails = new frmShowDetails(Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value));
            ShowDetails.OnSaveEventHandler += LoadPeopleData;

            ShowDetails.ShowDialog();

        }

        private void tsEditPerson_Click(object sender, EventArgs e)
        {

            if (dgvPeople.SelectedRows.Count > 0)
            {

                //Getting PersonID from row selected
                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                frmAddEditPerson EditPerson = new frmAddEditPerson(PersonID);
                EditPerson.OnSaveEventHandler += LoadPeopleData;

                EditPerson.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void AddPerson_Click(object sender, EventArgs e)
        {

            frmAddEditPerson AddPerson = new frmAddEditPerson();
            AddPerson.OnSaveEventHandler += LoadPeopleData;

            AddPerson.ShowDialog();

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.SelectedItem.ToString() != "PersonID")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }

    }

}
