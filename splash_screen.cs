using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ozelkalem
{
    public partial class splash_screen : Form
    {
        public splash_screen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelslide.Left += 2;
            if (panelslide.Left>250)
            {
                panelslide.Left = 0;
            }
        }

        private void splash_screen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
