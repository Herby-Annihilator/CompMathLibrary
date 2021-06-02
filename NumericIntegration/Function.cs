using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration
{
	public abstract class Function
	{
		public abstract double GetValue(double arg);
		public virtual double[] GetValues(double[] args)
		{
			double[] values = new double[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				values[i] = GetValue(arg: args[i]);
			}
			return values;
		}
	}
}
