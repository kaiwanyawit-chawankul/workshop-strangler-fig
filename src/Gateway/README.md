# terminal A (monolith)
DOTNET_URLS=http://localhost:5070 dotnet run
# terminal B (gateway)
cd ../Gateway
DOTNET_URLS=http://localhost:5080 dotnet watch run
# terminal C (tests) — change BaseAddress to :5080 then
cd ../../tests/AcceptanceTests
dotnet test