rm -f *.sln
echo "[Build]" && dotnet build && echo "[Run]" && `find bin -name "*.exe"`
