using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.Methods.Base;
using System;
using CompMathLibrary.Methods;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class GaussMethodCreator : DirectMethodsCreator
	{
		internal override Method Create(double[][] matrix, double[] vector)
		{
			DefaultCheck(matrix, vector);
			return new GaussMethod(matrix, vector);
		}
		public override string ToString()
		{
			return "Gauss method";
		}
	}
}
