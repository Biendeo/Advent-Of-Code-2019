﻿using AdventOfCodeLib.Day07.Part1;
using System.Collections.Generic;
using Xunit;

namespace Tests.Day07 {
	public class PartOneTests {
		[Theory]
		[MemberData(nameof(TestData))]
		public void TestOutputSignal(List<long> program, int expectedResult) {
			Assert.Equal(expectedResult, Solution.Solve(program));
		}

		public static IEnumerable<object[]> TestData {
			get {
				return new[] {
					new object[] { new List<long> { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 }, 43210 },
					new object[] { new List<long> { 3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0 }, 54321 },
					new object[] { new List<long> { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0 }, 65210 }
				};
			}
		}
	}
}
