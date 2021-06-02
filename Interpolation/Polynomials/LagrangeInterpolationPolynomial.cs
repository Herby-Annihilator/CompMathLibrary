using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Interpolation.Polynomials.Base;

namespace CompMathLibrary.Interpolation.Polynomials
{
	public class LagrangeInterpolationPolynomial : InterpolationPolynomial
	{
		public override double GetFunctionValueIn(double point)
		{
			double result = 0, numerator = 1, denominator = 1;
			int size = _arguments.Length;
			for (int k = 0; k < size; k++)
			{
				numerator = 1;
				denominator = 1;
				for (int j = 0; j < size; j++)
				{
					if (k != j)
					{
						numerator *= point - _arguments[j];
						denominator *= _arguments[k] - _arguments[j];
					}
				}
				result += (numerator * _values[k]) / denominator;
			}
			return result;
		}

		public LagrangeInterpolationPolynomial(double[] arguments, double[] values)
		{
			_arguments = arguments;
			_values = values;
		}

		public LagrangeInterpolationPolynomial()
		{
			Arguments = new double[0];
			Values = new double[0];
		}

		public override double[] GetFunctionValuesInPoints(double[] points)
		{
			int size = GetMinSizeOfArrays(points, _values);
			double[] result = new double[size];
			for (int i = 0; i < size; i++)
			{
				result[i] = GetFunctionValueIn(points[i]);
			}
			return result;
		}
	}
}
