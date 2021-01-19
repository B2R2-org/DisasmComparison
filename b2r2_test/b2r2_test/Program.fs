open System
open B2R2
open B2R2.FrontEnd.BinInterface
open B2R2.FrontEnd.BinFile

let rec disas hdl ctxt bp (addr: Addr) =
  if BinaryPointer.IsValid bp then
    match BinHandle.TryParseInstr (hdl, ctxt, bp=bp) with
    | Ok (ins) ->
      let len = ins.Length
      Console.WriteLine (addr.ToString ("X8") + " " + ins.Disasm ())
      let bp' = BinaryPointer.Advance bp (int len)
      disas hdl ctxt bp' (addr + uint64 len)
    | Error _ ->
      let bp' = BinaryPointer.Advance bp 1
      disas hdl ctxt bp' (addr + 1UL)
  else ()

[<EntryPoint>]
let main argv =
  let wordSize, filePath =
    if argv.Length = 2 then argv.[0], argv.[1]
    else exit 1
  let isa =
    if wordSize = "32" then ISA.Init Arch.IntelX86 Endian.Little
    else ISA.Init Arch.IntelX64 Endian.Little
  let hdl =
    BinHandle.Init (isa, ArchOperationMode.NoMode, false, None,
                    fileName=filePath)

  let sw = Diagnostics.Stopwatch.StartNew ()
  let bp = hdl.FileInfo.ToBinaryPointer hdl.FileInfo.BaseAddress
  disas hdl hdl.DefaultParsingContext bp 0UL
  sw.Stop ()
  Console.Error.WriteLine ("Total time: {0:F} sec", sw.Elapsed.TotalSeconds)
  0
