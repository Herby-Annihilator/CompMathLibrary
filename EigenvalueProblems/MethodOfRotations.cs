﻿using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.EigenvalueProblems.Base;
using CompMathLibrary.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems
{
	public class MethodOfRotations : EigenvalueProblem<MethodOfRotationsAnswer>
	{
		private double[] _sumVector;
		private double[][] _clone;
		protected MethodOfRotations(double[][] matrix, double precision) : base(matrix, precision)
		{
			_sumVector = new double[matrix.GetLength(0)];
			_clone = matrix.CloneMatrix();
		}

		internal override void Refresh()
		{
			_sumVector.FillBy(0);
			_matrix = _clone.CloneMatrix();
		}

		internal override MethodOfRotationsAnswer Solve()
		{
			double[][] dMatrix = _matrix.GetTheIdentityMatrix();
			FillSumVector();
			int k = _sumVector.IndexOf(_sumVector.Max(), (first, second) =>
			{
				if (first > second) return 1;
				if (first < second) return -1;
				return 0;
			});
			int l = FindMaxAbsElementIndexInSpecifiedRow(k);
			double s = FindSumOfNonDiagonalElements();
			double[][] currentOrthogonalMatrix;
			double alpha, beta, mu;
			while (s > _precision)
			{
				mu = FindMu(k, l);
				alpha = FindAlphaCoefficient(mu);
				beta = FindBetaCoefficient(mu);
				currentOrthogonalMatrix = CreateSpecialUMatrix(alpha, beta, k, l);
				dMatrix = dMatrix.MultiplyBy(currentOrthogonalMatrix, (first, second) => first * second, (first, second) => first + second);
				_matrix = RotateMatrix(_matrix, currentOrthogonalMatrix);
				s = FindSumOfNonDiagonalElements();
				ChangeSumVector(k, l);
				k = _sumVector.IndexOf(_sumVector.Max(), (first, second) =>
				{
					if (first > second) return 1;
					if (first < second) return -1;
					return 0;
				});
				l = FindMaxAbsElementIndexInSpecifiedRow(k);
			}
			MethodOfRotationsAnswer answer = new MethodOfRotationsAnswer();

			return answer;
		}
			
		private void FillSumVector()
		{
			for (int i = 0; i < _sumVector.Length; i++)
			{
				_sumVector[i] = RowSumWithoutDiagonalElement(i);
			}
		}

		private double RowSumWithoutDiagonalElement(int rowIndex)
		{
			double sum = 0;
			for (int i = 0; i < _matrix[i].Length; i++)
			{
				if (i != rowIndex)
				{
					sum += _matrix[rowIndex][i] * _matrix[rowIndex][i];
				}
			}
			return sum;
		}

		private int FindMaxAbsElementIndexInSpecifiedRow(int rowIndex)
		{
			int index = 0;
			double maxElement = Math.Abs(_matrix[rowIndex][index]);
			for (int i = 1; i < _matrix[i].Length; i++)
			{
				if (rowIndex != i && maxElement < Math.Abs(_matrix[rowIndex][i]))
				{
					maxElement = Math.Abs(_matrix[rowIndex][i]);
					index = i;
				}
			}
			return index;
		}

		private double FindMu(int kIndex, int lIndex) =>
			(2 * _matrix[kIndex][lIndex]) / (_matrix[kIndex][kIndex] - _matrix[lIndex][lIndex]);

		private double FindAlphaCoefficient(double mu) =>
			Math.Sqrt(0.5 * (1 + (1 / Math.Sqrt(1 + mu * mu))));

		private double FindBetaCoefficient(double mu) =>
			Math.Sign(mu) * Math.Sqrt(0.5 * (1 - (1 / Math.Sqrt(1 + mu * mu))));
		
		private double[][] CreateSpecialUMatrix(double alpha, double beta, int k, int l)
		{
			double[][] result = _matrix.GetTheIdentityMatrix();
			result[k][k] = alpha;
			result[l][l] = alpha;
			result[l][k] = beta;
			result[k][l] = -1 * beta;
			return result;
		}
		
		private double FindSumOfNonDiagonalElements() => _sumVector.Sum();
		
		private double[][] RotateMatrix(double[][] matrixToRotate, double[][] specialUMatrix)
		{
			double[][] result = specialUMatrix.FindTransposedMatrix().MultiplyBy(matrixToRotate, (first, second) => first * second, (first, second) => first + second);
			result = result.MultiplyBy(specialUMatrix, (first, second) => first * second, (first, second) => first + second);
			return result;
		}
		private void ChangeSumVector(int k, int l)
		{
			double sumK = 0, sumL = 0;
			for (int i = 0; i < _sumVector.Length; i++)
			{
				if (i != k)
					sumK += _matrix[k][i] * _matrix[k][i];
				if (i != l)
					sumL += _matrix[l][i] * _matrix[l][i];
			}
			_sumVector[k] = sumK;
			_sumVector[l] = sumL;
		}
	}
}