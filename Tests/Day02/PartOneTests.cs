using AdventOfCodeLib.Day02.Part1;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Day02 {
	public class PartOneTests {
		[Theory]
		[InlineData("1,0,0,0,99", "2,0,0,0,99")]
		[InlineData("2,3,0,3,99", "2,3,0,6,99")]
		[InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
		[InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
		public void TestProgram(string program, string expectedProgram) {
			Assert.Equal(new List<int>(expectedProgram.Split(",").Select(c => int.Parse(c))), Solution.SolveFromProgramOutputProgram(program, false));
		}
	}
}
