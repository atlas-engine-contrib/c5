dotnet test .\AtlasEngine.Modelling.C5.Core.Tests\AtlasEngine.Modelling.C5.Core.Tests.csproj
dotnet test .\AtlasEngine.Modelling.C5.Client.Tests\AtlasEngine.Modelling.C5.Client.Tests.csproj

dotnet msbuild "/t:rebuild;pack" /p:Version=0.9.6 /p:Configuration=Debug .\AtlasEngine.Modelling.C5.Core\AtlasEngine.Modelling.C5.Core.csproj
dotnet msbuild "/t:rebuild;pack" /p:Version=0.9.6 /p:Configuration=Debug .\AtlasEngine.Modelling.C5.Client\AtlasEngine.Modelling.C5.Client.csproj