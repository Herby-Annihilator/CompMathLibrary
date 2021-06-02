using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration
{
	public abstract class Integral
	{
		public abstract double IntegralFromFunction(Function function, double functionArg);
	}
}
