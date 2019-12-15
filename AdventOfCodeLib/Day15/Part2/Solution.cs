using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day15.Part2 {
	public static class Solution {
		private const int MapWidth = 50;
		private const int MapHeight = 50;

		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var outputBuffer = new List<long>();
			int firstMove = 2;

			var map = new List<List<int>>();
			for (int y = 0; y < MapHeight; ++y) {
				map.Add(new List<int>());
				for (int x = 0; x < MapWidth; ++x) {
					map[y].Add(0);
				}
			}

			var droidStart = new Coordinate(MapWidth / 2, MapHeight / 2);
			map[droidStart.Y][droidStart.X] = 2;
			var droid = droidStart.Clone();
			int lastMove = firstMove;
			Coordinate? oxygen = null;

			var computer = new IntcodeComputer(program); // Just to make the compiler happy
			computer = new IntcodeComputer(program, () => {
				if (outputBuffer.Count == 1) {
					long result = outputBuffer.Single();
					switch (result) {
						case 0:
							switch (lastMove) {
								case 1:
									map[droid.Y - 1][droid.X] = 1;
									break;
								case 2:
									map[droid.Y + 1][droid.X] = 1;
									break;
								case 3:
									map[droid.Y][droid.X - 1] = 1;
									break;
								case 4:
									map[droid.Y][droid.X + 1] = 1;
									break;
							}
							break;
						case 1:
							switch (lastMove) {
								case 1:
									--droid.Y;
									break;
								case 2:
									++droid.Y;
									break;
								case 3:
									--droid.X;
									break;
								case 4:
									++droid.X;
									break;
							}
							map[droid.Y][droid.X] = 2;
							break;
						case 2:
							switch (lastMove) {
								case 1:
									--droid.Y;
									break;
								case 2:
									++droid.Y;
									break;
								case 3:
									--droid.X;
									break;
								case 4:
									++droid.X;
									break;
							}
							oxygen = droid.Clone();
							map[droid.Y][droid.X] = 3;
							break;
					}
					outputBuffer.Clear();
				}

				var unknownTiles = UnknownTiles(map);
				var closestTile = ClosestTile(unknownTiles, droid);
				var path = PathToTile(map, droid, closestTile);
				while (path.Count == 0) {
					unknownTiles.Remove(closestTile);
					if (unknownTiles.Count == 0) {
						computer.Abort();
						return 1;
					}
					closestTile = ClosestTile(unknownTiles, droid);
					path = PathToTile(map, droid, closestTile);
				}

				lastMove = path.First();
				return lastMove;
			}, (x) => outputBuffer.Add(x));

			computer.RunProgram();

			return TimeToFill(map, oxygen!);
		}

		private static List<Coordinate> UnknownTiles(List<List<int>> map) {
			var r = new List<Coordinate>();
			for (int y = 0; y < MapHeight; ++y) {
				for (int x = 0; x < MapWidth; ++x) {
					if (map[y][x] == 0) {
						r.Add(new Coordinate(x, y));
					}
				}
			}
			return r;
		}

		private static Coordinate ClosestTile(List<Coordinate> tiles, Coordinate location) {
			return tiles.OrderBy(c => (c - location).Manhatten()).First();
		}

		private static List<int> PathToTile(List<List<int>> map, Coordinate start, Coordinate end) {
			var queue = new List<(List<int> Path, Coordinate Position)> {
				(new List<int>(), start)
			};
			var seenCoords = new HashSet<Coordinate> {
				start
			};

			while (queue.Count > 0) {
				queue.Sort((a, b) => ((a.Position - end).Manhatten() + a.Path.Count).CompareTo((b.Position - end).Manhatten() + b.Path.Count));
				var current = queue[0];
				queue.RemoveAt(0);

				var upCoord = new Coordinate(current.Position.X, current.Position.Y - 1);
				var downCoord = new Coordinate(current.Position.X, current.Position.Y + 1);
				var leftCoord = new Coordinate(current.Position.X - 1, current.Position.Y);
				var rightCoord = new Coordinate(current.Position.X + 1, current.Position.Y);

				if (end == upCoord || (!seenCoords.Contains(upCoord) && map[upCoord.Y][upCoord.X] != 1 && map[upCoord.Y][upCoord.X] != 0)) {
					var path = new List<int>(current.Path) {
						1
					};
					if (end == upCoord) return path;
					queue.Add((path, upCoord));
					seenCoords.Add(upCoord);
				}

				if (end == downCoord || (!seenCoords.Contains(downCoord) && map[downCoord.Y][downCoord.X] != 1 && map[downCoord.Y][downCoord.X] != 0)) {
					var path = new List<int>(current.Path) {
						2
					};
					if (end == downCoord) return path;
					queue.Add((path, downCoord));
					seenCoords.Add(downCoord);
				}

				if (end == leftCoord || (!seenCoords.Contains(leftCoord) && map[leftCoord.Y][leftCoord.X] != 1 && map[leftCoord.Y][leftCoord.X] != 0)) {
					var path = new List<int>(current.Path) {
						3
					};
					if (end == leftCoord) return path;
					queue.Add((path, leftCoord));
					seenCoords.Add(leftCoord);
				}

				if (end == rightCoord || (!seenCoords.Contains(rightCoord) && map[rightCoord.Y][rightCoord.X] != 1 && map[rightCoord.Y][rightCoord.X] != 0)) {
					var path = new List<int>(current.Path) {
						4
					};
					if (end == rightCoord) return path;
					queue.Add((path, rightCoord));
					seenCoords.Add(rightCoord);
				}
			}
			return new List<int>();
		}
		private static int TimeToFill(List<List<int>> map, Coordinate start) {
			var queue = new Queue<(List<int> Path, Coordinate Position)>();
			queue.Enqueue((new List<int>(), start));
			var seenCoords = new HashSet<Coordinate> {
				start
			};

			int minutes = 0;

			while (queue.Count > 0) {
				var current = queue.Dequeue();
				minutes = Math.Max(minutes, current.Path.Count);

				var upCoord = new Coordinate(current.Position.X, current.Position.Y - 1);
				var downCoord = new Coordinate(current.Position.X, current.Position.Y + 1);
				var leftCoord = new Coordinate(current.Position.X - 1, current.Position.Y);
				var rightCoord = new Coordinate(current.Position.X + 1, current.Position.Y);

				if (!seenCoords.Contains(upCoord) && map[upCoord.Y][upCoord.X] != 1 && map[upCoord.Y][upCoord.X] != 0) {
					var path = new List<int>(current.Path) {
						1
					};
					queue.Enqueue((path, upCoord));
					seenCoords.Add(upCoord);
				}

				if (!seenCoords.Contains(downCoord) && map[downCoord.Y][downCoord.X] != 1 && map[downCoord.Y][downCoord.X] != 0) {
					var path = new List<int>(current.Path) {
						2
					};
					queue.Enqueue((path, downCoord));
					seenCoords.Add(downCoord);
				}

				if (!seenCoords.Contains(leftCoord) && map[leftCoord.Y][leftCoord.X] != 1 && map[leftCoord.Y][leftCoord.X] != 0) {
					var path = new List<int>(current.Path) {
						3
					};
					queue.Enqueue((path, leftCoord));
					seenCoords.Add(leftCoord);
				}

				if (!seenCoords.Contains(rightCoord) && map[rightCoord.Y][rightCoord.X] != 1 && map[rightCoord.Y][rightCoord.X] != 0) {
					var path = new List<int>(current.Path) {
						4
					};
					queue.Enqueue((path, rightCoord));
					seenCoords.Add(rightCoord);
				}
			}
			return minutes;
		}
	}
}
