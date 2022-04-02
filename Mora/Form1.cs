using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora.Connection;
using Mora.Designs.Chat;
using Mora.Designs.UIcontrols;
using Mora.Models;
using Mora.ServiceChat;
using Mora_Messenger.Design;
using Mora_Messenger.Design.Button;
using Mora_Messenger.Design.UIControls;

namespace Mora
{
    public partial class Form1 : Form, IMsg
    {
        public static User UserData { get; private set; }
        UserCard mainCard;
        SignUpWindow signUpWindow;
        List<UserCard> userCard = new List<UserCard>();
        Color sidebar = Color.FromArgb(14, 22, 28);

        private string SelectUserID;
        private WsConnection wsConn;

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

            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        public static void GetData(User userData)
        {
            UserData = userData;
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

            wsConn = new WsConnection(ConfigurationManager.AppSettings.Get("ws_server_address"), ConfigurationManager.AppSettings.Get("port_server_address"), this);
            wsConn.Connect(UserData.data.id);

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
            mainCard.Text = UserData.data.login;
            Controls.Add(mainCard);

            foreach (var contact in UserData.contacts)
            {
                UserCard uCard = new UserCard();
                uCard.Text = contact.login;
                uCard.Name = contact.id;
                uCard.UserID = contact.id;
                uCard.Click += new EventHandler(uCard_Click);
                
                userCard.Add(uCard);
                UserCardBox.Controls.Add(uCard);

            }
        }

        #region Design

        public void DrawMsgBox(string id, string msg)
        {
            MsgBox msgBox = new MsgBox(msg);

            var msgBox2 = new MsgBox(msg);
            msgBox2.Text = "";
            msgBox2.BackColor = this.BackColor;

            if (Form1.UserData.data.id == id)
            {
                pMsgBoxUser.Controls.Add(msgBox);
                pMsgBox.Controls.Add(msgBox2);
            }
            else
            {
                pMsgBox.Invoke(new Action<MsgBox>((s) => pMsgBox.Controls.Add(s)), msgBox);
                pMsgBoxUser.Invoke(new Action<MsgBox>((s) => pMsgBoxUser.Controls.Add(s)), msgBox2);
            }

            pMsgBox.Invoke(new Action<int>((s) => pMsgBox.VerticalScroll.Value = s), pMsgBox.VerticalScroll.Maximum);
            pMsgBoxUser.Invoke(new Action<int>((s) => pMsgBoxUser.VerticalScroll.Value = s), pMsgBoxUser.VerticalScroll.Maximum);

            //tbMsgBox.Text = "";
        }

        #endregion

        private void uCard_Click(object sender, EventArgs e)
        {
            SelectUserID = ((sender as UserCard).UserID);

            pMsgBox.Controls.Clear();
            pMsgBoxUser.Controls.Clear();

            //List<object[]> msgHistory = new List<object[]>();
            //byte[] desArr = client.GetMsgHistory(id, SelectUserID);

            //msgHistory = (List<object[]>)DeserializeObj(desArr);

            //foreach (var msg in msgHistory)
            //{
            //    drawMsgBox((int)msg[1], msg[3].ToString());
            //}

            tbMesgBox.Enabled = true;
        }

        private void registerTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                wsConn.SendMsg(UserData.data.id, SelectUserID, tbMesgBox.Text);
                DrawMsgBox(UserData.data.id, tbMesgBox.Text);
                tbMesgBox.Text = "";
            }
        }

        #region NotUse

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

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
