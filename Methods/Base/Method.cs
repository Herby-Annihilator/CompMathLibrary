using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary;

namespace CompMathLibrary.Methods.Base
{
	public abstract class Method
	{
		public abstract Answer Solve();
		protected virtual double[][] CloneMatrix(double[][] matr)
		{
			double[][] clone = new double[matr.GetLength(0)][];
			for (int i = 0; i < clone.GetLength(0); i++)
			{
				clone[i] = new double[matr[i].Length];
				for (int j = 0; j < clone[i].Length; j++)
				{
					clone[i][j] = matr[i][j];
				}
			}
			return clone;
		}
	}
	
}
