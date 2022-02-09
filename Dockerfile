FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 5001
EXPOSE 5000
ENV ASPNETCORE_URLS="http://0.0.0.0:5000;https://0.0.0.0:5001"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="rafael88"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx 
ENTRYPOINT ["dotnet", "cryptoibero_cyptoprice_api.dll"]