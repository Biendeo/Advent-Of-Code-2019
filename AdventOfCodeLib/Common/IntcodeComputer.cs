using System;
using System.Collections.Generic;

namespace AdventOfCodeLib.Common {
	class IntcodeComputer {
		private enum ParameterMode {
			Position = 0,
			Immediate = 1,
			Relative = 2
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
			RelativeBase = 9,
			Halt = 99
		}

		private readonly List<long> initialProgram;
		private List<long> lastProgramState;

		private readonly Func<long> inputFunc;
		private readonly Action<long> outputFunc;

		public IntcodeComputer(List<long> program) : this(program, () => 1000000000, (x) => { }) { }

		public IntcodeComputer(List<long> program, Queue<long> inputBuffer, List<long> outputBuffer) : this(program, () => inputBuffer.Dequeue(), (x) => outputBuffer.Add(x)) { }

		public IntcodeComputer(List<long> program, Func<long> inputFunc, Action<long> outputFunc) {
			initialProgram = new List<long>(program);
			lastProgramState = new List<long>();
			this.inputFunc = inputFunc;
			this.outputFunc = outputFunc;
		}

		public void RunProgram() {
			int currentIndex = 0;
			long relativeBase = 0;
			var program = new List<long>(initialProgram);

			bool programEnd = false;

			while (currentIndex < program.Count && !programEnd) {
				switch (ExtractOpcode(program[currentIndex])) {
					case Opcode.Addition:
						PerformAddtion(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.Multiplication:
						PerformMultiplication(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.Input:
						PerformInput(ref program, ref currentIndex, ref relativeBase, inputFunc);
						break;
					case Opcode.Output:
						PerformOutput(ref program, ref currentIndex, ref relativeBase, outputFunc);
						break;
					case Opcode.JumpIfTrue:
						PerformJumpIfTrue(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.JumpIfFalse:
						PerformJumpIfFalse(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.LessThan:
						PerformLessThan(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.Equals:
						PerformEquals(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.RelativeBase:
						PerformRelativeBase(ref program, ref currentIndex, ref relativeBase);
						break;
					case Opcode.Halt:
						programEnd = true;
						break;
				}
			}

			lastProgramState = program;
		}

		public List<long> GetLastProgramState() {
			return new List<long>(lastProgramState);
		}

		private static Opcode ExtractOpcode(long instruction) {
			return (Opcode)(instruction % 100);
		}

		private static ParameterMode ExtractParameterMode(long instruction, long parameterNum) {
			if (parameterNum < 1 || parameterNum > 3) {
				throw new ArgumentException($"parameterNum cannot be {parameterNum}");
			}
			instruction /= 10;
			for (long i = 0; i < parameterNum; ++i) {
				instruction /= 10;
			}
			return (ParameterMode)(instruction % 10);
		}

		private static long ReadValue(long parameter, ParameterMode mode, ref List<long> program, long relativeBase) {
			if (mode == ParameterMode.Position) {
				return ReadValueFromMemory((int)parameter, ref program);
			} else if (mode == ParameterMode.Relative) {
				return ReadValueFromMemory((int)(parameter + relativeBase), ref program);
			} else {
				return parameter;
			}
		}

		private static void WriteValue(long value, long parameter, ParameterMode mode, ref List<long> program, long relativeBase) {
			if (mode == ParameterMode.Position) {
				WriteValueToMemory(value, (int)parameter, ref program);
			} else if (mode == ParameterMode.Relative) {
				WriteValueToMemory(value, (int)(parameter + relativeBase), ref program);
			} else {
				throw new ArgumentException("WriteValue can only be performed in position or relative mode!");
			}
		}

		private static long ReadValueFromMemory(int index, ref List<long> program) {
			while (index >= program.Count) {
				program.Add(0);
			}
			return program[index];
		}

		private static void WriteValueToMemory(long value, int index, ref List<long> program) {
			while (index >= program.Count) {
				program.Add(0);
			}
			program[index] = value;
		}

		private static void PerformAddtion(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			WriteValue(value1 + value2, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program, relativeBase);
			currentIndex += 4;
		}

		private static void PerformMultiplication(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			WriteValue(value1 * value2, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program, relativeBase);
			currentIndex += 4;
		}

		private static void PerformInput(ref List<long> program, ref int currentIndex, ref long relativeBase, Func<long> inputFunc) {
			WriteValue(inputFunc.Invoke(), program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			currentIndex += 2;
		}

		private static void PerformOutput(ref List<long> program, ref int currentIndex, ref long relativeBase, Action<long> outputFunc) {
			outputFunc.Invoke(ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase));
			currentIndex += 2;
		}

		private static void PerformJumpIfTrue(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			if (value1 != 0) {
				currentIndex = (int)value2;
			} else {
				currentIndex += 3;
			}
		}

		private static void PerformJumpIfFalse(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			if (value1 == 0) {
				currentIndex = (int)value2;
			} else {
				currentIndex += 3;
			}
		}

		private static void PerformLessThan(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			WriteValue(value1 < value2 ? 1 : 0, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program, relativeBase);
			currentIndex += 4;
		}

		private static void PerformEquals(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			long value1 = ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			long value2 = ReadValue(program[currentIndex + 2], ExtractParameterMode(program[currentIndex], 2), ref program, relativeBase);
			WriteValue(value1 == value2 ? 1 : 0, program[currentIndex + 3], ExtractParameterMode(program[currentIndex], 3), ref program, relativeBase);
			currentIndex += 4;
		}

		private static void PerformRelativeBase(ref List<long> program, ref int currentIndex, ref long relativeBase) {
			relativeBase += ReadValue(program[currentIndex + 1], ExtractParameterMode(program[currentIndex], 1), ref program, relativeBase);
			currentIndex += 2;
		}
	}
}
