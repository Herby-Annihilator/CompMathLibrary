using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems.Answers
{
	public class DegreeMethodAnswer : ProblemAnswer
	{
		public int IterationsCount { get; internal set; }
		public double[] Eigenvector { get; internal set; }
		public double Eigenvalue { get; internal set; }
	}
}
