using System;
using LabLib;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ILPTask task = null;

			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					7.0, -2, 6, 0, 5, 2,
				}),
				B = DenseVector.OfEnumerable (new [] { 
					-8.0, 22, 30
				}),
				M = 3,
				N = 6,
				A = DenseMatrix.OfArray (new [,] {
					{ 1.0, -5, 3, 1, 0, 0 },
					{ 4.0, -1, 1, 0, 1, 0, },
					{ 2.0, 4, 2, 0, 0, 1 },
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 0, 0, 0, 0, 0
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					1e100, 1e100, 1e100, 1e100, 1e100, 1e100
				}),
			};

			var result = task.SolveILPByCutoff ();
			Console.WriteLine ("=========== FINAL SOLUTION");
			Console.WriteLine (result);
			Console.WriteLine ("VALUE");
			Console.WriteLine (task.C * result);

		}
	}
}
