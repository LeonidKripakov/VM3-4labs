using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace VichMatLfb3_4
{
    public partial class Form1 : Form
    {
        private double[] x = new double[5];
        private double[] y = new double[5];
        private double[] z = new double[5];
        private double[] c = new double[5];
        private bool isDragging = false;
        private int lastX, lastY;


        public Form1()
        {
            InitializeComponent();
            chart1.MouseWheel += Chart1_MouseWheel;
            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < x.Length; i++)
            {
                chart1.Series[0].Points.AddXY(x[i], y[i]);
            }

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.MouseWheel += Chart1_MouseWheel;
        }

        private void Chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = sender as Chart;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            double xMin = xAxis.ScaleView.ViewMinimum;
            double xMax = xAxis.ScaleView.ViewMaximum;
            double yMin = yAxis.ScaleView.ViewMinimum;
            double yMax = yAxis.ScaleView.ViewMaximum;

            if (e.Delta < 0) // Scrolled down
            {
                xAxis.ScaleView.ZoomReset();
                yAxis.ScaleView.ZoomReset();
            }
            else if (e.Delta > 0) // Scrolled up
            {
                double posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 6;
                double posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 6;
                double posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 6;
                double posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 6;

                xAxis.ScaleView.Zoom(posXStart, posXFinish);
                yAxis.ScaleView.Zoom(posYStart, posYFinish);
            }
        }

        private void Chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastX = e.X;
                lastY = e.Y;
            }
        }

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var chart = sender as Chart;
                var xAxis = chart.ChartAreas[0].AxisX;
                var yAxis = chart.ChartAreas[0].AxisY;

                double xShift = xAxis.PixelPositionToValue(lastX) - xAxis.PixelPositionToValue(e.X);
                double yShift = yAxis.PixelPositionToValue(lastY) - yAxis.PixelPositionToValue(e.Y);

                xAxis.ScaleView.Scroll(xAxis.ScaleView.Position + xShift);
                yAxis.ScaleView.Scroll(yAxis.ScaleView.Position + yShift);

                lastX = e.X;
                lastY = e.Y;
            }
        }

        private void Chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            LeastSquare Kv3 = new LeastSquare() { x = this.x, y = this.y, RowCount = 4, ColumCount = 4 };
            LeastSquare Kv2 = new LeastSquare() { x = this.x, y = this.y, RowCount = 3, ColumCount = 3 };
            LeastSquare Kv1 = new LeastSquare() { x = this.x, y = this.y, RowCount = 2, ColumCount = 2 };
            InterpolationLangrag lan = new InterpolationLangrag() { X = x, Y = y };
            Kv1.LeastSquaresSolution1(1);
            double g = x.Min();
            chart1.Series[0].Points.Clear();
            while (g < x.Max())
            {
                this.chart1.Series[0].Points.AddXY(g, Kv3.LeastSquaresSolution3(g));
                g += 0.01;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            z[0] = double.Parse(textBox1.Text);
            z[1] = double.Parse(textBox3.Text);
            z[2] = double.Parse(textBox5.Text);
            z[3] = double.Parse(textBox7.Text);
            z[4] = double.Parse(textBox9.Text);
            c[0] = double.Parse(textBox2.Text);
            c[1] = double.Parse(textBox4.Text);
            c[2] = double.Parse(textBox6.Text);
            c[3] = double.Parse(textBox8.Text);
            c[4] = double.Parse(textBox10.Text);
            x[0] = double.Parse(textBox1.Text);
            x[1] = double.Parse(textBox3.Text);
            x[2] = double.Parse(textBox5.Text);
            x[3] = double.Parse(textBox7.Text);
            x[4] = double.Parse(textBox9.Text);
            y[0] = double.Parse(textBox2.Text);
            y[1] = double.Parse(textBox4.Text);
            y[2] = double.Parse(textBox6.Text);
            y[3] = double.Parse(textBox8.Text);
            y[4] = double.Parse(textBox10.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LeastSquare Kv1 = new LeastSquare() { x = z, y = c, RowCount = 2, ColumCount = 2 };
            Kv1.LeastSquaresSolution1(1);
            double g = 0;
            chart1.Series[1].Points.Clear();
            while (g < x.Max())
            {
                this.chart1.Series[1].Points.AddXY(g, Kv1.LeastSquaresSolution1(g));
                g += 0.01;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            LeastSquare Kv2 = new LeastSquare() { x = z, y = c, RowCount = 3, ColumCount = 3 };
            Kv2.LeastSquaresSolution2(1);
            double g = 0;
            chart1.Series[2].Points.Clear();
            while (g < x.Max())
            {
                this.chart1.Series[2].Points.AddXY(g, Kv2.LeastSquaresSolution2(g));
                g += 0.01;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LeastSquare Kv3 = new LeastSquare() { x = z, y = c, RowCount = 4, ColumCount = 4 };
            Kv3.LeastSquaresSolution3(0);
            double g = 0;
            chart1.Series[3].Points.Clear();
            while (g < x.Max())
            {
                this.chart1.Series[3].Points.AddXY(g, Kv3.LeastSquaresSolution3(g));
                g += 0.01;
            }
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            Newton nt = new Newton() { MasX = z, MasY = c };
            chart1.Series[4].Points.Clear();
            double g = 0;
            while (g < x.Max())
            {
                this.chart1.Series[4].Points.AddXY(g, nt.DoAllActions(g));
                g += 0.01;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            CubicSplineInterpolation spline = new CubicSplineInterpolation(x, y);
            chart1.Series[8].Points.Clear();
            double step = 0.01;
            for (double i = x.Min(); i <= x.Max(); i += step)
            {
                double yi = spline.Interpolate(i);
                chart1.Series[8].Points.AddXY(i, yi);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InterpolationLangrag lan = new InterpolationLangrag() { X = z, Y = c };
            double g = x.Min();
            chart1.Series[5].Points.Clear();
            while (g < x.Max())
            {
                this.chart1.Series[5].Points.AddXY(g, lan.DoAllActions(g));
                g += 0.01;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            chart1.Series[6].Points.Clear();
            double step = 0.01;
            CubicSplineInterpolation spline = new CubicSplineInterpolation(x, y);
            for (double i = x.Min(); i <= x.Max(); i += step)
            {
                double yi = spline.FirstDerivative(i);
                chart1.Series[6].Points.AddXY(i, yi);
            }

        }



        private void button10_Click(object sender, EventArgs e)
        {
            chart1.Series[7].Points.Clear();
            double step = 0.01;
            CubicSplineInterpolation spline = new CubicSplineInterpolation(x, y);
            for (double i = x.Min(); i <= x.Max(); i += step)
            {
                double yi = spline.SecondDerivative(i);
                chart1.Series[7].Points.AddXY(i, yi);
            }

        }
        private void button11_Click(object sender, EventArgs e)
        {
            double a = double.Parse(textBox11.Text);
            double b = double.Parse(textBox12.Text);
            double c = double.Parse(textBox13.Text);
            double d = double.Parse(textBox14.Text);
            this.chart1.Series[9].Points.Clear();
            double g = 3;
            while (g < 7)
            {
                double r = a + b * g + c * g * g + d * g * g * g;
                this.chart1.Series[9].Points.AddXY(g, r);
                g += 0.01;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }
    }
}
