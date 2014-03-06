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
			var tasks = new List<ILPTask> (new TasksXmlReader ("tasks.xml").ReadTasks ().Values);
			/*
			ILPTask[] tasks = new ILPTask[] {
				// 1
				new ILPTask () {
					C = DenseVector.OfEnumerable (new [] { 
						2.0, 1, -2, -1, 4, -5, 5, 5 
					}),
					B = DenseVector.OfEnumerable (new [] { 
						40.0, 107, 61
					}),
					M = 3,
					N = 8,
					A = DenseMatrix.OfArray (new [,] {
						{ 1.0, 0, 0, 12, 1, -3, 4, -1 },
						{ 0, 1, 0, 11, 12, 3, 5, 3 },
						{ 0, 0, 1, 1, 0, 22, -2, 1 },
					}),
					DL = DenseVector.OfEnumerable (new [] { 
						0.0, 0, 0, 0, 0, 0, 0, 0,
					}),
					DR = DenseVector.OfEnumerable (new [] { 
						3.0, 5, 5, 3, 4, 5, 6, 3, 
					}),
				},

				// 2
				new ILPTask () {
					C = DenseVector.OfEnumerable (new [] { 
						-1.0, 5, -2, 4, 3, 1, 2, 8, 3
					}),
					B = DenseVector.OfEnumerable (new [] { 
						3.0, 9, 9, 5, 9
					}),
					M = 5,
					N = 9,
					A = DenseMatrix.OfArray (new [,] {
						{ 1.0, -3, 2, 0, 1, -1, 4, -1, 0 },
						{ 1, -1, 6, 1, 0, -2, 2, 2, 0,  },
						{ 2, 2, -1, 1, 0, -3, 8, -1, 1 },
						{ 4, 1, 0, 0, 1, -1, 0, -1, 1 },
						{ 1, 1, 1, 1, 1, 1, 1, 1, 1 },
					}),
					DL = DenseVector.OfEnumerable (new [] { 
						0.0, 0, 0, 0, 0, 0, 0, 0, 0,
					}),
					DR = DenseVector.OfEnumerable (new [] { 
						5.0, 5, 5, 5, 5, 5, 5, 5, 5, 
					}),
				},
				// 3
				new ILPTask () { },
				// 4
				new ILPTask () { },
				// 5
				new ILPTask () { },
				// 6
				new ILPTask () {
					C = DenseVector.OfEnumerable (new [] { 
						2.0, 1, -2, -1, 4, -5, 5, 5,
					}),
					B = DenseVector.OfEnumerable (new [] { 
						30.0, 78, 18
					}),
					M = 3,
					N = 8,
					A = DenseMatrix.OfArray (new [,] {
						{ 1.0, 0, 0, 3, 1, -3, 4, -1 },
						{ 0, 1, 0, 4, -3, 3, 5, 3  },
						{ 0, 0, 1, 1, 0, 2, -2, 1 },
					}),
					DL = DenseVector.OfEnumerable (new [] { 
						0.0, 0, 0, 0, 0, 0, 0, 0
					}),
					DR = DenseVector.OfEnumerable (new [] { 
						5.0, 5, 3, 4, 5, 6, 6, 8
					}),
				},

				// 7
				new ILPTask () { },
				// 8
				new ILPTask () {
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
				},
				// 9
				new ILPTask () {
					C = DenseVector.OfEnumerable (new [] { 
						1, 2, 3, 1, 2, 3, 4.0
					}),
					B = DenseVector.OfEnumerable (new [] { 
						58, 66.3, 36.7, 13.5
					}),
					M = 4,
					N = 7,
					A = DenseMatrix.OfArray (new [,] {
						{ 2, 0, 1, 0, 0, 3, 5 },
						{ 0, 2, 2.1, 0, 0, 3.5, 5 },
						{ 0, 0, 3, 2, 0, 2, 1.1 },
						{ 0, 0, 3, 0, 2, 2, -2.5 },
					}),
					DL = DenseVector.OfEnumerable (new [] { 
						1.0, 1, 1, 1, 1, 1, 1 
					}),
					DR = DenseVector.OfEnumerable (new [] { 
						2.0, 3, 4, 5, 8, 7, 7 
					}),
				},

				// 10
				new ILPTask () {
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
				},

			};*/

			var task = tasks [0];
			var result = task.SolveILByBranching ();
			Console.WriteLine ("=========== FINAL SOLUTION");
			Console.WriteLine (result);
			Console.WriteLine ("VALUE");
			Console.WriteLine (task.C * result);
		}
	}
}
