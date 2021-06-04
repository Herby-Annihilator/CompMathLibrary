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
		public MultidimensionalPoint(int valuesLenght)
		{
			Value = new T[valuesLenght];
		}

		public void Add(MultidimensionalPoint<T> anotherPoint, Func<T, T, T> howToAdd)
		{
			int size;
			if (Value.Length > anotherPoint.Value.Length)
				size = anotherPoint.Value.Length;
			else
				size = Value.Length;
			for (int i = 0; i < size; i++)
			{
				Value[i] = howToAdd(Value[i], anotherPoint.Value[i]);
			}
		}
	}
}
