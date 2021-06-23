using CompMathLibrary.Data;
using CompMathLibrary.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.KoshiTask.RungeKutt
{
	public class RungeKuttaMethod
	{
		public List<Point>[] GetSystemSolution(GlobalVectorDerivativeFunction F, double argumentStartCondition, double argumentEndCondition, Vector functionsStartConditions, int stepsCount)
		{
			List<Point>[] result = new List<Point>[functionsStartConditions.Length];
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = new List<Point>();
				result[i].Add(new Point(argumentStartCondition, functionsStartConditions[i]));
			}
			Vector k1, k2, k3, k4;
			Vector y = new Vector(functionsStartConditions);
			double x = argumentStartCondition;
			double h = (argumentEndCondition - argumentStartCondition) / stepsCount;
			for (int i = 0; i < stepsCount; i++)
			{
				k1 = F.Calculate(x, y) * h;
				k2 = F.Calculate(x + 0.5 * h, y + k1 * 0.5) * h;
				k3 = F.Calculate(x + 0.5 * h, y + k2 * 0.5) * h;
				k4 = F.Calculate(x + h, y + k3) * h;

				y += (k1 + k2 * 2 + k3 * 2 + k4) / 6.0;
				x += h;

				for (int j = 0; j < result.Length; j++)
				{
					result[j].Add(new Point(x, y[j]));
				}
			}
			return result;
		}
	}
}
