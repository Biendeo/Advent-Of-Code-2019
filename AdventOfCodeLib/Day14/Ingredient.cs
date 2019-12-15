using System.Diagnostics;

namespace AdventOfCodeLib.Day14 {
	[DebuggerDisplay("{Amount} {Name}")]
	class Ingredient {
		public long Amount { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
