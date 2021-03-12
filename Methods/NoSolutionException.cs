using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Methods
{
	public class NoSolutionException : Exception
	{
		public NoSolutionException(string message) : base(message)
		{

		}
	}
}
