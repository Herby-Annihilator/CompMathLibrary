using System;
using System.Collections.Generic;

namespace CompMathLibrary.NonlinearEquations
{
	public class CombinedMethod : Method
	{
		public CombinedMethod(Segment segment, double step, double eps)
		{
			Segment = segment;
			Step = step;
			Epsilon = eps;
		}
		public override List<RootInfo> SolveEquation(Func<double, double> function, Func<double, double> firstDerivative, Func<double, double> secondDerivative)
		{
			List<RootInfo> result = new List<RootInfo>();
			List<double> splitPoints = SplitSegment();
			int segmetsCount = splitPoints.Count - 1;
			double a, b;
			double functionFromA, functionFromB, firtsDerivativeFromA, firtsDerivativeFromB, secondDerivativeFromA, secondDerivativeFromB;
			int iterationsCount;
			for (int i = 0; i < segmetsCount; i++)
			{
				a = splitPoints[i];
				b = splitPoints[i + 1];
				iterationsCount = 0;
				if ((function(a) * function(b)) < 0)
				{
					if (!(firstDerivative(a) == 0 || secondDerivative(a) == 0 || firstDerivative(b) == 0 || secondDerivative(b) == 0))
					{
						while (Math.Abs(a + b) < (Epsilon * 2))
						{
							iterationsCount++;

							functionFromA = function(a);
							firtsDerivativeFromA = firstDerivative(a);
							secondDerivativeFromA = secondDerivative(a);

							functionFromB = function(b);
							firtsDerivativeFromB = firstDerivative(b);
							secondDerivativeFromB = secondDerivative(b);

							if (functionFromA * secondDerivativeFromA < 0)
							{
								a -= functionFromA * (a - b) / (functionFromA - functionFromB);
							}
							else if (functionFromA * secondDerivativeFromA > 0)
							{
								a -= functionFromA / firtsDerivativeFromA;
							}
							if (functionFromB * secondDerivativeFromB < 0)
							{
								b -= functionFromB * (b - a) / (functionFromB - functionFromA);
							}
							else if (functionFromB * secondDerivativeFromB > 0)
							{
								b -= functionFromB / firtsDerivativeFromB;
							}
						}
						result.Add(new RootInfo((a + b) / 2, iterationsCount));
					}					
				}
			}
			return result;
		}		
	}
}
