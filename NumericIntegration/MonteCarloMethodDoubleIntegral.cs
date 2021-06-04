using CompMathLibrary.NumericIntegration.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Extensions;
using System.Linq;

namespace CompMathLibrary.NumericIntegration
{
	public class MonteCarloMethodDoubleIntegral : Integral
	{
		public int IterationsCount { get; set; }
		public List<MultidimensionalPoint<double>> PointsInArea { get; private set; }
		public List<MultidimensionalPoint<double>> PointsNotInArea { get; private set; }

		public MonteCarloMethodDoubleIntegral(int iterationsCount)
		{
			PointsInArea = new List<MultidimensionalPoint<double>>();
			PointsNotInArea = new List<MultidimensionalPoint<double>>();
			IterationsCount = iterationsCount;
		}
		public override MultidimensionalPoint<double> IntegralFromFunction(Function function, IntegrationLimit[] integrationLimits)
		{
			PointsInArea.Clear();
			PointsNotInArea.Clear();
			double jacobian = CalculateJacobian(integrationLimits);
			Random random = new Random();
			MultidimensionalPoint<double> result = new MultidimensionalPoint<double>(integrationLimits.Length);
			MultidimensionalPoint<double> tmpPoint = new MultidimensionalPoint<double>(integrationLimits.Length);
			MultidimensionalPoint<double> funcValue;
			for (int i = 0; i < IterationsCount; i++)
			{
				for (int j = 0; j < tmpPoint.Value.Length; j++)
				{
					tmpPoint.Value[i] = integrationLimits[i].FirstPoint + (integrationLimits[i].SecondPoint - integrationLimits[i].FirstPoint) * random.NextDouble();
				}
				funcValue = function.GetFunctionValue<double>(tmpPoint);
				if (tmpPoint.Value.Sum((value) => Math.Abs(value)) <= 1)  // попадание в область
				{					
					PointsInArea.Add(funcValue);
					result.Add(funcValue, (first, second) => first + second);
				}
				else
				{
					PointsNotInArea.Add(funcValue);
				}
			}
			result.Value.MultiplyBy(jacobian / IterationsCount);
			return result;
		}

		private double CalculateJacobian(IntegrationLimit[] integrationLimits)
		{
			double result = 1;
			for (int i = 0; i < integrationLimits.Length; i++)
			{
				result *= integrationLimits[i].SecondPoint - integrationLimits[i].FirstPoint;
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
