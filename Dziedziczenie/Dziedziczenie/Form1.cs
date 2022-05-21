using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace Dziedziczenie
{
    public partial class Form1 : Form
    {
        public Pilka pilka;
        public cegla Cegla, Cegla1, Cegla2;
        public List<cegla> cegly = new List<cegla>();
        public static Random losuj = new Random();
        public int rzedy = losuj.Next(5, 9);

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pilka = new Pilka("pilka.png", panel1.Width/2, panel1.Height-50);
            pilka.Parent = panel1;
            pilka.Visible = true;
            timer1.Stop();
            sredniToolStripMenuItem.BackColor = Color.Green;
            for (int i=0; i<rzedy; i++)
            {
                if (i % 2 == 1)
                    cegly.Add(new cegla(panel1, 128, 32, i * 32, -64, pilka));
                for (int k=0; k<panel1.Width / 64+1; k++)
                {
                    if(i%2==1)
                    {
                        cegly.Add(new cegla(panel1, 128, 32, i * 32, (k * 64) + 64, pilka));
                        k++;
                    }
                    if (k%2==0)
                    {
                        cegly.Add(new cegla(panel1, 128, 32, i * 32, k * 64, pilka));
                    }
                }
            }
            cegly.Add(new cegla(panel1, 128, 16, 425, 575, pilka));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                timer1.Stop();
            else 
                timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer1.Enabled) 
            {
                if (e.KeyCode == Keys.D)
                {
                    if (cegly.Last().Left <= 1070)
                        cegly.Last().Left += 50;
                }
                else if (e.KeyCode == Keys.A)
                {
                    if (cegly.Last().Left >= 0)
                        cegly.Last().Left -= 50;
                }
                else return;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string plik = "plik.txt";
            using (StreamWriter sw = File.CreateText(plik))
            {
                sw.WriteLine(pilka.Left); //pilka left
                sw.WriteLine(pilka.Top); //pilka top
                sw.WriteLine(cegly.Last().Left); //platforma left
                sw.WriteLine(cegly.Last().Top); //platforma top
                sw.WriteLine(timer1.Interval); //poziom trudnosci
                for(int i = 0; i < cegly.Count()-1; i++)
                {
                    sw.WriteLine(cegly[i].Left);
                    sw.WriteLine(cegly[i].Top);
                }
            }
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (cegla i in cegly.ToList())
            {
                i.Visible = false;
                cegly.Remove(i);
            }
            pilka.Visible = false;
            pilka = null;
            this.BeginInvoke((MethodInvoker)delegate {
                pilka = new Pilka("pilka.png", panel1.Width / 2, panel1.Height - 50);
                pilka.Parent = panel1;
                pilka.Visible = true;
                timer1.Stop();
                rzedy = losuj.Next(5, 9);
                latwyToolStripMenuItem.BackColor = Color.Transparent;
                sredniToolStripMenuItem.BackColor = Color.Green;
                trudnyToolStripMenuItem.BackColor = Color.Transparent;
                timer1.Interval = 16;
                for (int i = 0; i < rzedy; i++)
                {
                    if (i % 2 == 1)
                        cegly.Add(new cegla(panel1, 128, 32, i * 32, -64, pilka));
                    for (int k = 0; k < panel1.Width / 64+1; k++)
                    {
                        if (i % 2 == 1)
                        {
                            cegly.Add(new cegla(panel1, 128, 32, i * 32, (k * 64) + 64, pilka));
                            k++;
                        }
                        if (k % 2 == 0)
                        {
                            cegly.Add(new cegla(panel1, 128, 32, i * 32, k * 64, pilka));
                        }
                    }
                }
                cegly.Add(new cegla(panel1, 128, 16, 425, 575, pilka));
            });
        }

        private void latwyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            latwyToolStripMenuItem.BackColor = Color.Green;
            sredniToolStripMenuItem.BackColor = Color.Transparent;
            trudnyToolStripMenuItem.BackColor = Color.Transparent;
            timer1.Interval = 22;
        }

        private void sredniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            latwyToolStripMenuItem.BackColor = Color.Transparent;
            sredniToolStripMenuItem.BackColor = Color.Green;
            trudnyToolStripMenuItem.BackColor = Color.Transparent;
            timer1.Interval = 16;
        }

        private void trudnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            latwyToolStripMenuItem.BackColor = Color.Transparent;
            sredniToolStripMenuItem.BackColor = Color.Transparent;
            trudnyToolStripMenuItem.BackColor = Color.Green;
            timer1.Interval = 14;
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(new FileInfo("plik.txt").Length != 0)
            {
                foreach (cegla i in cegly.ToList())
                {
                    i.Visible = false;
                    cegly.Remove(i);
                }
                pilka.Visible = false;
                pilka = null;
                string plik = "plik.txt";
                string[] liniaPliku = File.ReadAllLines(plik);
                this.BeginInvoke((MethodInvoker)delegate {
                    pilka = new Pilka("pilka.png", int.Parse(liniaPliku[0]), int.Parse(liniaPliku[1]) + 50);
                    pilka.Parent = panel1;
                    pilka.Visible = true;
                    timer1.Stop();
                    timer1.Interval = int.Parse(liniaPliku[4]);
                    if (timer1.Interval == 22)
                    {
                        latwyToolStripMenuItem.BackColor = Color.Green;
                        sredniToolStripMenuItem.BackColor = Color.Transparent;
                        trudnyToolStripMenuItem.BackColor = Color.Transparent;
                    }
                    if (timer1.Interval == 16)
                    {
                        latwyToolStripMenuItem.BackColor = Color.Transparent;
                        sredniToolStripMenuItem.BackColor = Color.Green;
                        trudnyToolStripMenuItem.BackColor = Color.Transparent;
                    }
                    if (timer1.Interval == 14)
                    {
                        latwyToolStripMenuItem.BackColor = Color.Transparent;
                        sredniToolStripMenuItem.BackColor = Color.Transparent;
                        trudnyToolStripMenuItem.BackColor = Color.Green;
                    }
                    for (int i = 5; i < (File.ReadLines("plik.txt").Count() - 1); i++)
                    {
                        if (i % 2 == 1)
                            cegly.Add(new cegla(panel1, 128, 32, int.Parse(liniaPliku[i + 1]), int.Parse(liniaPliku[i]), pilka));
                    }
                    cegly.Add(new cegla(panel1, 128, 16, int.Parse(liniaPliku[3]), int.Parse(liniaPliku[2]), pilka));
                });
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var i in cegly)
            {
                if (i.kolizja())
                {
                    if(i!=cegly.Last())
                    {
                        i.Visible = false;
                        cegly.Remove(i);
                    }
                    break;
                }    
            }
            pilka.przemieszczanie();
        }
    }
}
