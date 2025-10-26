# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#EXPOSE 8080
#EXPOSE 443
WORKDIR /app


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/Domain.csproj"]
COPY ["Application/Application.csproj", "Application/Application.csproj"]
COPY ["Adapters/Adapters.csproj", "Adapters/Adapters.csproj"]
COPY ["DataSource/DataSource.csproj", "DataSource/DataSource.csproj"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/WebAPI.csproj"]
RUN dotnet restore "./WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN addgroup --group userapp1 --gid 2000
RUN adduser --uid 1000 --gid 2000 --gecos "" userapp1

RUN mkdir /.dotnet
RUN chown userapp1:userapp1 /.dotnet
RUN chmod g+w,o+w /.dotnet
RUN chown userapp1:userapp1  /app

USER userapp1:userapp1

ENTRYPOINT ["dotnet", "WebAPI.dll"]