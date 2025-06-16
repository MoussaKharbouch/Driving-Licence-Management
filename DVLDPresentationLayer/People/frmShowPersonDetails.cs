using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.People
{

    public partial class frmShowPersonDetails : Form
    {

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmShowPersonDetails()
        {

            InitializeComponent();

        }

        public frmShowPersonDetails(int PersonID)
        {

            InitializeComponent();

            ctrlPersonCard1.Refresh(PersonID);

        }

        private void frmShowDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();

        }

    }

}
