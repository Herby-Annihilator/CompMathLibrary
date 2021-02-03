using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Methods;

namespace CompMathLibrary
{
	public class CMReshala
	{
		public MethodsFactory Factory { get; private set; }
		public void SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, Method method)
		{
			Answer answer = method.Solve();
		}
	}
}
