using System;
using System.Collections.Generic;
using System.Text;
using CompMathLibrary.Methods.Base;

namespace CompMathLibrary.Methods
{
	public class GaussMethod : Method
	{
		private double[][] workingMatrix;
		private double[] workingVector;
		private int numberOfPermutations;
		public override Answer Solve()
		{
			Direct();
			Answer answer = new Answer();
			answer.Solution = null;
			answer.AnswerStatus = CheckAnswer();
			Back(answer);
			if (answer.Solution?.Count > 0)
			{
				answer.Determinant = GetDeterminant();
			}		
			return answer;
		}
		private double GetDeterminant()
		{
			double det = workingMatrix[0][0];
			for (int i = 1; i < workingMatrix.Length; i++)
			{
				det *= workingMatrix[i][i];
			}
			if (numberOfPermutations % 2 == 0)
			{
				return det;
			}
			else
			{
				return det * (-1);
			}
		}
		internal GaussMethod(double[][] matrixA, double[] vectorB) : this()
		{
			workingMatrix = CloneMatrix(matrixA);
			workingVector = (double[])vectorB.Clone();
		}
		
		internal GaussMethod()
		{
			numberOfPermutations = 0;
			workingVector = null;
			workingMatrix = null;
		}
		private void Direct()   // приводит матрицу к трапецевидному виду
		{
			int currentRow = 0;
			int currentCol = 0;			
			for (; currentCol < workingMatrix[0].Length; currentRow++, currentCol++)
			{
				ApplyPartialPivot(currentRow, currentCol);
				AddSpecifiedRowToOthers(currentRow, currentCol);
			}
		}
		private void ApplyPartialPivot(int currentRow, int currentCol)
		{
			int rowIndexOfMaxAbsInCol = GetRowIndexOfMaxABSofElementInColumn(currentRow, currentCol);
			if (rowIndexOfMaxAbsInCol != currentRow)
			{
				SwapRows(rowIndexOfMaxAbsInCol, currentRow);
				numberOfPermutations++;
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

		private void AddSpecifiedRowToOthers(int rowIndex, int colIndex)
		{
			try
			{
				double coefficient = 0;
				for (int i = 1; i < workingMatrix.GetLength(0) - rowIndex; i++)
				{
					coefficient = workingMatrix[rowIndex + i][colIndex] / workingMatrix[rowIndex][colIndex];  // a21/a11; a31/a11; etc.
					if (double.IsNaN(coefficient) || double.IsInfinity(coefficient))
					{
						throw new Exception("Division by zero. The matrix is degenerate.");
					}
					for (int j = colIndex; j < workingMatrix[i].Length; j++)
					{
						workingMatrix[rowIndex + i][j] -= workingMatrix[rowIndex][j] * coefficient;// a21 - a11*(a21/a11); a22 - a11*(a21/a11)
					}
					workingVector[rowIndex + i] -= workingVector[rowIndex] * coefficient;  // b2 - b1*(a21/a11); b3 - b1*(a21/a11)
				}
			}			
			catch (Exception e)
			{
				throw new Exception("Division by zero. The matrix is degenerate.");
			}
		}
		


		private void Back(Answer answer)
		{
			switch(answer.AnswerStatus)
			{
				case AnswerStatus.OneSolution:
					{
						answer.Solution = new List<double[]>();
						answer.Solution.Add(new double[workingMatrix[0].Length]);
						for (int currentRow = workingMatrix.GetLength(0) - 1; currentRow >=0; currentRow--)
						{
							answer.Solution[0][currentRow] = workingVector[currentRow] / workingMatrix[currentRow][currentRow];
							for (int i = 0; i < currentRow; i++)
							{
								workingVector[i] = workingVector[i] - 
									workingMatrix[i][currentRow] * answer.Solution[0][currentRow];
							}
						}
						break;
					}
				default:
					{
						answer.Solution = null;
						break;
					}
			}
		}
		private AnswerStatus CheckAnswer()
		{
			int freeVars = 0;
			for (int row = workingMatrix.GetLength(0) - 1; row >= 0; row--)
			{
				if (IsThisRowZero(row))
				{
					if (workingVector[row] != 0)
					{
						return AnswerStatus.NoSolutions;
					}
					continue;
				}
				else
				{
					for (int col = row; col < workingMatrix[row].Length; col++)
					{
						if (Math.Abs(workingMatrix[row][col]) > double.Epsilon)
						{
							freeVars++;
						}
					}
					break;
				}
			}
			if (freeVars == 1) return AnswerStatus.OneSolution;
			return AnswerStatus.SeveralSolutions;
		}
		private bool IsThisRowZero(int index)
		{
			for (int i = 0; i < workingMatrix[index].Length; i++)
			{
				if (Math.Abs(workingMatrix[index][i]) > double.Epsilon)
					return false;
			}
			return true;
		}
	}
}
