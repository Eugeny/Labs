using System;
using LabLib;

namespace Lab7
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int t = 0;
			CommivoyageurTask task = null;
			if (t == 0) {
				task = new CommivoyageurTask {
					N = 5,
					C = new int[,] {
						{ 0, 10, 25, 25, 10 },
						{ 1, 0, 10, 15, 2 },
						{ 8, 9, 0, 20, 10 },
						{ 14, 10, 24, 0, 15 },
						{ 10, 8, 25, 27, 0 },
					}
				};
			}

			Console.WriteLine (task.ToLoopString (task.SolveByBranching ()));
		}
	}
}
