﻿using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day20.Part1 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList());
		}

		public static int Solve(List<string> mapString) {
			var map = new Map(mapString);
			return PathToTile(map, map.Start, map.End).Count - 1;
		}

		private static List<Coordinate> PathToTile(Map map, Coordinate start, Coordinate end) {
			var queue = new Queue<(List<Coordinate> Path, Coordinate Position)>();
			queue.Enqueue((new List<Coordinate> { start }, start));
			var seenCoords = new HashSet<Coordinate> {
				start
			};

			while (queue.Count > 0) {
				var current = queue.Dequeue();

				var upCoord = new Coordinate(current.Position.X, current.Position.Y - 1);
				var downCoord = new Coordinate(current.Position.X, current.Position.Y + 1);
				var leftCoord = new Coordinate(current.Position.X - 1, current.Position.Y);
				var rightCoord = new Coordinate(current.Position.X + 1, current.Position.Y);

				if (end == upCoord || (!seenCoords.Contains(upCoord) && map.MapGrid[upCoord.Y][upCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						upCoord
					};
					if (end == upCoord) return path;
					queue.Enqueue((path, upCoord));
					seenCoords.Add(upCoord);
				}

				if (end == downCoord || (!seenCoords.Contains(downCoord) && map.MapGrid[downCoord.Y][downCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						downCoord
					};
					if (end == downCoord) return path;
					queue.Enqueue((path, downCoord));
					seenCoords.Add(downCoord);
				}

				if (end == leftCoord || (!seenCoords.Contains(leftCoord) && map.MapGrid[leftCoord.Y][leftCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						leftCoord
					};
					if (end == leftCoord) return path;
					queue.Enqueue((path, leftCoord));
					seenCoords.Add(leftCoord);
				}

				if (end == rightCoord || (!seenCoords.Contains(rightCoord) && map.MapGrid[rightCoord.Y][rightCoord.X] != '#')) {
					var path = new List<Coordinate>(current.Path) {
						rightCoord
					};
					if (end == rightCoord) return path;
					queue.Enqueue((path, rightCoord));
					seenCoords.Add(rightCoord);
				}

				var warp1 = map.Warps.Find(w => w.Point1 == current.Position);
				var warp2 = map.Warps.Find(w => w.Point2 == current.Position);
				var warpPosition = warp1?.Point2 ?? warp2?.Point1 ?? null;
				if (!(warpPosition is null) && (end == warpPosition! || (!seenCoords.Contains(warpPosition!)))) {
					var path = new List<Coordinate>(current.Path) {
						warpPosition!
					};
					if (end == warpPosition!) return path;
					queue.Enqueue((path, warpPosition!));
					seenCoords.Add(warpPosition!);
				}
			}
			return new List<Coordinate>();
		}
	}
}