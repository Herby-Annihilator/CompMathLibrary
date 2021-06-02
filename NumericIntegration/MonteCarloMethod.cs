using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration
{
	public class MonteCarloMethod : Integral
	{
		private Dimension[] _dimensions;
		public Dimension[] Dimensions { get => _dimensions; set => _dimensions = (Dimension[])value.Clone(); }
		public MonteCarloMethod(Dimension[] dimension)
		{
			Dimensions = dimension;
		}
		public override double IntegralFromFunction(Function function, double functionArg)
		{
			throw new NotImplementedException();
		}

		private double CalculateJacobian()
		{
			double result = 1;
			for (int i = 0; i < Dimensions.Length; i++)
			{
				result *= Dimensions[i].SecondPoint - Dimensions[i].FirstPoint;
			}
			return result;
		}
	}

	public class Dimension
	{
		public double FirstPoint { get; set; }
		public double SecondPoint { get; set; }

		public Dimension(double firstPoint, double secondPoint)
		{
			FirstPoint = firstPoint;
			SecondPoint = secondPoint;
		}
	}
}
