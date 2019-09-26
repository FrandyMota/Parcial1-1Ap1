using Parcial1_1AP1.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial1_1AP1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void RegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rEestudiante R = new rEestudiante();
            R.MdiParent = this;
            R.Show();
        }
    }
}