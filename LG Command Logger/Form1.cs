using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LG_Command_Logger
    {
    public partial class Form1 : Form
        {
        public Form1()
            {
            InitializeComponent();
            }

        private void Form1_Load(object sender, EventArgs e)
            {
            this.Dock = DockStyle.Fill;

            }


        private void onResize(object sender, EventArgs e)
            {
            panel_Contenedor.Location = new Point((this.Width / 2 - panel_Contenedor.Width / 2), (this.Height / 2 - panel_Contenedor.Height / 2));
            }
        }
    }
