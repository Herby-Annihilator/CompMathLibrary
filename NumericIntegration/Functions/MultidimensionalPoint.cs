using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.NumericIntegration.Functions
{
	public class MultidimensionalPoint<T>
	{
		public T[] Value { get; set; }

		public MultidimensionalPoint(T[] value)
		{
			Value = value;
		}
		public MultidimensionalPoint()
		{
			Value = new T[0];
		}
	}
}
