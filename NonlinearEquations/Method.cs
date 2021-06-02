using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NonlinearEquations
{
	public abstract class Method
	{
		public virtual double Epsilon { get; set; }
		public virtual Segment Segment { get; set; }
		public virtual double Step { get; set; }
		public abstract List<RootInfo> SolveEquation(Func<double, double> function, Func<double, double> firstDerivative, Func<double, double> secondDerivative);
		protected virtual List<double> SplitSegment()
		{
			List<double> result = new List<double>();
			double first, second;
			if (Segment.First > Segment.Second)
			{
				first = Segment.Second;
				second = Segment.First;
			}
			else
			{
				first = Segment.First;
				second = Segment.Second;
			}	
			for (double i = first; i < second; i += Step)
			{
				result.Add(i);
			}
			if (result[^1] != second)
				result.Add(second);
			return result;
		}

		protected virtual List<double> SeparateRoots() => SplitSegment();

		public virtual void Refresh(double eps, Segment segment, double step)
		{
			Segment = segment;
			Epsilon = eps;
			Step = step;
		}

	}

	public class RootInfo
	{
		public double Root { get; set; }
		public int IterationsCount { get; set; }

		public RootInfo(double root, int iterationsCount)
		{
			Root = root;
			IterationsCount = iterationsCount;
		}
	}

	public class Segment
	{
		public double First { get; set; }
		public double Second { get; set; }

		public Segment(double first, double second)
		{
			First = first;
			Second = second;
		}

		public double GetLength() => Math.Abs(First - Second);
	}
}
