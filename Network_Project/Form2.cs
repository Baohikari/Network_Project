using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Network_Project
{
    public partial class Client_Form : Form
    {
        private UdpClient UdpClient;
        public Client_Form()
        {
            InitializeComponent();
            UdpClient = new UdpClient(5000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += ReceiveStream;
            timer.Start();
        }
        private void ReceiveStream(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = UdpClient.Receive(ref serverEndPoint);

                using (MemoryStream ms = new MemoryStream(data))
                {
                    Image image = Image.FromStream(ms);
                    streaming_screen.Image = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void streaming_screen_Click(object sender, EventArgs e)
        {

        }
    }
}
