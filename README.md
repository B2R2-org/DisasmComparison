Comparing B2R2's disassembler with Iced, SharpDisasm and Capstone.NET
====

In this repository, we compare the speed of B2R2's disassembly with 3 other
well-known disassembly libraries. For the comparison, we use three different
[raw binary blobs](#binary-blobs-used) that we gathered from default linux
distros as follows.

# Binary Blobs Used

1. [blob_x86](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/blob_x86)
1. [blob_x86_64](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/blob_x86_64)
1. [libc_x86](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/libc_x86)

# Comparison Targets

We compare B2R2 (v0.5.0) against three .NET-based tools. For Capstone, we used both the
Python version and .NET version.

- [Iced](https://github.com/0xd4d/iced/)
- [SharpDisasm](https://github.com/spazzarama/SharpDisasm/)
- [Capstone.NET](https://github.com/9ee1/Capstone.NET)
- [Capstone(python)](http://www.capstone-engine.org/)

# Commands to Run Each Tool

`<bit-length>` is either 32 or 64, and `<target file path>` is the path to one
of the blobs.

### B2R2

```
dotnet run -c release --project b2r2_test/b2r2_test/b2r2_test.csproj <bit-length> <target file path>
```

### Iced

```
dotnet run -c release --project iced_test/iced_test/iced_test.csproj <bit-length> <target file path>
```

### SharpDisasm

```
dotnet run -c release --project sharpdisasm_test/sharpdisasm_test/sharpdisasm_test.csproj <bit-length> <target file path>
```

### Capstone.NET

```
dotnet run -c release --project Capstone.NET_test/Capstone.NET_test/Capstone.NET_test.csproj <bit-length> <target file path>
```

### Capstone(python)

```
python capstone_test_python.py <bit-length> <target file path>
```

# Results

Below is the result showing how many seconds it took to disassemble each binary
blob (lower is better). Each number is the average of 10 repeated runs. B2R2
took the *second* place, but please note that B2R2 is not merely a disassembly
engine: it can lift a binary into a well-formed IR, and it can recover CFG from
it, and many more.

| Rank | Tool              | blob_x86 | blob_x86_64 | libc_x86 | Average |
|------|-------------------|----------|-------------|----------|---------|
| 1    | Iced (C#)         | 25.067   | 21.549      | 0.594    | 15.737  |
| 2    | B2R2 (F#)         | 28.298   | 24.317      | 0.63     | 17.748  |
| 3    | SharpDisasm (C#)  | 29.03    | 25.234      | 0.7      | 18.321  |
| 4    | Capstone.NET (C#) | 127.847  | 106.414     | 2.245    | 78.835  |
| 5    | Capstone (python) | 132.693  | 111.604     | 2.22     | 82.172  |
