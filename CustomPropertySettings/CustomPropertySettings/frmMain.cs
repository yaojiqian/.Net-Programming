using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPropertySettings
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Properties.CustomPath.Default.Reload();

            //Properties.CustomPath.Default.StartDate = DateTime.Now;

            DateTime d = Properties.CustomPath.Default.StartDate;

            MessageBox.Show(Properties.CustomPath.Default.Name + " " + Properties.CustomPath.Default.Age + "\n" + d.ToString());

            //Properties.CustomPath theSetting = new Properties.CustomPath();
            //MessageBox.Sheo(theSetting.)
            Properties.CustomPath.Default.Name = "World";
            Properties.CustomPath.Default.Save();
        }
    }
}
