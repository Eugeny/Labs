using System;

namespace LabLib
{
	public class ResourceTask
	{
		public int C, N;
		public Func<int,int>[] F;

		public Solution SolveBellman ()
		{
			var sol = new Solution {
				C = C, N = N,
			};
			sol.B = new int[N, C + 1];
			sol.Z = new int[N, C + 1];
			for (int n = 0; n < N; n++)
				for (int c = 0; c <= C; c++)
					BellmanInner (sol, n, c);
			return sol;
		}

		private int BellmanInner (Solution sol, int n, int c)
		{
			if (sol.B [n, c] != 0)
				return sol.B [n, c];

			var result = 0;
			var resultZ = 0;
			if (n == 0) {
				result = F [n] (c);
				resultZ = c;
			} else {
				var bestZ = 0;
				var bestB = 0;
				for (int z = 0; z <= c; z++) {
					var b = F [n] (z) + BellmanInner (sol, n - 1, c - z);
					if (b > bestB) {
						bestB = b;
						bestZ = z;
					}
				}
				result = bestB;
				resultZ = bestZ;
			}

			sol.B [n, c] = result;
			sol.Z [n, c] = resultZ;
			return result;
		}

		public class Solution
		{
			public int C, N;
			public int[,] B, Z;

			public override string ToString ()
			{
				var s = string.Format ("{0,-4}", "X");
				for (int j = 0; j <= C; j++) {
					s += string.Format ("{0,-7}", j);
				}
				s += "\n";

				for (int i = 0; i < N; i++) {
					s += string.Format ("B{0,-3}", i);
					for (int j = 0; j <= C; j++) {
						s += string.Format ("{0,2}({1,-2}) ", B [i, j], Z [i, j]);
					}
					s += "\n";
				}

				s += "\nBest: ";
				var best = "";

				var c = C;
				for (int i = N - 1; i >= 0; i--) {
					best = Z [i, c] + " " + best;
					c -= Z [i, c];
				}
				s += best + "\n";
				return s;
			}
		}
	}
}

