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

namespace Mora
{
    public partial class SignUpWindow : Form, IServiceChatCallback
    {
        ServiceChatClient client;

        public int id { get; private set; }

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
            this.Controls.Add(tbPass);

            btnReg.Size = new Size(150, btnReg.Size.Height);
            btnReg.Location = new Point(this.Width / 2 - btnReg.Size.Width / 2 - 5, tbPass.Location.Y + tbPass.Size.Height + 50);
            btnReg.Text = "Sign In";
            this.Controls.Add(btnReg);

            btnSignUp.Size = new Size(150, btnSignUp.Size.Height);
            btnSignUp.Location = new Point(this.Width / 2 - btnSignUp.Size.Width / 2 - 5, btnReg.Location.Y + btnSignUp.Size.Height + 5);
            btnSignUp.Text = "Sign Up";
            this.Controls.Add(btnSignUp);
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            id = client.LoginUser(tbLogin.Text, tbPass.Text); // сделать без ошибок

            if (id != -1)
            {
                //btnReg.Text = id.ToString();
                
                this.Close();
            }
            else
            {
                btnReg.Text = id.ToString();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (client.AddUserInDB(tbLogin.Text, tbPass.Text))
            {
                btnSignUp.Text = "Registered";
            }

            else
                btnSignUp.Text = "reg false";
        }

        public void MsgCallBack(string msg, int id)
        {
            
        }
    }
}
