echo "[Push]"
echo -n "ApiKey: "
read ApiKey

dotnet nupkg push `find bin/Release -name "*.nupkg"` --api-key ${ApiKey} --source https://api.nuget.org/v3/index.json
