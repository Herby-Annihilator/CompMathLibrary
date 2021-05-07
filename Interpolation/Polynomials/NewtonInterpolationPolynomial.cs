using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Interpolation.Polynomials.Base;

namespace CompMathLibrary.Interpolation.Polynomials
{
	public class NewtonInterpolationPolynomial : InterpolationPolynomial
	{
		public NewtonInterpolationPolynomial()
		{
			Values = new double[0];
			Arguments = new double[0];
		}
		public override double GetFunctionValueIn(double point)
		{
			double result = 0;
			double num;
			int n = _values.Length;
			for (int j = 0; j < n; j++)
			{
				num = 1;
				for (int k = 0; k < j; k++)
				{
					num *= (point - _arguments[k]);
				}
				result += (num * GetDividedDifferenceOfTheSpecifiedOrder(j + 1, _arguments, _values));
			}
			return result;
		}

		private double GetDividedDifferenceOfTheSpecifiedOrder(int order, double[] args, double[] values)
		{
			double result = 0, denominator;
			for (int i = 0; i < order; i++)
			{
				denominator = 1;
				for (int j = 0; j < order; j++)
				{
					if (i != j)
					{
						denominator *= (args[i] - args[j]);
					}
				}
				result += values[i] / denominator;
			}
			return result;
		}

		public override double[] GetFunctionValuesInPoints(double[] points)
		{
			int size = GetMinSizeOfArrays(points, _values);
			double[] result = new double[points.Length];
			double sum;
			for (int i = 0; i < points.Length; i++) 
			{
				sum = 0;
				for (int j = 0; j < size; j++)
				{
					double num = 1;
					for (int k = 0; k < j; k++)
					{
						num *= (points[i] - _arguments[k]);
					}
					sum += (num * GetDividedDifferenceOfTheSpecifiedOrder(j + 1, _arguments, _values));
				}
				result[i] = sum;
			}
			return result;
		}
	}
}
