USE master;
GO

BACKUP DATABASE $(db_name) 
    TO DISK = '$(backup_path)\$(db_name)_database.bak' 
    WITH FORMAT
GO
 

BACKUP LOG $(db_name) 
    TO DISK = '$(backup_path)\$(db_name)_logs.bak' 
GO
 

