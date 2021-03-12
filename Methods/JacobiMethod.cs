using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using System.Linq;

namespace CompMathLibrary.Methods
{
	public class JacobiMethod : Method
	{
		protected double[][] matrixA;
		protected double[] vectorB;
		protected double[] previousApproximation;
		protected double precision;
		protected int numberOfIterations;
		internal JacobiMethod(double[][] matrix, double[] vector, double[] startApproximation, double precision)
		{
			matrixA = CloneMatrix(matrix);
			vectorB = (double[])vector.Clone();
			this.previousApproximation = (double[])startApproximation.Clone();
			this.precision = precision;
			numberOfIterations = 0;
		}
		public override Answer Solve()
		{
			double[] nextApproximation = new double[previousApproximation.Length];
			double strSum;
			previousApproximation.CopyTo(nextApproximation, 0);
			do
			{
				numberOfIterations++;
				nextApproximation.CopyTo(previousApproximation, 0);
				for (int i = 0; i < matrixA.GetLength(0); i++)
				{
					strSum = 0;
					for (int j = 0; j < matrixA[i].Length; j++)
					{
						if (j != i)
						{
							strSum += (matrixA[i][j] * previousApproximation[j])/* / matrixA[i][i]*/;
							if (double.IsInfinity(strSum))
							{
								throw new NoSolutionException("Метод расходится. Число итераций " + numberOfIterations + " точность " + precision);
							}
						}
					}
					nextApproximation[i] = (vectorB[i] /*/ matrixA[i][i]*/ - strSum) / matrixA[i][i];
					if (double.IsInfinity(nextApproximation[i]))
					{
						throw new NoSolutionException("Метод расходится. Число итераций " + numberOfIterations + " точность " + precision);
					}
				}
			} while (!IsPrecisionAchieved(previousApproximation, nextApproximation));
			IterativeAnswer answer = new IterativeAnswer();
			answer.Solution.Add(nextApproximation);
			answer.AnswerStatus = AnswerStatus.OneSolution;
			answer.NumberOfIterations = numberOfIterations;
			answer.ConditionOfDiagonalDominance = IsTheConditionOfDiagonalDominanceSatisfied();
			return answer;
		}
		protected virtual bool IsPrecisionAchieved(double[] previousApproximation, double[] currentApproximation) =>
			previousApproximation.Zip(currentApproximation, (prev, current) => prev - current)
				.Max((number) => Math.Abs(number)) <= precision;
		protected bool IsTheConditionOfDiagonalDominanceSatisfied()
		{
			double diagonal;
			double sum;
			for (int i = 0; i < matrixA.GetLength(0); i++)
			{
				diagonal = Math.Abs(matrixA[i][i]);
				sum = 0;
				for (int j = 0; j < matrixA[i].Length; j++)
				{
					if (i != j)
						sum += Math.Abs(matrixA[i][j]);
				}
				if (diagonal <= sum)
					return false;
			}
			return true;
		}
	}
}
