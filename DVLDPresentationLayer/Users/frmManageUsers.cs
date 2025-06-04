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

            LoadFilters();
            LoadUsersData();

        }

        private void LoadFilters()
        {

            cbFilter.Items.Add("None");
            cbFilter.Items.Add("UserID");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("Username");
            cbFilter.Items.Add("Password");
            cbFilter.Items.Add("IsActive");

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

            if (filterName == "IsActive")
            {

                tbValue.Visible = false;
                cbIsActive.Visible = true;

                if(value == "True")
                    dtData.DefaultView.RowFilter = "IsActive == true";
                else
                    dtData.DefaultView.RowFilter = "IsActive == false";

            }

            if (string.IsNullOrWhiteSpace(value))
            {

                dtData.DefaultView.RowFilter = string.Empty;
                return;

            }

            if (filterName == "None")
            {

                dtData.DefaultView.RowFilter = "";
                return;

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

                    if (isInteger(columnType))
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

            }

        }

        private void LoadUsersData()
        {

            DataTable dtUsers = User.GetUsersWithFullName();

            if (dtUsers.Rows.Count > 0)
                dgvUsers.DataSource = dtUsers;

            lblRecords.Text = dtUsers.Rows.Count.ToString();

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvUsers.DataSource);
            dgvUsers.Refresh();

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

        private void deleteUser(object sender, EventArgs e)
        {

            if (dgvUsers.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Are you sure you want delete this user?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                User user = User.FindUser(UserID);
                User.DeleteUser(UserID);

                LoadUsersData();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void AddUser_Click(object sender, EventArgs e)
        {

            /*frmAddEditPerson AddPerson = new frmAddEditPerson();
            AddPerson.OnSaveEventHandler += LoadPeopleDate;

            AddPerson.ShowDialog();*/

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvUsers.DataSource);

        }

        private void tbValue_TextChanged_1(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvUsers.DataSource);

        }

        private void cbIsActive_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilter.SelectedItem.ToString(), tbValue.Text, (DataTable)dgvUsers.DataSource);

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }

}
