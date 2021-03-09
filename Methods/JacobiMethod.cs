using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Methods
{
	public class JacobiMethod : Method
	{
		protected double[][] matrixA;
		protected double[] vectorB;
		protected double[] startApproximation;
		protected double precision;
		protected int numberOfIterations;
		internal JacobiMethod(double[][] matrix, double[] vector, double[] startApproximation, double precision)
		{
			matrixA = CloneMatrix(matrix);
			vectorB = (double[])vector.Clone();
			this.startApproximation = (double[])startApproximation.Clone();
			this.precision = precision;
			numberOfIterations = 0;
		}
		public override Answer Solve()
		{
			double[] nextApproximation = new double[startApproximation.Length];
			double strSum;
			do
			{
				numberOfIterations++;
				startApproximation.CopyTo(nextApproximation, 0);
				for (int i = 0; i < matrixA.GetLength(0); i++)
				{
					strSum = 0;
					for (int j = 0; j < matrixA[i].Length; j++)
					{
						if (j != i)
						{
							strSum += (matrixA[i][j] * startApproximation[j]) / matrixA[i][i];
						}
					}
					nextApproximation[i] = strSum * (-1) + vectorB[i] / matrixA[i][i];
				}
			} while (!IsPrecisionAchieved(startApproximation, nextApproximation));
			IterativeAnswer answer = new IterativeAnswer();
			answer.Solution.Add(nextApproximation);
			answer.AnswerStatus = AnswerStatus.OneSolution;
			answer.NumberOfIterations = numberOfIterations;
			return answer;
		}
		protected bool IsPrecisionAchieved(double[] previousApproximation, double[] currentApproximation)
		{
			for (int i = 0; i < previousApproximation.Length; i++)
			{
				if (Math.Abs(previousApproximation[i] - currentApproximation[i]) > precision)
				{
					return false;
				}
			}
			return true;
		}
	}
}
