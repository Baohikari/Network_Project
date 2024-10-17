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
        private List<byte> videoBuffer = new List<byte>();

        private bool isConnected = false;
        public Client_Form()
        {
            InitializeComponent();
            UdpClient = new UdpClient(5000); //Video port
            udpAudioClient = new UdpClient(5001); //Audio port

            //Setup audio playback
            waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
            waveOut = new WaveOutEvent();
            waveOut.Init(waveProvider);

            float initialVolume = trackBar1.Value / 100.0f;
            waveOut.Volume = initialVolume;

            waveOut.Play();

            //Thêm độ phân giải
            resolutionComboBox.Items.Add("640x480");
            resolutionComboBox.Items.Add("800x600");
            resolutionComboBox.Items.Add("1024x768");
            resolutionComboBox.Items.Add("1280x720");
            resolutionComboBox.Items.Add("1920x1080");

            resolutionComboBox.SelectedIndex = 0; //Lấy mặc định là 640x480
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Bắt đầu nhận luồng video
            Task.Run(() => ReceiveVideoStreamAsync());

            //Bắt đầu nhận luồng audio
            Task.Run(() => ReceiveAudioStreamAsync());

            MessageBox.Show("Bắt đầu xem stream");
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            string serverIP = serverIpTextBox.Text.Trim();

            if (string.IsNullOrEmpty(serverIP))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ IP của server.");
                return;
            }

            try
            {
                // Khởi tạo UdpClient và kết nối với server
                UdpClient = new UdpClient();
                UdpClient.Connect(serverIP, 5000); // Kết nối video

                udpAudioClient = new UdpClient();
                udpAudioClient.Connect(serverIP, 5001); // Kết nối audio

                isConnected = true;
                MessageBox.Show("Kết nối thành công tới server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối tới server: {ex.Message}");
            }
        }
        private async Task ReceiveVideoStreamAsync()
        {
            while (isConnected)
            {
                try
                {
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    UdpReceiveResult result = await UdpClient.ReceiveAsync().ConfigureAwait(false);
                    byte[] data = result.Buffer;

                    if (data.Length > 0)
                    {
                        videoBuffer.AddRange(data);
                    }

                    // Check for end-of-frame marker
                    if (videoBuffer.Count >= 4 &&
                        videoBuffer[videoBuffer.Count - 4] == 255 &&
                        videoBuffer[videoBuffer.Count - 3] == 255 &&
                        videoBuffer[videoBuffer.Count - 2] == 255 &&
                        videoBuffer[videoBuffer.Count - 1] == 255)
                    {
                        // Remove the marker
                        videoBuffer.RemoveRange(videoBuffer.Count - 4, 4);

                        // Try displaying the image
                        using (MemoryStream ms = new MemoryStream(videoBuffer.ToArray()))
                        {
                            try
                            {
                                Image image = Image.FromStream(ms);
                                if (resolutionComboBox.SelectedItem != null)
                                {
                                    string resolution = resolutionComboBox.SelectedItem.ToString();
                                    var dimensions = resolution.Split('x');
                                    int width = int.Parse(dimensions[0]);
                                    int height = int.Parse(dimensions[1]);

                                    Bitmap resizedImage = new Bitmap(image, new Size(width, height));
                                    streaming_screen.Invoke(new Action(() =>
                                    {
                                        streaming_screen.Image = resizedImage;
                                    }));
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Image processing error: " + ex.Message);
                            }
                        }
                        // Clear buffer after processing
                        videoBuffer.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while receiving video: " + ex.Message);
                }

                await Task.Delay(10).ConfigureAwait(false);
            }
        }


        private async Task ReceiveAudioStreamAsync()
        {
            while (isConnected)
            {
                try
                {
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    var result = await udpAudioClient.ReceiveAsync().ConfigureAwait(false);
                    waveProvider.AddSamples(result.Buffer, 0, result.Buffer.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Audio Error: " + ex.Message);
                }
                await Task.Delay(50).ConfigureAwait (false);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float volume = trackBar1.Value / 100.0f;
            waveOut.Volume = volume;
        }

        private void streaming_screen_Click(object sender, EventArgs e)
        {

        }

        private void resolutionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
}
