/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.1, 03/06/2012                           */
/* Creates the backup_paths table in master          */
/* Dependencies: populated master.dbo.backup_paths   */
/* N.B. This will TRASH THE OLD TABLE IF IT EXISTS   */
/* V 0.1 Initial Version 03/06/2012                  */
/*****************************************************/
declare @db varchar(50)
declare @sched_name varchar(50)
declare @start_time int
declare @min_past int
declare @admin varchar(50)
declare iterator cursor for select db,min_past from master.dbo.backup_paths;
open iterator
fetch next from iterator into @db,@min_past
while @@FETCH_STATUS = 0
begin
	select @db
	select @sched_name = @db + '_backup_sched'
	select @admin = master.dbo.getDomainName() + '\sql'
	select @admin
	select @start_time = @min_past *100
	if exists (select schedule_id from msdb.dbo.sysschedules where name = @sched_name)
	begin
		exec msdb.dbo.sp_delete_schedule @schedule_name=@sched_name;
	end
	exec msdb.dbo.sp_add_schedule @schedule_name=@sched_name
	,  @enabled = 1
	,  @freq_type = 4
	,  @freq_interval = 1
	,  @freq_subday_type = 8 
	,  @freq_subday_interval = 1 
	,  @active_start_date = 20120101 
	,  @active_start_time = @start_time
	,  @owner_login_name =  @admin;
fetch next from iterator into @db,@min_past
end
close iterator;
deallocate iterator