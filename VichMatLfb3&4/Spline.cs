using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_Least_squares
{
    public class Spline
    {
        private double[] x;
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] d;

        //public double fx(double xi, int j)
        //{


        //    return a[j] + b[j] * xi + c[j] * xi* xi + d[j] * xi * xi * xi;
        //}


        //public double hx(double x, double x1)
        //{
        //    return x1 - x;
        //}

        //public double dxSecond(double t)
        //{
        //    int j = 0;
        //    int n = x.Length;
        //    for (int i = 1; i < n; i++)
        //    {
        //        if (t <= x[i])
        //        {
        //            j = i - 1;
        //            break;
        //        }
        //    }
        //    double dx = t - x[j];
        //    return 2 * c[j] + 6 * d[j] * dx;
        //}
        public double dxSecond(double xi, double h)
        {
            return (Interpolation(xi + (2 * h)) - 2 * Interpolation(xi) + Interpolation(xi - (2 * h))) / (4 * Math.Pow(h, 2));
        }
        public double dxFirst(double xi, double h)
        {
            return (Interpolation(xi + h) - Interpolation(xi - h)) / (2 * h);
        }
        public double Interpolation(double xi)
        {
            int j = 0;
            while (j < x.Length - 1 && xi > x[j + 1])
            {
                j++;
            }

            double dx = xi - x[j];
            return a[j] + b[j] * dx + c[j] * dx * dx + d[j] * dx * dx * dx;
        }

        public void spline(double[] x, double[] y)
        {
            this.x = x;
            // Step 1
            //We know a
            a = y;
            double[] h = new double[x.Length - 1];
            //Step 2
            //We find h for segments
            for (int i = 0; i < x.Length - 1; i++)
            {
                h[i] = x[i + 1] - x[i];
            }
            double[] a_ = new double[x.Length];
            //Step 3 
            //We find alpha for find c in future
            for (int i = 1; i < x.Length - 1; i++)
            {
                a_[i] = 3 * ((a[i + 1] - a[i]) / h[i] - (a[i] - a[i - 1]) / h[i - 1]);
            }
            //Step 4 
            double[] l = new double[x.Length]; l[0] = 1;
            double[] u = new double[x.Length]; u[0] = 0;
            double[] z = new double[x.Length]; z[0] = 0;
            //Step 5
            //We 
            for (int i = 1; i < x.Length - 1; i++)
            {
                l[i] = 2 * (x[i + 1] - x[i - 1]) - h[i - 1] * u[i - 1];
                u[i] = h[i] / l[i];
                z[i] = (a_[i] - h[i - 1] * z[i - 1]) / l[i];
            }
            c = new double[x.Length];
            b = new double[x.Length];
            d = new double[x.Length];
            z[x.Length - 1] = 0;
            l[x.Length - 1] = 1;
            //Step 6
            //We find b,c and d 
            for (int i = x.Length - 2; i >= 0; i--)
            {
                c[i] = z[i] - u[i] * c[i + 1];
                b[i] = (y[i + 1] - y[i]) / h[i] - (c[i + 1] + 2 * c[i]) / 3 * h[i];
                d[i] = (c[i + 1] - c[i]) / 3 / h[i];
            }
        }
    }
}
