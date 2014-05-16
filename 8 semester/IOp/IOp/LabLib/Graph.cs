using System;
using System.Collections;
using System.Collections.Generic;

namespace LabLib
{
	public class Graph
	{
		private Dictionary<int, Vertex> vertices = new Dictionary<int, Vertex> ();
		private List<Edge> edges = new List<Edge> ();

		public Graph (int n)
		{
			for (int i = 0; i < n; i++)
				AddVertex (new Vertex ());
		}

		public void AddVertex (Vertex v)
		{
			vertices [v.ID] = v;
		}

		public void AddEdge (Edge e)
		{
			edges.Add (e);
		}

		public Vertex GetVertex (int id)
		{
			return vertices [id];
		}

		// возвращает ребра, инцидентные данной вершине
		public IEnumerable GetIncidentalEdges (Vertex v)
		{
			foreach (var e in edges)
				if (e.From == v || e.To == v)
					yield return e;
		}

		// возвращает связанные вершины, связанные с данной ребрами
		public IEnumerable GetConnectedVertices (Vertex v)
		{
			foreach (Edge e in GetIncidentalEdges(v)) {
				if (e.From == v)
					yield return e.To;
				if (e.To == v)
					yield return e.From;
			}
		}

		public Edge GetConnectingEdge (Vertex a, Vertex b, bool directional = false)
		{
			foreach (Edge e in GetIncidentalEdges(a)) {
				if (e.From == a && e.To == b)
					return e;
				if (e.To == a && e.From == b && !directional)
					return e;
			}
			return null;
		}

		// рекурсивно рассчитывает потенциалы, начиная с вершины start.
		private void CalculatePotentials (Vertex start, int p, Vertex initialStart)
		{
			if (start.Potential.HasValue && start.Potential.Value >= p)
				return;

			start.Potential = p;
			// для всех инцидентных данной вершине базисных ребер
			foreach (Edge edge in GetIncidentalEdges(start)) {
				if (edge.Flow > 0) {
					// если ребро прямое
					if (edge.To != start) {
						if (edge.To != initialStart)
							CalculatePotentials (edge.To, p - edge.Cost, initialStart);
					} else {
						// если ребро обратное
						if (edge.From != initialStart)
							CalculatePotentials (edge.From, p + edge.Cost, initialStart);
					}
				}
			}
		}

		// находит и возвращает цикл, включающий ребро start и начинающийся с вершины vstart
		private List<Edge> FindLoop (Edge start, Vertex vstart, List<Edge> oldHistory = null)
		{
			List<Edge> history = new List<Edge> ();
			if (oldHistory != null) {
				history.AddRange (oldHistory);
			} else {
				oldHistory = new List<Edge> ();
			}

			// пришли в начальную вершину (и в цикле больше нуля ребер)
			if (history.Count > 0 && history [0].From == vstart) {
				history.Add (start);
				return history;
			}
			if (history.Contains (start))
				return null;

			// запоминаем следующее ребро
			history.Add (start);

			foreach (Vertex v in GetConnectedVertices(vstart)) {
				// перебираем инцидентные ребра, которые пока вне цикла
				var edge = GetConnectingEdge (vstart, v);
				if (!oldHistory.Contains (edge)) {
					if (edge.Flow > 0) {
						// рекурсивно ищем цикл далее, используя выбранное ребро и все предыдущие
						var res = FindLoop (edge, v, history);
						if (res != null)
							return res;
					}
				}
			}
			return null;
		}

		// поиск потока минимальной стоимости
		public void GenerateMinimalCostFlow ()
		{
			while (true) {
				// рассчитываем потенциалы вершин
				foreach (var v in vertices.Values)
					v.Potential = null;

				CalculatePotentials (vertices [7], 0, vertices [7]);

				// выбираем ребро с положительной delta
				Edge replacementEdge = null;
				foreach (var edge in edges) 
					if (edge.From.Potential.HasValue && edge.To.Potential.HasValue)
				{

					edge.Delta = edge.From.Potential.Value - edge.To.Potential.Value - edge.Cost;
					if (edge.Delta > 0 && replacementEdge == null)
						replacementEdge = edge;
				}

				// ребро не найдено, конец алгоритма
				if (replacementEdge == null)
					break;

				// находим цикл, содержащий это ребро
				var loop = FindLoop (replacementEdge, replacementEdge.To);

				var positiveEdges = new List<Edge> ();
				var negativeEdges = new List<Edge> ();
				var lastVertex = replacementEdge.From;
				var minTheta = int.MaxValue;
				// проходим по циклу
				foreach (Edge edge in loop) {
					// определяем ребро с минимальным потоком (мин theta)
					if (edge.Flow < minTheta && edge.Flow > 0)
						minTheta = edge.Flow;
					// определяем, прямое это ребро
					if (edge.From == lastVertex) {
						lastVertex = edge.To;
						positiveEdges.Add (edge);
					} else {
						// или обратное
						lastVertex = edge.From;
						negativeEdges.Add (edge);
					}
				}

				// увеличиваем поток в положительных ребрах
				foreach (var edge in positiveEdges)
					edge.Flow += minTheta;

				// и уменьшаем в отрицательных
				foreach (var edge in negativeEdges)
					edge.Flow -= minTheta;
			}
		}

		// восстановление пути из вершины t по Форду-Фалкерсону
		private void LocateBackpath (Vertex t, List<Edge> p, List<Edge> n, ref int a)
		{
			// вершина помечена как имеющая входящую дугу
			if (t.G > 0) {
				var i = GetVertex (t.G);
				var e = GetConnectingEdge (i, t, directional: true);
				// обновляем alpha с новым запасом изменения потока в пути
				a = Math.Min (a, e.Capacity - e.Flow);
				// запоминаем дугу как прямую
				p.Add (e);
				// продолжаем поиск пути
				LocateBackpath (i, p, n, ref a);
			}
			// помечена как с исходящей дугой
			if (t.G < 0) {
				var i = GetVertex (-t.G);
				var e = GetConnectingEdge (t, i, directional: true);
				// обновляем alpha с новым запасом изменения потока в пути
				a = Math.Min (a, e.Flow);
				// запоминаем дугу как обратную
				n.Add (e);
				// продолжаем поиск пути
				LocateBackpath (i, p, n, ref a);
			}
		}

		// поиск максимального потока из s в t
		public List<Vertex> GenerateMaxFlow (Vertex s, Vertex t)
		{
			// итерация
			while (true) {
				// счетчики итераций поиска пути и меток
				int Ic = 1, It = 1;
				// "текущая" вершина
				var I = s;
				// список помеченных вершин
				var L = new List<Vertex> ();
				// добавляем туда начальную вершину
				L.Add (s);
				s.G = 0;
				s.P = 1;

				while (true) {
					// ищем непомеченные вершины...
					foreach (var j in vertices.Values)
						if (!L.Contains (j)) {
							var e = GetConnectingEdge (I, j, directional: true);
							// ...связанные с текущей прямыми дугами
							if (e != null && e.Flow < e.Capacity) {
								// помечаем
								j.G = I.ID;
								It++;
								j.P = It;
								// обновляем список
								L.Add (j);
							}
						}

					// ищем непомеченные вершины...
					foreach (var j in vertices.Values)
						if (!L.Contains (j)) {
							var e = GetConnectingEdge (j, I, directional: true);
							// ...связанные с текущей обратными дугами
							if (e != null && e.Flow > 0) {
								// помечаем
								j.G = -I.ID;
								It++;
								j.P = It;
								// обновляем список
								L.Add (j);
							}
						}

					if (L.Contains (t)) {
						// конечная вершина помечена, путь найден
						break;
					} else {
						// увеличиваем счетчик итераций
						Ic++;
						foreach (var j in vertices.Values)
							// выбираем новую текущую вершину
							if (L.Contains (j) && j.P == Ic) {
								I = j;
								continue;
							}
						// не нашли новую текущую вершину - увеличивающих путей нет
						if (I.P != Ic)
							return L;
					}
				}

				var p = new List<Edge> ();
				var n = new List<Edge> ();
				int a = int.MaxValue;
				// восстанавливаем увеличивающий путь
				// находим списки прямых и обратных дуг, а также alpha
				LocateBackpath (t, p, n, ref a);
				// увеличиваем поток в прямых
				foreach (var e in p)
					e.Flow += a;
				// и уменьшаем в обратных дугах
				foreach (var e in n)
					e.Flow -= a;
			}
		}

		public override string ToString ()
		{
			var es = "";
			var vs = "";
			var c = 0;
			foreach (var v in vertices.Values)
				vs += v.ToString () + "\n";
			foreach (var e in edges) {
				es += e.ToString () + "\n";
				c += e.Flow * e.Cost;
			}
			return string.Format ("[Graph]\n{0}\n{1}\nCost: {2}", vs, es, c);
		}
	}

	public class Vertex
	{
		private static int lastId = 1;
		public int ID;
		public int? Potential;
		internal int G, P;

		public static void ResetIndexes ()
		{
			lastId = 1;
		}

		public Vertex ()
		{
			ID = lastId++;
		}

		public override string ToString ()
		{
			return string.Format ("[Vertex {0} (pot={1}) P={2} G={3}]", ID, Potential, P, G);
		}
	}

	public class Edge
	{
		public Vertex From, To;
		public int Flow, Cost, Delta, Capacity;

		public override string ToString ()
		{
			return string.Format ("[Edge {0}->{1} F={2} C={3} Δ={4}]", From.ID, To.ID, Flow, Capacity, Delta);
		}
	}
}

