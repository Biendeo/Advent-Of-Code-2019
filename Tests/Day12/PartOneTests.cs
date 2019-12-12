using AdventOfCodeLib.Day12.Part1;
using Xunit;

namespace Tests.Day12 {
	public class PartOneTests {
		[Theory]
		[InlineData("<x=-8, y=-10, z=0>\n<x=5, y=5, z=10>\n<x=2, y=-7, z=3>\n<x=9, y=-8, z=-3>", 100, 1940)]
		public void TestOutputSignal(string startingPositions, int steps, int expectedEnergy) {
			Assert.Equal(expectedEnergy, Solution.Solve(startingPositions.Split("\n"), steps));
		}
	}
}
