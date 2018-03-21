using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace boundballs
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        BounsBall[] bb;

        public Form1()
        {
            InitializeComponent();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("boundballs.Resources.ball.bmp");
            bmp = new Bitmap(stream);
            bmp.MakeTransparent(Color.FromArgb(255, 255, 255));

            Random r = new Random();

            bb = new BounsBall[100];
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i] = new BounsBall(bmp.Size.Width, bmp.Size.Height,
                    r.Next(panel1.Size.Width - bmp.Size.Width), r.Next(panel1.Size.Height - bmp.Size.Width));

            }

            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            bmp.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
            for (int i = 0; i < bb.Length; i++)
            {
                e.Graphics.DrawImage(bmp,
                    new Point(bb[i].getX(), bb[i].getY()));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i].move(panel1.Size.Width, panel1.Size.Height);
            }

            panel1.Invalidate();
        }
    }
    class BounsBall
    {
        int x, y;
        int bw, bh;
        int dx, dy;
        static Random r = new Random();
        public BounsBall(int _bw, int _bh, int _x, int _y)
        {
            x = _x;
            y = _y;
            bw = _bw;
            bh = _bh;
            int[] d = { -10, -8, -6, -4, -2, 2, 4, 6, 8, 10 };
            //int[] d = { 0,0,0,0,0, 0,0,0,0,0  };

            dx = d[r.Next(10)];
            dy = d[r.Next(10)];
        }

        public void move(int width, int height)
        {
            if (x < 0)
            {
                dx *= -1;
            }
            else if (x > width - bw)
            {
                dx *= -1;
            }
            if (y < 0)
            {
                dy *= -1;
            }
            else if (y > height - bh)
            {
                dy *= -1;
            }
            x += dx;
            y += dy;
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }

    }
}
