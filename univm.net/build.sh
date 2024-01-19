#!/bin/sh
cd src/univm/
dotnet publish -c Release -o ../../bin/ -v q --nologo
cd ../univmc/
dotnet publish -c Release -o ../../bin/ -v q --nologo
cp -p isas/univm.isa ../../bin/isas
if [ -z "$PRESERVE_DEBUG_SYMBOLS" ];
then
	cd ../../bin/
	rm -f *.pdb
	rm -f *.dbg
fi
