@echo off
echo Starting API Versioning Demo...
echo.
cd ApiVersioningDemo
echo Restoring packages...
dotnet restore
echo.
echo Building project...
dotnet build
echo.
echo Starting application...
echo Navigate to: https://localhost:7000/swagger
echo.
dotnet run