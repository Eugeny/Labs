using System;
using LabLib;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab6
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int t = 2;
			TaskAssignment task = null;
			if (t == 0) {
				task = new TaskAssignment {
					N = 4,
					C = new int[,] {
						{ 2, 10, 9, 7 },
						{ 15, 4, 14, 8 },
						{ 13, 14, 16, 11 },
						{ 4, 15, 13, 19 },
					}
				};
			}
			if (t == 2) {
				task = new TaskAssignment {
					N = 7,
					C = new int[,] {
						{ 9, 6, 4, 9, 3, 8, 0 },
						{ 5, 8, 6, 8, 8, 3, 5 },
						{ 5, 2, 1, 1, 8, 6, 8 },
						{ 1, 0, 9, 2, 5, 9, 2 },
						{ 9, 2, 3, 3, 0, 3, 0 },
						{ 7, 3, 0, 9, 4, 5, 6 },
						{ 0, 9, 6, 0, 8, 8, 9 },
					}
				};
			}

			task = new TaskAssignment {
				N = 5,
				C = new int[,] {
					{ 2, 999, 3,4,5 },
					{-5,8,-1,0,0},
					{2,8,2,1,3},
					{2,8,4,3,1},
					{1,999,1,2,1}
						//{ 999, 2, 3, 1, 4 },
						//{ 1, 999, 2, 1, 3 },
						//{ 999, 1, 999, 1, 1 },
						//{ 1, 2, 10, 999, 3 },
						//{ 1, 2, 999, 4, 999 },
				}
			};
			var r = task.Solve ();
			for (int i = 0; i < task.N; i++) {
				for (int j = 0; j < task.N; j++)
					Console.Write ("{0} ", task.C [i, j]);
				Console.WriteLine ();
			}
			int c = 0;
			var Co = new int[,] {
				{ 2, 999, 3,4,5 },
				{-5,8,-1,0,0},
				{2,8,2,1,3},
				{2,8,4,3,1},
				{1,999,1,2,1}
				//{ 999, 2, 3, 1, 4 },
				//{ 1, 999, 2, 1, 3 },
				//{ 999, 1, 999, 1, 1 },
				//{ 1, 2, 10, 999, 3 },
				//{ 1, 2, 999, 4, 999 },
			};
			for (int i = 0; i < task.N; i++) {
				Console.WriteLine ("{0} -> {1}", i + 1, r [i] + 1);
				c += Co[i,r[i]];
			}
			Console.WriteLine(c);
		}
	}
}
