using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeScreenshot
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        public static extern int GetDesktopWindow();
        [DllImport("User32.dll")]
        public static extern int GetWindowDC(int hWnd);
        [DllImport("User32.dll")]
        public static extern int ReleaseDC(int hWnd, int hDC);

        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);
        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);
        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);
        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);
        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);
        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);
        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btn_takeScreenshot_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = TakeScreenshot();
            pictureBox1.Image = bitmap;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Bitmap TakeScreenshot()
        {
            int hdcSrc = GetWindowDC(GetDesktopWindow()), // Get a handle to the desktop window
            hdcDest = CreateCompatibleDC(hdcSrc), // Create a memory device context
            hBitmap = CreateCompatibleBitmap(hdcSrc, // Create a bitmap and place it in the memory DC
            GetDeviceCaps(hdcSrc, 8), GetDeviceCaps(hdcSrc, 10));
            // GDI32.GetDeviceCaps(hdcSrc,8) returns the width of the desktop window
            // GDI32.GetDeviceCaps(hdcSrc,10) returns the height of the desktop window
            SelectObject(hdcDest, hBitmap); // Required to create a color bitmap
            BitBlt(hdcDest, 0, 0, GetDeviceCaps(hdcSrc, 8), // Copy the on-screen image into the memory DC
            GetDeviceCaps(hdcSrc, 10), hdcSrc, 0, 0, 0x00CC0020);
            Bitmap image = new Bitmap(Image.FromHbitmap(new IntPtr(hBitmap)),
                Image.FromHbitmap(new IntPtr(hBitmap)).Width,
                Image.FromHbitmap(new IntPtr(hBitmap)).Height);

            //SaveImageAs(hBitmap, fileName, imageFormat); // Save the screen-capture to the specified file using the designated image format
            // Release the device context resources back to the system
            ReleaseDC(GetDesktopWindow(), hdcSrc);
            DeleteDC(hdcDest);
            DeleteObject(hBitmap);


            return image;
        }
    }
}
