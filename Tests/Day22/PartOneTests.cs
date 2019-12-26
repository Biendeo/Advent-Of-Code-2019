using AdventOfCodeLib.Day22.Part1;
using System.Linq;
using Xunit;

namespace Tests.Day22 {
	public class PartOneTests {
		[Theory]
		[InlineData("deal into new stack", 10, new int[]{ 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
		[InlineData("cut 3", 10, new int[]{ 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 })]
		[InlineData("cut -4", 10, new int[]{ 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 })]
		[InlineData("deal with increment 3", 10, new int[]{ 0, 7, 4, 1, 8, 5, 2, 9, 6, 3 })]
		[InlineData("deal with increment 7\ndeal into new stack\ndeal into new stack", 10, new int[]{ 0, 3, 6, 9, 2, 5, 8, 1, 4, 7 })]
		[InlineData("cut 6\ndeal with increment 7\ndeal into new stack", 10, new int[]{ 3, 0, 7, 4, 1, 8, 5, 2, 9, 6 })]
		[InlineData("deal with increment 7\ndeal with increment 9\ncut -2", 10, new int[]{ 6, 3, 0, 7, 4, 1, 8, 5, 2, 9 })]
		[InlineData("deal into new stack\ncut -2\ndeal with increment 7\ncut 8\ncut -4\ndeal with increment 7\ncut 3\ndeal with increment 9\ndeal with increment 3\ncut -1", 10, new int[]{ 9, 2, 5, 8, 1, 4, 7, 0, 3, 6 })]
		public void TestShuffle(string instructions, int numCards, int[] expectedOutput) {
			Assert.Equal(expectedOutput.ToList(), Solution.Solve(instructions.Split("\n").ToList(), numCards));
		}
	}
}
