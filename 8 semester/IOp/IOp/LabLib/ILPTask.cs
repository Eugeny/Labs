using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Threading;

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

			// Начальное состояние
			var r0 = double.NegativeInfinity;			// начальное R0 = -∞
			var mu0 = 1;								// μ0 = 1
			var mu = DenseVector.Create (N, (i) => i);  // μ - вектор 1..N

			while (true) {
				counter++;
				
				if (tasks.Count == 0) {
					// Задачи кончились
					if (mu0 == 1) // μ0 = 1, решение найдено
						return mu;
					if (mu0 == 0) // μ0 = 0, решения нет
						return null;
				}

				// Вынимаем задачу из очереди
				var task = tasks [0];
				tasks.RemoveAt (0);
				
				var solution = task.Solve (); // Решаем задачу
				var value = (solution != null) ? task.GetSolutionValue (solution) : 0; // Получаем c'x

				Console.WriteLine ("====================");
				Console.WriteLine ("TASK #{0} (parent #{1}), {2} LEFT", counter, task.ParentIteration, tasks.Count);
				Console.WriteLine (task);
				Console.WriteLine ("Solution: {0}", solution);
				Console.WriteLine ("Value: {0}", value);
				
				if (solution == null || value <= r0) {
					// Задача не решилась либо значение меньше R0
					continue; // Новая итерация
				}

				if (solution.IsInteger ()) {
					// Решение целое
					mu = solution; // Запоминаем его в μ
					mu0 = 1; // Устанавливаем μ0
					r0 = value; // и R0
					continue; // Новая итерация
				}

				// Решение не целое
				var j0 = -1;
				for (int i = 0; i < N; i++)
					if (!solution [i].IsInteger ()) {
						// Нашли нецелую переменную
						j0 = i; // Запомним индекс
						break;
					}

				// Выберем новое ограничение по переменной j0
				var l = Math.Floor (solution [j0]);
				if (l < 0 && l > solution [j0])
					l -= 1;

				// Новая задача 1
				var taskL = task.Clone ();
				taskL.DR [j0] = l; // заменяем D*[j0]
				taskL.ParentIteration = counter;
				tasks.Add (taskL); // ставим задачу в очередь

				// Новая задача 2
				var taskR = task.Clone ();
				taskR.DL [j0] = l + 1; // заменяем D_*[j0]
				taskR.ParentIteration = counter;
				tasks.Add (taskR); // ставим задачу в очередь
			}
		}
		// Метод Гомори
		public DenseVector SolveILPByCutoff ()
		{
			if (N == 0) {
				M = A.Column (0).Count;
				N = A.Row (0).Count;
			}
			if (DL == null) {
				DL = DenseVector.Create (N, (i) => 0);
				DR = DenseVector.Create (N, (i) => 1e100);
			}
			var task = Clone ();
			task.GeneratePlanAndSolve ();

			while (true) {
				// Решаем задачу
				var solution = task.Solve ();
				Console.WriteLine (task);
				for (int i = 0; i < N; i++)
					if (task.J.Contains (i))
						solution [i] += 0.0000001f;
				Console.WriteLine ("Solution: " + solution);
				Console.WriteLine ("Indexes: " + string.Join (" ", task.J));

				// Уменьшаем размерность
				for (int index = task.N - 1; index >= N; index--)
				// Перебираем базисные индексы в поисках искуственной переменной
					if (task.J.Contains (index)) {
						var row = 0;

						// Находим соответствующий столбец в A
						for (int i = 0; i < task.M; i++) {
							if (task.A.Row (i) [index] == 1)
								row = i;
						}

						Console.WriteLine ("Removing condition {0}", index);
						var dataRow = task.A.Row (row);

						// Вычитаем строку из остальных строк А и B
						for (int i = 0; i < task.M; i++) {
							if (i != row) { // Кроме нее самой
								var k = task.A.Row (i) [index] / task.A.Row (row) [index]; // Коэффициент приведения для строки
								task.A.SetRow (i, task.A.Row (i) - dataRow * k); // Вычитаем из А
								task.B [i] -= task.B [row] * k; // и В
							}
						}

						// Удаляем столбец и строку...
						var rowList = new List<int> ();
						var colList = new List<int> ();
						for (int i = 0; i < task.M; i++)
							rowList.Add (i);
						for (int i = 0; i < task.N; i++)
							colList.Add (i);
						rowList.Remove (row);
						colList.Remove (index);

						task.A = task.A.SelectRows (rowList).SelectColumns (colList); // из А...
						task.B = task.B.Select (rowList); // В...
						task.C = task.C.Select (colList); // и С
						task.M--; // уменьшаем размерность задачи
						task.N--;
						task.J.Remove (index); // убираем удаленную переменную из базиса
					}
				//--------------

				// Выбираем индекс i0 с наибольшей дробной частью в переменной
				var i0 = -1;
				double maxFrac = 0.0;
				for (int i = 0; i < N; i++) {
					// смотрим только нецелые базисные переменные
					if (!solution [i].IsInteger () && task.J.Contains (i) && i0 == -1) {
						// Нашли нецелую переменную
						if (solution [i].Frac () > maxFrac) {
							i0 = i;
							maxFrac = solution [i0].Frac ();
						}
					}
				}

				if (i0 == -1)
					// Все целые, завершаемся
					return DenseVector.Create (N, (i) => solution [i]); // возвращаем текущее решение

				// Есть нецелые, продолжаем
				// Считаем y, β, fj, f
				var Abi = task.A.SelectColumns (task.J).Inverse (); // Aб'
				var y = Abi.Row (task.J.IndexOf (i0)); // y = Aб'[i0]

				float beta = (float)(y * task.B); // β = y'B
				var ff = beta;
				var mf = Decimal.Floor((decimal)beta);
				ff -= (float)mf;
				//if (beta < 0)
				//	ff += 1;

				var fj = DenseVector.Create (task.N, (i) => (Abi * task.A).Row (task.J.IndexOf (i0)) [i].Frac ());
				// Fj = [ Aб'[i0] ]

				// Создаем и вставляем новое условие
				// Добавляем строку [-Fj] снизу матрицы
				var extra = DenseVector.Create (task.N, (i) => (-fj [i])); 
				task.A = (DenseMatrix)task.A.InsertRow (task.M, extra);
				// Добавляем столбец E[M] справа
				task.A = (DenseMatrix)task.A.InsertColumn (task.N, DenseMatrix.Identity (task.M + 1).Column (task.M));
				// Добавляем -f в B
				task.B = DenseVector.Create (task.M + 1, (i) => (i < task.M) ? task.B [i] : -ff);

				// Добавляем 0 в C
				task.C = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.C [i] : 0);
				task.DL = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.DL [i] : 0);
				task.DR = DenseVector.Create (task.N + 1, (i) => (i < task.N) ? task.DR [i] : 1e100);

				// Добавляем новую переменную в базис и увеличиваем размерность задачи
				task.J.Add (task.N);
				task.N++;
				task.M++;
			}
		}
	}
}

