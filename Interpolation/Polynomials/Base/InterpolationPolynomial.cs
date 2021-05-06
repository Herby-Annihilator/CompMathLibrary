using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Interpolation.Polynomials.Base
{
	public abstract class InterpolationPolynomial
	{
		protected double[] _arguments;
		protected double[] _values;
		public abstract double GetFunctionValueIn(double point);
		public abstract double[] GetFunctionValuesInPoints(double[] points);

		protected int GetMinSizeOfArrays(double[] first, double[] second)
		{
			int length;
			if (first.Length < second.Length)
				length = first.Length;
			else
				length = second.Length;
			return length;
		}
	}
}
