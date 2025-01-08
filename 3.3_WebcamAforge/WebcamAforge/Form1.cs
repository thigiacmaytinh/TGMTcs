//CÔNG TY TNHH GIẢI PHÁP THỊ GIÁC MÁY TÍNH
//support@viscomsolution.com
//0939.825.125


using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;

namespace WebcamAforge
{
    public partial class Form1 : Form
    {
        VideoCaptureDevice m_videoSource;
        Bitmap m_bmp;
        double m_aspect = 1.0;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCamera();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopWebcam();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void InitCamera()
        {
            cbCamera.Items.Clear();

            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videosources.Count == 0)
            {                
                return;
            }


            for (int i = 0; i < videosources.Count; i++)
            {
                cbCamera.Items.Add(videosources[i].Name);
            }
            cbCamera.Enabled = true;
            if (cbCamera.Items.Count == 1)
            {
                cbCamera.SelectedIndex = 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_start_Click(object sender, EventArgs e)
        {
            StartWebcam();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_stop_Click(object sender, EventArgs e)
        {
            StopWebcam();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void StartWebcam()
        {
            if (cbCamera.Items.Count == 0 || cbCamera.SelectedIndex == -1)
                return;
            if (m_videoSource != null)
            {
                m_videoSource.Stop();
            }
            else
            {
                FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                m_videoSource = new VideoCaptureDevice(videosources[cbCamera.SelectedIndex].MonikerString);
                m_videoSource.VideoResolution = selectResolution(m_videoSource); ;
            }

            m_videoSource.NewFrame += new NewFrameEventHandler(OnCameraFrame);
            m_videoSource.Start();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        VideoCapabilities selectResolution(VideoCaptureDevice device)
        {
            foreach (var cap in device.VideoCapabilities)
            {
                if (cap.FrameSize.Height == 1080)
                {
                    m_aspect = (double)cap.FrameSize.Width / cap.FrameSize.Height;
                    return cap;
                }

                if (cap.FrameSize.Width == 1920)
                {
                    m_aspect = (double)cap.FrameSize.Width / cap.FrameSize.Height;
                    return cap;
                }
            }

            VideoCapabilities videoCap = device.VideoCapabilities.Last();
            m_aspect = (double)videoCap.FrameSize.Width / videoCap.FrameSize.Height;
            return videoCap;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void StopWebcam()
        {
            if (m_videoSource != null)
            {
                m_videoSource.Stop();
            }
            m_videoSource = null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void OnCameraFrame(object sender, NewFrameEventArgs eventArgs)
        { 
            //m_bmp = (Bitmap)eventArgs.Frame.Clone();
            picWebcam.Image = new Bitmap(eventArgs.Frame);
        }

        
    }
}
