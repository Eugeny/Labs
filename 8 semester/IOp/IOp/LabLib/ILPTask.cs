using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LabLib
{
	public class ILPTask : LPTask<ILPTask>
	{
		protected int ParentIteration;

		public ILPTask ()
		{
		}

		// Метод ветвей и границ
		public DenseVector SolveILByBranching ()
		{
			Console.WriteLine (this);
			GeneratePlan ();
			var initial = this.Clone ();
			Console.WriteLine (initial.Solve ());
			
			int counter = 0;

			// Начальная задача
			var tasks = new List<ILPTask> ();
			tasks.Add (initial);
			var r0 = double.NegativeInfinity;
			var mu0 = 1;
			var mu = DenseVector.Create (N, (i) => i);

			while (true) {
				counter++;
				
				if (tasks.Count == 0) {
					// Задачи кончились
					if (mu0 == 1)
						return mu;
					if (mu0 == 0)
						return null;
					throw new Exception ("");
				}

				// Вынимаем задачу из очереди и решаем
				var task = tasks [0];
				tasks.RemoveAt (0);
				
				Console.WriteLine ("====================");
				Console.WriteLine ("TASK #{0} (parent #{1}), {2} LEFT", counter, task.ParentIteration, tasks.Count);
				Console.WriteLine (task);
				var solution = task.Solve ();
				var value = (solution != null) ? task.GetSolutionValue (solution) : 0;
				Console.WriteLine ("Solution: {0}", solution);
				Console.WriteLine ("Value: {0}", value);
				
				if (solution == null || value <= r0) {
					// Не решилась
					continue;
				}

				if (solution.IsInteger ()) {
					// Решение целое
					mu = solution;
					mu0 = 1;
					r0 = value;
					continue;
				}

				// Решение не целое
				var j0 = -1;
				for (int i = 0; i < N; i++)
					if (!solution [i].IsInteger ()) {
						// Нашли нецелую переменную
						j0 = i;
						break;
					}

				// Точка разбиения
				var l = Math.Floor (solution [j0]);
				if (l < 0 && l > solution [j0])
					l -= 1;

				// Готовим две новые задачи и ставим в очередь
				var taskL = task.Clone ();
				taskL.DR [j0] = l;
				var taskR = task.Clone ();
				taskR.DL [j0] = l + 1;
				taskL.ParentIteration = counter;
				taskR.ParentIteration = counter;
				tasks.Add (taskR);
				tasks.Add (taskL);

				//Console.ReadLine ();
			}
		}

		// Метод Гомори
		public DenseVector SolveILPByCutoff ()
		{
			var task = Clone ();
			var Ja = new List<int> ();

			task.GeneratePlanAndSolve ();

			while (true) {
				// Решаем задачу
				var solution = task.Solve ();
				Console.WriteLine (task);

				var integer = true;
				var i0 = -1;
				for (int i = 0; i < N; i++) {
					if (!solution [i].IsInteger () && task.J.Contains (i) && i0 == -1)
						// Нашли нецелую переменную
						i0 = i;
					integer &= solution [i].IsInteger ();
				}
				if (integer)
					// Все целые, завершаемся
					return DenseVector.Create (N, (i) => solution [i]);

				// Есть нецелые, продолжаем
				// Считаем y, β, aj, fj, f
				var y = (DenseVector)(DenseMatrix.Identity (task.M).Column (i0) * task.A.SelectColumns (task.J).Inverse ());     
				var aj = DenseVector.Create (task.N, (j) => y * task.A.Column (j));
				var beta = y * task.B;
				var fj = DenseVector.Create (task.N, (i) => aj [i].Frac ());
				var f = beta.Frac ();

				// Создаем и вставляем новое условие
				var extra = DenseVector.Create (task.N, (i) => (-fj [i]));
				task.A = (DenseMatrix)task.A.InsertRow (task.M, extra);
				task.A = (DenseMatrix)task.A.InsertColumn (task.N, DenseMatrix.Identity (task.M + 1).Column (task.M));
				task.B = DenseVector.Create (task.M + 1, (i) => (i < task.M) ? task.B [i] : -f);

				// Расширяем матрицы и вектора задачи
				task.C = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.C [i] : 0);
				task.DL = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.DL [i] : 0);
				task.DR = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.DR [i] : 1e10);

				// Добавляем новую переменную в базис и увеличиваем размерность задачи
				Ja.Add (task.N);
				task.J.Add (task.N);
				task.N++;
				task.M++;
			}
		}
	}
}

