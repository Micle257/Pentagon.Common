#!/bin/bash

prefix=$(head -n 1 version)
suffix=$(head -n2 version |  tail -n+2)
if [ ! -n "${suffix/[ ]*\n/}" ]
then
	ver=$prefix
else
	ver="${prefix}-$suffix"
fi

name="Pentagon.Common"

if [ -d "./build/$name/$ver" ]
then
	echo "Directory for version $ver already exists."
	exit 1
fi

dotnet build -c Release -p:Version=$ver src/$name/

dotnet pack -c Release -p:Version=$ver src/$name/

cp build/$name/$ver/*.nupkg /c/Users/Michal/Source/NuGet/

if [ ! -d "./build/$name/nuget" ]
then
	mkdir build/$name/nuget
fi

cp build/$name/$ver/*.nupkg build/$name/nuget/

dotnet nuget push build/$name/$ver/*.nupkg -k oy2gwo5jfyxcggpruhtairs6jiw6v5autb2e7vmzqecgpe -s https://api.nuget.org/v3/index.json

echo "Version $ver is deployed"