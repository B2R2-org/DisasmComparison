using System;
using System.IO;
using System.Diagnostics;
namespace disasmcli
{
    class Program
    {
        static void Main(string[] args)
        {
            SharpDisasm.ArchitectureMode mode = SharpDisasm.ArchitectureMode.x86_32;

            if (args.Length == 2)
            {
                switch (args[0])
                {
                    case "16": { mode = SharpDisasm.ArchitectureMode.x86_16; break; }
                    case "32": { mode = SharpDisasm.ArchitectureMode.x86_32; break; }
                    case "64": { mode = SharpDisasm.ArchitectureMode.x86_64; break; }
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

            SharpDisasm.Disassembler.Translator.IncludeAddress = true;

            var disasm = new SharpDisasm.Disassembler(codeBytes, mode, 0, true);

            foreach (var insn in disasm.Disassemble())
                Console.Out.WriteLine(insn.ToString());

            sw.Stop();
            Console.Error.WriteLine("Total dump time: {0:F} sec.", sw.Elapsed.TotalSeconds);
        }
    }
}
