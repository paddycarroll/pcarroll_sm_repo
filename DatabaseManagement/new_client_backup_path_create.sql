/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.1, 02/07/2012                           */
/* new_client_backup_path_create.sql                 */
/* Creates the entry in backup_paths table in master */
/* Dependencies: master.dbo.backup_paths exists      */
/* And grants permissions to CinegyUsers on the db   */
/* one argument, the database name -v database=""    */
/* V 0.1 Initial Version 02/07/2012                  */
/*****************************************************/
USE [master]
GO


/******* Populate table *************/

declare @counter int
declare @db varchar(50)
declare @db_path varchar(100)
declare @path_string varchar(100)
select @db = $(database)
select @counter = max(min_past) from backup_paths;
select @db_path = '\\' + rtrim(master.dbo.getDomainName()) + '\backups\database\hourly'
select @counter = @counter + 5
insert into master.dbo.backup_paths (db,path,min_past) values (@db,@db_path,@counter)

use @db
go
declare @cuser varchar(50)
select @cuser = rtrim(master.dbo.getDomainName()) +'\CinegyAdmins'

CREATE USER [@cuser] FOR LOGIN [@cuser]
GO

exec sp_addrolemember  @rolename=db_datareader ,@membername=[@cuser] ;

exec sp_addrolemember  @rolename=db_datawriter ,@membername=[@cuser] ;