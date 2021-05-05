using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.EigenvalueProblems;
using CompMathLibrary.Creators.MethodCreators;

namespace CompMathLibrary
{
	public class MethodsFactory
	{
		public Method Build(double[][] matrixA, double[] vectorB, DirectMethodsCreator creator)
		{
			return creator.Create(matrixA, vectorB);
		}
		public Method Build(double[][] matrixA, double[] vectorB, double[] approximation,
			double precision, IterativeMethodsCreator creator)
		{
			return creator.Create(matrixA, vectorB, approximation, precision);
		}

		public DegreeMethod Build(double[][] matrix, double[] startVector, double precision, DegreeMethodCreator creator)
		{
			return creator.Create(matrix, startVector, precision);
		}
		public ReversedDegreeMethod Build(double[][] matrix, double[] startVector, double precision, double startLambda, ReversedDegreeMethodCreator creator)
		{
			return (ReversedDegreeMethod)creator.Create(matrix, startVector, precision, startLambda);
		}

		public MethodOfRotations Build(double[][] matrix, double precision, MethodOfRotationsCreator creator)
		{
			return creator.Create(matrix, precision);
		}
	}
}
