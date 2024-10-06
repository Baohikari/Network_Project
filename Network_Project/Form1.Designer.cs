namespace Network_Project
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.header_lbl = new System.Windows.Forms.Label();
            this.request_lbl = new System.Windows.Forms.Label();
            this.server_request_btn = new System.Windows.Forms.Button();
            this.client_request_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // header_lbl
            // 
            this.header_lbl.AutoSize = true;
            this.header_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header_lbl.Location = new System.Drawing.Point(305, 32);
            this.header_lbl.Name = "header_lbl";
            this.header_lbl.Size = new System.Drawing.Size(205, 31);
            this.header_lbl.TabIndex = 0;
            this.header_lbl.Text = "Streaming App";
            // 
            // request_lbl
            // 
            this.request_lbl.AutoSize = true;
            this.request_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.request_lbl.Location = new System.Drawing.Point(339, 134);
            this.request_lbl.Name = "request_lbl";
            this.request_lbl.Size = new System.Drawing.Size(135, 25);
            this.request_lbl.TabIndex = 1;
            this.request_lbl.Text = "Who are you?";
            // 
            // server_request_btn
            // 
            this.server_request_btn.Location = new System.Drawing.Point(254, 204);
            this.server_request_btn.Name = "server_request_btn";
            this.server_request_btn.Size = new System.Drawing.Size(100, 30);
            this.server_request_btn.TabIndex = 2;
            this.server_request_btn.Text = "I am server";
            this.server_request_btn.UseVisualStyleBackColor = true;
            this.server_request_btn.Click += new System.EventHandler(this.server_request_btn_Click);
            // 
            // client_request_btn
            // 
            this.client_request_btn.Location = new System.Drawing.Point(452, 204);
            this.client_request_btn.Name = "client_request_btn";
            this.client_request_btn.Size = new System.Drawing.Size(100, 30);
            this.client_request_btn.TabIndex = 3;
            this.client_request_btn.Text = "I am client";
            this.client_request_btn.UseVisualStyleBackColor = true;
            this.client_request_btn.Click += new System.EventHandler(this.client_request_btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.client_request_btn);
            this.Controls.Add(this.server_request_btn);
            this.Controls.Add(this.request_lbl);
            this.Controls.Add(this.header_lbl);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header_lbl;
        private System.Windows.Forms.Label request_lbl;
        private System.Windows.Forms.Button server_request_btn;
        private System.Windows.Forms.Button client_request_btn;
    }
}

