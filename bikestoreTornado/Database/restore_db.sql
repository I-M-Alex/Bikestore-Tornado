RESTORE DATABASE bikestore  
FROM DISK = 'C:\Bikestore_Backups\bikestore.bak'  
WITH REPLACE, MOVE 'bikestore' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\bikestore.mdf',  
MOVE 'bikestore_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\bikestore.ldf'
