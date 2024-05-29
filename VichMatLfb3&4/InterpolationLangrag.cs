using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  VichMatLfb3_4

{
    public class InterpolationLangrag
    {

        public double[] X { get; set; }
        public double[] Y { get; set; }

        public double DoAllActions(double x)
        {
            double y = 0;
            for (int i = 0; i < X.Length; i++)
            {
                y += Y[i] * L(i, X, x);
            }
            return y;
        }
        public double L(int Index, double[] X, double x)
        {
            double l = 1;
            for (int i = 0; i < X.Length; i++)
            {
                if (i != Index)
                    l *= (x - X[i]) / (X[Index] - X[i]);
            }
            return l;
        }


    }
}
