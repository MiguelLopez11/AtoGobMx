FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR digital.atogobmx

EXPOSE 80
EXPOSE 5001
#COPY FILES PROJECT
COPY ./*.csproj ./
RUN dotnet restore
#COPY EVERYTHING ELSE
COPY . .
RUN dotnet publish -c Release -o out

#BUILD IMAGE
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /digital.atogobmx
COPY --from=build /digital.atogobmx/out .
ENTRYPOINT ["dotnet", "AtoGobMx.dll"]