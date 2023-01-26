using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace clase3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int i1, i2;
        Thread p1, p2;

        public void contador1()
        {
            i1= 0;
            while (i1 <= 1000) 
            {
                Thread.Sleep(1);
                label1.Text = ("" + i1);
                label1.Update();
                i1++;
            }
        }
        public void contador2()
        {
            i2 = 0;
            while (i2 <= 1000)
            {
                Thread.Sleep(1);
                label2.Text = ("" + i2);
                label2.Update();
                i2++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            p1 = new Thread(new ThreadStart(contador1));
            p1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p2 = new Thread(new ThreadStart(contador2));
            p2.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
