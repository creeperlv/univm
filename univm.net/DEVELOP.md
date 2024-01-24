# Guide of developing `UniVM.Net`

## Design Goal

The goal of `univm` and `univm.core` is to be reference implementation of `UniVM` in .net that is not too slow.

The goal of `univmc` and `univmc.core` is to be reference implementation of assembler of 

## Memory

### univm

Avoid as much as GC heap allocation as possible. Just use unsafe and pointers EVERYWHERE. For many case, especially when implement core vm, just pretend writing C :-P.

### univmc

For convenience, just use normal objects. Less bug is better here.

## Third-Party Libs

The core `univm`, `univm.core`  and `univm.syscalls` should have no third-party dependencies to maximize compatibility with normal .Net, Unity, bflat etc.

However, with `univmc` just whatever that is not malicious and unnecessary. It's  better for project `univmc.core`to be compatible with `unity`.
