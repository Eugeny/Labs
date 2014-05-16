using System;
using LabLib;

namespace Lab4
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int task = -11;
			Graph graph = null;

			if (task == -1) {
				graph = new Graph (6);
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (2), Flow = 1, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (1), Flow = 0, Cost = -2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (6), Flow = 0, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (5), Flow = 0, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (3), Flow = 9, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (2), Flow = 3, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (3), Flow = 0, Cost = -4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (4), Flow = 5, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (4), Flow = 1, Cost = 5 });
			}

			if (task == 1) {
				graph = new Graph (9);
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (2), Flow = 2, Cost = 9 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (8), Flow = 7, Cost = 5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (7), Flow = 3, Cost = 5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (6), Flow = 0, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (3), Flow = 4, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (9), Flow = 0, Cost = -2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (3), Flow = 0, Cost = -3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (4), Flow = 3, Cost = 6 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (5), Flow = 4, Cost = 8 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (3), Flow = 0, Cost = -1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (4), Flow = 0, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (9), Flow = 0, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (5), Flow = 5, Cost = 7 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (8), To = graph.GetVertex (7), Flow = 0, Cost = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (8), To = graph.GetVertex (9), Flow = 0, Cost = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (9), To = graph.GetVertex (6), Flow = 2, Cost = 6 });
			}

			if (task == 2) {
				graph = new Graph (8);
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (2), Flow = 5, Cost = 8 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (8), Flow = 0, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (3), Flow = 0, Cost = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (7), Flow = 0, Cost = 9 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (6), Flow = 3, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (3), Flow = 0, Cost = -2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (6), Flow = 0, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (5), To = graph.GetVertex (4), Flow = 6, Cost = 8 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (5), Flow = 0, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (3), Flow = 4, Cost = 11 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (5), Flow = 7, Cost = 6 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (6), Flow = 0, Cost = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (8), To = graph.GetVertex (7), Flow = 8, Cost = 5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (8), To = graph.GetVertex (6), Flow = 3, Cost = 5 });
				//graph.AddEdge (new Edge{ From = graph.GetVertex (), To = graph.GetVertex (), Flow = , Cost =  });
			}

			if (task == -11) {
				graph = new Graph (7);
				graph.AddEdge (new Edge{ From = graph.GetVertex (1), To = graph.GetVertex (6), Flow = 0, Cost = -5 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (1), Flow = 2, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (5), Flow = 0, Cost = -2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (2), To = graph.GetVertex (3), Flow = 3, Cost = 3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (3), To = graph.GetVertex (4), Flow = 0, Cost = 1 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (4), To = graph.GetVertex (5), Flow = 0, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (4), Flow = 6, Cost = 6 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (6), To = graph.GetVertex (2), Flow = 4, Cost = 10 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (1), Flow = 0, Cost = -3 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (5), Flow = 1, Cost = 4 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (6), Flow = 5, Cost = 2 });
				graph.AddEdge (new Edge{ From = graph.GetVertex (7), To = graph.GetVertex (3), Flow = 0, Cost = -2 });
				//graph.AddEdge (new Edge{ From = graph.GetVertex (), To = graph.GetVertex (), Flow = , Cost =  });
			}
			graph.GenerateMinimalCostFlow ();
			Console.WriteLine (graph);
		}
	}
}
