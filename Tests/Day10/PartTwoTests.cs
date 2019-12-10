using AdventOfCodeLib.Day10.Part2;
using Xunit;

namespace Tests.Day10 {
	public class PartTwoTests {
		[Theory]
		[InlineData(".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", 802)]
		public void TestOutputSignal(string input, int expectedResult) {
			Assert.Equal(expectedResult, Solution.Solve(input.Split("\n")));
		}
	}
}
