using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Methods;
using System.Linq;
using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.Creators.MethodCreators;
using CompMathLibrary.EigenvalueProblems;
using CompMathLibrary.EigenvalueProblems.Answers;

namespace CompMathLibrary
{
	public class CMReshala
	{
		private const double DEFAULT_PRECISION = 0.000001;
		private const double FINE = 100;  // штраф для прибавки к главной диагонали матрицы, чтобы найти максимльное/минимальное собственное число
		private MethodsFactory Factory { get; set; }
		public Answer SolveSystemOfLinearAlgebraicEquations(double[][] matrixA, double[] vectorB, DirectMethodsCreator creator)
		{
			return Factory.Build(matrixA, vectorB, creator).Solve();
		}
		public IterativeAnswer SolveSystemOfLinearAlgebraicEquationsIteratively(double[][] matrixA,
			double[] vectorB, double[] approximation, double precision, IterativeMethodsCreator creator) =>
			(IterativeAnswer)Factory.Build(matrixA, vectorB, approximation, precision, creator).Solve();
		public CMReshala()
		{
			Factory = new MethodsFactory();
		}
		/// <summary>
		/// Return's value is a max of sums of the rows' elements
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public double GetMatrixMNorm(double[][] matrix)
		{
			double norm = 0;
			double[] sums = new double[matrix.GetLength(0)];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					sums[i] += Math.Abs(matrix[i][j]);
				}
			}
			norm = sums[0];
			for (int i = 1; i < sums.Length; i++)
			{
				if (sums[i] > norm)
				{
					norm = sums[i];
				}
			}
			return norm;
		}
		public double[][] CreateRandomMatrix(int rowsCount, int colsCount, int min, int max)
		{
			Random random = new Random();
			double[][] matr = new double[rowsCount][];
			for (int i = 0; i < rowsCount; i++)
			{
				matr[i] = new double[colsCount];
				for (int j = 0; j < colsCount; j++)
				{
					matr[i][j] = random.Next(min, max);
				}
			}
			return matr;
		}
		public double[][] GetReversedMatrix(double[][] sourceMatrix, DirectMethodsCreator creator)
		{
			double[][] reversedMatrix = new double[sourceMatrix.GetLength(0)][];
			for (int i = 0; i < reversedMatrix.Length; i++)
			{
				reversedMatrix[i] = new double[sourceMatrix[i].Length];
			}
			int colsCount = reversedMatrix[0].Length;
			double[] tmpVector = new double[sourceMatrix.GetLength(0)];
			List<double[]> currentSolution;
			int nextIndex = 0;
			for (int i = 0; i < colsCount; i++)
			{
				tmpVector[nextIndex] = 1;
				currentSolution = SolveSystemOfLinearAlgebraicEquations(sourceMatrix, tmpVector,
					creator).Solution;
				if (currentSolution == null)
					throw new Exception("Reversed matrix was not found");
				for (int j = 0; j < reversedMatrix.GetLength(0); j++)
				{
					reversedMatrix[j][i] = currentSolution[0][j];
				}
				tmpVector[nextIndex] = 0;
				nextIndex++;
			}
			return reversedMatrix;
		}
		public double[][] CreateGilbertMatrix(int matrixSize)
		{
			double[][] gilbertMatrix = new double[matrixSize][];
			for (int i = 0; i < matrixSize; i++)
			{
				gilbertMatrix[i] = new double[matrixSize];
			}
			for (int i = 0; i < matrixSize; i++)
			{				
				for (int j = i; j < matrixSize; j++)
				{
					gilbertMatrix[i][j] = (double)1 / ((double)i + 1 + (double)j + 1 - 1);
					gilbertMatrix[j][i] = gilbertMatrix[i][j];
				}
			}
			return gilbertMatrix;
		}
		public bool IsTheConditionOfDiagonalDominanceSatisfied(double[][] matrix)
		{
			double diagonal;
			double sum;
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				diagonal = Math.Abs(matrix[i][i]);
				sum = 0;
				for (int j = 0; j < matrix[i].Length; j++)
				{
					if (i != j)
						sum += Math.Abs(matrix[i][j]);
				}
				if (diagonal <= sum)
					return false;
			}
			return true;
		}

		public double[][] CreateMatrixWithoutDiagonalDominance(int rowsCount, int min, int max)
		{
			Random random = new Random();
			double[][] matrix = CreateRandomMatrix(rowsCount, rowsCount, min, max);
			if (IsTheConditionOfDiagonalDominanceSatisfied(matrix))
			{
				int randomStr = random.Next(0, rowsCount);
				matrix[randomStr][randomStr] = matrix[randomStr].Sum((number) =>
				Math.Abs(number));
			}
			return matrix;
		}
		public double CalculateDiagonalDominance(double[][] matrix)
		{
			double maxDiagonal = 0;
			double ratio;
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				ratio = (matrix[i].Sum((number) => Math.Abs(number)) - Math.Abs(matrix[i][i]))
					/ Math.Abs(matrix[i][i]);
				if (ratio > maxDiagonal)
				{
					maxDiagonal = ratio;
				}
			}
			return maxDiagonal;
		}
		public double[] CreateRandomVector(int size, int min, int max)
		{
			Random random = new Random();
			double[] vector = new double[size];
			for (int i = 0; i < size; i++)
			{
				vector[i] = random.Next(min, max);
			}
			return vector;
		}

		public DegreeMethodAnswer FindLargestEigenvalueAbsAndEigenvector(double[][] matrix, double[] startVector, double precision)
		{
			DegreeMethod degreeMethod = Factory.Build(matrix, startVector, precision, new DegreeMethodCreator());
			return degreeMethod.Solve();
		}
		public DegreeMethodAnswer FindClosestEigenvalueToAGivenOne(double[][] matrix, double[] vector, double precision, double startLambda)
		{
			double[][] workingMatrix = new double[matrix.GetLength(0)][];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				workingMatrix[i] = (double[])matrix[i].Clone();
				workingMatrix[i][i] -= startLambda;
			}
			DegreeMethod reversedDegreeMethod = Factory.Build(GetReversedMatrix(workingMatrix, new GaussMethodCreator()), vector, precision, new DegreeMethodCreator());
			var answer = reversedDegreeMethod.Solve();
			answer.Eigenvalue = startLambda + 1 / answer.Eigenvalue;
			return answer;
		}

		public DegreeMethodAnswer FindMaxEigenvalue(double[][] matrix, double[] vector, double precision)
		{
			double[][] workingMatrix = new double[matrix.GetLength(0)][];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				workingMatrix[i] = (double[])matrix[i].Clone();
				workingMatrix[i][i] += FINE;
			}
			DegreeMethod degreeMethod = Factory.Build(workingMatrix, vector, precision, new DegreeMethodCreator());
			var answer = degreeMethod.Solve();
			answer.Eigenvalue -= FINE;
			return answer;
		}
		public DegreeMethodAnswer FindMinEigenvalue(double[][] matrix, double[] vector, double precision)
		{
			double[][] workingMatrix = new double[matrix.GetLength(0)][];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				workingMatrix[i] = (double[])matrix[i].Clone();
				workingMatrix[i][i] -= FINE;
			}
			DegreeMethod degreeMethod = Factory.Build(workingMatrix, vector, precision, new DegreeMethodCreator());
			var answer = degreeMethod.Solve();
			answer.Eigenvalue += FINE;
			return answer;
		}
		public DegreeMethodAnswer FindMinAbsEigenvalue(double[][] matrix, double[] vector, double precision)
		{
			return FindClosestEigenvalueToAGivenOne(matrix, vector, precision, 0);
		}

		public MethodOfRotationsAnswer SolveFullEigenValueProblem(double[][] matrix, double precision)
		{
			return Factory.Build(matrix, precision, new MethodOfRotationsCreator()).Solve();
		}


	}
}
