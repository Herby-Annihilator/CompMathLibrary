using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary
{
	public class MethodsFactory
	{
		private GaussMethod CreateGaussMethod(double[][] matrixA, double[] vectorB)
		{
			DefaultCheck(matrixA, vectorB);
			GaussMethod gaussMethod = new GaussMethod(matrixA, vectorB);
			return gaussMethod;
		}
		private SquareRootMethod CreateSquareRootMethod(double[][] matrixA, double[] vectorB)
		{
			DefaultCheck(matrixA, vectorB);
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				for (int j = 0; j < matrixA.GetLength(0); j++)
				{
					if (matrixA[i][j] != matrixA[j][i])
						throw new ArgumentException("Asymmetric matrix");
				}
			}
			return new SquareRootMethod(matrixA, vectorB);
		}
		private void DefaultCheck(double[][] matrix, double[] vector)
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

		public Method Build(double[][] matrixA, double[] vectorB, MethodType type)
		{
			switch (type)
			{
				case MethodType.Gauss:
					{
						return CreateGaussMethod(matrixA, vectorB);
					}
				case MethodType.SquareRoot:
					{
						return CreateSquareRootMethod(matrixA, vectorB);
					}
				default:
					{
						return CreateGaussMethod(matrixA, vectorB);
					}
			}
		}
	}
	public enum MethodType
	{
		Gauss,
		SquareRoot
	}
}
