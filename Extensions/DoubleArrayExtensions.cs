using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Extensions
{
	internal static class DoubleArrayExtensions
	{
		internal static double[][] CloneMatrix(this double[][] matrix)
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
	}
}
