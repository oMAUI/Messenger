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
using Mora_Messenger.Design.Button;
using Mora_Messenger.Design.UIControls;

namespace Mora_Messenger
{
    public partial class Form1 : Form
    {
        private LayOutForm1 layOut;

        List<UserCard> userCard = new List<UserCard>();

        Color sidebar = Color.FromArgb(14, 22, 28);

        List<string> name = new List<string>() { "salim", "valera", "islam", "mam", "pap", "liana", "Inal", "pirt", "sirt", "cirt", "lala", "topala" };

        private int id = 1;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(24, 32, 38);

            timer1.Enabled = true;
            timer1.Interval = 1;

            tbMsgBox.BackColor = Color.FromArgb(54, 62, 68);
            //pMsgBox.Visible = false;
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        #region Design

        private void drawLayOut(Graphics g)
        {
            layOut = new LayOutForm1();

            layOut.Panel(g, 0, 0, this.Width / 4 - 10, this.Height, 14, 22, 28);
            layOut.Panel(g, 0, 0, this.Width / 4 - 10, 100, 61, 90, 128);
            layOut.Panel(g, this.Width / 4 + 1, this.Height - 120, this.Width * 3 / 4, 120, 44, 52, 58);

            

            layOut.EllipseMsg(g, this.Width / 4 + 1 + 10, this.Height - 105, this.Width * 3 / 4 - 40, 50, 54, 62, 68);

            tbMsgBox.Location = new Point(this.Width / 4 + 1 + 25, this.Height - 99);
            tbMsgBox.Size = new Size(this.Width * 3 / 4 - 65, 40);
        }

        public void drawMsgBox(int id)
        {
            MsgBox msgBox = new MsgBox(tbMsgBox.Text);

            var msgBox2 = new MsgBox(tbMsgBox.Text);
            msgBox2.Text = "";
            msgBox2.BackColor = this.BackColor;

            if (id == this.id)
            {
                pMsgBoxUser.Controls.Add(msgBox);
                pMsgBox.Controls.Add(msgBox2);
            }
            else
            {
                pMsgBox.Controls.Add(msgBox);
                pMsgBoxUser.Controls.Add(msgBox2);
            }
            
            pMsgBoxUser.VerticalScroll.Value = pMsgBoxUser.VerticalScroll.Maximum;
            pMsgBox.VerticalScroll.Value = pMsgBox.VerticalScroll.Maximum;
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbMsgBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tbMsgBox.Text.Length > 0)
            {
                drawMsgBox(1);
                tbMsgBox.Text = "";
                //tableMsgBox.Controls.Add(msgBox, 1, 1);
            }

            if (e.KeyCode == Keys.Space)
            {
                drawMsgBox(0);
                tbMsgBox.Text = "";
            }

            
        }
        private void pMsgBox_Wheel(object sender, MouseEventArgs e)
        {
            pMsgBoxUser.VerticalScroll.Value = pMsgBox.VerticalScroll.Value;
        }

        private void pMsgBoxUser_Wheel(object sender, MouseEventArgs e)
        {
            pMsgBox.VerticalScroll.Value = pMsgBoxUser.VerticalScroll.Value;
        }

        private void Form1_Scroll(object sender, ScrollEventArgs e)
        {
           
            pMsgBox.VerticalScroll.Value = e.NewValue;
        }
    }
}
