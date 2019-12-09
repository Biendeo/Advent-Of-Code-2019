using AdventOfCodeLib.Day09.Part1;
using System.Collections.Generic;
using Xunit;

namespace Tests.Day09 {
	public class PartOneTests {
		[Theory]
		[MemberData(nameof(TestData))]
		public void TestOutputSignal(List<long> program, List<long> inputBuffer, List<long> expectedOutputBuffer) {
			var outputBuffer = new List<long>();
			Solution.Solve(program, inputBuffer, outputBuffer);
			Assert.Equal(expectedOutputBuffer, outputBuffer);
		}

		public static IEnumerable<object[]> TestData {
			get {
				return new[] {
					new object[] { new List<long> { 109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99 }, new List<long>(), new List<long> { 109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99 } },
					new object[] { new List<long> { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 }, new List<long>(), new List<long> { 1219070632396864 } },
					new object[] { new List<long> { 104, 1125899906842624, 99 }, new List<long>(), new List<long> { 1125899906842624 } },
				};
			}
		}
	}
}
