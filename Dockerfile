#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
EXPOSE 82
EXPOSE 443

# COPY *.sln .
# COPY ["./src/Equinox.Domain.Core/Equinox.Domain.Core.csproj", "src/Equinox.Domain.Core/"]
# COPY ["./src/Equinox.Domain/Equinox.Domain.csproj", "src/Equinox.Domain/"]
# COPY ["./src/Equinox.Infra.Data/Equinox.Infra.Data.csproj", "src/Equinox.Infra.Data/"]
# COPY ["./src/Equinox.Infra.CrossCutting.Bus/Equinox.Infra.CrossCutting.Bus.csproj", "src/Equinox.Infra.CrossCutting.Bus/"]
# COPY ["./src/Equinox.Infra.CrossCutting.Identity/Equinox.Infra.CrossCutting.Identity.csproj", "src/Equinox.Infra.CrossCutting.Identity/"]
# COPY ["./src/Equinox.Infra.CrossCutting.IoC/Equinox.Infra.CrossCutting.IoC.csproj", "src/Equinox.Infra.CrossCutting.IoC/"]
# COPY ["./src/Equinox.Application/Equinox.Application.csproj", "src/Equinox.Application/"]
COPY ["./src/Equinox.Services.Api/Equinox.Services.Api.csproj", "src/Equinox.Services.Api/"]
# COPY ["./src/Equinox.UI.Web/Equinox.UI.Web.csproj", "src/Equinox.UI.Web/"]

# run restore over API project - this pulls restore over the dependent projects as well
# RUN dotnet restore "./src/Equinox.Services.Api"
RUN dotnet restore "./src/Equinox.Services.Api/Equinox.Services.Api.csproj"

COPY . .

# RUN ls -la
# RUN pwd

RUN dotnet build -c Release -o /src/build

# run publish over the API project

# FROM build AS publish

# WORKDIR /app
# COPY --from=build ./src/build ./app/build
RUN dotnet publish -c Release -o /src/publish

# FROM base AS runtime

# WORKDIR /app

# COPY --from=publish /app/publish .

# ENTRYPOINT ["dotnet", "Equinox.Services.Api.dll"]