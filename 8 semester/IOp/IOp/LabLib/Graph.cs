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

		public Edge GetConnectingEdge (Vertex a, Vertex b)
		{
			foreach (Edge e in GetIncidentalEdges(a)) {
				if (e.From == a && e.To == b)
					return e;
				if (e.To == a && e.From == b)
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

		public Vertex ()
		{
			ID = lastId++;
		}

		public override string ToString ()
		{
			return string.Format ("[Vertex {0} (p={1})]", ID, Potential);
		}
	}

	public class Edge
	{
		public Vertex From, To;
		public int Flow, Cost, Delta;

		public override string ToString ()
		{
			return string.Format ("[Edge {0} -> {1}  x {2}  Δ={3}]", From.ID, To.ID, Flow, Delta);
		}
	}
}

