using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace VichMatLfb3_4

{
    public class LeastSquare
    {
        public double[] x;
        public double[] y;
        public double[] Answer = new double[4];
        public int RowCount = 4, ColumCount = 4;

        public double LeastSquaresSolution3(double Chislo)
        {
            int n = x.Length;
            double[,] coefficientsMatrix = { {Summ(3), Summ(2), Summ(1), n },
                                            {Summ(4),Summ(3),Summ(2),Summ(1) },
                                            {Summ(5),Summ(4),Summ(3),Summ(2) },
                                            {Summ(6),Summ(5),Summ(4),Summ(3) } };

            double[] constantsVector = { y.Sum(), TwoSumm(1), TwoSumm(2), TwoSumm(3) };

            SolveMatrix(coefficientsMatrix, constantsVector);

            return (float)(Math.Pow(Chislo, 3) * Answer[0] + Math.Pow(Chislo, 2) * Answer[1] + Answer[2] * Chislo + Answer[3]);
        }

        public double LeastSquaresSolution2(double Chislo)
        {
            int n = x.Length;
            double[,] coefficientsMatrix = { {Summ(2), Summ(1), n },
                                            {Summ(3),Summ(2),Summ(1) },
                                            {Summ(4),Summ(3),Summ(2) } };

            double[] constantsVector = { y.Sum(), TwoSumm(1), TwoSumm(2) };

            SolveMatrix(coefficientsMatrix, constantsVector);

            return (double)(Math.Pow(Chislo, 2) * Answer[0] + Math.Pow(Chislo, 1) * Answer[1] + Answer[2]);
        }


        public double LeastSquaresSolution1(double Chislo)
        {
            int n = x.Length;
            double[,] coefficientsMatrix = { {Summ(1), n },
                                            {Summ(2),Summ(1) },};

            double[] constantsVector = { y.Sum(), TwoSumm(1) };

            SolveMatrix(coefficientsMatrix, constantsVector);

            return (double)(Math.Pow(Chislo, 1) * Answer[0] + Answer[1]);
        }
        public double Summ(int stepen)
        {
            double To_Return = 0;
            for (int i = 0; i < x.Length; i++)
            {
                To_Return += (double)Math.Pow(x[i], stepen);
            }
            return To_Return;
        }

        public double TwoSumm(int stepen)
        {
            double To_Return = 0;
            for (int i = 0; i < x.Length; i++)
            {
                To_Return += ((double)Math.Pow(x[i], stepen) * y[i]);
            }
            return To_Return;
        }



        public void SolveMatrix(double[,] Matrix, double[] RightPart)
        {
            for (int i = 0; i < RowCount - 1; i++)
            {
                SortRows(i, Matrix, RightPart);
                for (int j = i + 1; j < RowCount; j++)
                {
                    if (Matrix[i, i] != 0) //если главный элемент не 0, то производим вычисления
                    {
                        double MultElement = Matrix[j, i] / Matrix[i, i];
                        for (int k = i; k < ColumCount; k++)
                            Matrix[j, k] -= Matrix[i, k] * MultElement;
                        RightPart[j] -= RightPart[i] * MultElement;
                    }
                }
            }
            //решение
            for (int i = (int)(RowCount - 1); i >= 0; i--)
            {
                Answer[i] = RightPart[i];
                for (int j = (int)(RowCount - 1); j > i; j--)
                    Answer[i] -= Matrix[i, j] * Answer[j];
                Answer[i] /= Matrix[i, i];
            }
        }


        private void SortRows(int SortIndex, double[,] Matrix, double[] RightPart)
        {

            double MaxElement = Matrix[SortIndex, SortIndex];
            int MaxElementIndex = SortIndex;
            for (int i = SortIndex + 1; i < RowCount; i++)
            {
                if (Matrix[i, SortIndex] > MaxElement)
                {
                    MaxElement = Matrix[i, SortIndex];
                    MaxElementIndex = i;
                }
            }

            if (MaxElementIndex > SortIndex)//если это не первый элемент
            {
                double Temp;

                Temp = RightPart[MaxElementIndex];
                RightPart[MaxElementIndex] = RightPart[SortIndex];
                RightPart[SortIndex] = Temp;

                for (int i = 0; i < ColumCount; i++)
                {
                    Temp = Matrix[MaxElementIndex, i];
                    Matrix[MaxElementIndex, i] = Matrix[SortIndex, i];
                    Matrix[SortIndex, i] = Temp;
                }
            }
        }

    }
}
