using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day06.Part1 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			return Solve(new List<(string Centre, string Orbit)>(File.ReadAllLines(inputFile).Select(l => (Centre: l.Split(")")[0], Orbit: l.Split(")")[1]))));
		}

		public static int Solve(List<(string Centre, string Orbit)> orbitalMap) {
			var planetDict = new Dictionary<string, Tree<string>>();
			foreach (var (Centre, Orbit) in orbitalMap) {
				Tree<string> centreTree;
				if (planetDict.ContainsKey(Centre)) {
					centreTree = planetDict[Centre];
				} else {
					centreTree = new Tree<string>(Centre);
					planetDict.Add(Centre, centreTree);
				}
				Tree<string> orbitTree;
				if (planetDict.ContainsKey(Orbit)) {
					orbitTree = planetDict[Orbit];
				} else {
					orbitTree = new Tree<string>(Orbit);
					planetDict.Add(Orbit, orbitTree);
				}
				centreTree.AddChild(orbitTree);
			}

			return planetDict.Values.AsParallel().Sum(t => t.Depth);
		}
	}
}
