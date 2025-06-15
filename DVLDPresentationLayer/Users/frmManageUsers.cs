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

            LoadItems();
            LoadFilters();

        }

        //Load filters in filters's combobox
        private void LoadFilters()
        {

            //Remove the events temperoraly so no null error shows
            cbFilters.SelectedIndexChanged -= cbFilters_SelectedIndexChanged;
            cbIsActive.SelectedIndexChanged -= cbIsActive_SelectedIndexChanged;

            Utils.Filtering.FillFilters((DataTable)dgvUsers.DataSource, cbFilters);

            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("True");
            cbIsActive.Items.Add("False");

            cbIsActive.Visible = false;

            cbFilters.SelectedIndex = 0;
            cbFilters_SelectedIndexChanged(null, null);

            cbIsActive.SelectedIndex = 0;

            //Adding events again
            cbFilters.SelectedIndexChanged += cbFilters_SelectedIndexChanged;
            cbIsActive.SelectedIndexChanged += cbIsActive_SelectedIndexChanged;

        }

        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvUsers.DataSource;
            bool succeeded = false;

            succeeded = Utils.Filtering.FilterDataTable(filterName, value, dtItems);

            if (!succeeded)
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void ApplyFilter(string filterName, Utils.Filtering.enBoolFilter value)
        {

            DataTable dtItems = (DataTable)dgvUsers.DataSource;
            bool succeeded = false;

            succeeded = Utils.Filtering.FilterDataTable(filterName, value, dtItems);

            if (!succeeded)
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void LoadItems()
        {

            DataTable dtUsers = User.GetUsersMainInfo();

            if (dtUsers.Rows.Count > 0)
                dgvUsers.DataSource = dtUsers;

            lblRecords.Text = dtUsers.Rows.Count.ToString();

        }

        public bool DeleteItem(int UserID)
        {

            User user = User.FindUser(UserID);

            if (user == null)
                return false;

            bool succeeded = false;

            try
            {

                succeeded = User.DeleteUser(user.UserID);
                    

            }
            catch
            {

                MessageBox.Show("User has data linked to it.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            LoadItems();
            return succeeded;

        }

        private void DeleteUser(object sender, EventArgs e)
        {

            //Getting UserID from selected row
            int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

            if (dgvUsers.SelectedRows.Count > 0)
            {

                if (MessageBox.Show("Are you sure you want delete this user?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                if (!DeleteItem(UserID))
                    MessageBox.Show("Data has not been saved succeesfully", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Data has been saved succeesfully", "Secceeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private Utils.Filtering.enBoolFilter GetIsActiveFilterValue(string IsActive)
        {

            switch (IsActive)
            {

                case "All":
                    return Utils.Filtering.enBoolFilter.All;
                case "True":
                    return Utils.Filtering.enBoolFilter.True;
                case "False":
                    return Utils.Filtering.enBoolFilter.False;

            }

            return Utils.Filtering.enBoolFilter.All;

        }

        private void AddUser_Click(object sender, EventArgs e)
        {

            frmAddEditUser AddPerson = new frmAddEditUser();
            AddPerson.OnSaveEventHandler += LoadItems;

            AddPerson.ShowDialog();

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilters.SelectedItem != null ? cbFilters.SelectedItem.ToString() : string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

            if (filterName == "IsActive")
            {

                tbValue.Visible = false;
                cbIsActive.Visible = true;

                DataTable dtItems = (DataTable)dgvUsers.DataSource;

                cbIsActive_SelectedIndexChanged(cbIsActive, EventArgs.Empty);

            }
            else
            {

                tbValue.Visible = true;
                cbIsActive.Visible = false;

                ApplyFilter(filterName, tbValue.Text);

            }

        }

        private void dgvUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvUsers, e, cmsUser);

        }

        private void tsShowDetails_Click(object sender, EventArgs e)
        {

            frmShowDetails ShowDetails = new frmShowDetails(Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value));
            ShowDetails.OnSaveEventHandler += LoadItems;

            ShowDetails.ShowDialog();

        }

        private void tsEdit_Click(object sender, EventArgs e)
        {

            if (dgvUsers.SelectedRows.Count > 0)
            {

                int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                frmAddEditUser EditPerson = new frmAddEditUser(UserID);
                EditPerson.OnSaveEventHandler += LoadItems;

                EditPerson.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsChangePassword_Click(object sender, EventArgs e)
        {

            if (dgvUsers.SelectedRows.Count > 0)
            {

                int UserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);

                frmChangePassword ChangePassword = new frmChangePassword(UserID);
                ChangePassword.OnSaveEventHandler += LoadItems;

                ChangePassword.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbIsActive.SelectedItem != null)
                ApplyFilter("IsActive", GetIsActiveFilterValue(cbIsActive.SelectedItem.ToString()));
            else
                ApplyFilter("IsActive", Utils.Filtering.enBoolFilter.All);

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() != "PersonID" && cbFilters.SelectedItem.ToString() != "UserID")
                return;

            Utils.UI.StopEnteringCharacters(e);

        }

    }

}