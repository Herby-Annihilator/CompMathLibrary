using CompMathLibrary.Methods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Creators.MethodCreators.Base
{
	public abstract class DirectMethodsCreator
	{
		internal abstract Method Create(double[][] matrix, double[] vector);
		protected void DefaultCheck(double[][] matrix, double[] vector)
		{
			if (matrix == null || vector == null)
				throw new ArgumentNullException("Matrix A or vector B is null");
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (matrix.GetLength(0) != matrix[i].Length)
					throw new ArgumentException("Matrix A is not quadro matrix or invalid matrix");
			}
			if (matrix.GetLength(0) != vector.Length)
				throw new ArgumentException("Count of rows in matrix A != count of numbers in vector B");
		}
	}
}
