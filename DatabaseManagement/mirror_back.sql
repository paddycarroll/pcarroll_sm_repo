/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.2, 02/07/2013                           */
/* makes a database backup to construct mirror       */
/* Dependencies: pre-existant backup                 */
/* N.B.                                              */
/* V 0.1 Initial Version 03/07/2012                  */
/* V 0.2 add instructions and commit                 */
/* Invoke from sqlcmd with -v backup_path="\\some\Path" -v db_name="some_name" /*
/*****************************************************/
USE master;
GO

BACKUP DATABASE $(db_name) 
    TO DISK = '$(backup_path)\$(db_name)_database.bak' 
    WITH FORMAT
GO
 

BACKUP LOG $(db_name) 
    TO DISK = '$(backup_path)\$(db_name)_logs.bak' 
GO
 

