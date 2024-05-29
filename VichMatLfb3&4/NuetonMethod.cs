using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VichMatLfb3_4
{
    public class Newton
    {
        public double[] MasX { get; set; }
        public double[] MasY { get; set; }


        public double DoAllActions(double xi)
        {
            int n = MasX.Length;
            double[] f = new double[n];
            Array.Copy(MasY, f, n);

            for (int j = 1; j < n; j++)
            {
                for (int i = n - 1; i >= j; i--)
                {
                    f[i] = (f[i] - f[i - 1]) / (MasX[i] - MasX[i - j]);
                }
            }

            double result = f[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                result = result * (xi - MasX[i]) + f[i];
            }

            return result;
        }
    }
}
