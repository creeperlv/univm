# Developing UniVM.c

This document is the guideline of developing `univm.c`.

## IDEs/Editors

This section introduces IDEs/Editors that has been experimented with.

### micro

Most code is written using `micro` editor with plugin `lsp`. The LSP Server is `clangd`.

### Visual Studio 2022

`Visual Studio 2022` can open and perform code hint with no problem by just open the `univm.c` folder

### Visual Studio 2005

To edit with `Visual Studio 2005`, use project under `project/VS2005`

### Kate

Just open `univm.c` folder. Code navigation works fine.

### nano

Just use `micro`, it's basically a better `nano`. If you insist, just remember to use C syntax highlight.

## Notes

### Exclude any auto-generated files before commiting to git

Here means exclude compiler generated files such as `.exe`, elf files etc.

### As much Universal as possible 

Mostly use `C99`. Some compilers might require variables be declared at the start of a functions.

The bottom line is that the code should work with (GNU/Linux|musl+busybox/Linux) and main line Windows (x86-64 and arm64). It would be nice to work with FreeBSD and DOS.

Better to stick with 8.3 naming scheme.

## Syscalls

### Default Syscall

When implementing default syscalls, number table should refer to FreeBSD syscalls.
