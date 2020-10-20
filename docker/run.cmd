docker run -v c:\Projects\Sashimi.Terraform:c:/build -v c:\temp:c:/artifacts -v %USERPROFILE%\.nuget:C:\Users\ContainerAdministrator\.nuget -e TEAMCITY_VERSION=1.2.3 -e Git_Branch=main latest

docker run -v c:\Projects\Sashimi.Terraform:c:/build -v c:\temp:c:/artifacts -v %USERPROFILE%\.nuget:C:\Users\ContainerAdministrator\.nuget -it --entrypoint cmd.exe latest