#!/bin/bash

set -e 

echo "Running tests..."
for path in test/*.Tests/*.csproj; do
	echo "Running test: $path"
	dotnet test -c Release ${path}
done

for path in test/*.PerformanceTests/*.PerformanceTests.csproj; do
	echo "Building performance test: $path"
	dotnet build -c Release ${path}
done