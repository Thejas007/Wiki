# Generic host
- https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host
- https://github.com/dotnet/runtime/blob/main/src/libraries/Microsoft.Extensions.Hosting/docs/HostShutdown.md
- https://medium.com/@rainer_8955/gracefully-shutdown-c-apps-2e9711215f6d
- https://github.com/dotnet/runtime/issues/59014
 
# We need to configure stop_grace_period :
- https://github.com/compose-spec/compose-spec/blob/master/spec.md#stop_grace_period
- https://aws.amazon.com/blogs/containers/graceful-shutdowns-with-ecs/

# We need to address operation timeout issue with workflowcore package
- https://github.com/danielgerlag/workflow-core/issues/953

# container
- https://docs.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows
- https://www.mankier.com/1/dotnet-publish
- https://gist.github.com/hiroaki-yamamoto/4e27167dd3cb74700ce86e678ff987cc
- https://youtrack.jetbrains.com/issue/JT-61434
- https://www.alibabacloud.com/blog/practical-exercises-for-docker-compose-part-2_594414

Publish
```
dotnet publish "Worker1.csproj" -p:Platform=x64 --configuration Release
```
Dockerfile1
```
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
RUN mkdir app
COPY bin/Release/net5.0/publish/* /app/
ENTRYPOINT ["dotnet", "Worker1.dll"]

LABEL app_name="Worker service"
```
docker-compose.yml
```
version: '3.4'

services:
  worker1:
    image: worker1
    build:
      context: .
      dockerfile: Dockerfile1
    container_name: "worker1"
    stop_grace_period: 30s
    network_mode: bridge
    environment:
      - DOTNET_ENVIRONMENT=Development      - 
      - RabbitMqConnection:HostName=host.docker.internal
```
