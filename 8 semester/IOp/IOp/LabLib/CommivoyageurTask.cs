using System;
using System.Collections.Generic;

namespace LabLib
{
	public class CommivoyageurTask
	{
		public int N;
		public int[,] C;
		public int[,] X;

		public CommivoyageurTask ()
		{
		}

		public CommivoyageurTask Copy ()
		{
			var c = new CommivoyageurTask (){ N = N, C = new int[N, N], X = new int[N, N] };
			for (int i = 0; i < N; i++)
				for (int j = 0; j < N; j++)
					c.C [i, j] = C [i, j];
			return c;
		}

		public List<int> SolveByBranching ()
		{
			for (int i = 0; i < N; i++)
				C [i, i] = 99999;

			int R0 = int.MaxValue;
			List<int> bestSolution = null;
			var tasks = new Queue<CommivoyageurTask> ();
			tasks.Enqueue (this.Copy ());

			while (true) {
				if (tasks.Count == 0)
					break;

				var task = tasks.Dequeue ();

				var assignments = new TaskAssignment () { C = task.C, N = N };
				var solution = assignments.Solve ();

				var totalCost = 0;
				for (int i = 0; i < N; i++)
					totalCost += C [i, solution [i]];

				if (totalCost >= R0)
					continue;

				for (int i = 0; i < N; i++)
					for (int j = 0; j < N; j++)
						task.X [i, j] = (solution [i] == j) ? 1 : 0;

				if (task.FindLoop (0).Count == N) {
					R0 = totalCost;
					bestSolution = task.FindLoop (0);
					continue;
				}

				var minLoop = task.FindLoop (0);
				for (int i = 0; i < N; i++) {
					var loop = task.FindLoop (i);
					if (loop.Count < minLoop.Count)
						minLoop = loop;
				}

				for (int i = 0; i < minLoop.Count; i++) {
					var newTask = task.Copy ();
					newTask.C [minLoop [i], minLoop [(i + 1) % minLoop.Count]] = 99999;
					tasks.Enqueue (newTask);
				}
			}
			return bestSolution;
		}

		public List<int> FindLoop (int start)
		{
			var res = new List<int> ();
			var i = start;
			while (true) {
				if (res.Contains (i))
					break;
				res.Add (i);

				for (int j = 0; j < N; j++)
					if (X [i, j] == 1) {
						i = j;
						break;
					}
			}
			return res;
		}

		public int GetLoopCost (List<int> loop)
		{
			var totalCost = 0;
			for (int i = 0; i < N; i++)
				totalCost += C [loop [i], loop [(i + 1) % loop.Count]];
			return totalCost;
		}

		public string ToLoopString (List<int> loop)
		{
			var s = "";
			foreach (var i in loop)
				s += " -> " + i;
			s += " = " + GetLoopCost(loop);
			return s;
		}
	}
}

