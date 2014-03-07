using System;
using LabLib;

namespace Lab3
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var tasks = new ResourceTask[] {
				// test
				new ResourceTask {
					N = 3, C = 6,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 3, 4, 5, 8, 9, 10 } [x],
						(x) => new int[]{ 0, 2, 3, 7, 9, 12, 13 } [x],
						(x) => new int[]{ 0, 1, 2, 6, 11, 11, 13 } [x],
					}
				},

				// 1
				new ResourceTask {
					N = 3, C = 6,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 1, 2, 2, 4, 5, 6 } [x],
						(x) => new int[]{ 0, 2, 3, 5, 7, 7, 8 } [x],
						(x) => new int[]{ 0, 2, 4, 5, 6, 7, 7 } [x],
					}
				},

				// 2
				new ResourceTask {
					N = 3, C = 6,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 1, 1, 3, 6, 10, 11 } [x],
						(x) => new int[]{ 0, 2, 3, 5, 6, 7, 13 } [x],
						(x) => new int[]{ 0, 1, 4, 4, 7, 8, 9 } [x],
					}
				},

				// 3
				new ResourceTask {
					N = 3, C = 7,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 1, 2, 4, 8, 9, 9, 23 } [x],
						(x) => new int[]{ 0, 2, 4, 6, 6, 8, 10, 11 } [x],
						(x) => new int[]{ 0, 3, 4, 7, 7, 8, 8, 24 } [x],
					}
				},

				// 4
				new ResourceTask {
					N = 3, C = 7,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 3, 3, 6, 7, 8, 9, 14 } [x],
						(x) => new int[]{ 0, 2, 4, 4, 5, 6, 8, 13 } [x],
						(x) => new int[]{ 0, 1, 1, 2, 3, 3, 10, 11 } [x],
					}
				},

				// 5
				new ResourceTask {
					N = 4, C = 8,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 2, 2, 3, 5, 8, 8, 10, 17 } [x],
						(x) => new int[]{ 0, 1, 2, 5, 8, 10, 11, 13, 15 } [x],
						(x) => new int[]{ 0, 4, 4, 5, 6, 7, 13, 14, 14 } [x],
						(x) => new int[]{ 0, 1, 3, 6, 9, 10, 11, 14, 16 } [x],
					}
				},

				// 6
				new ResourceTask {
					N = 4, C = 11,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 1, 3, 4, 5, 8, 9, 9, 11, 12, 12, 14 } [x],
						(x) => new int[]{ 0, 1, 2, 3, 3, 3, 7, 12, 13, 14, 17, 19 } [x],
						(x) => new int[]{ 0, 4, 4, 7, 7, 8, 12, 14, 14, 16, 18, 22 } [x],
						(x) => new int[]{ 0, 5, 5, 5, 7, 9, 13, 13, 15, 15, 19, 24 } [x],
					}
				},

				// 7
				new ResourceTask {
					N = 5, C = 11,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 4, 4, 6, 9, 12, 12, 15, 16, 19, 19, 19 } [x],
						(x) => new int[]{ 0, 1, 1, 1, 4, 7, 8, 8, 13, 13, 19, 20 } [x],
						(x) => new int[]{ 0, 2, 5, 6, 7, 8, 9, 11, 11, 13, 13, 18 } [x],
						(x) => new int[]{ 0, 1, 2, 4, 5, 7, 8, 8, 9, 9, 15, 19 } [x],
						(x) => new int[]{ 0, 2, 5, 7, 8, 9, 10, 10, 11, 14, 17, 21 } [x],
					}
				},

				// 8
				new ResourceTask {
					N = 6, C = 10,
					F = new Func<int, int>[] {
						(x) => new int[]{ 0, 1, 2, 2, 2, 3, 5, 8, 9, 13, 14 } [x],
						(x) => new int[]{ 0, 1, 3, 4, 5, 5, 5, 7, 7, 10, 12, 12 } [x],
						(x) => new int[]{ 0, 2, 2, 3, 4, 6, 6, 8, 9, 11, 17 } [x],
						(x) => new int[]{ 0, 1, 1, 1, 2, 3, 9, 9, 11, 12, 15 } [x],
						(x) => new int[]{ 0, 2, 7, 7, 7, 9, 9, 10, 11, 12, 13 } [x],
						(x) => new int[]{ 0, 2, 5, 5, 5, 6, 6, 7, 12, 18, 22 } [x],
					}
				},
			};
			Console.WriteLine (tasks [7].SolveBellman ());
		}
	}
}
