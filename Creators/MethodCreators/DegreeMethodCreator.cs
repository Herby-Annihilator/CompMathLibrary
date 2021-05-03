using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.EigenvalueProblems;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class DegreeMethodCreator : EigenvalueProblemsCreator
	{
		internal override DegreeMethod Create(double[][] matrix, double[] vector, double precision)
		{
			DefaultCheck(matrix, vector);
			CheckPrecision(precision);
			return new DegreeMethod(matrix, precision, vector);
		}
		protected void CheckPrecision(double precision)
		{
			if (precision < double.Epsilon)
				throw new ArgumentException("Precision can not be less than machine epsilon");
		}
		public override string ToString() => "Degree method";
	}
}
