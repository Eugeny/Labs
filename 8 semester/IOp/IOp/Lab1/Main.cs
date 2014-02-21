using System;
using System.Collections.Generic;
using LabLib;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ILPTask task = null;

			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					-2.0, 1, -2, -1, 8, -5, 3, 5, 1, 2,
				}),
				B = DenseVector.OfEnumerable (new [] { 
					27.0, 6, 18
				}),
				M = 3,
				N = 10,
				A = DenseMatrix.OfArray (new [,] {
					{ 1.0, 0, 0, 1, 1, -3, 4, -1, 3, 3 },
					{ 0.0, 1, 0, -2, 1, 1, 7, 3, 4, 5 },
					{ 0.0, 0, 1, 1, 0, 2, -2, 1, -4, 7 },
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					8.0, 7, 6, 7, 8, 5, 6, 7, 8, 5,
				}),
			};


			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					1.0, 2, 3, -1, 4, -5, 6
				}),
				B = DenseVector.OfEnumerable (new [] { 
					26, 185, 32.5
				}),
				M = 3,
				N = 7,
				A = DenseMatrix.OfArray (new [,] {
					{ 1.0, 0, 1, 0, 4, 3, 4 },
					{ 0.0, 1, 2, 0, 55, 3.5, 5 },
					{ 0.0, 0, 3, 1, 6, 2, -2.5 },
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 1, 0, 0, 0, 0, 0, 
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					1.0, 2, 5, 7, 8, 4, 2,
				}),
			};

			var result = task.SolveILP ();
			Console.WriteLine ("=========== FINAL SOLUTION");
			Console.WriteLine (result);
			Console.WriteLine ("VALUE");
			Console.WriteLine (task.C * result);
		}
	}
}
