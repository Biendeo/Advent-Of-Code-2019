﻿using AdventOfCodeLib.Day06.Part1;
using System.Collections.Generic;
using Xunit;

namespace Tests.Day06 {
	public class PartOneTests {
		[Fact]
		public void TestOrbits() {
			var orbitalMap = new List<(string Centre, string Orbit)> {
				(Centre: "COM", Orbit: "B"),
				(Centre: "B", Orbit: "C"),
				(Centre: "C", Orbit: "D"),
				(Centre: "D", Orbit: "E"),
				(Centre: "E", Orbit: "F"),
				(Centre: "B", Orbit: "G"),
				(Centre: "G", Orbit: "H"),
				(Centre: "D", Orbit: "I"),
				(Centre: "E", Orbit: "J"),
				(Centre: "J", Orbit: "K"),
				(Centre: "K", Orbit: "L"),
			};

			Assert.Equal(42, Solution.Solve(orbitalMap));
		}
	}
}
