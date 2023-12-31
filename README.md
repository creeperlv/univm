# UNIVM

Short for Universal Virtual Machine

Runs a custom instruction set.

128-Bit wide per instruction.

|0...31|32..127|
|-|-|
|Operation|Data|
|OpCode|Data0,Data1,Data2|

Add functionality via SysCall.

## Assembly Structure 

|Part|Desc|
|-|-|
|Header|Header information of the assembly|
|Text|Text Section of the assembly |
|Program| Program Section of the assembly|


### Header

|0..3|4..7|8..11|
|-|-|-|
|Global Mem Length|Text Count|Instruction Count|

## Memory Layout

ID 0: Stack
Always preallocated, offset as using size.
