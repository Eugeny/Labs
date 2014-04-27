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

		public IEnumerable GetIncidentalEdges (Vertex v)
		{
			foreach (var e in edges)
				if (e.From == v || e.To == v)
					yield return e;
		}

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

		private void CalculatePotentials (Vertex start, int p, Vertex initialStart)
		{
			if (start.Potential.HasValue && start.Potential.Value >= p)
				return;
			start.Potential = p;
			foreach (Edge edge in GetIncidentalEdges(start)) {
				if (edge.Flow > 0) {
					if (edge.To != start) {
						if (edge.To != initialStart)
							CalculatePotentials (edge.To, p - edge.Cost, initialStart);
					} else {
						if (edge.From != initialStart)
							CalculatePotentials (edge.From, p + edge.Cost, initialStart);
					}
				}
			}
		}

		private List<Edge> FindLoop (Edge start, Vertex vstart, List<Edge> oldHistory = null)
		{
			List<Edge> history = new List<Edge> ();
			if (oldHistory != null) {
				history.AddRange (oldHistory);
			} else {
				oldHistory = new List<Edge> ();
			}

			if (history.Count > 0 && history [0].From == vstart) {
				history.Add (start);
				return history;
			}
			if (history.Contains (start))
				return null;

			history.Add (start);

			foreach (Vertex v in GetConnectedVertices(vstart)) {
				var edge = GetConnectingEdge (vstart, v);
				if (!oldHistory.Contains (edge)) {
					if (edge.Flow > 0) {
						var res = FindLoop (edge, v, history);
						if (res != null)
							return res;
					}
				}
			}
			return null;
		}

		public void GenerateMinimalCostFlow ()
		{
			while (true) {
				foreach (var v in vertices.Values)
					v.Potential = null;

				CalculatePotentials (vertices [1], 0, vertices [1]);
				/*while (true) {
					var done = true;
					foreach (var v in vertices.Values) {
						if (!v.Potential.HasValue) {
							bool hasIncoming = false, hasOutgoing = false;
							foreach (Edge e in GetIncidentalEdges(v)) {
								if (e.Flow > 0 && e.From == v)
									hasOutgoing = true;
								if (e.Flow > 0 && e.To == v)
									hasIncoming = true;
							}
							if (!hasIncoming && hasOutgoing) {
								CalculatePotentials (v, 0, v);
								done = false;
							}
						}
					}
					if (done)
						break;
				}*/

				Edge replacementEdge = null;
				foreach (var edge in edges) {
					edge.Delta = edge.From.Potential.Value - edge.To.Potential.Value - edge.Cost;
					if (edge.Delta > 0 && replacementEdge == null)
						replacementEdge = edge;
				}

				if (replacementEdge == null)
					break;

				var loop = FindLoop (replacementEdge, replacementEdge.To);
				Console.WriteLine (this);

				List<Edge> positiveEdges = new List<Edge> ();
				List<Edge> negativeEdges = new List<Edge> ();
				var lastVertex = replacementEdge.From;
				var minTheta = int.MaxValue;
				foreach (Edge edge in loop) {
					if (edge.From == lastVertex) {
						lastVertex = edge.To;
						positiveEdges.Add (edge);
					} else {
						lastVertex = edge.From;
						if (edge.Flow < minTheta)
							minTheta = edge.Flow;
						negativeEdges.Add (edge);
					}
				}

				foreach (var edge in positiveEdges)
					edge.Flow += minTheta;
				foreach (var edge in negativeEdges)
					edge.Flow -= minTheta;
			}
		}

		private void LocateBackpath (Vertex t, List<Edge> p, List<Edge> n, ref int a)
		{
			Console.WriteLine (t.ID);
			if (t.G > 0) {
				var i = GetVertex (t.G);
				var e = GetConnectingEdge (i, t, directional: true);
				a = Math.Min (a, e.Capacity - e.Flow);
				p.Add (e);
				LocateBackpath (i, p, n, ref a);
			}
			if (t.G < 0) {
				var i = GetVertex (-t.G);
				var e = GetConnectingEdge (t, i, directional: true);
				a = Math.Min (a, e.Flow);
				n.Add (e);
				LocateBackpath (i, p, n, ref a);
			}
		}

		public void GenerateMaxFlow (Vertex s, Vertex t)
		{
			while (true) {
				int Ic = 1, It = 1;
				var I = s;
				var L = new List<Vertex> ();
				L.Add (s);
				s.G = 0;
				s.P = 1;

				while (true) {
					foreach (var j in vertices.Values)
						if (!L.Contains (j)) {
							var e = GetConnectingEdge (I, j, directional: true);
							if (e != null && e.Flow < e.Capacity) {
								j.G = I.ID;
								It++;
								j.P = It;
								L.Add (j);
							}
						}

					foreach (var j in vertices.Values)
						if (!L.Contains (j)) {
							var e = GetConnectingEdge (j, I, directional: true);
							if (e != null && e.Flow > 0) {
								j.G = -I.ID;
								It++;
								j.P = It;
								L.Add (j);
							}
						}

					if (L.Contains (t)) {
						break;
					} else {
						Ic++;
						foreach (var j in vertices.Values)
							if (L.Contains (j) && j.P == Ic) {
								I = j;
								continue;
							}
						if (I.P != Ic)
							return;
					}
				}

				var p = new List<Edge> ();
				var n = new List<Edge> ();
				int a = int.MaxValue;
				LocateBackpath (t, p, n, ref a);
				foreach (var e in p)
					e.Flow += a;
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

