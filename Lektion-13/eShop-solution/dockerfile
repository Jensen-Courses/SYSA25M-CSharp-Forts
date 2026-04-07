# Hämta hela dotnet ramverket för kompilering
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Skapar en arbetskatalog för bygg och release
WORKDIR /app

# Kopiera csproj filen till app katalogen
COPY *.csproj ./

# Hämta in alla beroenden...
RUN dotnet restore

# Kopiera alla övriga filer som vi behöver...
COPY . .

# Kör publish kommandot
RUN dotnet publish -c Release -o Release

# Hämta en ny image för dotnet med endast runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# Skapa en ny arbetskatalog
WORKDIR /app

COPY --from=build /app/Release .

EXPOSE 8080

ENTRYPOINT [ "dotnet", "eShop.dll" ]