FROM docker.io/microsoft/dotnet35:windowsservercore
MAINTAINER Gabriel Schenker
ENV sqlinstance SQL
ENV sqlsapassword Los#Techies-1
ENV sql c:\\sql
ENV sqldata c:\\sql\\data
ENV sqlbackup c:\\sql\\backup
COPY . /install
WORKDIR /install
RUN powershell wget http://download.microsoft.com/download/E/A/E/EAE6F7FC-767A-4038-A954-49B8B05D04EB/Express%2064BIT/SQLEXPR_x64_ENU.exe -OutFile sqlexpr_x64_enu.exe
RUN /install/sqlexpr_x64_enu.exe /q /x:/install/setup
RUN /install/setup/setup.exe /q \
      /ACTION=Install \
      /INSTANCENAME=%sqlinstance% \
      /FEATURES=SQLEngine /UPDATEENABLED=1 \
      /SECURITYMODE=SQL /SAPWD=%sqlsapassword% \
      /SQLSVCACCOUNT="NT AUTHORITY\System" \
      /SQLSYSADMINACCOUNTS="BUILTIN\ADMINISTRATORS" \
      /INSTALLSQLDATADIR=%sqldata% \
      /SQLUSERDBLOGDIR=%sqldata% \
      /SQLBACKUPDIR=%sqlbackup% \
      /TCPENABLED=1 \
      /NPENABLED=0 \
      /IACCEPTSQLSERVERLICENSETERMS
RUN powershell ./Set-SqlExpressStaticTcpPort %sqlinstance%
RUN powershell ./Move-dirs-and-stop-service %sqlinstance% %sql% %sqldata% %sqlbackup%
RUN del sqlexpr_x64_enu.exe
RUN rmdir .\setup /s /q
CMD powershell ./start detached %sqlinstance% %sqldata% %sqlbackup%
