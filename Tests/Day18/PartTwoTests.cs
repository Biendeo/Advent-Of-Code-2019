using AdventOfCodeLib.Day18.Part2;
using System.Linq;
using Xunit;

namespace Tests.Day18 {
	public class PartTwoTests {
		[Theory]
		[InlineData("#######\n#a.#Cd#\n##...##\n##.@.##\n##...##\n#cB#Ab#\n#######", 8)]
		[InlineData("###############\n#d.ABC.#.....a#\n######...######\n######.@.######\n######...######\n#b.....#.....c#\n###############", 24)]
		[InlineData("#############\n#DcBa.#.GhKl#\n#.###...#I###\n#e#d#.@.#j#k#\n###C#...###J#\n#fEbA.#.FgHi#\n#############", 32)]
		[InlineData("#############\n#g#f.D#..h#l#\n#F###e#E###.#\n#dCba...BcIJ#\n#####.@.#####\n#nK.L...G...#\n#M###N#H###.#\n#o#m..#i#jk.#\n#############", 72)]
		public void TestShortestPath(string map, int expectedOutput) {
			Assert.Equal(expectedOutput, Solution.Solve(map.Split("\n").ToList()));
		}
	}
}
