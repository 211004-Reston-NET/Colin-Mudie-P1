# We require .Net SDK 5.0 for out app to run.
from mcr.microsoft.com/dotnet/sdk:5.0 as build

#Seeting our working Directory 
workdir /app

# Time to copy our projects and paste it to the working directory
# * is the wildcard meaning it'll grab anything in your folder that has .sln extension
copy *.sln ./
copy Business-Logic/*.csproj Business-Logic/
copy Data-Access-Logic/*.csproj Data-Access-Logic/
copy MMTest/*.csproj MMTest/
copy Models/*.csproj Models/
copy WebUI/*.csproj WebUI/

# We need to build/restore our files (bin & obj)
run cd WebUI && dotnet restore

# copy the source files
copy . ./

## just to check that everything was copied over correctly
#cmd /bin/bash

# WE need to publish the application and its dependencies to a folder for deployment.
run dotnet publish WebUI -c Release -o publish --no-restore

from mcr.microsoft.com/dotnet/aspnet:5.0 as runetime

workdir /app
copy --from=build /app/publish ./

cmd ["dotnet", "WebUI.dll"]


