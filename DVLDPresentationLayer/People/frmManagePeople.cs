using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using DVLDBusinessLayer;
using DVLDPresentationLayer.People;
using DVLDPresentationLayer.Interfaces;

namespace DVLDPresentationLayer
{

    public partial class frmManagePeople : Form, IDeletable, IFilterable, ILoadable
    {

        public frmManagePeople()
        {

            InitializeComponent();

        }

        //Check filter and apply it on people's data (it can be None, or IsActive...)
        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvPeople.DataSource;

            if (!Utils.Filtering.FilterDataTable(filterName, value, dtItems))
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void LoadItems()
        {

            DataTable dtPeople = Person.GetPeopleMainInfo();

            if (dtPeople.Rows.Count > 0)
                dgvPeople.DataSource = dtPeople;

            lblRecords.Text = dtPeople.Rows.Count.ToString();

        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {

            LoadItems();

            if (dgvPeople.Columns.Count > 0)
            {

                Utils.Filtering.FillFilters((DataTable)dgvPeople.DataSource, cbFilters);
                cbFilters.SelectedIndex = 0;

            }

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);
            dgvPeople.Refresh();

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        private void dgvPeople_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvPeople, e, cmsPerson);

        }

        private void DeletePerson(object sender, EventArgs e)
        {

            if (dgvPeople.SelectedRows.Count == 0)
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (MessageBox.Show("Are you sure you want delete this person?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                try
                {

                    bool succeeded = DeleteItem(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value));

                    if (succeeded)
                    {

                        MessageBox.Show("Person deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItems();

                    }
                    else
                        MessageBox.Show("Failed to delete person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch
                {

                    MessageBox.Show("Data has not been saved succeesfully. Person has data linked to it.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
                MessageBox.Show("Failed to delete person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            frmShowDetails ShowDetails = new frmShowDetails(Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value));
            ShowDetails.OnSaveEventHandler += LoadItems;

            ShowDetails.ShowDialog();

        }

        public bool DeleteItem(int PersonID)
        {

            Person person = Person.FindPerson(PersonID);

            if (person == null)
                return false;

            try
            {

                if (Person.DeletePerson(person.PersonID)){
                
                    //Delete person's image after delete it
                    if (person.ImagePath != string.Empty)
                    {

                        if (File.Exists(person.ImagePath))
                            File.Delete(person.ImagePath);

                    }

                    LoadItems();

                    return true;

                }
                else
                    return false;

            }
            catch
            {

                throw;

            }

        }

        private void tsEditPerson_Click(object sender, EventArgs e)
        {

            if (dgvPeople.SelectedRows.Count > 0)
            {

                //Getting PersonID from row selected
                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                frmAddEditPerson EditPerson = new frmAddEditPerson(PersonID);
                EditPerson.OnSaveEventHandler += LoadItems;

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
            AddPerson.OnSaveEventHandler += LoadItems;

            AddPerson.ShowDialog();

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() == "PersonID")
            {

                Utils.UI.StopEnteringCharacters(e);

            }

        }

    }

}
