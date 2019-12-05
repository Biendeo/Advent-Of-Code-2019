using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeLib.Common {
	class IntcodeComputer {
		private enum ParameterMode {
			Position = 0,
			Immediate = 1
		}
		private enum Opcode {
			Addition = 1,
			Multiplication = 2,
			Input = 3,
			Output = 4,
			JumpIfTrue = 5,
			JumpIfFalse = 6,
			LessThan = 7,
			Equals = 8,
			Halt = 99
		}

		private readonly List<int> initialProgram;
		private readonly Queue<int> initialInputBuffer;

		private List<int> lastProgramState;
		private List<int> lastOutputBuffer;

		public IntcodeComputer(List<int> program, List<int> inputBuffer) {
			initialProgram = new List<int>(program);
			initialInputBuffer = new Queue<int>(inputBuffer);
			lastProgramState = new List<int>();
			lastOutputBuffer = new List<int>();
		}

		public void RunProgram() {
			int currentIndex = 0;
			var program = new List<int>(initialProgram);
			var inputBuffer = new Queue<int>(initialInputBuffer);
			var outputBuffer = new List<int>();

			bool programEnd = false;

			while (currentIndex < program.Count && !programEnd) {
				switch (ExtractOpcode(program[currentIndex])) {
					case Opcode.Addition:
						PerformAddtion(ref program, ref currentIndex);
						break;
					case Opcode.Multiplication:
						PerformMultiplication(ref program, ref currentIndex);
						break;
					case Opcode.Input:
						PerformInput(ref program, ref currentIndex, ref inputBuffer);
						break;
					case Opcode.Output:
						PerformOutput(ref program, ref currentIndex, ref outputBuffer);
						break;
					case Opcode.JumpIfTrue:
						PerformJumpIfTrue(ref program, ref currentIndex);
						break;
					case Opcode.JumpIfFalse:
						PerformJumpIfFalse(ref program, ref currentIndex);
						break;
					case Opcode.LessThan:
						PerformLessThan(ref program, ref currentIndex);
						break;
					case Opcode.Equals:
						PerformEquals(ref program, ref currentIndex);
						break;
					case Opcode.Halt:
						programEnd = true;
						break;
				}
			}

			lastProgramState = program;
			lastOutputBuffer = outputBuffer;
		}

		public List<int> GetLastProgramState() {
			return new List<int>(lastProgramState);
		}

		public List<int> GetLastOutputBuffer() {
			return new List<int>(lastOutputBuffer);
		}

		private static Opcode ExtractOpcode(int instruction) {
			return (Opcode)(instruction % 100);
		}

		private static ParameterMode ExtractParameterMode(int instruction, int parameterNum) {
			if (parameterNum < 1 || parameterNum > 3) {
				throw new ArgumentException($"parameterNum cannot be {parameterNum}");
			}
			instruction /= 10;
			for (int i = 0; i < parameterNum; ++i) {
				instruction /= 10;
			}
			return (ParameterMode)(instruction % 10);
		}

		private static int ReadValue(int parameter, ParameterMode mode, ref List<int> program) {
			if (mode == ParameterMode.Position) {
				return program[parameter];
			} else {
				return parameter;
			}
		}

		private static void WriteValue(int value, int parameter, ParameterMode mode, ref List<int> program) {
			if (mode == ParameterMode.Position) {
				program[parameter] = value;
			} else {
				throw new ArgumentException("WriteValue can only be performed in position mode!");
			}
		}

		private static void PerformAddtion(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			WriteValue(value1 + value2, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program);
			currentIndex += 4;
		}

		private static void PerformMultiplication(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			WriteValue(value1 * value2, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program);
			currentIndex += 4;
		}

		private static void PerformInput(ref List<int> program, ref int currentIndex, ref Queue<int> inputBuffer) {
			WriteValue(inputBuffer.Dequeue(), program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			currentIndex += 2;
		}

		private static void PerformOutput(ref List<int> program, ref int currentIndex, ref List<int> outputBuffer) {
			outputBuffer.Add(ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program));
			currentIndex += 2;
		}

		private static void PerformJumpIfTrue(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			if (value1 != 0) {
				currentIndex = value2;
			} else {
				currentIndex += 3;
			}
		}

		private static void PerformJumpIfFalse(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			if (value1 == 0) {
				currentIndex = value2;
			} else {
				currentIndex += 3;
			}
		}

		private static void PerformLessThan(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			WriteValue(value1 < value2 ? 1 : 0, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program);
			currentIndex += 4;
		}

		private static void PerformEquals(ref List<int> program, ref int currentIndex) {
			int value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			int value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program);
			WriteValue(value1 == value2 ? 1 : 0, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program);
			currentIndex += 4;
		}
	}
}
