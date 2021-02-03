using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Methods
{
	internal class GaussMethod : Method
	{
		private double[][] workingMatrix;
		private double[] workingVector;
		public override Answer Solve()
		{
			throw new NotImplementedException();
		}
		internal GaussMethod(double[][] matrixA, double[] vectorB)
		{
			workingMatrix = matrixA;
			workingVector = vectorB;
		}
		internal GaussMethod()
		{
			workingVector = null;
			workingMatrix = null;
		}
		private void Direct()
		{
			int currentRow = 0;
			int currentCol = 0;
			
			for (; currentRow < workingMatrix.GetLength(0); currentRow++)
			{
				ApplyPartialPivot(currentRow, currentCol);
			}
		}
		private void ApplyPartialPivot(int currentRow, int currentCol)
		{
			int rowIndexOfMaxAbsInCol = GetRowIndexOfMaxABSofElementInColumn(currentRow, currentCol);
			if (rowIndexOfMaxAbsInCol != currentRow)
			{
				SwapRows(rowIndexOfMaxAbsInCol, currentRow);
			}
		}
		private int GetRowIndexOfMaxABSofElementInColumn(int startRow, int columnIndex)
		{
			int maxElemIndex = startRow;
			for (int i = startRow + 1; i < workingMatrix.GetLength(0); i++)
			{
				if (Math.Abs(workingMatrix[i][columnIndex]) > Math.Abs(workingMatrix[maxElemIndex][columnIndex]))
				{
					maxElemIndex = i;
				}
			}
			return maxElemIndex;
		}

		private void SwapRows(int firstRowIndex, int secondRowIndex)
		{
			double[] matrixBuffer = new double[workingMatrix.GetLength(0)];
			double buffer;
			workingMatrix[firstRowIndex].CopyTo(matrixBuffer, 0);
			workingMatrix[secondRowIndex].CopyTo(workingMatrix[firstRowIndex], 0);
			matrixBuffer.CopyTo(workingMatrix[secondRowIndex], 0);
			buffer = workingVector[firstRowIndex];
			workingVector[firstRowIndex] = workingVector[secondRowIndex];
			workingVector[secondRowIndex] = buffer;
		}
		private void Back()
		{

		}
	}
}
