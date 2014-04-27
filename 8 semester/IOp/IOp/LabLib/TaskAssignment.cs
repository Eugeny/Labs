using System;
using System.Collections.Generic;

namespace LabLib
{
	public class TaskAssignment
	{
		public int N;
		public int[,] C;

		public TaskAssignment ()
		{
		}

		public Dictionary<int, int> Solve ()
		{
			for (int i = 0; i < N; i++) {
				var min = int.MaxValue;
				for (int j = 0; j < N; j++)
					min = Math.Min (min, C [i, j]);
				for (int j = 0; j < N; j++)
					C [i, j] -= min;
			}
			for (int i = 0; i < N; i++) {
				var min = int.MaxValue;
				for (int j = 0; j < N; j++)
					min = Math.Min (min, C [j, i]);
				for (int j = 0; j < N; j++)
					C [j, i] -= min;
			}

			while (true) {
				Vertex.ResetIndexes ();
				var g = new Graph (N * 2 + 2);
				var S = g.GetVertex (N * 2 + 1);
				var T = g.GetVertex (N * 2 + 2);
				for (int i = 0; i < N; i++) {
					g.AddEdge (new Edge{ From = S, To = g.GetVertex (i + 1), Capacity = 1 });
					g.AddEdge (new Edge{ From = g.GetVertex (N + i + 1), To = T, Capacity = 1 });
				}
				for (int i = 0; i < N; i++)
					for (int j = 0; j < N; j++)
						if (C [i, j] == 0) {
							g.AddEdge (new Edge{ From = g.GetVertex (i + 1), To = g.GetVertex (N + j + 1), Capacity = 1 });
						}

				var L = g.GenerateMaxFlow (S, T);

				var s = 0;
				foreach (Edge e in g.GetIncidentalEdges(T))
					s += e.Flow;
				if (s == N) {
					var result = new Dictionary<int, int> ();
					for (int i = 0; i < N; i++)
						for (int j = 0; j < N; j++) {
							var e = g.GetConnectingEdge (g.GetVertex (i + 1), g.GetVertex (N + j + 1));
							if (e != null && e.Flow > 0)
								result [i] = j;
						}
					return result;
				}


				var N1 = new List<int> ();
				var N2 = new List<int> ();
				for (int i = 0; i < N; i++) { 
					if (L.Contains (g.GetVertex (i + 1)))
						N1.Add (i);
					if (L.Contains (g.GetVertex (N + i + 1)))
						N2.Add (i);
				}

				var alpha = int.MaxValue;
				for (int i = 0; i < N; i++) { 
					for (int j = 0; j < N; j++) {
						if (N1.Contains (i) && !N2.Contains (j)) {
							alpha = Math.Min (alpha, C [i, j]);
						}
					}
				}

				foreach (var i in N1)
					for (int j = 0; j < N; j++)
						C [i, j] -= alpha;

				foreach (var i in N2)
					for (int j = 0; j < N; j++)
						C [j, i] += alpha;
			}
		}
	}
}

