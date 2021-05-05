using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.EigenvalueProblems;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class MethodOfRotationsCreator
	{
		internal MethodOfRotations Create(double[][] matrix, double precision)
		{
			if (matrix == null)
				throw new ArgumentNullException("Matrix null");
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (matrix.GetLength(0) != matrix[i].Length)
					throw new ArgumentException("Matrix A is not quadro matrix or invalid matrix");
			}
			if (precision < double.Epsilon)
				throw new ArgumentException("Precision is VERY small");
			if (IsThereZeroOnMainDiagonal(matrix))
				throw new ArgumentException("Zeros on the main diagonal");
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(0); j++)
				{
					if (matrix[i][j] != matrix[j][i])
						throw new ArgumentException("Asymmetric matrix");
				}
			}
			return new MethodOfRotations(matrix, precision);
		}

		protected bool IsThereZeroOnMainDiagonal(double[][] matrix)
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
