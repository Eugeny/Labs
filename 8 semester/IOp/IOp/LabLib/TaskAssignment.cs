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
		// решение задачи о назначениях
		public Dictionary<int, int> Solve ()
		{
			// в каждой строке
			for (int i = 0; i < N; i++) {
				var min = int.MaxValue;
				// находим минимальное значение
				for (int j = 0; j < N; j++)
					min = Math.Min (min, C [i, j]);
				// и вычитаем из элементов строки
				for (int j = 0; j < N; j++)
					C [i, j] -= min;
			}
			DumpC();

			// в каждом столбце
			for (int i = 0; i < N; i++) {
				var min = int.MaxValue;
				// находим минимальное значение
				for (int j = 0; j < N; j++)
					min = Math.Min (min, C [j, i]);
				// и вычитаем из элементов столбца
				for (int j = 0; j < N; j++)
					C [j, i] -= min;
			}
			DumpC();

			int ic = 0;
			while (true) {
				ic += 1;

				Console.WriteLine("I " + ic);
				DumpC();
			
				Vertex.ResetIndexes ();
				// создаем граф с 2N+2 вершинами
				var g = new Graph (N * 2 + 2);
				var S = g.GetVertex (N * 2 + 1);
				var T = g.GetVertex (N * 2 + 2);
				// добавляем дуги от вершин к стоку и истоку
				for (int i = 0; i < N; i++) {
					g.AddEdge (new Edge{ From = S, To = g.GetVertex (i + 1), Capacity = 1 });
					g.AddEdge (new Edge{ From = g.GetVertex (N + i + 1), To = T, Capacity = 1 });
				}
				// добавляем дуги между задачами и исполнителями
				for (int i = 0; i < N; i++)
					for (int j = 0; j < N; j++)
					// для которых Cij = 0
						if (C [i, j] == 0) {
							g.AddEdge (new Edge{ From = g.GetVertex (i + 1), To = g.GetVertex (N + j + 1), Capacity = 1 });
						}

				// находим максимальный поток и список последних помеченных вершин
				var L = g.GenerateMaxFlow (S, T);

				//foreach (var x in L)
					//Console.Write(x + " ");

				var s = 0;
				// находим суммарный поток в стоке
				foreach (Edge e in g.GetIncidentalEdges(T))
					s += e.Flow;

				Console.WriteLine("Flow " + s);
				if (s == N) {
					// поток равен N, задача решена
					var result = new Dictionary<int, int> ();
					// записываем решение
					for (int i = 0; i < N; i++)
						for (int j = 0; j < N; j++) {
							var e = g.GetConnectingEdge (g.GetVertex (i + 1), g.GetVertex (N + j + 1));
							if (e != null && e.Flow > 0)
								result [i] = j;
						}
					// и возвращаем его
					return result;
				}

				var N1 = new List<int> ();
				var N2 = new List<int> ();
				// наполняем списки N1 и N2 помеченными вершинами
				for (int i = 0; i < N; i++) { 
					if (L.Contains (g.GetVertex (i + 1)))
						N1.Add (i);
					if (L.Contains (g.GetVertex (N + i + 1)))
						N2.Add (i);
				}

				var alpha = int.MaxValue;
				// находим alpha из дуг, начинающихся в N1 и заканчивающихся не в N2
				for (int i = 0; i < N; i++)
					for (int j = 0; j < N; j++)
						if (N1.Contains (i) && !N2.Contains (j))
							alpha = Math.Min (alpha, C [i, j]);

				// вычитаем alpha из строк в N1
				foreach (var i in N1)
					for (int j = 0; j < N; j++)
						C [i, j] -= alpha;

				// прибавляем alpha к столбцам в N2
				foreach (var i in N2)
					for (int j = 0; j < N; j++)
						C [j, i] += alpha;
			}
		}

		public void DumpC() {

			for (int i = 0; i < N; i++) {
				for (int j = 0; j < N; j++)
					Console.Write ("{0} ", C [i, j]);
				Console.WriteLine ();
			}
			Console.WriteLine ();
			Console.WriteLine ();
		}

	}
}

