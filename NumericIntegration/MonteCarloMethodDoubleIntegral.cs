using CompMathLibrary.NumericIntegration.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Extensions;

namespace CompMathLibrary.NumericIntegration
{
	public class MonteCarloMethodDoubleIntegral : Integral
	{
		private IntegrationLimit[] _integrationLimits;
		public int IterationsCount { get; set; }
		public int CountOfPointsInArea { get; private set; } = 0;
		public int CountOfPointsNotInArea { get; private set; } = 0;
		public IntegrationLimit[] IntegrationLimits { get => _integrationLimits; set => _integrationLimits = (IntegrationLimit[])value.Clone(); }
		public MonteCarloMethodDoubleIntegral(IntegrationLimit[] integrationLimits, int iterationsCount)
		{
			IntegrationLimits = integrationLimits;
			IterationsCount = iterationsCount;
		}
		public override MultidimensionalPoint<double> IntegralFromFunction(Function function, MultidimensionalPoint<double> functionArg)
		{
			CountOfPointsInArea = 0;
			CountOfPointsNotInArea = 0;
			double jacobian = CalculateJacobian();
			Random random = new Random();
			MultidimensionalPoint<double> tmpPoint = new MultidimensionalPoint<double>();
			for (int i = 0; i < IterationsCount; i++)
			{
				tmpPoint.Value.ChangeCollection(value => );
			}
		}

		private double CalculateJacobian()
		{
			double result = 1;
			for (int i = 0; i < IntegrationLimits.Length; i++)
			{
				result *= IntegrationLimits[i].SecondPoint - IntegrationLimits[i].FirstPoint;
			}
			return result;
		}

	}

	public class IntegrationLimit
	{
		public double FirstPoint { get; set; }
		public double SecondPoint { get; set; }

		public IntegrationLimit(double firstPoint, double secondPoint)
		{
			FirstPoint = firstPoint;
			SecondPoint = secondPoint;
		}
	}
}
