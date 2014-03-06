using System;
using LabLib;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;

namespace Lab2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var tasks = new List<ILPTask> (new TasksXmlReader ("tasks2.xml").ReadTasks ().Values);
			ILPTask task = null;


			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					-3.5, 1, 0, 0, 0,
				}),
				B = DenseVector.OfEnumerable (new [] { 
					15.0, 6, 0
				}),
				M = 3,
				N = 5,
				A = DenseMatrix.OfArray (new [,] {
					{ 5.0, -1, 1, 0, 0 },
					{ -1.0, 2, 0, 1, 0 },
					{ -7.0, 2, 0, 0, 1 },
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 0, 0, 0, 0,
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					1e100, 1e100, 1e100, 1e100, 1e100, 
				}),
			};


			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					2.0,-5,0,0,0
				}),
				B = DenseVector.OfEnumerable (new [] { 
					-1.0,10,3,
				}),
				M = 3,
				N = 5,
				A = DenseMatrix.OfArray (new [,] {
					{-2.0,-1,1,0,0},
					{ 3.0,1,0,1,0},
					{ -1.0,1,0,0,1 },
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 0, 0, 0, 0,
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					1e100, 1e100, 1e100, 1e100, 1e100, 
				}),
			};


			task = new ILPTask () {
				C = DenseVector.OfEnumerable (new [] { 
					21.0,11,0
				}),
				B = DenseVector.OfEnumerable (new [] { 
					13.0,
				}),
				M = 1,
				N = 3,
				A = DenseMatrix.OfArray (new [,] {
					{7.0,4,1},
				}),
				DL = DenseVector.OfEnumerable (new [] { 
					0.0, 0, 0,
				}),
				DR = DenseVector.OfEnumerable (new [] { 
					1e100, 1e100, 1e100, 
				}),
			};




			task = tasks[8];
			var result = task.SolveILPByCutoff ();
			Console.WriteLine ("=========== FINAL SOLUTION");
			Console.WriteLine (result);
			Console.WriteLine ("VALUE");
			Console.WriteLine (task.C * result);

		}
	}
}
