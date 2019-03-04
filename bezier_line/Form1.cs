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

namespace bezier_line
{
    public partial class Form1 : Form
    {
        Graphics g;
        Rectangle rec1 = new Rectangle(50, 50, 10, 10);
        Rectangle rec2 = new Rectangle(100, 100, 10, 10);
        Rectangle rec3 = new Rectangle(150, 150, 10, 10);
        Rectangle rec4 = new Rectangle(160, 160, 10, 10);
        Rectangle selected;
        Rectangle buffer;
        List<Rectangle> rect = new List<Rectangle> { };
        Pen pen = new Pen(Brushes.Blue);
        Point p = new Point();
        List<Point> po = new List<Point> { };
        public bool drag = false;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            g = this.CreateGraphics();

            rect.Add(rec1);
            rect.Add(rec2);
            rect.Add(rec3);
            rect.Add(rec4);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            foreach (Rectangle r in rect)
            {
                g.FillRectangle(Brushes.Black, r);
            }
            g.FillRectangle(Brushes.White, buffer);
            g.FillRectangle(Brushes.Black, selected);

            for (int i = 0; i <= po.Count - 2; i++)
            {
                g.DrawLine(pen, po[i], po[i + 1]);
            }


        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            foreach (Rectangle r in rect)
            {
                if (e.X >= r.X && e.X <= r.X + r.Width && e.Y >= r.Y && e.Y <= e.Y + r.Height)
                {
                    selected = r;
                    buffer = r;
                }
            }
            Invalidate();


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                selected.Location = e.Location;
            }
            Invalidate();
        }



        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            for (int i = 0; i <= rect.Count - 1; i++)
            {
                if (rect[i] == buffer)
                {
                    rect[i] = selected;
                }
            }
            bezier();



        }
        public void bezier()
        {
            po.Clear();
            for (float t = 0; t <= 1.0; t = Convert.ToSingle(t + 0.01))
            {
                float x = (-t * t * t + 3 * t * t - 3 * t + 1) * rect[0].X
                    + (3 * t * t * t - 6 * t * t + 3 * t) * rect[1].X
                    + (-3 * t * t * t + 3 * t * t) * rect[2].X
                    + (t * t * t) * rect[3].X;
                float y = (-t * t * t + 3 * t * t - 3 * t + 1) * rect[0].Y
                    + (3 * t * t * t - 6 * t * t + 3 * t) * rect[1].Y
                    + (-3 * t * t * t + 3 * t * t) * rect[2].Y
                    + (t * t * t) * rect[3].Y;
                p = (Point.Round(new PointF(x, y)));
                po.Add(p);

            }
        }
    }
}
