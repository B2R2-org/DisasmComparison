Comparing B2R2's disassembler with Iced, SharpDisasm and Capstone.NET
====

In this repository, we compare B2R2's disassembly speed with 3 other well-known
disassembly libraries. We used three different [raw binary
blobs](#binary-blobs-used) appeared in our paper.

# Binary Blobs Used

1. [blob_x86](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/blob_x86)
1. [blob_x86_64](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/blob_x86_64)
1. [libc_x86](https://github.com/B2R2-org/BAR19-Artifact/raw/master/blobs/libc_x86)

# Comparison Targets

- [Iced](https://github.com/0xd4d/iced/)
- [SharpDisasm](https://github.com/spazzarama/SharpDisasm/)
- [Capstone.NET](https://github.com/9ee1/Capstone.NET)
- [Capstone(python)](http://www.capstone-engine.org/)

### B2R2

**dotnet run -c release --project** `b2r2_test/b2r2_test/b2r2_test.csproj` `bit-length[32|64]` `<target file path>`

or

**dotnet build -c release --project** `b2r2_test/b2r2_test/b2r2_test.csproj` 
and then **executable** `bit-length[32|64]` `<target file path>`

### Iced

**dotnet run -c release --project** `iced_test/iced_test/iced_test.csproj` `bit-length[32|64]` `<target file path>`

or

**dotnet build -c release --project** `iced_test/iced_test/iced_test.csproj` 
and then **executable** `bit-length[32|64]` `<target file path>`

### SharpDisasm

**dotnet run -c release --project** `sharpdisasm_test/sharpdisasm_test/sharpdisasm_test.csproj` `bit-length[16|32|64]` `<target file path>`

or

**dotnet build -c release --project** `sharpdisasm_test/sharpdisasm_test/sharpdisasm_test.csproj` 
and then **executable** `bit-length[16|32|64]` `<target file path>`

### Capstone.NET

**dotnet run -c release --project** `Capstone.NET_test/Capstone.NET_test/Capstone.NET_test.csproj` `bit-length[16|32|64]` `<target file path>`

or

**dotnet build -c release --project** `Capstone.NET_test/Capstone.NET_test/Capstone.NET_test.csproj` 
and then **executable** `bit-length[16|32|64]` `<target file path>`

### Capstone(python)

**python** `capstone_test_python.py bit-length[32|64]` `<target file path>`

# Results (Time to disassemble binaries; lower is better)

| Tool            | blob_x86 | blob_x86_64 | libc_x86 |
|-----------------|----------|-------------|----------|
| B2R2            |  28.298  |   24.317    |  0.63    |
| Iced            |  25.067  |   21.549    |  0.594   |
| SharpDisasm     |  29.03   |   25.234    |  0.7     |
| Capstone.NET    |  127.847 |   106.414   |  2.245   |
| Capstone(python)|  132.693 |   111.604   |  2.22    |
