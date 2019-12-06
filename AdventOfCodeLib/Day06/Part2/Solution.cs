using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeLib.Day06.Part2 {
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

			var youTree = planetDict["YOU"];
			var searchQueue = new Queue<(Tree<string> Tree, int Steps)>();
			var searchedNodes = new HashSet<string> {
				"YOU",
				youTree.Parent!.Value
			};
			searchQueue.Enqueue((Tree: youTree.Parent!, Steps: 0));

			while (searchQueue.Count > 0) {
				var currentTree = searchQueue.Dequeue();
				if (currentTree.Tree.Parent != null && !searchedNodes.Contains(currentTree.Tree.Parent!.Value)) {
					searchQueue.Enqueue((Tree: currentTree.Tree.Parent, Steps: currentTree.Steps + 1));
					searchedNodes.Add(currentTree.Tree.Parent!.Value);
				}
				foreach (var child in currentTree.Tree.Children) {
					if (child.Value == "SAN") {
						return currentTree.Steps;
					}
					if (!searchedNodes.Contains(child.Value)) {
						searchQueue.Enqueue((Tree: child, Steps: currentTree.Steps + 1));
						searchedNodes.Add(child.Value);
					}
				}
			}

			return -1;
		}
	}
}
