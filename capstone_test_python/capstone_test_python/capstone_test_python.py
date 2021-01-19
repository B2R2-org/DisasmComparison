
import sys
from capstone import *
from stopwatch import Stopwatch

def eprint(*args, **kwargs):
    print(*args, file=sys.stderr, **kwargs)

if __name__ == '__main__':

    if len(sys.argv) == 3:
        bit_length = sys.argv[1]
        file_path = sys.argv[2]
    else:
        print("Must use arguments: <bit-length[32|64]> <target file path>")
        exit(0)

    if bit_length == "32":
        md = Cs(CS_ARCH_X86, CS_MODE_32)
    elif bit_length == "64":
        md = Cs(CS_ARCH_X86, CS_MODE_64)
    else:
        md = Cs(CS_ARCH_X86, CS_MODE_32)

    with open(file_path, 'rb') as f:
        bytes = f.read()
    
    md.skipdata = True
    stopwatch = Stopwatch()
    stopwatch.reset()
    stopwatch.start()
    for ins in md.disasm(bytes, 0x0):
        print("0x{0:x}:\t{1}".format(ins.address, ins.op_str))
    stopwatch.stop()
    eprint("Total dump time: {0:.2f} sec.".format(stopwatch.duration))
