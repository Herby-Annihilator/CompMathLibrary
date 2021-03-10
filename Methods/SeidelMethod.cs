using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.Methods
{
	public class SeidelMethod : JacobiMethod
	{
		internal SeidelMethod(double[][] matrix, double[] vector, double[] startApproximation, double precision) : base(matrix, vector, startApproximation, precision)
		{
		}
		public override Answer Solve()
		{
			double[] tempApproximation = new double[previousApproximation.Length];
			double strSum;
			do
			{
				numberOfIterations++;
				previousApproximation.CopyTo(tempApproximation, 0);
				for (int i = 0; i < matrixA.GetLength(0); i++)
				{
					strSum = 0;
					for (int j = 0; j < matrixA[i].Length; j++)
					{
						if (j != i)
						{
							strSum += (matrixA[i][j] * previousApproximation[j]) / matrixA[i][i];
						}
					}
					previousApproximation[i] = strSum * (-1) + vectorB[i] / matrixA[i][i];
				}
			} while (!IsPrecisionAchieved(tempApproximation, previousApproximation));
			IterativeAnswer answer = new IterativeAnswer();
			answer.Solution.Add(tempApproximation);
			answer.AnswerStatus = AnswerStatus.OneSolution;
			answer.NumberOfIterations = numberOfIterations;
			return answer;
		}
	}
}
