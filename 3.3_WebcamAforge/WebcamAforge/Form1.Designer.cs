namespace WebcamAforge
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_resolution = new System.Windows.Forms.ComboBox();
            this.cb_webcam = new System.Windows.Forms.ComboBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.picWebcam = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebcam)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.cb_resolution);
            this.panel1.Controls.Add(this.cb_webcam);
            this.panel1.Controls.Add(this.btn_stop);
            this.panel1.Controls.Add(this.btn_start);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 70);
            this.panel1.TabIndex = 0;
            // 
            // cb_resolution
            // 
            this.cb_resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_resolution.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cb_resolution.FormattingEnabled = true;
            this.cb_resolution.Location = new System.Drawing.Point(303, 23);
            this.cb_resolution.Name = "cb_resolution";
            this.cb_resolution.Size = new System.Drawing.Size(171, 27);
            this.cb_resolution.TabIndex = 5;
            // 
            // cb_webcam
            // 
            this.cb_webcam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_webcam.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cb_webcam.FormattingEnabled = true;
            this.cb_webcam.Location = new System.Drawing.Point(33, 23);
            this.cb_webcam.Name = "cb_webcam";
            this.cb_webcam.Size = new System.Drawing.Size(264, 27);
            this.cb_webcam.TabIndex = 4;
            this.cb_webcam.SelectedIndexChanged += new System.EventHandler(this.cb_webcam_SelectedIndexChanged);
            // 
            // btn_stop
            // 
            this.btn_stop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop.Location = new System.Drawing.Point(574, 23);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(88, 28);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.Location = new System.Drawing.Point(480, 23);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(88, 28);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // picWebcam
            // 
            this.picWebcam.BackColor = System.Drawing.Color.White;
            this.picWebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWebcam.Location = new System.Drawing.Point(0, 70);
            this.picWebcam.Name = "picWebcam";
            this.picWebcam.Size = new System.Drawing.Size(800, 380);
            this.picWebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picWebcam.TabIndex = 26;
            this.picWebcam.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picWebcam);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWebcam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picWebcam;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.ComboBox cb_webcam;
        private System.Windows.Forms.ComboBox cb_resolution;
    }
}

