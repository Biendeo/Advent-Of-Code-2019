using AdventOfCodeLib.Day12.Part2;
using Xunit;

namespace Tests.Day12 {
	public class PartTwoTests {
		[Theory]
		[InlineData("<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>", 2772)]
		public void TestRepeatingState(string startingPositions, int expectedSteps) {
			Assert.Equal(expectedSteps, Solution.Solve(startingPositions.Split("\n")));
		}
	}
}
