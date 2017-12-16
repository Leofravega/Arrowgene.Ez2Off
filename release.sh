# https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore2x

VERSION="1.1"

mkdir ./release
for RUNTIME in win-x86 win-x64 linux-x64 osx-x64; do
    dotnet publish Arrowgene.Ez2Off.CLI/Arrowgene.Ez2Off.CLI.csproj --runtime $RUNTIME --configuration Release --output ../publish/$RUNTIME-$VERSION
    tar cjf ./release/$RUNTIME-$VERSION.tar.gz ./publish/$RUNTIME-$VERSION
done 
 