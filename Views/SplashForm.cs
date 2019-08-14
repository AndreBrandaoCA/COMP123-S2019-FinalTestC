using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace COMP123_S2019_FinalTestC.Views
{
    public partial class SplashForm : COMP123_S2019_FinalTestC.Views.MasterForm
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashFormTimer_Tick(object sender, EventArgs e)
        {
            Program.splashForm.Hide();
            Program.characterForm.Show();
        }
    }
}
