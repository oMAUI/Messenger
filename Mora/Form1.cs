using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora.ServiceChat;
using Mora_Messenger.Design;
using Mora_Messenger.Design.Button;
using Mora_Messenger.Design.UIControls;

namespace Mora
{
    public partial class Form1 : Form, IServiceChatCallback
    {
        ServiceChatClient client;
        private LayOutForm1 layOut;
        SignUpWindow signUpWindow;

        List<UserCard> userCard = new List<UserCard>();
        List<string> name = new List<string>() { "salim", "valera", "islam", "mam", "pap", "liana", "Inal", "pirt", "sirt", "cirt", "lala", "topala" };

        Color sidebar = Color.FromArgb(14, 22, 28);

        int id = 0;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(24, 32, 38);

            timer1.Enabled = false;
            timer1.Interval = 1;

            this.UserCardBox.AutoScroll = true;
            this.UserCardBox.AutoScrollPosition = new Point(-20, -20);
            this.UserCardBox.VerticalScroll.Visible = false;
            this.UserCardBox.HorizontalScroll.Visible = false;

            tbMsgBox.BackColor = Color.FromArgb(54, 62, 68);
            //pMsgBox.Visible = false;
        }

        public void MsgCallBack(string msg, int id)
        {
            drawMsgBox(id, msg);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //client.Diconnection(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //textBox2.Text = db.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Hide();
            //signUpWindow = new SignUpWindow();
            //signUpWindow.ShowDialog();
            //this.Show();

            //client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            //id = client.Connection("Mora");



            //bool db = client.DBconnection("Server=95.217.232.188;Port=7777;Username=habitov;Password=habitov");

            timer1.Enabled = true;

            pMsgBox.BackColor = this.BackColor;
            pMsgBox.Size = new Size(this.Width / 4, this.Height - 200);
            pMsgBox.Location = new Point(this.Width * 2 / 4 + 10 - this.pMsgBox.Size.Width, 10);
            pMsgBox.VerticalScroll.Visible = false;

            pMsgBoxUser.BackColor = this.BackColor;
            pMsgBoxUser.Size = new Size(this.Width / 4, this.Height - 200);
            pMsgBoxUser.Location = new Point(this.Width * 3 / 4 - 10, 10);


            panel1.BackColor = this.BackColor;
            panel1.Size = new Size(20, pMsgBox.Height);
            panel1.Location = new Point(pMsgBox.Location.X + pMsgBox.Width - 20, pMsgBox.Location.Y);
            panel2.BackColor = this.BackColor;
            panel2.Size = new Size(20, pMsgBoxUser.Height);
            panel2.Location = new Point(pMsgBoxUser.Location.X + pMsgBoxUser.Width - 20, pMsgBoxUser.Location.Y);

            //UserCardBox.BackColor = sidebar;
            UserCardBox.Size = new Size(this.Width / 4 + 10, this.Height - 150);
            UserCardBox.Location = new Point(0, 110);

            UserCard mainCard = new UserCard();
            mainCard.Size = new Size(this.Width / 4, 102);
            mainCard.Location = new Point(0, 0);
            mainCard.BackColor = Color.FromArgb(11, 57, 72);
            Controls.Add(mainCard);

            int startLocation = 110;

            Random rand = new Random();
            for (int i = 0; i < 25; i++)
            {
                UserCard uCard = new UserCard();
                uCard.Location = new Point(10, 500);
                uCard.Text = name[rand.Next(0, name.Count)];
                uCard.Visible = true;

                UserCardBox.Controls.Add(uCard);
            }

            int a = UserCardBox.Controls.Count;
            int b;
        }

        #region Design

        private void drawLayOut(Graphics g)
        {
            layOut = new LayOutForm1();

            //layOut.Panel(g, 0, 0, this.Width / 4 - 10, this.Height, 14, 22, 28);
            //layOut.Panel(g, 0, 0, this.Width / 4 - 10, 100, 61, 90, 128);
            //layOut.Panel(g, this.Width / 4 + 1, this.Height - 120, this.Width * 3 / 4, 120, 44, 52, 58);

            layOut.EllipseMsg(g, this.Width / 4 + 1 + 10, this.Height - 105, this.Width * 3 / 4 - 40, 50, 54, 62, 68);

            tbMsgBox.Location = new Point(this.Width / 4 + 1 + 25, this.Height - 99);
            tbMsgBox.Size = new Size(this.Width * 3 / 4 - 65, 40);
        }

        public void drawMsgBox(int id, string msg)
        {
            MsgBox msgBox = new MsgBox(msg);

            var msgBox2 = new MsgBox(msg);
            msgBox2.Text = "";
            msgBox2.BackColor = this.BackColor;

            if(this.id == id)
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

            tbMsgBox.Text = "";
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

        private void tbMsgBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tbMsgBox.Text.Length > 0)
            {
                //client.SendMsg(tbMsgBox.Text, this.id, Convert.ToInt32(textBox1.Text));
                //tbMsgBox.Text = "";
                //tableMsgBox.Controls.Add(msgBox, 1, 1);
                drawMsgBox(1, tbMsgBox.Text);
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

        private void registerButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
