using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientServer
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;

        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            //Start server host
            textBox3.Text += "Server starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txthost.Text);
            server.Start(ip, Convert.ToInt32(txtport.Text));
        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
           
        }
        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update mesage to txtStatus
            textBox3.Invoke((MethodInvoker)delegate ()
            {
                textBox3.Text += e.MessageString;
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


}
