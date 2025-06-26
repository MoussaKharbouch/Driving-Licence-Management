using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDPresentationLayer.Tests
{

    public partial class frmTakeTest : Form
    {

        public frmTakeTest()
        {

            InitializeComponent();
            
        }

        public frmTakeTest(int AppointmentID)
        {

            InitializeComponent();
            ctrlTestInfo1.Refresh(AppointmentID);

        }

    }

}
