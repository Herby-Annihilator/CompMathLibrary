using CompMathLibrary.EigenvalueProblems.Answers;
using CompMathLibrary.Extensions;
using System.Linq;
using CompMathLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompMathLibrary.EigenvalueProblems
{
	internal class ReversedDegreeMethod : DegreeMethod
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
			double[][] workingMatrix = _matrix.SubtractOrAddMatrix(
				_matrix.GetTheIdentityMatrix().MultiplyByANumber(_startLambda, (a, b) => a * b),
				(a, b) => a - b);
			int iterationNumber = 0;
			double nextLambda;
			double previousLambda = _startLambda;
			double numerator;
			double denominator;
			GaussMethod gaussMethod;
			Answer answer;
			do
			{
				iterationNumber++;
				gaussMethod = new GaussMethod(workingMatrix, _previousApproximation);
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
				NormalizeVector(_nextApproximation);
				numerator = DotProductOfVectors(_nextApproximation, _previousApproximation);
				denominator = DotProductOfVectors(_previousApproximation, _previousApproximation);
				nextLambda = previousLambda + (numerator / denominator);
				_previousApproximation = (double[])_nextApproximation.Clone();
				previousLambda = nextLambda;
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
