# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# IMPORTANT: specify the csproj explicitly
RUN dotnet publish ./FamilyBudgetExpenseTracker.csproj -c Release -o /app/publish

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render listens on PORT; we’ll use 10000 by default
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "FamilyBudgetExpenseTracker.dll"]
