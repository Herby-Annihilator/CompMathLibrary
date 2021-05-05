using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems.Answers
{
	public class MethodOfRotationsAnswer : ProblemAnswer
	{
		public double[] Eigenvalues { get; set; }
		public double[][] Eigenvectors { get; set; }
		public double[][] Residuals { get; set; }
	}
}
