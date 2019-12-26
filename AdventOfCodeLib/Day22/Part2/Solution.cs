using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCodeLib.Day22.Part2 {
	public static class Solution {

		public static long SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList());
		}

		public static long Solve(List<string> instructions) {
			// Rather than simulate all cards, just track the index of the target card.
			long index = 2020;
			const long numCards = 119315717514047L;
			const long numShuffles = 101741582076661L;

			// I have no idea how this works; this was cleaned up from https://github.com/giuliohome/AdventOfCode2019/blob/master/day22_part2.cs

			instructions.Reverse();

			var (a, b) = CondenseInstructions(instructions, numCards, index);

			if (a < 0) a += numCards;
			if (b < 0) b += numCards;

			return (long)((BigInteger.ModPow(a, numShuffles, numCards) * index + b * (BigInteger.ModPow(a, numShuffles, numCards) + numCards - 1) * BigInteger.ModPow(a - 1, numCards - 2, numCards) + numCards) % numCards);
		}

		public static (BigInteger a, BigInteger b) CondenseInstructions(List<string> instructions, long numCards, long index) {
			BigInteger a = 1;
			BigInteger b = 0;
			foreach (string instruction in instructions) {
				if (instruction == "deal into new stack") {
					a = -a % numCards;
					b = (-(b + 1)) % numCards;
					index = numCards - 1 - index;
				} else if (instruction.StartsWith("cut")) {
					long amount = long.Parse(instruction.Substring(4));
					b = (b + amount) % numCards;
					if (amount < 0) amount += numCards;
					index = (index + amount) % numCards;
				} else if (instruction.StartsWith("deal with increment")) {
					long amount = long.Parse(instruction.Substring(20));
					var p = BigInteger.ModPow(amount, numCards - 2, numCards);
					a = a * p % numCards;
					b = b * p % numCards;
					if (index % amount == 0) {
						index /= amount;
					} else {
						long start = 0;
						long div_acc = 0;
						long test = -1;
						for (long smart = 0; smart < 1000000; ++smart) {
							long div = ((numCards - start) / amount) + 1;
							div_acc += div;
							long rest = start + div * amount - numCards;
							long diff = index - rest;
							if (diff % amount == 0) {
								test = (diff / amount) + div_acc;
								break;
							}
							start = rest;
						}
						if (test * amount % numCards == index) {
							index = test;
						} else {
							// Ideally this bit shouldn't run.
							for (long scan = 0; scan < numCards; ++scan) {
								if (scan * amount % numCards == index) {
									index = amount;
									break;
								}
							}
						}
					}
				}
			}

			return (a, b);
		}
	}
}
