using CompMathLibrary.EigenvalueProblems.Answers;
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
		internal MethodOfRotations(double[][] matrix, double precision) : base(matrix, precision)
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
			while (s >= _precision)
			{
				mu = FindMu(k, l);
				alpha = FindAlphaCoefficient(mu, k, l);
				beta = FindBetaCoefficient(mu, k, l);
				currentOrthogonalMatrix = CreateSpecialUMatrix(alpha, beta, k, l);
				dMatrix = MultMatrixByMatrix(dMatrix, currentOrthogonalMatrix);
				_matrix = RotateMatrix(_matrix, currentOrthogonalMatrix);
				ChangeSumVector(k, l);
				s = FindSumOfNonDiagonalElements(_matrix);				
				k = _sumVector.IndexOf(_sumVector.Max(), (first, second) =>
				{
					if (first > second) return 1;
					if (first < second) return -1;
					return 0;
				});
				l = FindMaxAbsElementIndexInSpecifiedRow(k);
			}
			MethodOfRotationsAnswer answer = new MethodOfRotationsAnswer();
			answer.Eigenvalues = DiagonalElementsAsVector(_matrix);
			answer.Eigenvectors = new double[answer.Eigenvalues.Length][];
			for (int i = 0; i < answer.Eigenvalues.Length; i++)
			{
				answer.Eigenvectors[i] = dMatrix.PresentSpecifiedColumnAsVector(i);
			}
			answer.Residuals = new double[answer.Eigenvalues.Length][];
			for (int i = 0; i < answer.Eigenvalues.Length; i++)
			{
				answer.Residuals[i] = _clone.MultiplyByColumn(answer.Eigenvectors[i], (first, second) => first * second, (first, second) => first + second).DoOperationWithVector(answer.Eigenvectors[i].MultiplyBy(answer.Eigenvalues[i]), (first, second) => first - second);
			}
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
			int size = _matrix[rowIndex].Length;
			for (int i = 0; i < size; i++)
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
			for (int i = 1; i < _matrix[rowIndex].Length; i++)
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

		private double FindAlphaCoefficient(double mu, int k, int l) => _matrix[k][k] == _matrix[l][l] ? Math.Sqrt(0.5) : Math.Sqrt(0.5 * (1 + (1 / Math.Sqrt(1 + mu * mu))));

		private double FindBetaCoefficient(double mu,int k, int l) => _matrix[k][k] == _matrix[l][l] ? Math.Sqrt(0.5) : Math.Sign(mu) * Math.Sqrt(0.5 * (1 - (1 / Math.Sqrt(1 + mu * mu))));
		
		private double[][] CreateSpecialUMatrix(double alpha, double beta, int k, int l)
		{
			double[][] result = _matrix.GetTheIdentityMatrix();
			result[k][k] = alpha;
			result[l][l] = alpha;
			result[l][k] = beta;
			result[k][l] = (-1) * beta;
			return result;
		}
		
		private double FindSumOfNonDiagonalElements() => _sumVector.Sum();
		private double FindSumOfNonDiagonalElements(double[][] matr)
		{
			double res = 0;
			for (int i = 0; i < matr.GetLength(0); i++)
			{
				for (int j = 0; j < matr[i].Length; j++)
				{
					if (i != j)
					{
						res += matr[i][j] * matr[i][j];
					}
				}
			}
			return res;
		}


		private double[][] RotateMatrix(double[][] matrixToRotate, double[][] specialUMatrix)
		{
			double[][] result;
			result = MultMatrixByMatrix(FindTransposenMatrix(specialUMatrix), matrixToRotate);
			result = MultMatrixByMatrix(result, specialUMatrix);
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

		private double[] DiagonalElementsAsVector(double[][] matrix)
		{
			double[] result = new double[matrix.GetLength(0)];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				result[i] = matrix[i][i];
			}
			return result;
		}


		double[][] MultMatrixByMatrix(double[][] matr, double[][] matr1) //умножить матрицу на матрицу
		{
			int n = matr.GetLength(0);
			double[][] res = new double[n][];
			for (int i = 0; i < n; i++)
			{
				res[i] = new double[n];
				for (int j = 0; j < n; j++)
				{
					double sum = 0;
					for (int k = 0; k < n; k++)
					{
						sum += matr[i][k] * matr1[k][j];
					}
					res[i][j] = sum;
				}
			}
			return res;
		}

		double[][] FindTransposenMatrix(double[][] matr) //найти транспонированную матрицу
		{
			int n = matr.GetLength(0);
			double[][] res = new double[n][];
			for (int i = 0; i < n; i++)
			{
				res[i] = new double[n];
				for (int j = 0; j < n; j++)
					res[i][j] = matr[j][i];
			}
			return res;
		}

	}
}
