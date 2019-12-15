using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCodeLib.Day14 {
	[DebuggerDisplay("Ingredients: {Ingredients.Count}, Output: {Output}")]
	class Reaction {
		public List<Ingredient> Ingredients { get; }
		public Ingredient Output { get; }

		public Reaction(string reactionString) {
			Ingredients = new List<Ingredient>();

			var regex = new Regex("([0-9]+) ([A-Z]+)");
			foreach (Match? match in regex.Matches(reactionString)) {
				if (match != null) {
					Ingredients.Add(new Ingredient {
						Amount = long.Parse(match.Groups[1].Value),
						Name = match.Groups[2].Value
					});
				}
			}

			Output = Ingredients.Last();
			Ingredients.Remove(Output);
		}

		private void ConsolidateInputs() {
			var newIngredients = new List<Ingredient>();
			var ingredientNames = new HashSet<string>(Ingredients.Select(i => i.Name));
			foreach (string name in ingredientNames) {
				newIngredients.Add(new Ingredient {
					Amount = Ingredients.Where(i => i.Name == name).Sum(i => i.Amount),
					Name = name
				});
			}
			Ingredients.Clear();
			Ingredients.AddRange(newIngredients);
		}

		public void SubstituteReaction(Reaction otherReaction) {
			var ingredient = Ingredients.Single(i => i.Name == otherReaction.Output.Name);
			Ingredients.Remove(ingredient);
			long iterationsRequired = (ingredient.Amount - 1) / otherReaction.Output.Amount + 1;

			foreach (var otherIngredient in otherReaction.Ingredients) {
				Ingredients.Add(new Ingredient {
					Amount = otherIngredient.Amount * iterationsRequired,
					Name = otherIngredient.Name
				});
			}

			ConsolidateInputs();
		}
	}
}
