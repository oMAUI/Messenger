using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora.Designs.UIcontrols;
using Mora.ServiceChat;
using Mora_Messenger.Design;
using Mora_Messenger.Design.Button;
using Mora_Messenger.Design.UIControls;

namespace Mora
{
    public partial class Form1 : Form, IServiceChatCallback
    {
        UserCard mainCard;

        ServiceChatClient client;
        private LayOutForm1 layOut;
        SignUpWindow signUpWindow;

        List<UserCard> userCard = new List<UserCard>();

        Color sidebar = Color.FromArgb(14, 22, 28);

        public static int id { get; private set; }
        public static string login { get; private set; }
        public static string password { get; private set; }

        int SelectUserID = 0;

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

            signUpWindow = new SignUpWindow();

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

        public static void GetData(string[] userData)
        {
            id = int.Parse(userData[0]);
            login = userData[1];
            password = userData[2];
        }

        private Object DeserializeObj(byte[] arr)
        {
            using(var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(arr, 0, arr.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = bf.Deserialize(ms);
                return obj;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            signUpWindow.ShowDialog();
            this.Show();

            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            client.Connection(login, id);

            //bool db = client.DBconnection("Server=95.217.232.188;Port=7777;Username=habitov;Password=habitov");

            timer1.Enabled = true;

            tbMesgBox.BackColor = Color.FromArgb(54, 62, 68);
            tbMesgBox.Location = new Point(this.Width / 4 + 1 + 25, this.Height - 99);
            tbMesgBox.Size = new Size(this.Width * 3 / 4 - 65, 50);
            tbMesgBox.TextPreview = "";
            tbMesgBox.Font = new Font("Arial", 17.25F, FontStyle.Regular);
            tbMesgBox.RoundingPrecent = 40;
            tbMesgBox.Enabled = false;

            pMsgBox.BackColor = this.BackColor;
            pMsgBox.Size = new Size(this.Width / 4, this.Height - 150);
            pMsgBox.Location = new Point(this.Width * 2 / 4 + 10 - this.pMsgBox.Size.Width, 10);
            pMsgBox.VerticalScroll.Visible = false;

            pMsgBoxUser.BackColor = this.BackColor;
            pMsgBoxUser.Size = new Size(this.Width / 4, this.Height - 150);
            pMsgBoxUser.Location = new Point(this.Width * 3 / 4 - 10, 10);

            //UserCardBox.BackColor = sidebar;
            UserCardBox.Size = new Size(this.Width / 4 + 5, this.Height - 150);
            UserCardBox.Location = new Point(0, 110);

            panel1.BackColor = this.BackColor;
            panel1.Size = new Size(20, pMsgBox.Height);
            panel1.Location = new Point(pMsgBox.Location.X + pMsgBox.Width - 20, pMsgBox.Location.Y);
            panel2.BackColor = this.BackColor;
            panel2.Size = new Size(20, pMsgBoxUser.Height);
            panel2.Location = new Point(pMsgBoxUser.Location.X + pMsgBoxUser.Width - 20, pMsgBoxUser.Location.Y);
            panel3.BackColor = this.BackColor;
            panel3.Size = new Size(20, UserCardBox.Height);
            panel3.Location = new Point(UserCardBox.Location.X + UserCardBox.Width - 20, UserCardBox.Location.Y);

            mainCard = new UserCard();
            mainCard.Size = new Size(this.Width / 4, 102);
            mainCard.Location = new Point(0, 0);
            mainCard.BackColor = Color.FromArgb(11, 57, 72);
            mainCard.Font = new Font("Arial", 17.25F, FontStyle.Regular);
            mainCard.ForeColor = Color.FromArgb(241, 250, 238);
            mainCard.Text = login;
            Controls.Add(mainCard);

            List<List<string[]>> userContact;

            byte[] serializeData = client.GetUserContact(id);
            userContact = (List<List<string[]>>)DeserializeObj(serializeData);

            foreach (var list in userContact)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    UserCard uCard = new UserCard();
                    //uCard.Location = new Point(10, 500);
                    uCard.Text = list[i][1];
                    uCard.UserID = int.Parse(list[i][0]);
                    uCard.Name = list[i][1] + list[i][0];
                    uCard.Click += new EventHandler(uCard_Click);
                    //uCard.Visible = true;
                    userCard.Add(uCard);
                    UserCardBox.Controls.Add(uCard);
                }
            }
        }

        #region Design

        private void drawLayOut(Graphics g)
        {
            layOut = new LayOutForm1();

            //layOut.Panel(g, 0, 0, this.Width / 4 - 10, this.Height, 14, 22, 28);
            //layOut.Panel(g, 0, 0, this.Width / 4 - 10, 100, 61, 90, 128);
            //layOut.Panel(g, this.Width / 4 + 1, this.Height - 120, this.Width * 3 / 4, 120, 44, 52, 58);

            //layOut.EllipseMsg(g, this.Width / 4 + 1 + 10, this.Height - 105, this.Width * 3 / 4 - 40, 50, 54, 62, 68);
        }

        public void drawMsgBox(int id, string msg)
        {
            MsgBox msgBox = new MsgBox(msg);

            var msgBox2 = new MsgBox(msg);
            msgBox2.Text = "";
            msgBox2.BackColor = this.BackColor;

            if(Form1.id == id)
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

            //tbMsgBox.Text = "";
        }

        #endregion

        private void uCard_Click(object sender, EventArgs e)
        {
            SelectUserID = ((sender as UserCard).UserID);

            pMsgBox.Controls.Clear();
            pMsgBoxUser.Controls.Clear();

            List<object[]> msgHistory = new List<object[]>();
            byte[] desArr = client.GetMsgHistory(id, SelectUserID);

            msgHistory = (List<object[]>)DeserializeObj(desArr);

            foreach(var msg in msgHistory)
            {
                drawMsgBox((int)msg[1], msg[3].ToString());
            }

            tbMesgBox.Enabled = true;
        }

        private void registerTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                client.SendMsg(tbMesgBox.Text, id, SelectUserID);
                tbMesgBox.Text = "";
                //drawMsgBox(1, tbMesgBox.Text);
            }
        }

        #region NotUse

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawLayOut(e.Graphics);
        }
        int asd;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void pMsgBox_Wheel(object sender, MouseEventArgs e)
        {
            pMsgBoxUser.VerticalScroll.Value = pMsgBox.VerticalScroll.Value;
        }

        private void pMsgBoxUser_Wheel(object sender, MouseEventArgs e)
        {
            pMsgBox.VerticalScroll.Value = pMsgBoxUser.VerticalScroll.Value;
        }
        #endregion
    }
}
