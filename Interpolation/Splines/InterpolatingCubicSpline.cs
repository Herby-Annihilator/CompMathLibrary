using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Interpolation.Splines
{
	public class InterpolatingCubicSpline
	{
        public InterpolatingCubicSpline(double[] args, double[] values)
		{
            int size = args.Length;
            Arguments = (double[])args.Clone();
            Values = (double[])values.Clone();
            a = new double[size];
            b = new double[size];
            c = new double[size];
            d = new double[size];
            h = new double[size];
            Build();
        }
		public double[] Arguments { get; set; }
		public double[] Values { get; set; }

        // Массивы нужны для вычислений различных коеффициентов
        private double[] h;
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] d;

        private void Build()
        {
            int n = Arguments.Length;
            for (int i = 1; i < n; i++)
            {
                h[i] = Arguments[i] - Arguments[i - 1];
            }
            List<double> alpha = new List<double>();
            List<double> betta = new List<double>();
            alpha.Add(0);
            betta.Add(0);
            for (int i = 1; i < n - 1; i++) 
            {
                double k1 = 2 * (h[i + 1] + h[i]);
                double k2 = 6 * (((Values[i + 1] - Values[i]) / h[i + 1]) - ((Values[i] - Values[i - 1]) / h[i]));
                double divider = (h[i] * alpha[i - 1] + k1);
                alpha.Add(-h[i + 1] / divider);
                betta.Add((k2 - h[i] * betta[i - 1]) / divider);
            }
           
            c[0] = c[n - 1] = 0;
            for (int i = n - 2; i >= 1; i--)
            {
                c[i] = c[i + 1] * alpha[i] + betta[i]; 
            }
            
            for (int i = 1; i < n; i++)
            {
                a[i] = Values[i];
                d[i] = (c[i] - c[i - 1]) / h[i];
                b[i] = h[i] * c[i] / 2 - h[i] * h[i] * d[i] / 6 + (Values[i] - Values[i - 1]) / h[i];
            }
        }

        public double GetFunctionValueInPoint(double point)
        {
            int n = Arguments.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (point >= Arguments[i] && point <= Arguments[i + 1]) 
                {
                    double cur_diff = point - Arguments[i + 1];
                    return (a[i + 1] + b[i + 1] * cur_diff + c[i + 1] * cur_diff * cur_diff / 2 + d[i + 1] * cur_diff * cur_diff * cur_diff / 6);
                }
            }
            return double.NaN;
        }
    }
}
