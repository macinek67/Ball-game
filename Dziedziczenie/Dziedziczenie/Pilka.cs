using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Dziedziczenie
{ 
    public class Pilka : PictureBox
    {
        public int dx=5, dy=5;
        private Bitmap MyImage;
        public void przemieszczanie()
        {
            if(Left < 0)
            {
                dx = -dx;
                if (Top < 0)
                    dy = -dy;
            }
            if(Top < 0 && Left > 0)
                dy = -dy;
            if(Bottom > Parent.Height)
                dy = -dy;
            if(Right > Parent.Width && Bottom < Parent.Height)
                dx = -dx;
            this.Left += dx;
            this.Top += dy;
        }
        public Pilka(string plikZRysunkiem, int left, int top)
        {
            this.Width = 50;
            this.Height = 50;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(plikZRysunkiem);
            this.Image = (Image)MyImage;
            this.Left = left;
            this.Top = top-50;
        }
    }
}
