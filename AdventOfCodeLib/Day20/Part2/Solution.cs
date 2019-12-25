using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day20.Part2 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList());
		}

		public static int Solve(List<string> mapString) {
			var map = new Map(mapString);
			return PathToTile(map, map.Start, map.End).Count - 1;
		}

		private static List<Coordinate> PathToTile(Map map, Coordinate start, Coordinate end) {
			var queue = new Queue<(List<Coordinate> Path, Coordinate Position, int Depth)>();
			queue.Enqueue((new List<Coordinate> { start }, start, 0));
			var seenCoords = new HashSet<(Coordinate Location, int Depth)> {
				(start, 0)
			};

			while (queue.Count > 0) {
				var current = queue.Dequeue();

				var upCoord = new Coordinate(current.Position.X, current.Position.Y - 1);
				var downCoord = new Coordinate(current.Position.X, current.Position.Y + 1);
				var leftCoord = new Coordinate(current.Position.X - 1, current.Position.Y);
				var rightCoord = new Coordinate(current.Position.X + 1, current.Position.Y);

				if (end == upCoord || (!seenCoords.Contains((upCoord, current.Depth)) && map.MapGrid[upCoord.Y][upCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						upCoord
					};
					if (end == upCoord && current.Depth == 0) return path;
					queue.Enqueue((path, upCoord, current.Depth));
					seenCoords.Add((upCoord, current.Depth));
				}

				if (end == downCoord || (!seenCoords.Contains((downCoord, current.Depth)) && map.MapGrid[downCoord.Y][downCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						downCoord
					};
					if (end == downCoord && current.Depth == 0) return path;
					queue.Enqueue((path, downCoord, current.Depth));
					seenCoords.Add((downCoord, current.Depth));
				}

				if (end == leftCoord || (!seenCoords.Contains((leftCoord, current.Depth)) && map.MapGrid[leftCoord.Y][leftCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						leftCoord
					};
					if (end == leftCoord && current.Depth == 0) return path;
					queue.Enqueue((path, leftCoord, current.Depth));
					seenCoords.Add((leftCoord, current.Depth));
				}

				if (end == rightCoord || (!seenCoords.Contains((rightCoord, current.Depth)) && map.MapGrid[rightCoord.Y][rightCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						rightCoord
					};
					if (end == rightCoord && current.Depth == 0) return path;
					queue.Enqueue((path, rightCoord, current.Depth));
					seenCoords.Add((rightCoord, current.Depth));
				}

				var warp1 = map.Warps.Find(w => w.Point1 == current.Position);
				var warp2 = map.Warps.Find(w => w.Point2 == current.Position);
				var warpPosition = warp1?.Point2 ?? warp2?.Point1 ?? null;
				bool warpOut = false;
				if (current.Position.X == 2 || current.Position.X == map.MapGrid[current.Position.Y].Length - 3 || current.Position.Y == 2 || current.Position.Y == map.MapGrid.Count - 3) {
					warpOut = true;
				}
				if (!(warpPosition is null) && (end == warpPosition! || (!seenCoords.Contains((warpPosition!, warpOut ? current.Depth - 1 : current.Depth + 1)))) && !(warpOut && current.Depth == 0)) {
					var path = new List<Coordinate>(current.Path) {
						warpPosition!
					};
					if (end == warpPosition!) return path;
					queue.Enqueue((path, warpPosition!, warpOut ? current.Depth - 1 : current.Depth + 1));
					seenCoords.Add((warpPosition!, warpOut ? current.Depth - 1 : current.Depth + 1));
				}
			}
			return new List<Coordinate>();
		}
	}
}
