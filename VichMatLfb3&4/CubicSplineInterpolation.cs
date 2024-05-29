    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace VichMatLfb3_4
    {

            public class CubicSplineInterpolation
            {
                private double[] xs, ys, a, b, c, d;

                public CubicSplineInterpolation(double[] xs, double[] ys)
                {
                    int n = xs.Length;
                    this.xs = xs;
                    this.ys = ys;
                    a = new double[n - 1];
                    b = new double[n - 1];
                    c = new double[n];
                    d = new double[n - 1];

                    double[] h = new double[n - 1];
                    double[] alpha = new double[n - 1];

                    for (int i = 0; i < n - 1; i++)
                    {
                        h[i] = xs[i + 1] - xs[i];
                        alpha[i] = (ys[i + 1] - ys[i]) / h[i];
                    }

                    double[] l = new double[n];
                    double[] mu = new double[n];
                    double[] z = new double[n];

                    l[0] = 1;
                    mu[0] = z[0] = 0;

                    for (int i = 1; i < n - 1; i++)
                    {
                        l[i] = 2 * (xs[i + 1] - xs[i - 1]) - h[i - 1] * mu[i - 1];
                        mu[i] = h[i] / l[i];
                        z[i] = (alpha[i] - alpha[i - 1]) / l[i];
                    }

                    l[n - 1] = 1;
                    z[n - 1] = c[n - 1] = 0;

                    for (int j = n - 2; j >= 0; j--)
                    {
                        c[j] = z[j] - mu[j] * c[j + 1];
                        b[j] = (ys[j + 1] - ys[j]) / h[j] - h[j] * (c[j + 1] + 2 * c[j]) / 3;
                        d[j] = (c[j + 1] - c[j]) / (3 * h[j]);
                        a[j] = ys[j];
                    }
                }

                public double Interpolate(double x)
                {
                    int i = Array.BinarySearch(xs, x);
                    if (i < 0) i = ~i - 1;
                    if (i >= xs.Length - 1) i = xs.Length - 2;

                    double dx = x - xs[i];
                    return a[i] + b[i] * dx + c[i] * dx * dx + d[i] * dx * dx * dx;
                }

                public double FirstDerivative(double x)
                {
                    int i = Array.BinarySearch(xs, x);
                    if (i < 0) i = ~i - 1;
                    if (i >= xs.Length - 1) i = xs.Length - 2;

                    if (i == 0)
                    {
                        // Граничный случай для первой точки
                        return (Interpolate(xs[i + 1]) - Interpolate(xs[i])) / (xs[i + 1] - xs[i]);
                    }
                    else if (i == xs.Length - 1)
                    {
                        // Граничный случай для последней точки
                        return (Interpolate(xs[i]) - Interpolate(xs[i - 1])) / (xs[i] - xs[i - 1]);
                    }
                    else
                    {
                        // Общий случай
                        return (Interpolate(xs[i + 1]) - Interpolate(xs[i - 1])) / (xs[i + 1] - xs[i - 1]);
                    }
                }

                public double SecondDerivative(double x)
                {
                    int i = Array.BinarySearch(xs, x);
                    if (i < 0) i = ~i - 1;
                    if (i >= xs.Length - 1) i = xs.Length - 2;

                    if (i == 0)
                    {
                        // Граничный случай для первой точки
                        return (FirstDerivative(xs[i + 1]) - FirstDerivative(xs[i])) / (xs[i + 1] - xs[i]);
                    }
                    else if (i == xs.Length - 1)
                    {
                        // Граничный случай для последней точки
                        return (FirstDerivative(xs[i]) - FirstDerivative(xs[i - 1])) / (xs[i] - xs[i - 1]);
                    }
                    else
                    {
                        // Общий случай
                        return (FirstDerivative(xs[i + 1]) - FirstDerivative(xs[i - 1])) / (xs[i + 1] - xs[i - 1]);
                    }
                }
            }
        }
