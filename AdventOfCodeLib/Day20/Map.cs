using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeLib.Day20 {
	internal class Map {
		public Coordinate Start { get; set; }
		public Coordinate End { get; set; }
		public List<Warp> Warps { get; set; }
		public List<string> MapGrid { get; set; }

		public Map(List<string> rawMap) {
			Warps = new List<Warp>();
			Start = new Coordinate(-1, -1);
			End = new Coordinate(-1, -1);
			var rawMapEditable = rawMap.Select(r => r.Select(c => c).ToList()).ToList();

			for (int y = 0; y < rawMapEditable.Count; ++y) {
				for (int x = 0; x < rawMapEditable[y].Count; ++x) {
					if (rawMapEditable[y][x] == ' ') {
						rawMapEditable[y][x] = '#';
					} else if (rawMapEditable[y][x] >= 'A' && rawMapEditable[y][x] <= 'Z' && x < rawMapEditable[y].Count - 1 && y < rawMapEditable.Count) {
						char letter1_1 = rawMapEditable[y][x];
						char letter2_1 = rawMapEditable[y + 1][x];
						bool vertical_1 = true;
						bool mazeBefore_1 = y < rawMapEditable.Count / 2 && y > 2 || y == rawMapEditable.Count - 2;
						if (letter2_1 < 'A' || letter2_1 > 'Z') {
							letter2_1 = rawMapEditable[y][x + 1];
							vertical_1 = false;
							mazeBefore_1 = x < rawMapEditable[y].Count / 2 && x > 2 || x == rawMapEditable[y].Count - 2;
						}
						if (letter1_1 == 'A' && letter2_1 == 'A') {
							if (vertical_1) {
								if (mazeBefore_1) {
									Start = new Coordinate(x, y - 1);
								} else {
									Start = new Coordinate(x, y + 2);
								}
							} else {
								if (mazeBefore_1) {
									Start = new Coordinate(x - 1, y);
								} else {
									Start = new Coordinate(x + 2, y);
								}
							}
						} else if (letter1_1 == 'Z' && letter2_1 == 'Z') {
							if (vertical_1) {
								if (mazeBefore_1) {
									End = new Coordinate(x, y - 1);
								} else {
									End = new Coordinate(x, y + 2);
								}
							} else {
								if (mazeBefore_1) {
									End = new Coordinate(x - 1, y);
								} else {
									End = new Coordinate(x + 2, y);
								}
							}
						} else {
							for (int y2 = 0; y2 < rawMapEditable.Count - 1; ++y2) {
								for (int x2 = 0; x2 < rawMapEditable[y2].Count - 1; ++x2) {
									if (y2 != y || x2 != x) {
										char letter1_2 = rawMapEditable[y2][x2];
										char letter2_2 = rawMapEditable[y2 + 1][x2];
										bool vertical_2 = true;
										bool mazeBefore_2 = y2 < rawMapEditable.Count / 2 && y2 > 2 || y2 == rawMapEditable.Count - 2;
										if (letter2_2 < 'A' || letter2_2 > 'Z') {
											letter2_2 = rawMapEditable[y2][x2 + 1];
											vertical_2 = false;
											mazeBefore_2 = x2 < rawMapEditable[y2].Count / 2 && x2 > 2 || x2 == rawMapEditable[y2].Count - 2;
										}
										if (letter1_1 == letter1_2 && letter2_1 == letter2_2) {
											Coordinate point1;
											if (vertical_1) {
												if (mazeBefore_1) {
													point1 = new Coordinate(x, y - 1);
												} else {
													point1 = new Coordinate(x, y + 2);
												}
											} else {
												if (mazeBefore_1) {
													point1 = new Coordinate(x - 1, y);
												} else {
													point1 = new Coordinate(x + 2, y);
												}
											}
											Coordinate point2;
											if (vertical_2) {
												if (mazeBefore_2) {
													point2 = new Coordinate(x2, y2 - 1);
												} else {
													point2 = new Coordinate(x2, y2 + 2);
												}
											} else {
												if (mazeBefore_2) {
													point2 = new Coordinate(x2 - 1, y2);
												} else {
													point2 = new Coordinate(x2 + 2, y2);
												}
											}
											Warps.Add(new Warp(point1, point2));
											rawMapEditable[y2][x2] = '#';
											if (vertical_2) {
												rawMapEditable[y2 + 1][x2] = '#';
											} else {
												rawMapEditable[y2][x2 + 1] = '#';
											}
										}
									}
								}
							}
						}
						rawMapEditable[y][x] = '#';
						if (vertical_1) {
							rawMapEditable[y + 1][x] = '#';
						} else {
							rawMapEditable[y][x + 1] = '#';
						}
					}
				}
			}

			MapGrid = rawMapEditable.Select(r => string.Concat(r)).ToList();
		}
	}
}
