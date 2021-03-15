using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods;
using CompMathLibrary.Methods.Base;
using CompMathLibrary.Creators.MethodCreators.Base;

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
	}
}
