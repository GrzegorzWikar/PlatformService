# Etap 1: Buduj aplikację
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Skopiuj i przywróć pliki .csproj
COPY *.csproj ./
RUN dotnet restore

# Skopiuj resztę kodu źródłowego i opublikuj aplikację
COPY . ./
RUN dotnet publish -c Release -o out

# Etap 2: Utwórz ostateczny obraz
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Skopiuj opublikowaną aplikację z pierwszego etapu
COPY --from=build-env /app/out .

# Ustaw punkt wejścia dla kontenera
ENTRYPOINT ["dotnet", "PlatformService.dll"]
