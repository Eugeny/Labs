using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LabLib
{
	public class LPTask
	{
		public DenseVector C;
		public DenseVector B;
		public DenseVector DL, DR;
		public DenseMatrix A;
		public List<Int32> J;
		public int N, M;

		public List<int> Jn {
			get {
				var l = new List<int> ();
				for (int i = 0; i < N; i++) {
					if (!J.Contains (i))
						l.Add (i);
				}
				return l;
			}
		}

		public DenseVector X__ {
			get {
				var xj = (DenseVector)(A.SelectColumns (J).Inverse () * B);
				return DenseVector.Create (N, (i) => J.Contains (i) ? xj [J.IndexOf (i)] : 0);
			}
		}

		public double Value {
			get {
				return C * X__;
			}
		}

		public LPTask ()
		{
		}

		public LPTask Clone ()
		{
			var r = new LPTask ();
			r.A = (DenseMatrix)A.Clone ();
			r.B = (DenseVector)B.Clone ();
			r.C = (DenseVector)C.Clone ();
			r.J = new List<int> (J);
			r.N = N;
			r.M = M;
			r.DL = (DenseVector)DL.Clone ();
			r.DR = (DenseVector)DR.Clone ();
			return r;
		}

		public double GetSolutionValue (DenseVector sol)
		{
			return C * sol;
		}

		public DenseVector Solve ()
		{
			List<int> Jplus = null;
			List<int> Jminus = null;

			while (true) {
				J.Sort ();
				var Ab = A.SelectColumns (J);
				var Abi = Ab.Inverse ();
				var Cb = C.Select (J);
				var E = DenseMatrix.Identity (M);
			
				Console.WriteLine (String.Join (" ", J));

				var nn = new DenseVector (N);
				var final = true;
				var jk = -1;
			
				var delta = (DenseVector)(Cb * Abi);
				delta *= A;
				delta -= C;

				if (Jplus == null) {
					Jplus =	new List<int> ();
					Jminus = new List<int> ();
					foreach (int j in Jn) {
						if (delta [j] >= 0)
							Jplus.Add (j);
						else
							Jminus.Add (j);
					}
				}
				
				for (int i = 0; i < N; i++) {
					if (!J.Contains (i)) {
						if (Jplus.Contains (i))
							nn [i] = DL [i];
						else
							nn [i] = DR [i];
					}
				}
				
				var sumann = DenseVector.Create (M, (i) => 0);
				foreach (int i in Jn) {
					sumann += (DenseVector)(A.Column (i) * nn [i]);
				}
				
				var tmp = (DenseMatrix)Abi * (B - sumann);
				for (int i = 0; i < N; i++) {
					if (J.Contains (i)) {
						nn [i] = tmp [J.IndexOf (i)];
						var fits = nn [i] >= DL [i] && nn [i] <= DR [i];
						final &= fits;
						if (!fits && jk == -1)
							jk = i;
					}
				}
			
				if (final)
					return nn;
			
				var mu = new DenseVector (N);
				mu [jk] = (nn [jk] < DL [jk] ? 1 : -1);
				var dy = mu [jk] * E.Column (J.IndexOf (jk)) * Abi;
				for (int i = 0; i < N; i++) {
					if (!J.Contains (i) && i != jk) {
						mu [i] = dy * A.Column (i);
					}
				}
			
				var theta = new DenseVector (N);
				for (int i = 0; i < N; i++) {
					if (Jn.Contains (i)) {
						if ((Jplus.Contains (i) && mu [i] < 0) || (Jminus.Contains (i) && mu [i] > 0)) {
							theta [i] = -delta [i] / mu [i];
						} else {
							theta [i] = double.PositiveInfinity;
						}
					} else {
						theta [i] = double.PositiveInfinity;
					}
				}
			
				var j0 = theta.MinimumIndex ();
				var theta0 = theta [j0];
			
				if (theta0 == double.PositiveInfinity) {
					return null;
				}
			
				J.Remove (jk);
				J.Add (j0);

				if (Jplus.Contains (j0))
					Jplus.Remove (j0);
				if (mu [jk] == 1)
					Jplus.Add (jk);

				Jminus.Clear ();
				foreach (var j in Jn) {
					if (!Jplus.Contains (j))
						Jminus.Add (j);
				}
			}
		}

		public void GeneratePlan ()
		{
			var indexes = new List<int> ();
			for (int i = 0; i < M; i++)
				indexes.Add (i);

			foreach (var basis in new Facet.Combinatorics.Combinations<int>(indexes, M, Facet.Combinatorics.GenerateOption.WithoutRepetition)) {
				J = new List<int> (basis);
				var Ab = A.SelectColumns (J);
				if (Ab.Determinant () != 0)
					return;
			}
		}

		public DenseVector GeneratePlanAndSolve ()
		{
			GeneratePlan ();
			return Solve ();
		}

		public override string ToString ()
		{
			var s = "--------------\nTask:\n";
			s += "C:\n" + C.ToVectorString (99, 99, null) + "\n";
			s += "A:\n" + A.ToMatrixString (99, 99, null) + "\n";
			s += "B:\n" + B.ToVectorString (99, 99, null) + "\n";
			for (int i = 0; i < N; i++) {
				s += string.Format ("{0} <= X{1} <= {2}\n", DL [i], i, DR [i]);
			}
			return s;
		}
	}
}

