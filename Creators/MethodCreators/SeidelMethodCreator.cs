using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.Methods;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class SeidelMethodCreator : IterativeMethodsCreator
	{
		internal override Method Create(double[][] matrix, double[] vector,
			double[] approximation, double precision)
		{
			DefaultCheck(matrix, vector);
			CheckIterativeConditions(matrix, vector, approximation, precision);
			return new JacobiMethod(matrix, vector, approximation, precision);
		}
		public override string ToString() => "Seidel method";
	}
}
