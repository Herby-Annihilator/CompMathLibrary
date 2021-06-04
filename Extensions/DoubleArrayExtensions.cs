using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Extensions
{
	public static class DoubleArrayExtensions
	{
		public static double[][] CloneMatrix(this double[][] matrix)
		{
			double[][] toReturn = new double[matrix.GetLength(0)][];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				toReturn[i] = new double[matrix[i].Length];
				for (int j = 0; j < matrix[i].Length; j++)
				{
					toReturn[i][j] = matrix[i][j];
				}
			}
			return toReturn;
		}
		public static double[][] GetTheIdentityMatrix(this double[][] matrix)
		{
			int rowsCount = matrix.GetLength(0);
			double[][] toReturn = new double[rowsCount][];
			int colsCount;
			for (int i = 0; i < rowsCount; i++)
			{
				colsCount = matrix[i].Length;
				toReturn[i] = new double[colsCount];
				for (int j = 0; j < colsCount; j++)
				{
					if (i == j)
					{
						toReturn[i][j] = 1;
					}
					else
					{
						toReturn[i][j] = 0;
					}
				}
			}
			return toReturn;
		}
		public static T[][] SubtractOrAddMatrix<T>(this T[][] matrix, T[][] anotherMatrix, Func<T, T, T> operation)
		{
			int rowsCount = matrix.GetLength(0);
			if (anotherMatrix == null)
				return matrix;
			if (anotherMatrix.GetLength(0) != rowsCount)
				return matrix;
			int colsCount;
			T[][] toReturn = new T[rowsCount][];
			for (int i = 0; i < rowsCount; i++)
			{
				colsCount = matrix[i].Length;
				if (anotherMatrix[i].Length != colsCount)
					return matrix;
				toReturn[i] = new T[colsCount];
				for (int j = 0; j < colsCount; j++)
				{
					toReturn[i][j] = operation(matrix[i][j], anotherMatrix[i][j]);
				}
			}
			return toReturn;
		}
		public static T[][] MultiplyByANumber<T>(this T[][] matrix, T number, Func<T, T, T> multiplicationOperation)
		{
			int rowsNumber = matrix.GetLength(0);
			T[][] toReturn = new T[rowsNumber][];
			int colsNumber;
			for (int i = 0; i < rowsNumber; i++)
			{
				colsNumber = matrix[i].Length;
				toReturn[i] = new T[colsNumber];
				for (int j = 0; j < colsNumber; j++)
				{
					toReturn[i][j] = multiplicationOperation(matrix[i][j], number);
				}
			}
			return toReturn;
		}	

		public static double[] MultiplyBy(this double[] vector, double number)
		{
			double[] result = new double[vector.Length];
			for (int i = 0; i < vector.Length; i++)
			{
				result[i] = vector[i] * number;
			}
			return result;
		}

		public static int IndexOf<T>(this T[] arr, T element, Func<T, T, int> comparer)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				if (comparer(arr[i], element) == 0)
					return i;
			}
			return -1;
		}
		public static void FillBy<T>(this T[] arr, T element)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = element;
			}
		}

		public static T[] DoOperationWithVector<T>(this T[] vector, T[] anotherVector, Func<T, T, T> operation)
		{
			int length;
			if (vector.Length < anotherVector.Length)
				length = vector.Length;
			else
				length = anotherVector.Length;
			T[] result = new T[length];
			for (int i = 0; i < length; i++)
			{
				result[i] = operation(vector[i], anotherVector[i]);
			}
			return result;
		}

		public static T[] Map<T>(this T[] arr, Func<T, T> func)
		{
			T[] result = new T[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				result[i] = func(arr[i]);
			}
			return result;
		}

		public static void ChangeCollection<T>(this T[] arr, Func<T, T> howToChangeEachElement)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = howToChangeEachElement(arr[i]);
			}
		}
	}
}
