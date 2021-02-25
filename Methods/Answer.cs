using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Methods
{
	public class Answer
	{
		public List<double[]> Solution { get; internal set; }
		public AnswerStatus AnswerStatus { get; internal set; }
		public double Determinant { get; internal set; }
		public Answer()
		{
			Determinant = 0;
			Solution = new List<double[]>();
			AnswerStatus = AnswerStatus.NoSolutions;
		}
	}

	public enum AnswerStatus
	{
		OneSolution,
		SeveralSolutions,
		NoSolutions
	}
}
