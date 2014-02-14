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
			var task = new ILPTask (){
				C = DenseVector.OfEnumerable (new [] {1.0,1,-2,3}),
				B = DenseVector.OfEnumerable (new [] {1.0,9}),
				M = 2,
				N = 4,
				A = DenseMatrix.OfArray (new [,] {
					{1.0,-1,3,-2},
					{1,-5,11,-6},
				}),
				DL =DenseVector.OfEnumerable (new [] {0,0,0,0.0}),
				DR = DenseVector.OfEnumerable (new [] {999,999,999,999.0}),
			};
			
			task = new ILPTask (){
				C = DenseVector.OfEnumerable (new [] {3,3,13.0,0,0}),
				B = DenseVector.OfEnumerable (new [] {-8,-8.0}),
				M = 2,
				N = 5,
				A = DenseMatrix.OfArray (new [,] {
					{3,-6,-7,-1,0.0},
					{-6,3,-7,0,-1.0},
				}),
				DL =DenseVector.OfEnumerable (new [] {0,0,0,0,0.0}),
				DR = DenseVector.OfEnumerable (new [] {5,5,5,double.PositiveInfinity,double.PositiveInfinity}),
			};
			var result = task.SolveILP ();
			Console.WriteLine (result);
			Console.WriteLine (task.C * result);
		}
		
	}
}
