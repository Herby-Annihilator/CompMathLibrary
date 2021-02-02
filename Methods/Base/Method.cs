﻿using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary;

namespace CompMathLibrary.Methods.Base
{
	public abstract class Method
	{
		public abstract double[] Solve(double[][] matrixA, double[] vectorB);
	}
}
