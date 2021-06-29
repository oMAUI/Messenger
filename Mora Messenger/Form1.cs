using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora_Messenger.Design;

namespace Mora_Messenger
{
    public partial class Form1 : Form
    {
        private LayOutForm1 layOut;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(24, 32, 38);

            timer1.Enabled = true;
            timer1.Interval = 1;

            tbMsgBox.BackColor = Color.FromArgb(54, 62, 68);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            //drawLayOut(g);

            
        }

        #region Design

        private void drawLayOut(Graphics g)
        {
            layOut = new LayOutForm1();

            layOut.Panel(g, 0, 0, this.Width / 4, this.Height, 14, 22, 28);
            layOut.Panel(g, 0, 0, this.Width / 4, 100, 34, 42, 48);
            layOut.Panel(g, this.Width / 4 + 1, this.Height - 120, this.Width * 3 / 4, 120, 44, 52, 58);

            layOut.EllipseMsg(g, this.Width / 4 + 1 + 10, this.Height - 105, this.Width * 3 / 4 - 40, 50, 54, 62, 68);

            tbMsgBox.Location = new Point(this.Width / 4 + 1 + 25, this.Height - 99);
            tbMsgBox.Size = new Size(this.Width * 3 / 4 - 65, 40);
        }

        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawLayOut(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
