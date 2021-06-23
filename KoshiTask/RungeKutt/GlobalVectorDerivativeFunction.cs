using CompMathLibrary.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.KoshiTask.RungeKutt
{
	public class GlobalVectorDerivativeFunction
	{
		public IDerivativeFunction[] DerivativeFunctions { get; set; }
		public GlobalVectorDerivativeFunction()
		{
			DerivativeFunctions = new IDerivativeFunction[0];
		}
		public GlobalVectorDerivativeFunction(IDerivativeFunction[] derivativeFunctions)
		{
			DerivativeFunctions = derivativeFunctions;
		}

		public Vector Calculate(double x, Vector derivativeArgs)
		{
			Vector result = new Vector(DerivativeFunctions.Length);
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = DerivativeFunctions[i].Calculate(x, derivativeArgs);
			}
			return result;
		}
	}
}
