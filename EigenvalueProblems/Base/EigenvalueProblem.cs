using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems.Base
{
	internal abstract class EigenvalueProblem<TAnswer> where TAnswer: Answer
	{
		protected double[][] _matrix;
		protected double _precision;
		internal EigenvalueProblem(double[][] matrix, double precision)
		{
			_matrix = matrix.CloneMatrix();
			_precision = precision;
		}
		internal abstract TAnswer Solve();
	}
}
