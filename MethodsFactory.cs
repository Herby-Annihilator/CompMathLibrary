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

		public Method Build(double[][] matrixA, double[] vectorB, DirectMethodType type = DirectMethodType.Gauss)
		{
			return type switch
			{
				DirectMethodType.Gauss => CreateGaussMethod(matrixA, vectorB),
				DirectMethodType.SquareRoot => CreateSquareRootMethod(matrixA, vectorB),
				_ => CreateGaussMethod(matrixA, vectorB),
			};
		}
		public Method Build(double[][] matrixA, double[] vectorB, double[] approximation,
			double precision, IterativeMethodType type = IterativeMethodType.Jacobi)
		{
			return type switch
			{
				IterativeMethodType.Jacobi => CreateJacobiMethod(matrixA, vectorB, approximation, precision),
				IterativeMethodType.Seidel => CreateSeidelMethod(matrixA, vectorB, approximation, precision),
				_ => CreateJacobiMethod(matrixA, vectorB, approximation, precision),
			};
		}

		private JacobiMethod CreateJacobiMethod(double[][] matrix, double[] vector, double[] approx,
			double precision)
		{
			DefaultCheck(matrix, vector);
			CheckIterativeConditions(matrix, vector, approx, precision);
			return new JacobiMethod(matrix, vector, approx, precision);
		}
		private JacobiMethod CreateSeidelMethod(double[][] matrix, double[] vector, double[] approx,
			double precision)
		{
			DefaultCheck(matrix, vector);
			CheckIterativeConditions(matrix, vector, approx, precision);
			return new SeidelMethod(matrix, vector, approx, precision);
		}
		private void CheckIterativeConditions(double[][] matrix, double[] vector, double[] approx, double precision)
		{
			if (approx == null)
				throw new ArgumentNullException("Approximation was null");
			if (approx.Length != vector.Length)
				throw new ArgumentException("Invalid size of approximation vector");
			if (precision < double.Epsilon)
				throw new ArgumentException("Precision is VERY small");
			if (AreThereZeroOnMainDiagonal(matrix))
				throw new ArgumentException("Zeros on the main diagonal");
		}
		private bool AreThereZeroOnMainDiagonal(double[][] matrix)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				if (matrix[i][i] == 0)
					return true;
			}
			return false;
		}
	}
	public enum DirectMethodType
	{
		Gauss,
		SquareRoot
	}
	public enum IterativeMethodType
	{
		Jacobi,
		Seidel
	}
}
