using CompMathLibrary.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.KoshiTask.RungeKutt
{
	public interface IDerivativeFunction
	{
		double Calculate(double x, Vector derivativeArgs);
	}
}
