using AdventOfCodeLib.Day18.Part1;
using System.Linq;
using Xunit;

namespace Tests.Day18 {
	public class PartOneTests {
		[Theory]
		[InlineData("#########\n# b.A.@.a#\n#########", 8)]
		[InlineData("########################\n#f.D.E.e.C.b.A.@.a.B.c.#\n######################.#\n#d.....................#\n########################", 86)]
		[InlineData("########################\n#...............b.C.D.f#\n#.######################\n#.....@.a.B.c.d.A.e.F.g#\n########################", 132)]
		[InlineData("#################\n#i.G..c...e..H.p#\n########.########\n#j.A..b...f..D.o#\n########@########\n#k.E..a...g..B.n#\n########.########\n#l.F..d...h..C.m#\n#################", 136)]
		[InlineData("########################\n#@..............ac.GI.b#\n###d#e#f################\n###A#B#C############S####\n###g#h#i################\n########################", 81)]
		public void TestShortestPath(string map, int expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(map.Split("\n").ToList()));
		}
	}
}
