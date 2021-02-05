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
			double det = Math.Abs(workingMatrix[0][0]);
			for (int i = 1; i < workingMatrix.Length; i++)
			{
				det *= Math.Abs(workingMatrix[i][i]);
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
			workingMatrix = matrixA;
			workingVector = vectorB;
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
			double coefficient = 0;
			for (int i = rowIndex; i < workingMatrix.GetLength(0) - 1; i++)
			{
				coefficient = workingMatrix[i + 1][colIndex] / workingMatrix[i][colIndex];  // a21/a11; a31/a11; etc.
				for (int j = colIndex; j < workingMatrix[i].Length; j++)
				{
					workingMatrix[i + 1][j] -= workingMatrix[i][j] * coefficient;// a21 - a11*(a21/a11); a22 - a11*(a21/a11)
				}
				workingVector[i + 1] -= workingVector[i] * coefficient;  // b2 - b1*(a21/a11); b3 - b1*(a21/a11)
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
						for (int currentRow = workingMatrix.GetLength(0); currentRow >=0; currentRow--)
						{
							answer.Solution[0][currentRow] = workingVector[currentRow];
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
