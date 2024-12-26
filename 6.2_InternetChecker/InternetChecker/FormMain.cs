using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TGMTcs;
using System.Net.Sockets;
using System.Globalization;
using System.Timers;
using System.Net.NetworkInformation;

namespace InternetChecker
{
    public partial class FormMain : Form
    {
        
        static FormMain m_instance;

        string server = "https://sieuthi.phuoctrung.com";
        string userReceive = "anhvietlienket@gmail.com";
        string secretKey = "OXXBIRALRTDAWGIIIKDQFTAYECVBSANDGVHIQIDO";


        System.Timers.Timer timerChecktime = new System.Timers.Timer();


        bool m_formClosed = false;
        bool m_soundPlayed = false;
        bool m_isUserClose = false;

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public FormMain()
        {
            InitializeComponent();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " " + TGMTutil.GetVersion();
#if DEBUG
            server = "http://localhost";
            userReceive = "vohungvi27@gmail.com";

            this.Text += " (Debug)";
#endif

            timerChecktime.Elapsed += TimerChecktime_Elapsed;
            timerChecktime.Interval = 5 * 1000; //5 seconds
            timerChecktime.Enabled = true;


            CheckInternetTimeAsync();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_isUserClose)
                return;
            e.Cancel = true;
            this.Hide();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public static FormMain GetInstance()
        {
            if (m_instance == null)
                m_instance = new FormMain();
            return m_instance;
        }       

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void UpdateFormText(string windowText)
        {
            if(windowText == "THONG BAO" || windowText == "Page Setup")
            {
                SendEmailDialogShown(windowText);                
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////

        void SendEmailDialogShown(string windowText)
        {
            try
            {
                Bitmap bmp = TGMTwindows.TakeScreenshot();
                bmp.Save(DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".jpg");


                string api = "/api/email/send";
                var values = new Dictionary<string, string>
                {
                    {"title", "Phần mềm POS hiện popup tại máy " + Environment.MachineName},
                    {"userReceive", userReceive},
                    {"purpose", "DialogShown"},
                    {"body", "Chào bạn. Phần mềm POS đã hiện popup " + windowText + " vào lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                    {"imageBase64", TGMTimage.ImageToBase64(bmp) },
                    {"secretKey", secretKey}
                };
                TGMTonline.SendPOSTrequest(server + api, values, OnResponse);

                TGMTsound.PlaySoundAsync("ting-2.mp3");
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void OnResponse(int statusCode, string response)
        {
            //do nothing
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void WriteErrorLog(string ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_install_Click(object sender, EventArgs e)
        {
            if (!File.Exists("TimeChecker.exe"))
            {
                MessageBox.Show("Không tìm thấy file TimeChecker.exe");
                return;

            }
            string cmd = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe";
            cmd += " /K" + Application.StartupPath + "\\TimeChecker.exe";

            Process.Start("cmd.exe", cmd);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void SendEmailProgramStoped()
        {
            if (File.Exists(@"D:\debug.txt"))
                return;

            try
            {
                string api = "/api/email/send";
                var values = new Dictionary<string, string>
                {
                    {"title", "Phần mềm kiểm tra ngày tháng " +  Environment.MachineName + " đã dừng"},
                    {"userReceive", userReceive},
                    {"purpose", "WrongDatetime"},
                    {"body", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                    {"secretKey", secretKey}
                };
                TGMTonline.SendPOSTrequest(server + api, values, OnResponse);

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void TimerChecktime_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerChecktime.Stop();
            CheckInternetTime();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void CheckInternetTimeAsync()
        {
            var t = new Thread(() => CheckInternetTime());
            t.Start();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void CheckInternetTime()
        { 
            bool isConnected = TGMTonline.IsInternetAvailable();

            DisplayResult(isConnected);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void DisplayResult(bool isConnected)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.TopMost = !isConnected;

                if (isConnected)
                {
                    pictureBox1.Image = Properties.Resources.check_mark;
                    m_soundPlayed = false;
                    panel1.Visible = false;
                }
                else
                {
                    this.Show();
                    this.BringToFront();
                    this.WindowState = FormWindowState.Normal;

                    pictureBox1.Image = Properties.Resources.check_error;

                    if(!m_soundPlayed)
                    {
                        m_soundPlayed = true;
                        TGMTsound.PlaySoundAsync("FAIL.mp3");
                    }

                    panel1.Visible = true;

                }

                timerChecktime.Start();
            }));
            
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void SendEmailWrongTime()
        {
            string api = "/api/email/send";
            var values = new Dictionary<string, string>
                {
                    {"title", "Máy tính " +  Environment.MachineName + " sai ngày tháng"},
                    {"userReceive", userReceive},
                    {"purpose", "WrongDatetime"},
                    {"body", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                    {"secretKey", secretKey}
                };
            TGMTonline.SendPOSTrequest(server + api, values, OnResponse);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_isUserClose = true;
            timerChecktime.Stop();
            notifyIcon1.Dispose();
            Application.Exit();
        }
    }
}
