using CompMathLibrary.Methods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Creators.MethodCreators.Base
{
	public abstract class IterativeMethodsCreator
	{
		internal abstract Method Create(double[][] matrix, double[] vector, double[] approximation, double precision);
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
		protected void CheckIterativeConditions(double[][] matrix, double[] vector, double[] approx, double precision)
		{
			if (approx == null)
				throw new ArgumentNullException("Approximation was null");
			if (approx.Length != vector.Length)
				throw new ArgumentException("Invalid size of approximation vector");
			if (precision < double.Epsilon)
				throw new ArgumentException("Precision is VERY small");
			if (IsThereZeroOnMainDiagonal(matrix))
				throw new ArgumentException("Zeros on the main diagonal");
		}
		private bool IsThereZeroOnMainDiagonal(double[][] matrix)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (matrix[i][i] == 0)
					return true;
			}
			return false;
		}
	}
}
