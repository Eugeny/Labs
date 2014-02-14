using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LabLib
{
	public class ILPTask : LPTask
	{
		public ILPTask ()
		{
		}
		
		public DenseVector SolveILP ()
		{
			Console.WriteLine (this);
			Console.WriteLine (GeneratePlanAndSolve ());
			
			int counter = 0;
			
			var tasks = new List<LPTask> ();
			var r0 = double.NegativeInfinity;
			var mu0 = 1;
			var mu = this.X;
			tasks.Add (this.Clone ());
			
			while (true) {
				counter++;
				
				if (tasks.Count == 0) {
					if (mu0 == 1)
						return mu;
					if (mu0 == 0)
						return null;
					throw new  Exception ("");
				}
				var task = tasks [0];
				tasks.RemoveAt (0);
				
				Console.WriteLine ("====================");
				Console.WriteLine ("TASK #{0}, #{1} LEFT", counter, tasks.Count);
				Console.WriteLine (task);
				var solution = task.Solve ();
				var value = (solution != null) ? task.GetSolutionValue (solution) : 0;
				Console.WriteLine ("Solution: {0}", solution);
				Console.WriteLine ("Value: {0}", value);
				
				if (solution == null || value <= r0) {
					r0 = value;
					continue;
				}
				if (solution.IsInteger ()) {
					mu = solution;
					mu0 = 1;
					r0 = value;
					continue;
				}
				
				var j0 = -1;
				for (int i = 0; i < N; i++) 
					if (!solution [i].IsInteger ()) {
						j0 = i;
						break;
					}
				
				var l = Math.Floor (solution [j0]);
				if (l < 0 && l > solution [j0])
					l -= 1;
				
				var taskL = task.Clone ();
				taskL.DR [j0] = l;
				var taskR = task.Clone ();
				taskR.DL [j0] = l + 1;
				tasks.Add (taskR);
				tasks.Add (taskL);
			}
		}
	}
}

