# https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore2x

mkdir ./release
for runtime in win-x86 win-x64 linux-x64 osx-x64; do
    dotnet publish Arrowgene.Ez2Off.CLI/Arrowgene.Ez2Off.CLI.csproj --runtime $runtime --configuration Release  --output ../publish/$runtime
    tar cjf ./release/$runtime.tar.gz ./publish/$runtime
done 
 