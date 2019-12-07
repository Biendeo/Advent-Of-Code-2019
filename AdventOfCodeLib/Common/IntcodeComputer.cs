using System;
using System.Collections.Generic;

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
		private List<int> lastProgramState;

		private readonly Func<int> inputFunc;
		private readonly Action<int> outputFunc;


		public IntcodeComputer(List<int> program) : this(program, () => 1000000000, (x) => { }) { }

		public IntcodeComputer(List<int> program, Queue<int> inputBuffer, List<int> outputBuffer) : this(program, () => inputBuffer.Dequeue(), (x) => outputBuffer.Add(x)) { }

		public IntcodeComputer(List<int> program, Func<int> inputFunc, Action<int> outputFunc) {
			initialProgram = new List<int>(program);
			lastProgramState = new List<int>();
			this.inputFunc = inputFunc;
			this.outputFunc = outputFunc;
		}

		public void RunProgram() {
			int currentIndex = 0;
			var program = new List<int>(initialProgram);

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
						PerformInput(ref program, ref currentIndex, inputFunc);
						break;
					case Opcode.Output:
						PerformOutput(ref program, ref currentIndex, outputFunc);
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
		}

		public List<int> GetLastProgramState() {
			return new List<int>(lastProgramState);
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

		private static void PerformInput(ref List<int> program, ref int currentIndex, Func<int> inputFunc) {
			WriteValue(inputFunc.Invoke(), program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program);
			currentIndex += 2;
		}

		private static void PerformOutput(ref List<int> program, ref int currentIndex, Action<int> outputFunc) {
			outputFunc.Invoke(ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program));
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
