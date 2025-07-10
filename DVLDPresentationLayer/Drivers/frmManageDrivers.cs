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

namespace DVLDPresentationLayer.Drivers
{

    public partial class frmManageDrivers : Form
    {

        public frmManageDrivers()
        {

            InitializeComponent();

        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {

            LoadItems();
            cbFilters.SelectedIndex = 0;

        }

        public void ApplyFilter(string filterName, string value)
        {

            DataTable dtItems = (DataTable)dgvDrivers.DataSource;
            bool succeeded = false;

            if (dtItems.Columns.Count == 0)
                return;

            succeeded = Utils.Filtering.FilterDataTable(filterName, value, dtItems);

            if (!succeeded)
                MessageBox.Show("Invalid filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                lblRecords.Text = dtItems.DefaultView.Count.ToString();

        }

        public void LoadItems()
        {

            DataTable dtDrivers = clsDriver.GetDriversMainInfo();

            dgvDrivers.DataSource = dtDrivers;

            lblRecords.Text = dtDrivers.Rows.Count.ToString();

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbValue.Text = string.Empty;

            string filterName = cbFilters.SelectedItem != null ? cbFilters.SelectedItem.ToString() : string.Empty;

            if (cbFilters.SelectedItem.ToString() == "None")
                tbValue.Enabled = false;
            else
                tbValue.Enabled = true;

             ApplyFilter(filterName, tbValue.Text);

        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

            ApplyFilter(cbFilters.SelectedItem.ToString(), tbValue.Text);

        }

        //Stop entering characters when filter's value is a number
        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() != "Person ID" && cbFilters.SelectedItem.ToString() != "Driver ID")
                return;

            Utils.UI.StopEnteringCharacters(e);

        }

    }

}
