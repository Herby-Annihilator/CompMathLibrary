using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Methods
{
	public class SquareRootMethod : Method
	{
		private double[][] matrixA;
		private double[] vectorB;
		private int[] orderOfUnknownsVariables;
		internal SquareRootMethod(double[][] matrix, double[] vector)
		{
			matrixA = CloneMatrix(matrix);
			vectorB = (double[])vector.Clone();
			orderOfUnknownsVariables = new int[vector.Length];
			for (int i = 0; i < orderOfUnknownsVariables.Length; i++)
			{
				orderOfUnknownsVariables[i] = i;
			}
		}
		public override Answer Solve()
		{
			Answer answer = new Answer();
			double[][] upperTriangularMatrix = CreateUpperTriangularMatrix();
			int[] diagonal = new int[vectorB.Length];
			double totalSum;
			double currentDifference;
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				totalSum = 0;
				for (int k = 0; k < i; k++)
				{
					totalSum += upperTriangularMatrix[k][i] * upperTriangularMatrix[k][i] * diagonal[k];
				}
				currentDifference = matrixA[i][i] - totalSum;
				diagonal[i] = Math.Sign(currentDifference);
				upperTriangularMatrix[i][i] = Math.Sqrt(Math.Abs(currentDifference));
				if (upperTriangularMatrix[i][i] < double.Epsilon)   // проверка на ноль
				{
					if (ReplaceDiagonalElement(i))   // если удалось переставить, то повторяем
					{
						i--;
						continue;
					}				
				}
				for (int j = i + 1; j < matrixA.GetLength(0); j++)
				{
					totalSum = 0;
					for (int k = 0; k < i; k++)
					{
						totalSum += upperTriangularMatrix[k][i] * upperTriangularMatrix[k][j] * diagonal[k];
					}
					currentDifference = matrixA[i][j] - totalSum;
					upperTriangularMatrix[i][j] = currentDifference / 
						(upperTriangularMatrix[i][i] * diagonal[i]);
					if (double.IsNaN(upperTriangularMatrix[i][j]) || 
						double.IsInfinity(upperTriangularMatrix[i][j]))
					{
						throw new Exception("Получено некорректное значение в треугольной матрице" +
							" строка: " + i + " столбец: " + j);
					}
				}
			}
			double[] y = new double[vectorB.Length];
			double[] z = new double[vectorB.Length];
			double determinant = 1;
			for (int i = 0; i < vectorB.Length; i++)
			{
				totalSum = 0;
				for (int k = 0; k < i; k++)
				{
					totalSum += z[k] * upperTriangularMatrix[k][i];
				}
				z[i] = (vectorB[i] - totalSum) / upperTriangularMatrix[i][i];
				y[i] = z[i] / diagonal[i];
				determinant *= upperTriangularMatrix[i][i];
			}
			determinant *= determinant;
			answer.AnswerStatus = AnswerStatus.OneSolution;
			answer.Determinant = determinant * DeterminantSign(diagonal);
			answer.Solution = new List<double[]>();
			double[] x = new double[vectorB.Length];
			int currentVariable;
			for (int i = vectorB.Length - 1; i >= 0; i--)
			{
				totalSum = 0;
				currentVariable = orderOfUnknownsVariables[i];
				for (int k = i + 1; k < matrixA.GetLength(0); k++)
				{
					totalSum += upperTriangularMatrix[i][k] * x[orderOfUnknownsVariables[k]];
				}
				x[currentVariable] = (y[i] - totalSum) / upperTriangularMatrix[i][i];
			}
			answer.Solution.Add(x);
			return answer;
		}
		private int DeterminantSign(int[] diagonal)
		{
			int sign = 1;
			for (int i = 0; i < diagonal.Length; i++)
			{
				sign *= diagonal[i];
			}
			return sign;
		}
		private bool ReplaceDiagonalElement(int elementIndex)
		{
			for (int i = elementIndex + 1; i < matrixA.GetLength(0); i++)
			{
				if (orderOfUnknownsVariables[i] == i) // можно переставлять
				{
					ReplaceColumns(elementIndex, i);
					ReplaceRows(elementIndex, i);
					return true;
				}
			}
			return false;
		}
		private void ReplaceColumns(int firstColumn, int secondColumn)
		{
			int tmp = orderOfUnknownsVariables[firstColumn];
			orderOfUnknownsVariables[firstColumn] = orderOfUnknownsVariables[secondColumn];
			orderOfUnknownsVariables[secondColumn] = tmp;
			double temp;
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				temp = matrixA[i][firstColumn];
				matrixA[i][firstColumn] = matrixA[i][secondColumn];
				matrixA[i][secondColumn] = temp;
			}
		}
		private void ReplaceRows(int firstRow, int secondRow)
		{
			double tmp;
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				tmp = matrixA[firstRow][i];
				matrixA[firstRow][i] = matrixA[secondRow][i];
				matrixA[secondRow][i] = tmp;
				tmp = vectorB[firstRow];
				vectorB[firstRow] = vectorB[secondRow];
				vectorB[secondRow] = tmp;
			}
		}
		private double[][] CreateUpperTriangularMatrix()
		{
			double[][] upperTriangularMatrix = new double[vectorB.Length][];
			for (int i = 0; i < vectorB.Length; i++)
			{
				upperTriangularMatrix[i] = new double[vectorB.Length];
			}
			return upperTriangularMatrix;
		}
	}
}
