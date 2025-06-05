using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer.Users
{

    public partial class frmManageUsers : Form
    {

        public frmManageUsers()
        {

            InitializeComponent();

        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {

            LoadUsersData();
            LoadFilters();

        }

        //Load filters in filters's combobox
        private void LoadFilters()
        {

            //Remove the events temperoraly so no null error shows
            cbFilter.SelectedIndexChanged -= cbFilter_SelectedIndexChanged;
            cbIsActive.SelectedIndexChanged -= cbIsActive_SelectedIndexChanged;

            cbFilter.Items.Add("None");
            cbFilter.Items.Add("UserID");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("Username");
            cbFilter.Items.Add("Password");
            cbFilter.Items.Add("IsActive");


            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("True");
            cbIsActive.Items.Add("False");

            cbIsActive.SelectedIndex = 0;
            cbIsActive.Visible = false;

            cbFilter.SelectedIndex = 0;

            //Adding events again
            cbFilter.SelectedIndexChanged += cbFilter_SelectedIndexChanged;
            cbIsActive.SelectedIndexChanged += cbIsActive_SelectedIndexChanged;

        }

        //Apply normal filter (not special situation)
        private void ApplyFilter(string filterName, string value, DataTable dtData)
        {

            Type columnType = dtData.Columns[filterName].DataType;

            if (columnType == typeof(int))
            {

                int numericValue;

                if (int.TryParse(value, out numericValue) && value != string.Empty)
                {

                    dtData.DefaultView.RowFilter = string.Format("{0} = {1}", filterName, value.Replace("'", "''"));

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

            //Default visibility (combobox is hidden while textbox is visible)
            tbValue.Visible = true;
            cbIsActive.Visible = false;

            if (filterName == "IsActive")
            {

                tbValue.Visible = false;
                cbIsActive.Visible = true;

                if (value == "All")
                    dtData.DefaultView.RowFilter = string.Empty;
                else if(value == "True")
                    dtData.DefaultView.RowFilter = "IsActive = true";
                else if (value == "False")
                    dtData.DefaultView.RowFilter = "IsActive = false";

            }

            else if (string.IsNullOrEmpty(value))
            {
                dtData.DefaultView.RowFilter = "";
                lblRecords.Text = dtData.DefaultView.Count.ToString();
                return;
            }


            else if (filterName == "None")
            {

                dtData.DefaultView.RowFilter = "";

            }
            else
            {

                if (!dtData.Columns.Contains(filterName))
                    MessageBox.Show("This filter is invalid!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (dtData.Columns.Contains(filterName) && value != string.Empty)
                    ApplyFilter(filterName, value, dtData);

            }

        }

        private void LoadUsersData()
        {

            DataTable dtUsers = User.GetUsersWithFullName();

            if (dtUsers.Rows.Count > 0)
                dgvUsers.DataSource = dtUsers;

            lblRecords.Text = dtUsers.Rows.Count.ToString();

        }

        private void deleteUser(object sender, EventArgs e)
        {

            if (dgvUsers.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Are you sure you want delete this user?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                //Getting UserID from selected row
                int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                User user = User.FindUser(UserID);

                try
                {

                    if (!User.DeleteUser(user.UserID))
                        MessageBox.Show("Data has not been saved succeesfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Data has been saved succeesfully", "Secceeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {

                    MessageBox.Show("Data has not been saved succeesfully. Person has data linked to it.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                LoadUsersData();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void AddUser_Click(object sender, EventArgs e)
        {

            /*frmAddEditPerson AddPerson = new frmAddEditPerson();
            AddPerson.OnSaveEventHandler += LoadPeopleDate;

            AddPerson.ShowDialog();*/

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilter.SelectedItem != null ? cbFilter.SelectedItem.ToString() : string.Empty;

            string filterValue;

            if (filterName == "IsActive")
                filterValue = cbIsActive.SelectedItem != null ? cbIsActive.SelectedItem.ToString() : string.Empty;
            else
            {

                filterValue = tbValue.Text;

            }

            CheckFilter(filterName, filterValue, (DataTable)dgvUsers.DataSource);

        }

        private void dgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {

                dgvUsers.ClearSelection();
                dgvUsers.Rows[e.RowIndex].Selected = true;

                cmsUser.Show(Cursor.Position);

            }

        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            /*frmShowDetails ShowDetails = new frmShowDetails(Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value));
            ShowDetails.OnSaveEventHandler += LoadPeopleDate;

            ShowDetails.ShowDialog();*/

        }

        private void tsEdit_Click(object sender, EventArgs e)
        {

            if (dgvUsers.SelectedRows.Count > 0)
            {

                int PersonID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["PersonID"].Value);

                /*frmAddEditPerson EditPerson = new frmAddEditPerson(PersonID);
                EditPerson.OnSaveEventHandler += LoadPeopleDate;

                EditPerson.ShowDialog();*/

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            CheckFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvUsers.DataSource);

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            CheckFilter(cbFilter.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), (DataTable)dgvUsers.DataSource);

            lblRecords.Text = dgvUsers.RowCount.ToString();

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.SelectedItem.ToString() != "PersonID" && cbFilter.SelectedItem.ToString() != "UserID")
                return;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }

    }

}