using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day08.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			return Solve(new List<int>(File.ReadAllText(inputFile).Trim().Select(d => int.Parse(new char[] { d }))), 25, 6);
		}

		public static int Solve(List<int> imagePixels, int width, int height) {
			int layers = imagePixels.Count / (width * height);
			int layerSize = width * height;

			int[,,] image = new int[layers, width, height];
			foreach (int layer in Enumerable.Range(0, layers)) {
				foreach (int x in Enumerable.Range(0, width)) {
					foreach (int y in Enumerable.Range(0, height)) {
						image[layer, x, y] = imagePixels[layer * layerSize + x * height + y];
					}
				}
			}

			int layerWithFewestZeros = -1;
			int fewestZeroes = int.MaxValue;
			foreach (int layer in Enumerable.Range(0, layers)) {
				int zeroes = 0;
				foreach (int x in Enumerable.Range(0, width)) {
					foreach (int y in Enumerable.Range(0, height)) {
						if (image[layer, x, y] == 0) ++zeroes;
					}
				}
				if (zeroes < fewestZeroes) {
					layerWithFewestZeros = layer;
					fewestZeroes = zeroes;
				}
			}
			int ones = 0;
			int twos = 0;
			foreach (int x in Enumerable.Range(0, width)) {
				foreach (int y in Enumerable.Range(0, height)) {
					if (image[layerWithFewestZeros, x, y] == 1) ++ones;
					else if (image[layerWithFewestZeros, x, y] == 2) ++twos;
				}
			}
			return ones * twos;
		}
	}
}
