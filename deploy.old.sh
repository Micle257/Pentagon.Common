RED='\e[1;31m'
CYAN='\e[1;36m'
GREEN='\e[1;32m'
WHITE='\e[1;37m'
NC='\e[0m' # No Color
i="${CYAN}i${WHITE}  "
e="${RED}x${WHITE}  "
s="${GREEN}+${WHITE}  "

# An error exit function
error_exit()
{
	echo -e "${e}${1}${NC}" 1>&2
	exit 1
}

prefix=$(head -n 1 version)
suffix=$(head -n2 version |  tail -n+2)
if [ ! -n "${suffix/[ ]*\n/}" ]; then
	ver=$prefix
else
	ver="${prefix}-$suffix"
fi

name="Pentagon.Common"

echo -e "${i}Name: $name${NC}"
echo -e "${i}Version: $ver${NC}"

if [ -d "./build/$name/$ver" ]; then
	error_exit "Directory for version $ver already exists."
fi

echo -e "${i}Running tests in $name...${NC}"

dotnet test -v m || error_exit "Dotnet test failed."

echo -e "${i}Building $name...${NC}"

dotnet build -v m -c Release -p:Version=$ver src/$name/ || error_exit "Dotnet build failed."

echo -e "${i}Packing $name...${NC}"

dotnet pack --no-build --no-restore -v m -c Release -p:Version=$ver src/$name/ || error_exit "Dotnet pack failed."

echo -e "${i}Copying nuget package to local cache...${NC}"

cp -n build/$name/$ver/*.nupkg /c/Users/Michal/Source/NuGet/ || error_exit "Copying to local nuget cache failed."

if [ ! -d "./build/$name/nuget" ]; then
	mkdir build/$name/nuget || error_exit "Making nuget folder failed."
fi

cp -n build/$name/$ver/*.nupkg build/$name/nuget/ || error_exit "Copying to local nuget pre-cache failed."

echo -e "${i}Pusing nuget package to nuget.org...${NC}"

dotnet nuget push -v m build/$name/$ver/*.nupkg -k oy2gwo5jfyxcggpruhtairs6jiw6v5autb2e7vmzqecgpe -s https://api.nuget.org/v3/index.json || error_exit "Dotnet nuget push failed."

echo -e "${s}Version $ver of $name is deployed.${NC}"
