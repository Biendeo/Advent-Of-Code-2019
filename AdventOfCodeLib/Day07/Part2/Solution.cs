using AdventOfCodeLib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCodeLib.Day07.Part2 {
	public static class Solution {
		public static long SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		public static long Solve(List<long> program) {
			return new List<long> { 5, 6, 7, 8, 9 }.GetAllCombinations().AsParallel().Max(settings => {
				var buffers = new List<Queue<long>>();
				foreach (int i in Enumerable.Range(0, settings.Count())) {
					buffers.Add(new Queue<long>());
					buffers[i].Enqueue(settings.ToList()[i]);
				}
				buffers[0].Enqueue(0);
				var computers = new List<IntcodeComputer>();
				foreach (int i in Enumerable.Range(0, settings.Count())) {
					computers.Add(new IntcodeComputer(program, () => {
						var myInput = buffers[i];
						lock (myInput) {
							while (myInput.Count == 0) {
								Monitor.Wait(myInput);
							}
							return myInput.Dequeue();
						}
					}, (x) => {
						var myOutput = buffers[(i + 1) % settings.Count()];
						lock (myOutput) {
							myOutput.Enqueue(x);
							if (myOutput.Count == 1) {
								Monitor.PulseAll(myOutput);
							}
						}
					}));
				}
				var threads = new List<Thread>();
				foreach (int i in Enumerable.Range(0, settings.Count())) {
					threads.Add(new Thread(() => {
						computers[i].RunProgram();
					}));
					threads.Last().Start();
				}
				threads.ForEach(t => t.Join());
				return buffers.First().Single();
			});
		}

		private static IEnumerable<IEnumerable<T>> GetAllCombinations<T>(this List<T> l) {
			foreach (var x in l) {
				var newList = new List<T>(l);
				newList.Remove(x);
				if (newList.Count == 0) {
					yield return new List<T> { x };
				} else {
					foreach (var r in newList.GetAllCombinations()) {
						yield return new List<T> { x }.Concat(r);
					}
				}
			}
		}
	}
}
