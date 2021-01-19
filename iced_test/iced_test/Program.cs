using System;
using System.IO;
using System.Diagnostics;
using Iced.Intel;

namespace iced_test
{
    class Program
    {
        const int HEXBYTES_COLUMN_BYTE_LENGTH = 10;
        const ulong exampleCodeRIP = 0x0;

        static void Main(string[] args)
        {
            int exampleCodeBitness = 32;

            if (args.Length == 2)
            {
                switch (args[0])
                {
                    case "32": { exampleCodeBitness = 32; break; }
                    case "64": { exampleCodeBitness = 64; break; }
                    default:
                        break;
                }
            }
            else
            {
                Console.Write("Must use arguments: <bit-length[32|64]> <target file path>");
                return;
            }

            string filePath = args[1];

            BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open));
            var fileSize = Convert.ToInt32(new FileInfo(filePath).Length);
            var codeBytes = reader.ReadBytes(fileSize);

            var sw = Stopwatch.StartNew();

            var codeReader = new ByteArrayCodeReader(codeBytes);
            var decoder = Decoder.Create(exampleCodeBitness, codeReader);
            decoder.IP = exampleCodeRIP;
            ulong endRip = decoder.IP + (uint)codeBytes.Length;

            var instructions = new InstructionList();
            while (decoder.IP < endRip)
            {
                decoder.Decode(out instructions.AllocUninitializedElement());
            }

            var formatter = new MasmFormatter();
            formatter.Options.DigitSeparator = "`";
            formatter.Options.FirstOperandCharIndex = 10;
            var output = new StringOutput();
            foreach (ref var instr in instructions)
            {
                formatter.Format(instr, output);
                Console.WriteLine(instr.IP.ToString("X8") + " " + output.ToStringAndReset());
            }

            sw.Stop();
            Console.Error.WriteLine("Total dump time: {0:F} sec.", sw.Elapsed.TotalSeconds);
        }
    }
}
