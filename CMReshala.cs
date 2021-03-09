using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Methods;

namespace CompMathLibrary
{
	public class CMReshala
	{
		private const double DEFAULT_PRECISION = 0.000001;
		private MethodsFactory Factory { get; set; }
		public Answer SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, DirectMethodType method)
		{
			return Factory.Build(matrixA, vectorB, method).Solve();
		}
		public IterativeAnswer SolveSystemOfLinearAlgebraicEquationsIteratively(double[][] matrixA,
			double[] vectorB, double[] approximation, double precision = DEFAULT_PRECISION,
			IterativeMethodType type = IterativeMethodType.Jacobi) =>
			(IterativeAnswer)Factory.Build(matrixA, vectorB, approximation, precision, type).Solve();
		public CMReshala()
		{
			Factory = new MethodsFactory();
		}
		/// <summary>
		/// Return's value is a max of sums of the rows' elements
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public double GetMatrixMNorm(double[][] matrix)
		{
			double norm = 0;
			double[] sums = new double[matrix.GetLength(0)];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					sums[i] += Math.Abs(matrix[i][j]);
				}
			}
			norm = sums[0];
			for (int i = 1; i < sums.Length; i++)
			{
				if (sums[i] > norm)
				{
					norm = sums[i];
				}
			}
			return norm;
		}
		public double[][] CreateRandomMatrix(int rowsCount, int colsCount)
		{
			Random random = new Random();
			double[][] matr = new double[rowsCount][];
			for (int i = 0; i < rowsCount; i++)
			{
				matr[i] = new double[colsCount];
				for (int j = 0; j < colsCount; j++)
				{
					matr[i][j] = random.Next();
				}
			}
			return matr;
		}
		public double[][] GetReversedMatrix(double[][] sourceMatrix, DirectMethodType methodType = DirectMethodType.Gauss)
		{
			double[][] reversedMatrix = new double[sourceMatrix.GetLength(0)][];
			for (int i = 0; i < reversedMatrix.Length; i++)
			{
				reversedMatrix[i] = new double[sourceMatrix[i].Length];
			}
			int colsCount = reversedMatrix[0].Length;
			double[] tmpVector = new double[sourceMatrix.GetLength(0)];
			List<double[]> currentSolution;
			int nextIndex = 0;
			for (int i = 0; i < colsCount; i++)
			{
				tmpVector[nextIndex] = 1;
				currentSolution = SolveSystemOfLinearAlgebraicEquations(sourceMatrix, tmpVector,
					methodType).Solution;
				for (int j = 0; j < reversedMatrix.GetLength(0); j++)
				{
					reversedMatrix[j][i] = currentSolution[0][j];
				}
				tmpVector[nextIndex] = 0;
				nextIndex++;
			}
			return reversedMatrix;
		}
		public double[][] CreateGilbertMatrix(int matrixSize)
		{
			double[][] gilbertMatrix = new double[matrixSize][];
			for (int i = 0; i < matrixSize; i++)
			{
				gilbertMatrix[i] = new double[matrixSize];
			}
			for (int i = 0; i < matrixSize; i++)
			{				
				for (int j = i; j < matrixSize; j++)
				{
					gilbertMatrix[i][j] = (double)1 / ((double)i + 1 + (double)j + 1 - 1);
					gilbertMatrix[j][i] = gilbertMatrix[i][j];
				}
			}
			return gilbertMatrix;
		}
	}
}
