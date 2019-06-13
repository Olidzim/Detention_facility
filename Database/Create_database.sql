IF EXISTS(SELECT * FROM master.sys.databases 
          WHERE name='Detention_facility')
BEGIN
PRINT 'Database "Detention_facility" already exists'
END
ELSE
BEGIN
CREATE DATABASE Detention_facility
PRINT 'Database "Detention_facility" created'
END
