/*****************************************************/
/* Paddy Carroll                                     */
/* delete_all_job_sched.sql                          */
/* Version 0.1, 03/06/2012                           */
/* trashes dmc backup jobs and schedules             */
/* Dependencies: None                                */
/*                                                   */
/* V 0.1 Initial Version 07/06/2012                  */
/*****************************************************/
use master
go
declare @db varchar(50)
declare @jid uniqueidentifier
declare @scid int
declare dbs cursor for select db from backup_paths
open dbs
fetch next from dbs into @db
while @@FETCH_STATUS = 0
begin
	select @db
	if exists (select job_id from msdb.dbo.sysjobs where name = rtrim(@db)+'_database_backup')
	begin 
		select 'delete job' act,job_id,name from msdb.dbo.sysjobs where name = rtrim(@db)+'_database_backup'
		select @jid=job_id from msdb.dbo.sysjobs where name = rtrim(@db)+'_database_backup'
		exec msdb.dbo.sp_delete_job @job_id = @jid
	end
	if exists (select schedule_id from msdb.dbo.sysschedules where name = @db)
	begin
		select 'delete schedule' act,schedule_id,name from msdb.dbo.sysschedules where name = @db
		select @scid=schedule_id from msdb.dbo.sysschedules where name = @db
		exec msdb.dbo.sp_delete_schedule @schedule_id=@scid
	end
	fetch next from dbs into @db
end
close dbs
deallocate dbs