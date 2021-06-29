using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MORA_Msngr.ServiceChat;

namespace MORA_Msngr
{
    public partial class Form1 : Form, IServiceChatCallback
    {
        ServiceChatClient client;
        int id;

        public Form1()
        {
            InitializeComponent();
        }

        public void MsgCallBack(string msg)
        {
            listBox1.Items.Add(msg);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Diconnection(id);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            id = client.Connection(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.SendMsg(textBox3.Text, id);
        }
    }
}
