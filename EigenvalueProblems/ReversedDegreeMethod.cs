using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.Extensions;
using System.Linq;
using CompMathLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems
{
	public class ReversedDegreeMethod : DegreeMethod
	{
		private readonly double _startLambda;
		protected ReversedDegreeMethod(double[][] matrix, double precision) : base(matrix, precision)
		{
		}
		internal ReversedDegreeMethod(double[][] matrix, double precision, double[] approximation,
			double startLambda) : base(matrix, precision, approximation)
		{
			_startLambda = startLambda;
		}

		internal override DegreeMethodAnswer Solve()
		{
			int size = _matrix.GetLength(0);
			for (int i = 0; i < size; i++)
			{
				_matrix[i][i] -= _startLambda;
			}
			int iterationNumber = 0;
			double nextLambda = _startLambda;
			double previousLambda;
			double numerator;
			double denominator;
			GaussMethod gaussMethod;
			Answer answer;
			do
			{
				iterationNumber++;
				previousLambda = nextLambda;
				gaussMethod = new GaussMethod(_matrix, _previousApproximation);
				answer = gaussMethod.Solve();
				if (answer.AnswerStatus != AnswerStatus.OneSolution)
				{
					return new DegreeMethodAnswer()
					{
						Eigenvalue = 0,
						Eigenvector = null,
						IterationsCount = iterationNumber
					};
				}
				_nextApproximation = answer.Solution[0];
				numerator = DotProductOfVectors(_previousApproximation, _previousApproximation);
				denominator = DotProductOfVectors(_nextApproximation, _previousApproximation);
				nextLambda = _startLambda + (numerator / denominator);
				NormalizeVector(_nextApproximation);
				_previousApproximation = (double[])_nextApproximation.Clone();	
			} while (!IsPrecisionAchived(previousLambda, nextLambda));
			return new DegreeMethodAnswer()
			{
				Eigenvalue = nextLambda,
				Eigenvector = _nextApproximation,
				IterationsCount = iterationNumber
			};
		}
	}
}
