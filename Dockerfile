FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80

ENV LANG pt_BR.UTF-8
ENV TZ=America/Sao_Paulo

CMD ["dotnet", "VoteAPI.API.dll"]