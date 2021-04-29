using System;
using System.Drawing;

namespace CG_4
{
    internal class CheckLines
    {
        private int vector_mult(int ax, int ay, int bx, int by)
        {
            return ax * by - bx * ay;
        }

        private bool areCrossing(Point p1, Point p2, Point p3, Point p4)
        {
            int v1 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p1.X - p3.X, p1.Y - p3.Y);
            int v2 = vector_mult(p4.X - p3.X, p4.Y - p3.Y, p2.X - p3.X, p2.Y - p3.Y);
            int v3 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p3.X - p1.X, p3.Y - p1.Y);
            int v4 = vector_mult(p2.X - p1.X, p2.Y - p1.Y, p4.X - p1.X, p4.Y - p1.Y);
            if (((v1 < 0 && v2 > 0) || (v2 < 0 && v1 > 0)) && ((v3 < 0 && v4 > 0) || (v4 < 0 && v3 > 0)))
                return true;
            return false;
        }

        public Point[] areCrossing(Point p1, Point p2, Rectangle rectangle)
        {
            Point[] result = new Point[2];
            Point rec_p1 = new Point(rectangle.X, rectangle.Y);
            Point rec_p2 = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            Point rec_p3 = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            Point rec_p4 = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            int iter = 0;
            if (areCrossing(p1, p2, rec_p1, rec_p2))
                result[iter++] = CrossingPoint(p1, p2, rec_p1, rec_p2);
            if (areCrossing(p1, p2, rec_p1, rec_p3))
                result[iter++] = CrossingPoint(p1, p2, rec_p1, rec_p3);
            if (areCrossing(p1, p2, rec_p4, rec_p2))
                result[iter++] = CrossingPoint(p1, p2, rec_p4, rec_p2);
            if (areCrossing(p1, p2, rec_p4, rec_p3))
                result[iter++] = CrossingPoint(p1, p2, rec_p4, rec_p3);
            return result;
        }

        private int A, B, C;

        public void LineEquation(Point p1, Point p2)
        {
            A = p2.Y - p1.Y;
            B = p1.X - p2.X;
            C = -p1.X * (p2.Y - p1.Y) + p1.Y * (p2.X - p1.X);
        }

        private Point CrossingPoint(Point p1, Point p2, Point p3, Point p4)
        {
            int a1, b1, c1, a2, b2, c2;
            LineEquation(p1, p2);
            a1 = A; b1 = B; c1 = C;
            LineEquation(p3, p4);
            a2 = A; b2 = B; c2 = C;
            Point pt = new Point();
            double d = (double)(a1 * b2 - b1 * a2);
            double dx = (double)(-c1 * b2 + b1 * c2);
            double dy = (double)(-a1 * c2 + c1 * a2);
            pt.X = (int)(dx / d);
            pt.Y = (int)(dy / d);
            return pt;
        }
    }
}