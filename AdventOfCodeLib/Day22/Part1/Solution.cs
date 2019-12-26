using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day22.Part1 {
	public static class Solution {

		public static int SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile).ToList(), 10007).IndexOf(2019);
		}

		public static List<int> Solve(List<string> instructions, int numCards) {
			var deck = Enumerable.Range(0, numCards).ToList();

			foreach (string instruction in instructions) {
				if (instruction == "deal into new stack") {
					deck.Reverse();
				} else if (instruction.StartsWith("cut")) {
					int amount = int.Parse(instruction.Substring(4));
					if (amount < 0) amount += numCards;
					deck.AddRange(deck.GetRange(0, amount));
					deck.RemoveRange(0, amount);
				} else if (instruction.StartsWith("deal with increment")) {
					int amount = int.Parse(instruction.Substring(20));
					var newDeck = new List<int>(Enumerable.Repeat(0, numCards));
					for (int i = 0; i < numCards * amount; i += amount) {
						newDeck[i % numCards] = deck[i / amount];
					}
					deck = newDeck;
				}
			}

			return deck;
		}
	}
}