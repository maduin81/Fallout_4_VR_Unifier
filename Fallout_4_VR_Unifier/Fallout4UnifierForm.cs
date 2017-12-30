using System;
using System.Windows.Forms;

namespace Fallout_4_VR_Unifier
{
    public partial class Fallout4UnifierForm : Form
    {
        private readonly Merger _merger;
        public Fallout4UnifierForm()
        {
            InitializeComponent();
            _merger =  new Merger();
        }

        private void FO4_Click(object sender, EventArgs e)
        {            
            _merger.RunMerge("flat");
            Application.Exit();
        }

        private void FO4VR_Click(object sender, EventArgs e)
        {            
            _merger.RunMerge("vr");
            Application.Exit();
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {            
            _merger.Unmerge();
            MessageBox.Show("Uninstalled");
        }
    }
}
