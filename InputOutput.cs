using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CompMathLibrary
{
	public static class InputOutput
	{
		public static void ReadVectorBAndMatrixAFromFile(string fileName, out double[][] matrixA, out double[] vectorB)
		{
			matrixA = null;
			vectorB = null;
			StreamReader reader = new StreamReader(fileName);
			string buffer;
			bool matrixAWritten = false;
			bool vectorBWritten = false;
			List<string[]> strs = new List<string[]>();
			List<double> vector = new List<double>();
			while ((buffer = reader.ReadLine()) != null)
			{
				if (string.IsNullOrWhiteSpace(buffer))
				{
					continue;
				}
				if (!matrixAWritten)
				{
					while(buffer != null && !string.IsNullOrWhiteSpace(buffer))
					{
						strs.Add(buffer.Split(' ', StringSplitOptions.RemoveEmptyEntries));
						buffer = reader.ReadLine();
					}
					matrixAWritten = true;
					continue;
				}
				if (!vectorBWritten)
				{
					while (buffer != null && !string.IsNullOrWhiteSpace(buffer))
					{
						vector.Add(Convert.ToDouble(buffer));
						buffer = reader.ReadLine();
					}
					vectorBWritten = true;
				}

			}
			vectorB = vector.ToArray();
			matrixA = new double[strs.Count][];
			for (int i = 0; i < strs.Count; i++)
			{
				matrixA[i] = new double[strs[i].Length];
				for (int j = 0; j < strs[i].Length; j++)
				{
					matrixA[i][j] = Convert.ToDouble(strs[i][j]);
				}
			}
			reader.Close();
		}
		public static void SaveMatrixToFileCorrectly(string fileName, double[][] matrix)
		{
			bool rewriteVectorBToFile = File.Exists("vectorB.txt");
			SaveMatrixToFile(fileName, matrix);
			SaveMatrixToHiddenFile("matrixA.txt", matrix);
			if (rewriteVectorBToFile)
			{
				AppendFirstFileToSecondFile("vectorB.txt", fileName, "\r\n");
			}			
		}
		public static void SaveMatrixToFile(string fileName, double[][] matrix)
		{
			StreamWriter writer = new StreamWriter(fileName);
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					writer.Write(matrix[i][j] + " ");
				}
				writer.WriteLine();
			}
			writer.Close();
		}
		
		public static void SaveVectorToFileCorrectly(string fileName, double[] vector)
		{
			if (File.Exists("matrixA.txt"))
			{
				CopyFirstFileToSecondFile("matrixA.txt", fileName);
				File.SetAttributes(fileName, RemoveAttribute(File.GetAttributes(fileName),
					FileAttributes.Hidden));
			}
			SaveVectorToHiddenFile("vectorB.txt", vector);
			AppendFirstFileToSecondFile("vectorB.txt", fileName, "\r\n");
		}
		private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
		{
			return attributes & ~attributesToRemove;
		}
		public static void SaveVectorToHiddenFile(string fileName, double[] vector)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
			SaveVectorToFile(fileName, vector);
			File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);
		}
		public static void SaveMatrixToHiddenFile(string fileName, double[][] matrix)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
			SaveMatrixToFile(fileName, matrix);
			File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);
		}
		
		public static void SaveVectorToFile(string fileName, double[] vector)
		{
			StreamWriter writer = new StreamWriter(fileName);
			for (int i = 0; i < vector.Length; i++)
			{
				writer.WriteLine(vector[i]);
			}
			writer.Close();
		}

		public static void AppendFirstFileToSecondFile(string firstFile, string secondFile, string separationStr)
		{
			StreamWriter writer = new StreamWriter(secondFile, true);
			StreamReader reader = new StreamReader(firstFile);
			writer.Write(separationStr + reader.ReadToEnd());
			reader.Close();
			writer.Close();
		}
		public static void CopyFirstFileToSecondFile(string firstFile, string secondFile)
		{
			if (File.Exists(secondFile))
				File.Delete(secondFile);
			File.Copy(firstFile, secondFile);
		}
	}
}
