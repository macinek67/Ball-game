using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Dziedziczenie
{
    public class cegla : Button
    {
        public Pilka pilka = null;
        public cegla(Panel panel, int szer, int wys, int top, int left, Pilka pilka)
        {
            this.Parent = panel;
            this.Width = szer;
            this.Height = wys;
            this.Left = left;
            this.Top = top;
            this.pilka = pilka;
            this.Visible = true;
            this.BackColor = Color.DimGray;
        }
        public bool kolizja()
        {
            //czy mozliwa kolizja z bocznymi sciankami
            if (!(pilka.Bottom < this.Top || pilka.Top > this.Bottom))
                if ((pilka.Left < this.Right && pilka.Right > this.Right) || (pilka.Right > this.Left && pilka.Left < this.Left))
                {
                    pilka.dy = -pilka.dy;
                    pilka.dx = -pilka.dx;
                    return true;
                }

            //czy mozliwa kolizja z gorna i dolna scianka
            if (!(pilka.Right < this.Left || pilka.Left > this.Right))
                if ((pilka.Bottom < this.Top && pilka.Top > this.Bottom) || (pilka.Top < this.Bottom && pilka.Bottom > this.Top))
                {
                    
                    pilka.dy = -pilka.dy;
                    return true;
                }
            return false;
        }
    }
}