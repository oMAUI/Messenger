using Mora.Designs.Animations;
using Mora.Designs.Button;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora.Designs.UIcontrols;
using Mora.ServiceChat;
using System.Data.Common;
using System.Threading;

namespace Mora
{
    public partial class SignUpWindow : Form, IServiceChatCallback
    {
        ServiceChatClient client;

        public int id { get; private set; }
        public string[] userData { get; private set; }

        public SignUpWindow()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(24, 32, 38);
            this.StartPosition = FormStartPosition.CenterScreen;

            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            

            Animator.Start();
        }

        private void SignUpWindow_Load(object sender, EventArgs e)
        {

            tbLogin.Size = new Size(200, 40);
            tbLogin.Location = new Point(this.Width / 2 - tbLogin.Size.Width / 2 - 5, this.Height / 3 - tbLogin.Size.Height);
            tbLogin.TextPreview = "login";
            this.Controls.Add(tbLogin);

            tbPass.Size = new Size(200, 40);
            tbPass.Location = new Point(this.Width / 2 - tbPass.Size.Width / 2 - 5, this.Height / 3 );
            tbPass.TextPreview = "password";
            tbPass.UseSystemPasswordChar = true;
            this.Controls.Add(tbPass);

            btnReg.Size = new Size(150, btnReg.Size.Height);
            btnReg.Location = new Point(this.Width / 2 - btnReg.Size.Width / 2 - 5, tbPass.Location.Y + tbPass.Size.Height + 50);
            btnReg.Text = "Sign In";
            this.Controls.Add(btnReg);

            btnSignUp.Size = new Size(150, btnSignUp.Size.Height);
            btnSignUp.Location = new Point(this.Width / 2 - btnSignUp.Size.Width / 2 - 5, btnReg.Location.Y + btnSignUp.Size.Height + 5);
            btnSignUp.Text = "Sign Up";
            this.Controls.Add(btnSignUp);

            lbCheck.Location = new Point(this.Width / 2 - lbCheck.Size.Width / 2 - 5, tbLogin.Location.Y - 40);
            lbCheck.ForeColor = Color.FromArgb(33, 158, 188);
            this.Controls.Add(lbCheck);

            this.ActiveControl = btnReg;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            userData = client.LoginUser(tbLogin.Text, tbPass.Text); // сделать без ошибок

            if (userData != null)
            {
                lbCheck.Text = "Вход выполнен";
                lbCheck.Location = new Point(this.Width / 2 - lbCheck.Size.Width / 2 - 5, tbLogin.Location.Y - 40);
                lbCheck.ForeColor = Color.FromArgb(33, 158, 188);
                Form1.GetData(userData);
                Thread.Sleep(20);
                this.Close();
            }
            else
            {
                lbCheck.ForeColor = Color.FromArgb(230, 57, 70);
                lbCheck.Text = @"                      Ошибка Входа!
Проверти корректность введеных данных!";
                lbCheck.Location = new Point(this.Width / 2 - lbCheck.Size.Width / 2 - 5, lbCheck.Location.Y);
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (client.AddUserInDB(tbLogin.Text, tbPass.Text))
            {
                lbCheck.Text = "Регистрацтя прошла успешно!";
                lbCheck.Location = new Point(this.Width / 2 - lbCheck.Size.Width / 2 - 5, tbLogin.Location.Y - 40);
                lbCheck.ForeColor = Color.FromArgb(33, 158, 188);
            }

            else
            {
                lbCheck.ForeColor = Color.FromArgb(230, 57, 70);
                lbCheck.Text = @"Такой пользователь уже существует!";
                lbCheck.Location = new Point(this.Width / 2 - lbCheck.Size.Width / 2 - 5, lbCheck.Location.Y);
            }
        }

        public void MsgCallBack(string msg, int id)
        {
            
        }
    }
}
