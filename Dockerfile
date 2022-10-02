FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine3.14 AS base
WORKDIR /api

RUN adduser --disabled-password \
  --home /api \
  --gecos '' dotnetuser  \
    && chown -R dotnetuser /api

USER dotnetuser

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.14 as publish
WORKDIR /src

COPY src/API .

RUN dotnet publish "API.csproj"  \
    -f net6.0 \
    -c Release -o /out  \
    -r alpine-x64  \
    --sc true  \
    /p:PublishTrimmed=true  \
    /p:TrimMode=Link  \
    /p:PublishSingleFile=true


FROM base AS final
WORKDIR /api
COPY --from=publish /out .
ENTRYPOINT [ "./API" ]