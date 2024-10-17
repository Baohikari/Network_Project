using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using NAudio.Wave;

namespace Network_Project
{
    public partial class Server_Form : Form
    {
        private UdpClient udpAudioServer;
        private WaveInEvent waveIn;
        private IPEndPoint clientAudioEndPoint;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private UdpClient udpServer;
        private IPEndPoint clientEndPoint;
        public Server_Form()
        {
            IPAddress localAddress = GetLocalIPAddress(); //Hàm lấy địa chỉ IP
            InitializeComponent();
            udpServer = new UdpClient();
            udpAudioServer = new UdpClient();
            clientEndPoint = new IPEndPoint(localAddress, 5000);
            clientAudioEndPoint = new IPEndPoint(localAddress, 5001);

            Console.WriteLine(localAddress.ToString());
        }

        private void start_streaming_btn_Click(object sender, EventArgs e)
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.StartRecording();


            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("Không tìm thấy camera.");
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] audioData = e.Buffer;

            udpAudioServer.Send(audioData, audioData.Length, clientAudioEndPoint);
        }


        private void videoSource_NewFrame(object sender, NewFrameEventArgs e)
        {
            if(e.Frame == null)
            {
                return; //bỏ qua quá trình nếu frame là null
            }
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            // Chuyển đổi hình ảnh thành byte[]
            using (var ms = new System.IO.MemoryStream())
            {
                try
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] data = ms.ToArray();
                    //Chia nhỏ dữ liệu (do báo lỗi của chương trình trước đó)
                    const int maxUdpPacketSize = 65000;
                    for (int offset = 0; offset < data.Length; offset += maxUdpPacketSize)
                    {
                        int size = Math.Min(maxUdpPacketSize, data.Length - offset);
                        byte[] packet = new byte[size];
                        Array.Copy(data, offset, packet, 0, size);
                        udpServer.Send(packet, packet.Length, clientEndPoint);
                    }
                    byte[] market = { 255, 255, 255, 255 };
                    udpServer.Send(market, market.Length, clientEndPoint);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error while processing frame: {ex.Message}");
                }

                // Gửi dữ liệu qua UDP
                //udpServer.Send(data, data.Length, clientEndPoint);
            }

            // Hiển thị video lên PictureBox
            if(streaming_screen.Image != null)
            {
                streaming_screen.Image.Dispose();
            }
            streaming_screen.Image = bitmap;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Stop Video
            if(videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            //Stop audio
            if(waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
            }
        }

        private IPAddress GetLocalIPAddress()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211 &&
                    networkInterface.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    var ipProperties = networkInterface.GetIPProperties();
                    foreach (var ip in ipProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address;
                        }
                    }
                }
            }
            throw new Exception("Không tìm thấy địa chỉ IP Wi-Fi.");
        }

        private void start_streaming_btn_Resize(object sender, EventArgs e)
        {
        }
    }
}
