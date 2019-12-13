using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day13.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var outputBuffer = new List<long>();
			var inputBuffer = new Queue<long>();

			program[0] = 2;
			int lastBallX = 19;
			int lastBallY = 15;

			var map = new List<List<int>>();
			for (int y = 0; y < 23; ++y) {
				map.Add(new List<int>());
				for (int x = 0; x < 45; ++x) {
					map[y].Add(0);
				}
			}

			int score = 0;

			var computer = new IntcodeComputer(program, () => {
				for (int i = 0; i < outputBuffer.Count; i += 3) {
					int x = (int)outputBuffer[i];
					int y = (int)outputBuffer[i + 1];
					int id = (int)outputBuffer[i + 2];

					if (x == -1 && y == 0) {
						score = id;
					} else {
						map[y][x] = id;
					}
				}
				outputBuffer.Clear();

				int ballX = map.Where(c => c.IndexOf(4) != -1).Single().IndexOf(4);
				int ballY = map.IndexOf(map.Where(c => c.IndexOf(4) != -1).Single());
				int paddleX = map.Where(c => c.IndexOf(3) != -1).Single().IndexOf(3);
				int paddleY = map.IndexOf(map.Where(c => c.IndexOf(3) != -1).Single());

				int nextBallX = 2 * ballX - lastBallX;
				int nextBallY = 2 * ballY - lastBallY;

				int action = 0;

				if (ballX == paddleX && ballY == paddleY - 1) {
					action = 0;
				} else if (nextBallX > paddleX) {
					action = 1;
				} else if (nextBallX < paddleX) {
					action = -1;
				}

				lastBallX = ballX;
				lastBallY = ballY;

				return action;
			}, (x) => outputBuffer.Add(x));

			computer.RunProgram();

			for (int i = 0; i < outputBuffer.Count; i += 3) {
				int x = (int)outputBuffer[i];
				int y = (int)outputBuffer[i + 1];
				int id = (int)outputBuffer[i + 2];

				if (x == -1 && y == 0) {
					score = id;
				} else {
					map[y][x] = id;
				}
			}

			return score;
		}
	}
}
