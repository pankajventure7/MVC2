#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["WebApplication9/WebApplication9.csproj", "WebApplication9/"]
RUN dotnet restore "WebApplication9/WebApplication9.csproj"
COPY . .
WORKDIR "/src/WebApplication9"
RUN dotnet build "WebApplication9.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication9.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication9.dll"]