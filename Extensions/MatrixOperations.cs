using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Extensions
{
	public static class MatrixOperations
	{
		public static T[][] FindTransposedMatrix<T>(this T[][] matrix)
		{
			int maxCountOfColumnsInMatrix = MaxCountOfColumnsInMatrix(matrix);
			T[][] transposedMatrix = new T[maxCountOfColumnsInMatrix][];
			for (int i = 0; i < maxCountOfColumnsInMatrix; i++)
			{
				transposedMatrix[i] = new T[matrix.GetLength(0)];
				for (int j = 0; j < matrix.GetLength(0); j++)
				{
					transposedMatrix[i][j] = matrix[j][i];
				}
			}
			return transposedMatrix;
		}
		private static int MaxCountOfColumnsInMatrix<T>(T[][] matrix)
		{
			int max = 0;
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (matrix[i].Length > max)
				{
					max = matrix[i].Length;
				}
			}
			return max;
		}
		
		public static T[][] MultiplyBy<T>(this T[][] matrix, T[][] anotherMatrix, Func<T, T, T> howToMultiply, Func<T, T, T> howToAdd)
		{
			int rows = matrix.GetLength(0);
			int cols = MaxCountOfColumnsInMatrix(anotherMatrix);
			T sum = default;
			T[][] result = new T[rows][];
			for (int i = 0; i < rows; i++)
			{
				result[i] = new T[cols];
				for (int j = 0; j < cols; j++)
				{
					sum = default;
					for (int k = 0; k < matrix[i].Length; k++)
					{
						sum = howToAdd(sum, howToMultiply(matrix[i][k], anotherMatrix[k][j]));
					}
					result[i][j] = sum;
				}
			}
			return result;
		}

		public static T[] MultiplyByColumn<T>(this T[][] matrix, T[] vector, Func<T, T, T> howToMultiply, Func<T, T, T> howToAdd)
		{
			T[] result = new T[vector.Length];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < vector.Length; j++)
				{
					result[i] = howToAdd(result[i], howToMultiply(matrix[i][j], vector[j]));
				}
			}
			return result;
		}

		public static T[] PresentSpecifiedColumnAsVector<T>(this T[][] matrix, int columnIndex)
		{
			T[] result = new T[matrix.GetLength(0)];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				result[i] = matrix[i][columnIndex];
			}
			return result;
		}
	}
}
