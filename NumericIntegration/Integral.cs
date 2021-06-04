using CompMathLibrary.NumericIntegration.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration
{
	public abstract class Integral
	{
		public abstract MultidimensionalPoint<double> IntegralFromFunction(Function function, IntegrationLimit[] integrationLimits);
	}
}
