FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-image
WORKDIR /home/app
COPY ["./*.sln", "./"]
COPY ["./*/*.csproj", "./"]
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file \
./${file%.*}/; done
RUN dotnet restore
COPY . .
RUN dotnet publish Offices.WebApi/Offices.WebApi.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /publish
COPY --from=build-image /publish .
ENV ASPNETCORE_URLS=http://+:7000
ENTRYPOINT ["dotnet", "Offices.WebApi.dll"]	