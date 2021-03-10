using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Methods
{
	public class IterativeAnswer : Answer
	{
		public int NumberOfIterations { get; set; }
		public bool ConditionOfDiagonalDominance { get; internal set; }
	}
}
