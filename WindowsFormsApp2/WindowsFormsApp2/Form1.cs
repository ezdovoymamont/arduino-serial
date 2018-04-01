using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
                comboBox1.Items.Add(ports[i]);
            if(ports.Length > 0)
            {
                comboBox1.SelectedIndex = 0;
               serialPort1.PortName = (string)comboBox1.SelectedItem;
            }
                
            serialPort1.Encoding = Encoding.Default;

            serialPort1.Open();

    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //send from serial port1. Data will be received at port2
            // unsigned char get_req[] = "$GET_ADC,С\r\n";
            string s = textBox2.Text + ",C\r\n";

            char[] arr = s.ToCharArray();
            int c = 0;
            for (int i = 0; i < arr.Length-3; i++)
            {
                  c = (c ^ arr[i]);
            }
            arr[arr.Length - 3] = (char)c;
            //textBox1.Text = textBox1.Text + "\n>" + c + "\n";
            serialPort1.WriteLine(new string(arr));
            //textBox1.Text = textBox1.Text + "\n>" + new string(arr) + "\n";
            //textBox1.Text = textBox1.Text + "[" + (int)arr[arr.Length - 3] + "]\n";
            //MessageBox.Show("отправили данные");
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string str = serialPort1.ReadExisting();

            this.BeginInvoke(new MethodInvoker(delegate {
                textBox1.Text = textBox1.Text + "<" + str + ">";

            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
