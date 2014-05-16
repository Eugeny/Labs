using System;
using LabLib;

namespace Lab5
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int task = -1;
			int S = 0, T = 0;
			Graph graph = null;

			if (task == 0) {
				graph = new Graph (7);
				S = 6;
				T = 7;
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (1), Flow = 4, Capacity = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (3), Flow = 5, Capacity = 9 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (3), Flow = 2, Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (4), Flow = 2, Capacity = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (2), Flow = 1, Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (5), Flow = 6, Capacity = 6 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (4), Flow = 1, Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (5), Flow = 0, Capacity = 10 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (T), Flow = 2, Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (5), Flow = 1, Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (T), Flow = 7, Capacity = 9 });
			}

			if (task == 1) {
				graph = new Graph (8);
				S = 7;
				T = 8;
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (1), Capacity = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (2), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (3), Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (5), Capacity = 6 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (3), Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (4), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (3), Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (4), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (5), Capacity = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (5), Capacity = 5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (6), Capacity = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (T), Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (6), Capacity = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (T), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (T), Capacity = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (5), Capacity = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (T), Capacity = 5 });
			}


			if (task == -1) {
				graph = new Graph (9);
				S = 8;
				T = 9;
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (2), Capacity = 14 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (4), Capacity = 12 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (S), To = graph.GetVertex (5), Capacity = 10 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (4), Capacity = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (3), Capacity = 8 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (T), Capacity = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (6), Capacity = 5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (5), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (T), Capacity = 100 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (6), Capacity = 15 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (5), Capacity = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (6), Capacity = 10 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (T), Capacity = 20 });
			}

			graph.GenerateMaxFlow (graph.GetVertex (S), graph.GetVertex (T));
			Console.WriteLine (graph);
			var s = 0;
			foreach (Edge e in graph.GetIncidentalEdges(graph.GetVertex(T)))
				s += e.Flow;
			Console.WriteLine("Flow: {0}", s);
		}
	}
}
