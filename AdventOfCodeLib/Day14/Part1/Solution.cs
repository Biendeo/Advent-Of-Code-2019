using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day14.Part1 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile));
		}
		public static long Solve(string[] reactionStrings) {
			var reactions = reactionStrings.Select(r => new Reaction(r)).ToList();

			var fuelReaction = reactions.Single(r => r.Output.Name == "FUEL");

			while (fuelReaction.Ingredients.Count != 1 || fuelReaction.Ingredients.First().Name != "ORE") {
				var reaction = reactions.Where(r => fuelReaction.Ingredients.Exists(i => i.Name == r.Output.Name) && !reactions.Exists(r2 => r2 != r && r2 != fuelReaction && r2.Ingredients.Exists(i => i.Name == r.Output.Name))).First();
				fuelReaction.SubstituteReaction(reaction);
				reactions.Remove(reaction);
			}

			return fuelReaction.Ingredients.Single().Amount;
		}
	}
}