using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Creators.MethodCreators.Base;
using CompMathLibrary.Methods;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Creators.MethodCreators
{
	public class SquareRootMethodCreator : DirectMethodsCreator
	{
		internal override Method Create(double[][] matrix, double[] vector)
		{
			DefaultCheck(matrix, vector);
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(0); j++)
				{
					if (matrix[i][j] != matrix[j][i])
						throw new ArgumentException("Asymmetric matrix");
				}
			}
			return new SquareRootMethod(matrix, vector);
		}
		public override string ToString() => "Square root method";
	}
}
