using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary
{
	public class CMReshala
	{
		public MethodsFactory Factory { get; private set; }
		public void SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, Method method)
		{
			method.Solve(matrixA, vectorB);
		}
	}
}
