using System;
using LabLib;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;

namespace SimplexTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var task = new LPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					1.0, -3, -5, 1
				}),
				B = DenseVector.OfEnumerable (new [] { 
					5.0, 9
				}),
				M = 2,
				N = 4,
				A = DenseMatrix.OfArray (new [,] {
					{ 1.0, 4, 4, 1 },
					{ 1.0, 7, 8, 2 },
				}),
				Jb = new List<int>{ 0, 2 }
			};
			var result = task.SolveSimplex ();
			for (int i = 0; i < task.M; i++)
				Console.WriteLine (task.Jb[i]);
			Console.WriteLine (result);
			Console.WriteLine ("VALUE");

			var s = 0.0;
			for (int i = 0; i < task.M; i++)
				s += task.C [task.Jb [i]] * result [i];

			Console.WriteLine (s);
		}
	}
}
