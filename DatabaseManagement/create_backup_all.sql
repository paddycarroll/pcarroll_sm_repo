/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.2, 08/06/2012                           */
/* Creates the backup_all procedure in master        */
/* Dependencies: master.dbo.backup_paths, populated  */
/* Errors 50001 and 50002 created (create_errors.sql)*/
/* V 0.1 Initial Version 03/06/2012                  */
/* V 0.2 fix double slash 08/06/2012                 */
/*****************************************************/
USE [master]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[backup_all](@db varchar(100))
as
begin
declare @bad_message varchar(100)
declare @good_message varchar(100)
declare @root_filename varchar(150)
declare @db_filename varchar(150)
declare @log_filename varchar(150)
declare @dtg varchar(30)
declare @state varchar(20)

select @dtg = replace(replace(convert(varchar,GETUTCDATE(),20),' ','_'),':','-');
select @bad_message='No backup of ' + @db + ' database is currently in mirror mode'
select @root_filename =  rtrim(path) + char(92) + dbo.getDomainName()+'_'+dbo.getPlatformName()+'_'+@db +'_'+@dtg from master.dbo.backup_paths where db = @db;
select @log_filename = @root_filename + '_log.bak';
select @db_filename = @root_filename + '_db.bak';
select @state=mirroring_role_desc from sys.database_mirroring where database_id in ( select database_id from sys.databases where name = @db )
if 'MIRROR'=@state
begin
	raiserror (50001 , 1,1,@root_filename) 
end
else
begin try
	backup log @db to disk = @log_filename 
	backup database @db to disk = @db_filename
end try
begin catch
	raiserror (50002 , 16,10,@root_filename) 
end catch
end


