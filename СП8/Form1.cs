using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СП8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        public class MyPoints
        {
            int index = 0;
            private Point[] points;

            public MyPoints(int size)
            {
                if (size <= 0)
                { size = 2; }
                points = new Point[size];
            }
            public void Set_Point(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }
            public void Reset_Points()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }


            private bool isMouse = false;

            private MyPoints myPoints = new MyPoints(2);
            Bitmap bmp;
            Graphics g;
            Pen pen = new Pen(Color.Red, 2f);


            private void SetSize()
            {
                Rectangle rectangle = Screen.PrimaryScreen.Bounds;
                bmp = new Bitmap(rectangle.Width, rectangle.Height);
                g = Graphics.FromImage(bmp);

                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse &= false;
            myPoints.Reset_Points();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse)
            {
                return;
            }
            myPoints.Set_Point(e.X, e.Y);
            if (myPoints.GetCountPoints() >= 2)
            {
                g.DrawLines(pen, myPoints.GetPoints());
                pictureBox1.Image = bmp;
                myPoints.Set_Point(e.X, e.Y);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPG(*.JPG)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(sfd.FileName);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bmp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
    }
}