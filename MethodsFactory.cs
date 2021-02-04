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
			if (matrixA == null || vectorB == null)
				throw new ArgumentNullException("Matrix A or vector B is null");
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				if (matrixA.GetLength(0) != matrixA[i].Length)
					throw new ArgumentException("Matrix A is not quadro matrix or invalid matrix");
			}
			if (matrixA.GetLength(0) != vectorB.Length)
				throw new ArgumentException("Count of rows in matrix A != count of numbers in vector B");
			GaussMethod gaussMethod = new GaussMethod(matrixA, vectorB);
			return gaussMethod;
		}

		public Method Build(double[][] matrixA, double[] vectorB, MethodType type)
		{
			switch (type)
			{
				default:
					{
						return CreateGaussMethod(matrixA, vectorB);
					}
			}
		}
	}
	public enum MethodType
	{
		Gauss
	}
}
