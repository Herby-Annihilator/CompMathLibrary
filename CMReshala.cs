﻿using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Methods;

namespace CompMathLibrary
{
	public class CMReshala
	{
		private MethodsFactory Factory { get; set; }
		public Answer SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, MethodType method)
		{
			return Factory.Build(matrixA, vectorB, method).Solve();
		}
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
					matr[i][j] = random.NextDouble();
				}
			}
			return matr;
		}
		public double[][] GetReversedMatrix(double[][] sourceMatrix, MethodType methodType = MethodType.Gauss)
		{

		}
	}
}
