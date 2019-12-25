using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeLib.Day20 {
	internal class Warp {
		public Coordinate Point1 { get; set; }
		public Coordinate Point2 { get; set; }

		public Warp(Coordinate point1, Coordinate point2) {
			Point1 = point1;
			Point2 = point2;
		}
	}
}
