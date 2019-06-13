@echo off
echo Create tables and stored procedures in Detention_facility database? Y/N
set /p v=
if %v% == Y goto yes
if %v% == N goto end
:yes
sqlcmd -S %name%\%instance% -i %cd%\Tables.sql
sqlcmd -S %name%\%instance% -i %cd%\Stored_procedures.sql
pause 
exit
:end
exit