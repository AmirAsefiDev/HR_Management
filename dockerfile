# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# copy solution file
COPY *.sln .

# copy project files
COPY src/*/*.csproj ./src/
RUN dotnet restore

# copy everything else
COPY . .

# publish
RUN dotnet publish -c Release -o /app/out

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out .

# Render uses this port
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# ✅ اگر اسم DLL فرق دارد، این را تغییر بده
ENTRYPOINT ["dotnet", "HR_Management.API.dll"]
