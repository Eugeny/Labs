using System;
using System.Collections.Generic;

namespace LabLib
{
	public class CommivoyageurTask
	{
		public int N;
		public int[,] C;
		public int[,] X;

		public CommivoyageurTask Copy ()
		{
			var c = new CommivoyageurTask (){ N = N, C = new int[N, N], X = new int[N, N] };
			for (int i = 0; i < N; i++)
				for (int j = 0; j < N; j++)
					c.C [i, j] = C [i, j];
			return c;
		}

		// решение задачи коммивояжера методом ветвей и границ
		public List<int> SolveByBranching ()
		{
			for (int i = 0; i < N; i++)
				C [i, i] = 99999;

			// начальная оценка
			int R0 = int.MaxValue;
			// лучшее решение
			List<int> bestSolution = null;
			// очередь задач
			var tasks = new Queue<CommivoyageurTask> ();
			// ставим первую задачу в очередь
			tasks.Enqueue (this.Copy ());

			while (true) {
				Console.WriteLine("task...");

				// если задач больше нет, заканчиваем
				if (tasks.Count == 0)
					break;

				// берем задачу из очереди
				var task = tasks.Dequeue ();

				// создаем задачу о назначениях с такой же матрицей стоимости
				var assignments = new TaskAssignment () { C = task.C, N = N };
				// решаем ее
				var solution = assignments.Solve ();

				var totalCost = 0;
				// находим общую стоимость маршрута
				for (int i = 0; i < N; i++)
					totalCost += C [i, solution [i]];

				if (totalCost >= R0)
					// если стоимость не меньше чем текущая оценка, переходим к следующей задаче
					continue;

				// устанавливаем матрицу решения X исходя из решения задачи о назначениях
				for (int i = 0; i < N; i++)
					for (int j = 0; j < N; j++)
						task.X [i, j] = (solution [i] == j) ? 1 : 0;

				// если длина цикла в маршруте = N, то это решение, запоминаем его и продолжаем
				if (task.FindLoop (0).Count == N) {
					R0 = totalCost;
					bestSolution = task.FindLoop (0);
					continue;
				}

				var minLoop = task.FindLoop (0);
				// ищем цикл минимальной длины в решении
				for (int i = 0; i < N; i++) {
					var loop = task.FindLoop (i);
					if (loop.Count < minLoop.Count)
						minLoop = loop;
				}

				// для каждого ребра в минимальном цикле
				for (int i = 0; i < minLoop.Count; i++) {
					// копируем задачу
					var newTask = task.Copy ();
					// и устанавливаем высокую стоимость прохода по этому ребру
					newTask.C [minLoop [i], minLoop [(i + 1) % minLoop.Count]] = 99999;
					// ставим задачу в очередь
					tasks.Enqueue (newTask);
				}
			}
			return bestSolution;
		}

		public List<int> FindLoop (int start)
		{
			var res = new List<int> ();
			var i = start;
			while (true) {
				if (res.Contains (i))
					break;
				res.Add (i);

				for (int j = 0; j < N; j++)
					if (X [i, j] == 1) {
						i = j;
						break;
					}
			}
			return res;
		}

		public int GetLoopCost (List<int> loop)
		{
			var totalCost = 0;
			for (int i = 0; i < N; i++)
				totalCost += C [loop [i], loop [(i + 1) % loop.Count]];
			return totalCost;
		}

		public string ToLoopString (List<int> loop)
		{
			var s = "";
			var c = 0;
			foreach (var i in loop)
				s += " -> " + (i+1);
			s += " = " + GetLoopCost(loop);
			return s;
		}
	}
}

