using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration.Functions
{
	public abstract class Function
	{
		public abstract MultidimensionalPoint<double> GetFunctionValue<T>(MultidimensionalPoint<double> arg);
	}
}
