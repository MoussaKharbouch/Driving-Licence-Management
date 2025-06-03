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

    public partial class frmShowDetails : Form
    {

        public delegate void OnSave();
        public event OnSave OnSaveEventHandler;

        public frmShowDetails()
        {

            InitializeComponent();

        }

        public frmShowDetails(int PersonID)
        {

            InitializeComponent();

            ctrlPersonCard1.Initialize(PersonID);

        }

        private void frmShowDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (OnSaveEventHandler != null)
                OnSaveEventHandler();
        }

    }

}
