using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.EigenvalueProblems.Base;

using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems
{
	internal class PartialProblem : EigenvalueProblem<PartialProblemAnswer>
	{
		private double[] _approximation;
		private double _startLambda;
		internal PartialProblem(double[][] matrix, double precision) : base(matrix, precision)
		{
		}

		internal PartialProblem(double[][] matrix, double precision, double[] approximation,
			double startLambda) : this(matrix, precision)
		{
			_approximation = (double[])approximation.Clone();
			_startLambda = startLambda;
		}

		internal override PartialProblemAnswer Solve()
		{
			int numberOfIterations = 0;
			double[] previosApproximation = (double[])_approximation.Clone();
			double[] nextApproximation;
			double nextLambda;
			double dotProductNumerator;
			double dotProductDenominator;
			do
			{
				numberOfIterations++;
				nextApproximation = GetNextVector(previosApproximation);
				dotProductNumerator = DotProductOfVectors(nextApproximation, previosApproximation);
				dotProductDenominator = DotProductOfVectors(previosApproximation, previosApproximation);
				nextLambda = dotProductNumerator / dotProductDenominator;
				previosApproximation = (double[])nextApproximation.Clone();
			} while (!IsPrecisionAchived(_startLambda, nextLambda));
			PartialProblemAnswer answer = new PartialProblemAnswer();
			return answer;
		}
		private double[] GetNextVector(double[] startVector) =>
			MultiplyMatrixByColumn(_matrix, startVector);

		private double[] MultiplyMatrixByColumn(double[][] matrix, double[] column)
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
		private double DotProductOfVectors(double[] first, double[] second)
		{
			double result = 0;
			for (int i = 0; i < first.Length; i++)
			{
				result += first[i] * second[i];
			}
			return result;
		}
		private bool IsPrecisionAchived(double previousLambda, double nextLambda) =>
			Math.Abs(Math.Abs(previousLambda) - Math.Abs(nextLambda)) <= _precision;
	}
}
