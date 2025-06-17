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

namespace DVLDPresentationLayer.Test_Types
{

    public partial class frmManageTestTypes : Form
    {

        public frmManageTestTypes()
        {

            InitializeComponent();

        }

        public void LoadItems()
        {

            DataTable dtTestTypes = clsTestType.GetTestTypes();

            if (dtTestTypes.Rows.Count > 0)
                dgvTestTypes.DataSource = dtTestTypes;

            lblRecords.Text = dtTestTypes.Rows.Count.ToString();

        }

        private void tsEditTestType_Click(object sender, EventArgs e)
        {

            if (dgvTestTypes.SelectedRows.Count > 0)
            {

                int TestTypeID = Convert.ToInt32(dgvTestTypes.SelectedRows[0].Cells["TestTypeID"].Value);

                frmUpdateTestType UpdateTestType = new frmUpdateTestType(TestTypeID);
                UpdateTestType.OnSaveEventHandler += LoadItems;

                UpdateTestType.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void dgvTestTypes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvTestTypes, e, cmsTestType);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {

            LoadItems();

        }

    }

}
