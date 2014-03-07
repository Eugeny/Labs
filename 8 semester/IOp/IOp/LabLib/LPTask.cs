using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Threading;

namespace LabLib
{
	public class LPTask<T> where T: LPTask<T>, new()
	{
		// Параметры задачи
		public DenseVector C;
		public DenseVector B;
		public DenseVector DL, DR;
		public DenseMatrix A;
		public List<Int32> J;
		public int N, M;
		// Небазисные индексы
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

		public LPTask ()
		{
		}

		public T Clone ()
		{
			var r = new T ();
			r.A = (DenseMatrix)A.Clone ();
			r.B = (DenseVector)B.Clone ();
			r.C = (DenseVector)C.Clone ();
			if (J != null)
				r.J = new List<int> (J);
			r.N = N;
			r.M = M;
			r.DL = (DenseVector)DL.Clone ();
			r.DR = (DenseVector)DR.Clone ();
			return r;
		}
		// Значение f(X) = C*X для текущего решения
		public double GetSolutionValue (DenseVector sol)
		{
			return C * sol;
		}
		// Решение двойственным симплекс-методом
		public DenseVector Solve ()
		{
			List<int> Jplus = null; // Jн+
			List<int> Jminus = null; // Jн-
			


			// Расчет Δ
			var Ab = A.SelectColumns (J);
			var Abi = Ab.Inverse ();
			var Cb = C.Select (J);
			var delta = (DenseVector)(Cb * Abi);
			delta *= A;
			delta -= C;


			Jplus =	new List<int> ();
			Jminus = new List<int> ();
			foreach (int jj in Jn) {
				if (delta [jj] >= 0)
					Jplus.Add (jj);
				else
					Jminus.Add (jj);
			}

			var iters = 0;

			while (true) {
				iters++;

				//Thread.Sleep(10);
				J.Sort ();

				Console.WriteLine (String.Join (" ", J));


				// Расчет Δ
				Ab = A.SelectColumns (J);
				Abi = Ab.Inverse ();
				Cb = C.Select (J);
				delta = (DenseVector)(Cb * Abi);
				delta *= A;
				delta -= C;

				var nn = new DenseVector (N);
				var final = true;
				var jk = -1;


				// Расчет ℵ
				for (int i = 0; i < N; i++) {
					if (!J.Contains (i)) {
						if (Jplus.Contains (i))
							nn [i] = DL [i];
						else
							nn [i] = DR [i];
					}
				}
				
				var sum = DenseVector.Create (M, (i) => 0);
				foreach (int i in Jn) {
					sum += (DenseVector)(A.Column (i) * nn [i]);
				}
				var nValues = (DenseMatrix)Abi * (B - sum);

				for (int i = 0; i < N; i++) {
					nn [i] += 1e-5;
					if (J.Contains (i)) {
						nn [i] = nValues [J.IndexOf (i)];

						//if (nn [i].IsInteger ())
						//	nn [i] = Math.Round (nn [i]);

						// Проверка ℵ по ограничениям
						var fits = nn [i] >= DL [i] && nn [i] <= DR [i];
						final &= fits;
						if (!fits && jk == -1)
							// Запоминаем jk
							jk = i;
					}
				}
			
				// Если ℵ подходит, то завершаем
				if (final)
					return nn;
			
				// Расчет μ
				var mu = new DenseVector (N);
				mu [jk] = (nn [jk] < DL [jk] ? 1 : -1);
				var dy = mu [jk] * Abi.Row (J.IndexOf (jk));
				for (int i = 0; i < N; i++) {
					if (!J.Contains (i) && i != jk) {
						mu [i] = dy * A.Column (i);
					}
				}
			
				// Расчет σ
				var sigma = new DenseVector (N);
				for (int i = 0; i < N; i++) {
					if (Jn.Contains (i)) {
						if ((Jplus.Contains (i) && mu [i] < 0) || (Jminus.Contains (i) && mu [i] > 0)) {
							sigma [i] = -delta [i] / mu [i];
						} else {
							sigma [i] = double.PositiveInfinity;
						}
					} else {
						sigma [i] = double.PositiveInfinity;
					}
				}
			
				// Поиск σ0
				var j0 = sigma.MinimumIndex ();
				var sigma0 = sigma [j0];
			
				if (sigma0 == double.PositiveInfinity) {
					// Нет решений
					return null;
				}
			
				// Обновляем базис и Jн+/Jн-
				J.Remove (jk);
				J.Add (j0);

				if (Jplus.Contains (j0))
					Jplus.Remove (j0);
				if (mu [jk] == 1)
					Jplus.Add (jk);

				// Пересчет Jн-
				Jminus.Clear ();
				foreach (var j in Jn) {
					if (!Jplus.Contains (j))
						Jminus.Add (j);
				}
			}
		}
		// Поиск начального невырожденного плана
		public void GeneratePlan ()
		{
			if (N == 0) {
				M = A.Column (0).Count;
				N = A.Row (0).Count;
			}
			var indexes = new List<int> ();
			for (int i = 0; i < N; i++)
				indexes.Add (i);

			foreach (var basis in new Facet.Combinatorics.Combinations<int>(indexes, M, Facet.Combinatorics.GenerateOption.WithoutRepetition)) {
				J = new List<int> (basis);
				var Ab = A.SelectColumns (J);
				//if (Solve () != null)
				if (Math.Abs (Ab.Determinant ()) > 0.001)
					// |Aб| != 0
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
				//s += string.Format ("{0} <= X{1} <= {2}\n", DL [i], i, DR [i]);
			}
			return s;
		}
	}
}

