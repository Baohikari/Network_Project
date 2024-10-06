namespace Network_Project
{
    partial class Server_Form
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
            this.streaming_screen = new System.Windows.Forms.PictureBox();
            this.start_streaming_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.streaming_screen)).BeginInit();
            this.SuspendLayout();
            // 
            // streaming_screen
            // 
            this.streaming_screen.Location = new System.Drawing.Point(12, 12);
            this.streaming_screen.Name = "streaming_screen";
            this.streaming_screen.Size = new System.Drawing.Size(776, 299);
            this.streaming_screen.TabIndex = 0;
            this.streaming_screen.TabStop = false;
            // 
            // start_streaming_btn
            // 
            this.start_streaming_btn.Location = new System.Drawing.Point(340, 367);
            this.start_streaming_btn.Name = "start_streaming_btn";
            this.start_streaming_btn.Size = new System.Drawing.Size(118, 44);
            this.start_streaming_btn.TabIndex = 1;
            this.start_streaming_btn.Text = "Start Streaming";
            this.start_streaming_btn.UseVisualStyleBackColor = true;
            this.start_streaming_btn.Click += new System.EventHandler(this.start_streaming_btn_Click);
            // 
            // Server_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.start_streaming_btn);
            this.Controls.Add(this.streaming_screen);
            this.Name = "Server_Form";
            this.Text = "Server Form";
            ((System.ComponentModel.ISupportInitialize)(this.streaming_screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox streaming_screen;
        private System.Windows.Forms.Button start_streaming_btn;
    }
}