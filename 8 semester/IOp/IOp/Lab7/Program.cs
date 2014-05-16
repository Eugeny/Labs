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
						{ 2, 999, 3,4,5 },
						{-5,8,-1,0,0},
						{2,8,2,1,3},
						{2,8,4,3,1},
						{1,999,1,2,1}
					}
				};
			}

			Console.WriteLine (task.ToLoopString (task.SolveByBranching ()));
		}
	}
}
