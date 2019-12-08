using AdventOfCodeLib.Day08.Part1;
using System.Collections.Generic;
using Xunit;

namespace Tests.Day08 {
	public class PartOneTests {
		[Theory]
		[MemberData(nameof(TestData))]
		public void TestOutputSignal(List<int> imagePixels, int width, int height, int expectedResult) {
			Assert.Equal(expectedResult, Solution.Solve(imagePixels, width, height));
		}

		public static IEnumerable<object[]> TestData {
			get {
				return new[] {
					new object[] { new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 }, 3, 2, 1 },
				};
			}
		}
	}
}
