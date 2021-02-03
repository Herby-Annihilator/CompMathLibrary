using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Methods
{
	public class Answer
	{
		public List<double[]> Solution { get; internal set; }
		public AnswerStatus AnswerStatus { get; internal set; }
	}

	public enum AnswerStatus
	{
		OneSolution,
		SeveralSolutions,
		NoSolutions
	}
}
