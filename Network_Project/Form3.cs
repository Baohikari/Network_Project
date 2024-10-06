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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Network_Project
{
    public partial class Server_Form : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private UdpClient udpServer;
        private IPEndPoint clientEndPoint;
        public Server_Form()
        {
            InitializeComponent();
            udpServer = new UdpClient();
            clientEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
        }

        private void start_streaming_btn_Click(object sender, EventArgs e)
        {
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

        private void videoSource_NewFrame(object sender, NewFrameEventArgs e)
        {
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            // Chuyển đổi hình ảnh thành byte[]
            using (var ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                //Chia nhỏ dữ liệu (do báo lỗi của chương trình trước đó)
                const int maxUdpPacketSize = 65000;
                for(int offset = 0; offset < data.Length; offset += maxUdpPacketSize)
                {
                    int size = Math.Min(maxUdpPacketSize, data.Length - offset);
                    byte[] packet = new byte[size];
                    Array.Copy(data, offset, packet, 0, size);
                    udpServer.Send(packet, packet.Length, clientEndPoint);
                }

                // Gửi dữ liệu qua UDP
                //udpServer.Send(data, data.Length, clientEndPoint);
            }

            // Hiển thị video lên PictureBox
            streaming_screen.Image = bitmap;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }
    }
}
