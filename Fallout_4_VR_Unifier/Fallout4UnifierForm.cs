using System;
using System.Windows.Forms;

namespace Fallout_4_VR_Unifier
{
    public partial class Fallout4UnifierForm : Form
    {
        public Fallout4UnifierForm()
        {
            InitializeComponent();
        }

        private void FO4_Click(object sender, EventArgs e)
        {
            var merger = new Merger();
            merger.RunMerge("flat");
            Application.Exit();
        }

        private void FO4VR_Click(object sender, EventArgs e)
        {
            var merger = new Merger();
            merger.RunMerge("vr");
            Application.Exit();
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            var merger = new Merger();
            merger.Unmerge();
            MessageBox.Show("Uninstalled");
        }
    }
}
