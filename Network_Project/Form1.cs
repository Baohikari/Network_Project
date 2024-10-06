using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void server_request_btn_Click(object sender, EventArgs e)
        {
            Server_Form sv_form = new Server_Form();
            sv_form.Show();
            this.Hide();
        }

        private void client_request_btn_Click(object sender, EventArgs e)
        {
            Client_Form cl_form =  new Client_Form();
            cl_form.Show();
            this.Hide();
        }
    }
}
