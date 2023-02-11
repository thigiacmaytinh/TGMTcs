using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace ComPort
{
    public partial class Form1 : Form
    {
        static SerialPort _serialPort = new SerialPort();
        Thread readThread;

        ////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();

            //Giá trị mặc định
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;

            readThread = new Thread(Read);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        // hiện danh sách các cổng trong máy
        void SetPortName(string portName)
        {

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                MessageBox.Show("Không đúng tên cổng");
            }

            _serialPort.Close();
            _serialPort.PortName = portName;            
            _serialPort.Open();

            MessageBox.Show("Đã chọn cổng " + portName);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        // hiện danh sách port lên combobox
        void ShowPortList()
        {
            string[] portList = SerialPort.GetPortNames();
            if (portList.Length == 0)
            {
                cbPorts.Text = "Không tìm thấy cổng";
                cbPorts.Enabled = false;
                return;
            }

            foreach (string s in portList)
            {
                cbPorts.Items.Add(s);
            }            
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowPortList();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        private void cbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPortName(cbPorts.SelectedItem.ToString());
            textBox1.Enabled = true;
            txtSend.Enabled = true;
            btnSend.Enabled = true;

            readThread.Start();
            readThread.Abort();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        void Read()
        {
            while (true)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    textBox1.Text += message;
                }
                catch (TimeoutException) { }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        //gửi dữ liệu qua cổng COM
        void Write(string text)
        {
            if (text == "")
                return;

            _serialPort.WriteLine(text);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _serialPort.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSend.Text == "")
            {
                MessageBox.Show("Text rỗng");
                return;
            }

            Write(txtSend.Text);
            txtSend.Clear();
            readThread.Abort();
        }
    }
}
