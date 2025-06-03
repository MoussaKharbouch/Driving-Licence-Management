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
using DVLDPresentationLayer.People;

namespace DVLDPresentationLayer
{

    public partial class frmManagePeople : Form
    {

        public frmManagePeople()
        {

            InitializeComponent();

        }

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

        private bool isInteger(Type type)
        {

            return (type == typeof(int));

        }

        private void ApplyFilter(string filterName, string value, DataTable dtData)
        {

            if (dtData == null)
                return;

            if (string.IsNullOrWhiteSpace(value))
            {

                dtData.DefaultView.RowFilter = string.Empty;
                return;

            }

            if (filterName == "None")
            {

                dtData.DefaultView.RowFilter = "";

            }
            else
            {

                if (!dtData.Columns.Contains(filterName))
                {

                    MessageBox.Show("This filter is invalid!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


                if (dtData.Columns.Contains(filterName) && value != string.Empty)
                {

                    Type columnType = dtData.Columns[filterName].DataType;

                    if(isInteger(columnType))
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
                    if(columnType == typeof(string))
                    {

                        dtData.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterName, value.Replace("'", "''"));

                    }

                }

            }

        }

        private void LoadPeopleDate()
        {

            DataTable dtPeople = Person.GetPeopleMainInfo();

            if (dtPeople.Rows.Count > 0)
                dgvPeople.DataSource = dtPeople;

            lblRecords.Text = dtPeople.Rows.Count.ToString();

        }

        private void DeleteImage(string ImagePath)
        {

            if (File.Exists(ImagePath))
                File.Delete(ImagePath);

        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {

            LoadFilters();
            LoadPeopleDate();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvPeople.DataSource);
            dgvPeople.Refresh();

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvPeople.DataSource);

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

                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                Person person = Person.FindPerson(PersonID);
                Person.DeletePerson(PersonID);

                if (person.ImagePath != string.Empty)
                    DeleteImage(person.ImagePath);

                LoadPeopleDate();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            frmShowDetails ShowDetails = new frmShowDetails(Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value));
            ShowDetails.OnSaveEventHandler += LoadPeopleDate;

            ShowDetails.ShowDialog();

        }

        private void tsEditPerson_Click(object sender, EventArgs e)
        {

            if (dgvPeople.SelectedRows.Count > 0)
            {

                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                frmAddEditPerson EditPerson = new frmAddEditPerson(PersonID);
                EditPerson.OnSaveEventHandler += LoadPeopleDate;

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
            AddPerson.OnSaveEventHandler += LoadPeopleDate;

            AddPerson.ShowDialog();

        }

    }

}
