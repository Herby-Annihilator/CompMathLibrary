using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Methods;

namespace CompMathLibrary
{
	public class CMReshala
	{
		private MethodsFactory Factory { get; set; }
		public Answer SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, MethodType method)
		{
			return Factory.Build(matrixA, vectorB, method).Solve();
		}
		public CMReshala()
		{
			Factory = new MethodsFactory();
		}
	}
}
