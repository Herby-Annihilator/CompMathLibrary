using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.EigenvalueProblems.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems
{
	public class DegreeMethod : EigenvalueProblem<DegreeMethodAnswer>
	{
		protected double[] _approximation;
		protected double[] _nextApproximation;
		protected double[] _previousApproximation;

		protected DegreeMethod(double[][] matrix, double precision) : base(matrix, precision)
		{
		}

		internal DegreeMethod(double[][] matrix, double precision, double[] approximation) :
			this(matrix, precision)
		{
			_approximation = (double[])approximation.Clone();
			_previousApproximation = (double[])approximation.Clone();
		}

		internal override DegreeMethodAnswer Solve()
		{
			int numberOfIterations = 0;
			double nextLambda = 100;
			double previousLambda;
			do
			{
				numberOfIterations++;
				previousLambda = nextLambda;
				nextLambda = DoIterationAndReturnNextLambda();
			} while (!IsPrecisionAchived(previousLambda, nextLambda));
			DegreeMethodAnswer answer = new DegreeMethodAnswer()
			{
				IterationsCount = numberOfIterations,
				Eigenvalue = nextLambda,
				Eigenvector = (double[])_previousApproximation.Clone()
			};
			return answer;
		}
		private double DoIterationAndReturnNextLambda()
		{
			double nextLambda;
			_nextApproximation = GetNextVector(_previousApproximation);
			nextLambda = GetNextLambda(_previousApproximation, _nextApproximation);
			_previousApproximation = (double[])_nextApproximation.Clone();
			NormalizeVector(_previousApproximation);
			return nextLambda;
		}
		protected double[] GetNextVector(double[] startVector) =>
			MultiplyMatrixByColumn(_matrix, startVector);

		protected double[] MultiplyMatrixByColumn(double[][] matrix, double[] column)
		{
			int size = column.Length;
			double[] result = new double[size];
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					result[i] += matrix[i][j] * column[j];
				}
			}
			return result;
		}
		private double GetNextLambda(double[] previousVector, double[] nextVector)
		{
			double dotProductNumerator = DotProductOfVectors(nextVector, previousVector);
			double dotProductDenominator = DotProductOfVectors(previousVector, previousVector);
			return dotProductNumerator / dotProductDenominator;
		}
		protected double DotProductOfVectors(double[] first, double[] second)
		{
			double result = 0;
			for (int i = 0; i < first.Length; i++)
			{
				result += first[i] * second[i];
			}
			return result;
		}
		protected void NormalizeVector(double[] vector)
		{
			double norm = Math.Sqrt(DotProductOfVectors(vector, vector));
			for (int i = 0; i < vector.Length; i++)
			{
				vector[i] /= norm;
			}
		}
		protected bool IsPrecisionAchived(double previousLambda, double nextLambda) =>
			Math.Abs(Math.Abs(previousLambda) - Math.Abs(nextLambda)) <= _precision;
		internal override void Refresh()
		{
			_previousApproximation = (double[])_approximation.Clone();
			_nextApproximation = null;
		}
	}
}
