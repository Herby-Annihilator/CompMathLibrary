using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems.Answers
{
	public class PartialProblemAnswer : Answer
	{
		public int IterationCount { get; set; }
		public double[] Eigenvector { get; set; }
	}
}
