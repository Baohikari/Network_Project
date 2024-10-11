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
using NAudio.Wave;

namespace Network_Project
{
    public partial class Client_Form : Form
    {
        private UdpClient udpAudioClient;
        private UdpClient UdpClient;
        private BufferedWaveProvider waveProvider;
        private WaveOutEvent waveOut;
        public Client_Form()
        {
            InitializeComponent();
            string localIP = GetLocalIPAddress();
            UdpClient = new UdpClient(5000); //Video port
            udpAudioClient = new UdpClient(5001); //Audio port

            //Setup audio playback
            waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
            waveOut = new WaveOutEvent();
            waveOut.Init(waveProvider);
            waveOut.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Start receiving video stream
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += ReceiveStream;
            timer.Start();

            //Start receiving audio stream
            Timer audioTimer = new Timer();
            audioTimer.Interval = 100;
            audioTimer.Tick += ReceiveAudioStream;
            audioTimer.Start();
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

        private void ReceiveAudioStream(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] audioData = udpAudioClient.Receive(ref serverEndPoint);
                waveProvider.AddSamples(audioData, 0, audioData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio Error: " + ex.Message);
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Không tìm thấy địa chỉ IP của máy.");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void streaming_screen_Click(object sender, EventArgs e)
        {

        }
    }
}
