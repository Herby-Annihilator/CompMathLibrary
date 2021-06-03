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
			double a, b, c = 0, x0 = 0;  // из х0 - касательная, из с - хорда
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
						if (function(a) * secondDerivative(a) > 0)
						{
							x0 = a;  // касательная из а
							c = b;  // хорда из b
						}
						else if (function(b) * secondDerivative(b) > 0)
						{
							x0 = b; // касательная из b
							c = a; // хорда из а
						}
						while (Math.Abs(x0 - c) > Epsilon)
						{
							iterationsCount++;
							x0 -= function(x0) / firstDerivative(x0);
							c -= function(c) * (x0 - c) / (function(x0) - function(c));
						}						
						result.Add(new RootInfo((a + b) / 2, iterationsCount));
					}					
				}
			}
			return result;
		}		
	}
}
