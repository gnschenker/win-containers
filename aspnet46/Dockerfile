FROM windowsservercore:10.0.14300.1000
MAINTAINER Gabriel Schenker
RUN powershell -Command Add-WindowsFeature Web-Server
#install Chocolatey
RUN @powershell -NoProfile -ExecutionPolicy unrestricted -Command "(iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))) >$null 2>&1" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
WORKDIR /windows/system32
#install ASP.NET 4.6 (though it says "net45" it is ASP.NET 4.6)
#install tools and targets to allow build (via msbuild) without VS installed
RUN powershell add-windowsfeature web-asp-net45 \
  && choco install microsoft-build-tools -y \
  && choco install dotnet4.6-targetpack -y \
  && choco install nuget.commandline -y \
  && nuget install MSBuild.Microsoft.VisualStudio.Web.targets
