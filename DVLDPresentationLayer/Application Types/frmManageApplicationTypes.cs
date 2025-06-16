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

namespace DVLDPresentationLayer.ApplicationTypes
{

    public partial class frmManageApplicationTypes : Form
    {

        public frmManageApplicationTypes()
        {

            InitializeComponent();

        }

        public void LoadItems()
        {

            DataTable dtApplicationTypes = ApplicationType.GetApplicationTypes();

            if (dtApplicationTypes.Rows.Count > 0)
                dgvApplicationTypes.DataSource = dtApplicationTypes;

            lblRecords.Text = dtApplicationTypes.Rows.Count.ToString();

        }

        private void tsEditApplicationType_Click(object sender, EventArgs e)
        {

            if (dgvApplicationTypes.SelectedRows.Count > 0)
            {

                int ApplicationTypeID = Convert.ToInt32(dgvApplicationTypes.SelectedRows[0].Cells["ApplicationTypeID"].Value);

                frmUpdateApplicationType UpdateApplicationType = new frmUpdateApplicationType(ApplicationTypeID);
                UpdateApplicationType.OnSaveEventHandler += LoadItems;

                UpdateApplicationType.ShowDialog();

            }
            else
            {

                MessageBox.Show("No row is selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void dgvApplicationTypes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            Utils.UI.ShowCMS(dgvApplicationTypes, e, cmsApplicationType);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {

            LoadItems();

        }

    }

}
