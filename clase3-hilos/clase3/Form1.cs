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
        private volatile bool isRunning1 = false;
        private volatile bool isRunning2 = false;

        public void contador1()
        {
            i1 = 0;
            isRunning1 = true;
            while (i1 <= 1000 && isRunning1)
            {
                Thread.Sleep(1);
                SafeUpdateLabel(label1, i1.ToString());
                i1++;
            }
        }

        public void contador2()
        {
            i2 = 0;
            isRunning2 = true;
            while (i2 <= 1000 && isRunning2)
            {
                Thread.Sleep(1);
                SafeUpdateLabel(label2, i2.ToString());
                i2++;
            }
        }

        private void SafeUpdateLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                try
                {
                    // Si el hilo no es el principal, invoca en el hilo principal.
                    label.Invoke(new Action(() => label.Text = text));
                }
                catch (ObjectDisposedException)
                {
                    // Si el control ya fue destruido, ignorar la actualización.
                }
                catch (InvalidOperationException)
                {
                    // Si el control ya no está disponible, ignorar la actualización.
                }
            }
            else
            {
                // Si ya estamos en el hilo principal, actualiza directamente.
                label.Text = text;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (p1 == null || !p1.IsAlive)
            {
                p1 = new Thread(new ThreadStart(contador1));
                p1.IsBackground = true; // Configurar como hilo en segundo plano.
                p1.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (p2 == null || !p2.IsAlive)
            {
                p2 = new Thread(new ThreadStart(contador2));
                p2.IsBackground = true; // Configurar como hilo en segundo plano.
                p2.Start();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Detener los hilos de manera segura al cerrar el formulario.
            isRunning1 = false;
            isRunning2 = false;

       
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {
            // No se recomienda deshabilitar la validación de hilos.
            // CheckForIllegalCrossThreadCalls = false;
        }
    }
}
