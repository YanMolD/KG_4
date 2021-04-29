using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CG_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private static Rectangle re = new Rectangle(10, 10, 100, 100);
        private Rectangle _re = new Rectangle(10, 10, 490, 390);

        private List<random_line> line = new List<random_line>();

        public void Draw(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rand = new Random();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                line.Add(new random_line(rand));
            }
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                gr.DrawLine(new Pen(Color.Black), line[i].a, line[i].b);
                gr.DrawString("a", this.Font, Brushes.Black, line[i].a);
                gr.DrawString("b", this.Font, Brushes.Black, line[i].b);
            }
        }

        private CheckLines easy_alg = new CheckLines();

        public void ReDraw(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                Point[] dot = easy_alg.areCrossing(line[i].a, line[i].b, re);

                gr.DrawString($"a", this.Font, Brushes.Black, line[i].a);
                gr.DrawString($"b", this.Font, Brushes.Black, line[i].b);
                Point point_buf = new Point();
                if (dot[0] == point_buf && dot[1] == point_buf)
                {
                    if (line[i].a.X > re.X && line[i].a.Y > re.Y && line[i].a.X < re.X + re.Width && line[i].a.Y < re.Y + re.Height)
                    {
                        gr.DrawLine(new Pen(Color.Yellow), line[i].b, line[i].a);
                    }
                    else
                        gr.DrawLine(new Pen(Color.Black), line[i].a, line[i].b);
                }
                else
                {
                    if (dot[1] == point_buf && dot[0] != point_buf)
                    {
                        if (line[i].a.X > re.X && line[i].a.Y > re.Y && line[i].a.X < re.X + re.Width && line[i].a.Y < re.Y + re.Height)
                        {
                            gr.DrawLine(new Pen(Color.Yellow), dot[0], line[i].a);
                            gr.DrawLine(new Pen(Color.Black), dot[0], line[i].b);
                        }
                        else
                        {
                            gr.DrawLine(new Pen(Color.Yellow), dot[0], line[i].b);
                            gr.DrawLine(new Pen(Color.Black), dot[0], line[i].a);
                        }
                    }
                    else
                    {
                        gr.DrawLine(new Pen(Color.Yellow), dot[0], dot[1]);
                        if (Math.Abs(line[i].a.X - dot[1].X) > Math.Abs(line[i].a.X - dot[0].X))
                        {
                            gr.DrawLine(new Pen(Color.Black), line[i].a, dot[0]);
                            gr.DrawLine(new Pen(Color.Black), dot[1], line[i].b);
                        }
                        else
                        {
                            gr.DrawLine(new Pen(Color.Black), line[i].a, dot[1]);
                            gr.DrawLine(new Pen(Color.Black), dot[0], line[i].b);
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            drawrect(e.Graphics);
            if (!checkBox1.Checked)
                Draw(e.Graphics);
            if (checkBox1.Checked)
                ReDraw(e.Graphics);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            line.Clear();
            Invalidate();
        }

        public void drawrect(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            gr.DrawRectangle(new Pen(Color.Red), re);
            gr.DrawRectangle(new Pen(Color.Red), _re);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown1.Enabled = false;
            }
            if (!checkBox1.Checked)
            {
                numericUpDown1.Enabled = true;
            }
            Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            re.X = trackBar1.Value;

            Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            re.Y = trackBar2.Value;

            Invalidate();
        }
    }
}