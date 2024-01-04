#!/bin/sh
cd src/univm/
dotnet publish -c Release -o ../../bin/ -v q --nologo
cd ../univmc/
dotnet publish -c Release -o ../../bin/ -v q --nologo
if [ -z "$PRESERVE_PDBS" ];
then
	cd ../../bin/
	rm *.pdb
fi
