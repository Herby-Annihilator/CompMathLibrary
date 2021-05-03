using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.EigenvalueProblems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class ReversedDegreeMethodCreator : DegreeMethodCreator
	{
		public DegreeMethod Create(double[][] matrix, double[] vector, double precision, double startLambda)
		{
			DefaultCheck(matrix, vector);
			CheckPrecision(precision);
			CheckStartLambda(startLambda);
			return new ReversedDegreeMethod(matrix, precision, vector, startLambda);
		}
		protected void CheckStartLambda(double lambda)
		{

		}
		

		public override string ToString() => "Reversed degree method";
	}
}
