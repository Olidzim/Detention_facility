@echo off
echo Enter server name
set /p name=
echo Enter server instance
set /p instance=
echo Create Detention_facility database in %name%/%instance% data source? Y/N
set /p v=
if %v% == Y goto yes
if %v% == N goto end
:yes
sqlcmd -S %name%\%instance% -i %cd%\Create_database.sql
sqlcmd -S %name%\%instance% -i %cd%\Tables.sql
sqlcmd -S %name%\%instance% -i %cd%\Stored_procedures.sql
pause 
exit
:end
exit