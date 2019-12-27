using AdventOfCodeLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCodeLib.Day23.Part2 {
	public static class Solution {
		public static int SolveFromInputFile(string inputFile) {
			var convertedProgram = new List<long>(File.ReadAllText(inputFile).Split(",").Select(c => long.Parse(c)));
			return Solve(convertedProgram);
		}

		private static int Solve(List<long> program) {
			var inputBuffers = new List<Queue<long>>();
			var outputBuffers = new List<List<long>>();
			foreach (int i in Enumerable.Range(0, 50)) {
				inputBuffers.Add(new Queue<long>(new long[] { i }));
				outputBuffers.Add(new List<long>());
			}
			var finalBuffer = new Queue<long>();
			var waitingStates = Enumerable.Repeat(false, 50).ToList();
			var yPacketsSentByNAT = new HashSet<long>();
			bool resultFound = false;
			long result = -1;
			var computers = new List<IntcodeComputer>();
			foreach (int i in Enumerable.Range(0, 50)) {
				computers.Add(new IntcodeComputer(program, () => {
					var myInput = inputBuffers[i];
					lock (myInput) {
						if (resultFound) computers[i].Abort();
						if (myInput.Count == 0) {
							lock (waitingStates) {
								waitingStates[i] = true;
							}
							if (Environment.ProcessorCount < 50) Thread.Sleep(1); // Unless you can run 50 simultaneous threads, give other threads the chance to provide input.
							return -1;
						} else {
							lock (waitingStates) {
								waitingStates[i] = false;
							}
							return myInput.Dequeue();
						}
					}
				}, (x) => {
					var myOutput = outputBuffers[i];
					lock (myOutput) {
						myOutput.Add(x);
						if (myOutput.Count == 3) {
							Queue<long> targetBuffer;
							if (myOutput[0] == 255) {
								targetBuffer = finalBuffer;
							} else {
								targetBuffer = inputBuffers[(int)myOutput[0]];
							}
							lock (targetBuffer) {
								targetBuffer.Enqueue(myOutput[1]);
								targetBuffer.Enqueue(myOutput[2]);
							}
							myOutput.Clear();
						}
					}
				}));
			}
			var threads = new List<Thread>();
			foreach (int i in Enumerable.Range(0, 50)) {
				threads.Add(new Thread(() => {
					computers[i].RunProgram();
				}));
				threads.Last().Start();
			}
			while (!resultFound) {
				bool allWaiting = false;
				while (!allWaiting) {
					lock (waitingStates) {
						allWaiting = waitingStates.All(b => b);
					}
					Thread.Sleep(1);
				}
				lock (inputBuffers[0]) {
					lock (finalBuffer) {
						if (finalBuffer.Count >= 2) {
							while (finalBuffer.Count > 2) {
								finalBuffer.Dequeue();
							}
							long x = finalBuffer.Dequeue();
							long y = finalBuffer.Dequeue();
							inputBuffers[0].Enqueue(x);
							inputBuffers[0].Enqueue(y);
							if (yPacketsSentByNAT.Contains(y)) {
								resultFound = true;
								result = y;
							} else {
								yPacketsSentByNAT.Add(y);
							}
						}
					}
				}
			}
			threads.ForEach(t => t.Join());
			return (int)result;
		}
	}
}
