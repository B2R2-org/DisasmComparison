using System;
using System.IO;
using Gee.External.Capstone;
using Gee.External.Capstone.X86;
using System.Diagnostics;

namespace Capstone.NET_test
{
    class Program
    {
        static void Main(string[] args)
        {
            X86DisassembleMode disassembleMode = X86DisassembleMode.Bit32;

            if (args.Length == 2)
            {
                switch (args[0])
                {
                    case "16": { disassembleMode = X86DisassembleMode.Bit16; break; }
                    case "32": { disassembleMode = X86DisassembleMode.Bit32; break; }
                    case "64": { disassembleMode = X86DisassembleMode.Bit64; break; }
                    default:
                        break;
                }
            }
            else
            {
                Console.Write("Must use arguments: <bit-length[16|32|64]> <target file path>");
                return;
            }

            string filePath = args[1];

            BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));
            var fileSize = Convert.ToInt32(new FileInfo(filePath).Length);
            var codeBytes = reader.ReadBytes(fileSize);

            var sw = Stopwatch.StartNew();
            CapstoneX86Disassembler disassembler = CapstoneX86Disassembler.CreateX86Disassembler(disassembleMode);

            disassembler.EnableSkipDataMode = true;
            disassembler.EnableInstructionDetails = false;
            disassembler.DisassembleSyntax = DisassembleSyntax.Intel;

            foreach (X86Instruction instruction in disassembler.Iterate(codeBytes))
            {
                var address = instruction.Address;
                var mnemonic = instruction.Mnemonic;
                var operand = instruction.Operand;
                Console.WriteLine("{0:X}:\t{1}\t{2}", address, mnemonic, operand);
            }

        sw.Stop();
            Console.Error.WriteLine("Total dump time: {0:F} sec.", sw.Elapsed.TotalSeconds);
        }
    }
}
