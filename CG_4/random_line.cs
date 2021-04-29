using System;
using System.Drawing;

namespace CG_4
{
    internal class random_line
    {
        public Point a;
        public Point b;

        public random_line(Random rand)
        {
            a = new Point();
            b = new Point();
            a.X = (rand.Next(0, 500));
            a.Y = (rand.Next(0, 400));
            b.X = (rand.Next(0, 500));
            b.Y = (rand.Next(0, 400));
        }
    }
}