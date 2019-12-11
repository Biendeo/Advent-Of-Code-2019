using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCodeLib.Day11.Part2 {
	public static class Solution {
		public static string SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static string Solve(List<long> program) {
			var outputBuffer = new Queue<long>();
			var inputBuffer = new Queue<long>();
			inputBuffer.Enqueue(1);
			bool programFinished = false;
			var computer = new IntcodeComputer(program, () => {
				lock (inputBuffer) {
					while (inputBuffer.Count == 0) {
						Monitor.Wait(inputBuffer);
					}
					return inputBuffer.Dequeue();
				}
			}, (x) => {
				lock (outputBuffer) {
					outputBuffer.Enqueue(x);
					if (outputBuffer.Count == 2) {
						Monitor.PulseAll(outputBuffer);
					}
				}
			});

			var computerThread = new Thread(() => {
				computer.RunProgram();
				lock (outputBuffer) {
					programFinished = true;
					Monitor.PulseAll(outputBuffer);
				}
			});
			computerThread.Start();

			var paintedTiles = new HashSet<Coordinate>();
			var whiteTiles = new HashSet<Coordinate>();

			int currentDirection = 0;
			var directions = new List<Coordinate> { new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(0, -1), new Coordinate(-1, 0) };
			var currentPosition = new Coordinate(0, 0);

			while (!programFinished) {
				int paintValue = -1;

				lock (outputBuffer) {
					while (outputBuffer.Count < 2 && !programFinished) {
						Monitor.Wait(outputBuffer);
					}
					if (programFinished) {
						break;
					}
					paintValue = (int)outputBuffer.Dequeue();
					currentDirection += (int)outputBuffer.Dequeue() * 2 - 1;
				}

				if (currentDirection < 0) {
					currentDirection += directions.Count;
				} else if (currentDirection >= directions.Count) {
					currentDirection -= directions.Count;
				}
				paintedTiles.Add(currentPosition);
				if (paintValue == 0) {
					whiteTiles.Remove(currentPosition);
				} else if (paintValue == 1) {
					whiteTiles.Add(currentPosition.Clone());
				}

				currentPosition += directions[currentDirection];
				lock (inputBuffer) {
					inputBuffer.Enqueue(whiteTiles.Contains(currentPosition) ? 1 : 0);
					Monitor.PulseAll(inputBuffer);
				}
			}

			computerThread.Join();

			int minX = paintedTiles.Min(c => c.X);
			int maxX = paintedTiles.Max(c => c.X);
			int minY = paintedTiles.Min(c => c.Y);
			int maxY = paintedTiles.Max(c => c.Y);

			var sb = new StringBuilder();

			for (int y = maxY; y >= minY; --y) {
				for (int x = minX; x <= maxX; ++x) {
					if (whiteTiles.Contains(new Coordinate(x, y))) {
						sb.Append('#');
					} else {
						sb.Append(' ');
					}
				}
				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}
