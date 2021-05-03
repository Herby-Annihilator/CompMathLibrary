using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems.Base
{
	public abstract class EigenvalueProblem<TAnswer> where TAnswer: ProblemAnswer
	{
		protected double[][] _matrix;
		protected double _precision;
		protected EigenvalueProblem(double[][] matrix, double precision)
		{
			_matrix = matrix.CloneMatrix();
			_precision = precision;
		}
		internal abstract TAnswer Solve();
		internal abstract void Refresh();
	}
}
