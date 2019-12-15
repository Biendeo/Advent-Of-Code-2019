using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day14.Part2 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			return Solve(File.ReadAllLines(inputFile));
		}
		public static long Solve(string[] reactionStrings) {
			var reactions = reactionStrings.Select(r => new Reaction(r)).ToList();

			var inventory = new Dictionary<string, long> {
				{ "ORE", 1_000_000_000_000L }
			};

			MakeMaterial("FUEL", 1, inventory, reactions);

			long oreRequired = 1_000_000_000_000L - inventory["ORE"];

			while (MakeMaterial("FUEL", Math.Max(1, inventory["ORE"] / 2 / oreRequired), inventory, reactions)) { }

			return inventory["FUEL"];
		}

		private static bool MakeMaterial(string materialName, long requiredAmount, Dictionary<string, long> inventory, List<Reaction> reactions) {
			if (materialName == "ORE") {
				return false;
			}
			if (!inventory.ContainsKey(materialName)) {
				inventory[materialName] = 0L;
			}

			var reaction = reactions.Single(r => r.Output.Name == materialName);

			long iterationsRequired = (requiredAmount - (materialName == "FUEL" ? 0 : inventory.GetValueOrDefault(materialName)) - 1) / reaction.Output.Amount + 1;

			foreach (var ingredient in reaction.Ingredients) {
				if (inventory.GetValueOrDefault(ingredient.Name) < ingredient.Amount * iterationsRequired) {
					if (!MakeMaterial(ingredient.Name, ingredient.Amount * iterationsRequired, inventory, reactions)) {
						return false;
					}
				}
				inventory[ingredient.Name] -= ingredient.Amount * iterationsRequired;
			}

			inventory[materialName] += iterationsRequired * reaction.Output.Amount;

			return true;
		}
	}
}
