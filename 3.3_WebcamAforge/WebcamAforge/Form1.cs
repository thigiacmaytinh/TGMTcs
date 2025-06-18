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
            picWebcam.ImageLocation = "webcam_aforge.jpg";
            LoadWebcam();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopWebcam();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cb_webcam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResolution();
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

        void LoadWebcam()
        {
            cb_webcam.Items.Clear();

            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videosources.Count == 0)
                return;


            for (int i = 0; i < videosources.Count; i++)
            {
                cb_webcam.Items.Add(videosources[i].Name);
            }
            cb_webcam.Enabled = true;
            if (cb_webcam.Items.Count == 1)
            {
                cb_webcam.SelectedIndex = 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void StartWebcam()
        {
            if (cb_webcam.Items.Count == 0 || cb_webcam.SelectedIndex == -1)
                return;
            if (m_videoSource != null)
            {
                m_videoSource.Stop();
            }

            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            m_videoSource = new VideoCaptureDevice(videosources[cb_webcam.SelectedIndex].MonikerString);
            m_videoSource.VideoResolution = selectResolution(m_videoSource, cb_resolution.SelectedIndex);
            

            m_videoSource.NewFrame += new NewFrameEventHandler(OnCameraFrame);
            m_videoSource.Start();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LoadResolution()
        {
            if (cb_webcam.Items.Count == 0)
                return;

            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videosources[cb_webcam.SelectedIndex].MonikerString);

            cb_resolution.Items.Clear();
            foreach (var cap in videoSource.VideoCapabilities)
            {
                cb_resolution.Items.Add(cap.FrameSize.Width + "x" + cap.FrameSize.Height + " (" + cap.MaximumFrameRate + " FPS)");
            }
            if (cb_resolution.Items.Count > 0)
            {
                cb_resolution.SelectedIndex = 0;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        VideoCapabilities selectResolution(VideoCaptureDevice device, int resolutionIndex)
        {
            VideoCapabilities videoCap = device.VideoCapabilities[resolutionIndex];
            if (videoCap == null)
                return null;

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
            picWebcam.Image = new Bitmap(eventArgs.Frame);
        }
    }
}
