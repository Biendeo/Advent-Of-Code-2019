using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day04.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			string[] input = File.ReadAllText(inputFile).Split("-");
			return SolveFromRange(int.Parse(input[0]), int.Parse(input[1]));
		}

		public static int SolveFromRange(int lowerRange, int upperRange) {
			return Enumerable.Range(lowerRange, upperRange - lowerRange).AsParallel().Count(p => IsPasswordValid(p));
		}

		public static bool IsPasswordValid(int password) {
			return password >= 100000 && password <= 999999 && IsPasswordIncreasing(password) && DoesPasswordHaveMatchingDigits(password);
		}

		private static int GetDigit(int number, int digit) {
			for (int i = 0; i < digit; ++i) {
				number /= 10;
			}
			return number % 10;
		}

		private static bool IsPasswordIncreasing(int password) {
			int lastDigit = 9;
			for (int i = 0; i < 6; ++i) {
				if (GetDigit(password, i) > lastDigit) {
					return false;
				}
				lastDigit = GetDigit(password, i);
			}
			return true;
		}

		private static bool DoesPasswordHaveMatchingDigits(int password) {
			for (int i = 0; i < 5; ++i) {
				if (GetDigit(password, i) == GetDigit(password, i + 1) && (i <= 0 || GetDigit(password, i - 1) != GetDigit(password, i)) && (i >= 4 || GetDigit(password, i + 1) != GetDigit(password, i + 2))) {
					return true;
				}
			}
			return false;
		}
	}
}
