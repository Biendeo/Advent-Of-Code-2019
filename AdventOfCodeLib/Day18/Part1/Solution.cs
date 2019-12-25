using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Graph = System.Collections.Generic.List<(char Source, char Destination, int Distance, int KeysRequired)>;

namespace AdventOfCodeLib.Day18.Part1 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList());
		}

		public static int Solve(List<string> map) {
			var graph = MapToGraph(map);
			return GetShortestPath(graph);
		}

		private static Graph MapToGraph(List<string> map) {
			var landmarks = new HashSet<(char Symbol, Coordinate Location)>();

			for (int y = 0; y < map.Count; ++y) {
				for (int x = 0; x < map[y].Length; ++x) {
					if (map[y][x] != '#' && map[y][x] != '.') {
						landmarks.Add((map[y][x], new Coordinate(x, y)));
					}
				}
			}

			var graph = new Graph();

			foreach (var source in landmarks.Where(l => l.Symbol == '@' || (l.Symbol >= 'a' && l.Symbol <= 'z'))) {
				foreach (var destination in landmarks.Where(l => l.Symbol >= 'a' && l.Symbol <= 'z' && source != l)) {
					var path = PathToTile(map, source.Location, destination.Location);
					int keysRequired = 0;
					foreach (var tile in path) {
						if (map[tile.Y][tile.X] >= 'A' && map[tile.Y][tile.X] <= 'Z') {
							keysRequired |= 1 << (map[tile.Y][tile.X] - 'A');
						}
					}
					graph.Add((source.Symbol, destination.Symbol, path.Count, keysRequired));
				}
			}

			return graph;
		}

		private static List<Coordinate> PathToTile(List<string> map, Coordinate start, Coordinate end) {
			var queue = new List<(List<Coordinate> Path, Coordinate Position)> {
				(new List<Coordinate>(), start)
			};
			var seenCoords = new HashSet<Coordinate> {
				start
			};

			while (queue.Count > 0) {
				InsertionSort(queue, (a, b) => ((a.Position - end).Manhatten() + a.Path.Count).CompareTo((b.Position - end).Manhatten() + b.Path.Count));
				var current = queue[0];
				queue.RemoveAt(0);

				var upCoord = new Coordinate(current.Position.X, current.Position.Y - 1);
				var downCoord = new Coordinate(current.Position.X, current.Position.Y + 1);
				var leftCoord = new Coordinate(current.Position.X - 1, current.Position.Y);
				var rightCoord = new Coordinate(current.Position.X + 1, current.Position.Y);

				if (end == upCoord || (!seenCoords.Contains(upCoord) && map[upCoord.Y][upCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						upCoord
					};
					if (end == upCoord) return path;
					queue.Add((path, upCoord));
					seenCoords.Add(upCoord);
				}

				if (end == downCoord || (!seenCoords.Contains(downCoord) && map[downCoord.Y][downCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						downCoord
					};
					if (end == downCoord) return path;
					queue.Add((path, downCoord));
					seenCoords.Add(downCoord);
				}

				if (end == leftCoord || (!seenCoords.Contains(leftCoord) && map[leftCoord.Y][leftCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						leftCoord
					};
					if (end == leftCoord) return path;
					queue.Add((path, leftCoord));
					seenCoords.Add(leftCoord);
				}

				if (end == rightCoord || (!seenCoords.Contains(rightCoord) && map[rightCoord.Y][rightCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						rightCoord
					};
					if (end == rightCoord) return path;
					queue.Add((path, rightCoord));
					seenCoords.Add(rightCoord);
				}
			}
			return new List<Coordinate>();
		}

		private static int GetShortestPath(Graph graph) {
			int keysRequired = 0;
			var seenStates = new Dictionary<int, int>();
			foreach (char key in graph.Where(e => e.Source == '@').Select(e => e.Destination)) {
				keysRequired |= 1 << (key - 'a');
			}
			var queue = new List<(char LastKey, int KeysObtained, int Distance, int Heuristic)>();
			foreach (var (Source, Destination, Distance, KeysRequired) in graph.Where(e => e.Source == '@' && e.KeysRequired == 0)) {
				int keysObtained = 1 << (Destination - 'a');
				seenStates[keysObtained | ((Destination - 'a') << 26)] = Distance;
				queue.Add((Destination, keysObtained, Distance, GetHeuristic(graph, Destination, keysObtained)));
			}
			queue.Sort((a, b) => (a.Distance + a.Heuristic).CompareTo(b.Distance + b.Heuristic));
			while (queue.Count > 0) {
				var (LastKey, KeysObtained, Distance, Heuristic) = queue.First();
				queue.RemoveAt(0);
				if (AreAllKeysAcquired(keysRequired, KeysObtained)) return Distance;
				foreach (var nextEdge in graph.Where(e => e.Source == LastKey && ((KeysObtained & (1 << (e.Destination - 'a'))) == 0) && AreRequiredKeysAcquired(e.KeysRequired, KeysObtained))) {
					int newKeysObtained = KeysObtained | (1 << nextEdge.Destination - 'a');
					int newDistance = Distance + nextEdge.Distance;
					if (!seenStates.ContainsKey(newKeysObtained | ((nextEdge.Destination - 'a') << 26)) || seenStates[newKeysObtained | ((nextEdge.Destination - 'a') << 26)] > newDistance) {
						seenStates[newKeysObtained | ((nextEdge.Destination - 'a') << 26)] = newDistance;
						queue.Add((nextEdge.Destination, newKeysObtained, newDistance, GetHeuristic(graph, nextEdge.Destination, newKeysObtained)));
					}
				}
				InsertionSort(queue, (a, b) => (a.Distance + a.Heuristic).CompareTo(b.Distance + b.Heuristic));
			}
			return 10000000;
		}

		private static int GetHeuristic(Graph graph, char lastKey, int keysObtained) {
			var keysRemaining = graph.Where(e => e.Source == '@' && ((keysObtained & (1 << (e.Destination - 'a'))) == 0)).Select(e => e.Destination);
			return keysRemaining.Sum(k => graph.Where(e => e.Destination == k && (lastKey == e.Source || (keysObtained & (1 << e.Source)) != 0)).Min(e => e.Distance));
		}

		private static void InsertionSort<T>(List<T> list, Comparison<T> comparison) {
			for (int i = 1; i < list.Count; ++i) {
				var x = list[i];
				int j = i - 1;
				for (; j >= 0 && comparison(list[j], x) > 0; --j) {
					list[j + 1] = list[j];
				}
				list[j + 1] = x;
			}
		}

		private static bool AreAllKeysAcquired(int allKeys, int keysObtained) => (allKeys & keysObtained) == allKeys;

		private static bool AreRequiredKeysAcquired(int keysRequired, int keysObtained) => (keysRequired & keysObtained) == keysRequired;
	}
}