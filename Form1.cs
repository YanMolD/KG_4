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
        static int Counter = 0;

        static int re_w = 100;
        static int re_h = 100;
        static int re_x = 10;
        static int re_y = 10;
        static Rectangle re = new Rectangle(re_x, re_y, re_w, re_h);
        Rectangle _re = new Rectangle(re_x, re_y, 490, 390);
        bool IsClicked = false;

        int deltaX = 0;
        int deltaY = 0;

        List<random_line> line = new List<random_line>();
        public void Draw(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rand = new Random();
            for (int i = 0; i < Counter; i++)
            {
                line.Add(new random_line(rand));
            }
            for (int i = 0; i < Counter; i++)
            {
                gr.DrawLine(new Pen(Color.Black), line[i].a, line[i].b);
                gr.DrawString("a", this.Font, Brushes.Black, line[i].a);
                gr.DrawString("b", this.Font, Brushes.Black, line[i].b);

               /* PointF[] dot = alg.cohensutherland(line[i], _re);
                gr.DrawLine(new Pen(Color.Black), dot[0], dot[1]);
                gr.DrawString("a", this.Font, Brushes.Black, dot[0]);
                gr.DrawString("b", this.Font, Brushes.Black, dot[0]);*/
            }

        }
        cohen_sutherland alg = new cohen_sutherland();
        public void ReDraw(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < Counter; i++)
            {
                PointF[] dot = alg.cohensutherland(line[i], re);
               // Console.WriteLine($"a: {line[i].a} ");

                gr.DrawString($"a", this.Font, Brushes.Black, line[i].a);
                gr.DrawString($"b", this.Font, Brushes.Black, line[i].b);

                if (dot[0] == line[i].a && dot[1] == line[i].b)
                    gr.DrawLine(new Pen(Color.Black), line[i].a, line[i].b);
                else
                {
                    if (mode)
                        gr.DrawLine(new Pen(Color.Yellow), dot[0], dot[1]);
                    gr.DrawLine(new Pen(Color.Black), line[i].a, dot[0]);
                    gr.DrawLine(new Pen(Color.Black), dot[1], line[i].b);
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
            Counter = Convert.ToInt32(numericUpDown1.Value);
            Invalidate();
        }
        public void drawrect(Graphics gr)
        {
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            gr.DrawRectangle(new Pen(Color.Yellow), re);
            gr.DrawRectangle(new Pen(Color.Yellow), _re);
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
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.X < re.X + re.Width) && (e.X > re.X))
                if ((e.Y < re.Y + re.Height) && (e.Y > re.Y))
                {
                    IsClicked = true;
                    deltaX = e.X - re.X;
                    deltaY = e.Y - re.Y;

                }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.X < re.X + re.Width) && (e.X > re.X))
                if ((e.Y < re.Y + re.Height) && (e.Y > re.Y))
                {
                    Cursor.Current = Cursors.Hand;
                }
            if (IsClicked)
            {
                if (!checkBox1.Checked)
                    line.Clear();
                re.X = e.X - deltaX;
                re.Y = e.Y - deltaY;

                Invalidate();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            IsClicked = false;
        }
        bool mode = false;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                mode = true;
            }
            else
                mode = false;
            Invalidate();
        }

    }
}
