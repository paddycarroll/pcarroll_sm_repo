# Raven Database Full Backup Script
# Paddy Carroll
# V 0.1 16/01/2013

# initialise event

$evt=new-object System.Diagnostics.EventLog("Application")
$evt.Source="RavenDBBackup"
$errevent=[System.Diagnostics.EventLogEntryType]::error
$infoevent=[System.Diagnostics.EventLogEntryType]::information



if($args.count -lt 1)
{
    $evt.WriteEntry('Script requires one parameter, the backup all flag "true" or "false" ',$errevent,12)
    "Script requires one parameter, the backup all flag 'true' or 'false' "
    throw 'error, incorrect number of parameters'
}

# set up event source

if (! [System.Diagnostics.EventLog]::SourceExists("RavenDBBackup"))
{
    throw "Administrative script to create event source has not been run: CreateRavenDBBackup.ps1" 
}



# set up variables
$serverlist = 'RavenServers.json'
$backupall = ($args[0] -eq 'true') 
$port=8080
$backup_path = "\\"+$env:USERDOMAIN+"\"+"backup\database\raven\"
$raven_tools = "C:\tools\raven\"
$smuggler=$raven_tools + 'Raven.Smuggler' 
$dbs=@{}

(gc $serverlist | ConvertFrom-Json).GetEnumerator() | % { 
    $hostname =$_ 

    # check host

    if(! (test-connection -Computername $hostname -Count 1 ))
    {
       'Host ' + $hostname + ' is not currently contactable, moving to next'
       $evt.WriteEntry('Host ' + $hostname + ' is not currently contactable, moving to next',$errevent,11)
       continue
    }
    # set time

    $dtg=[System.DateTime]::UtcNow

    # get databases

    $client=new-object System.Net.WebClient
    $url='http://'+$hostname+':'+$port+'/Databases'

    # for each database
    try{
        ($client.DownloadString($url) | ConvertFrom-Json).GetEnumerator() | % {
            $db=$_
            $backup_name= $env:userdomain + '_' +$hostname + '_' + $db + '_' + $dtg.Year + '-' + $dtg.Month + '-' + $dtg.day + '_' + $dtg.Hour + '.smuggler.bak'
            $backup_media=$backup_path+$backup_name
            $smuggler_url='http://'+$hostname+':8080/Databases/'+$db 
            # do backup

            # if we have no record for db add a false one
            if (! $dbs.ContainsKey($db) )
            {
                $dbs[$db] = $false
            }
            # and if it is false or if the global backupall flag is set
            if($dbs[$db] -eq $false -or $backupall -eq $true)
                {
                # backup the database & mark the reslt in dbs true or false as appropriate
                try{
                    & $smuggler out $smuggler_url $backup_media  -incremental
                    $dbs[$db]=$true
                    $evt.WriteEntry("Raven backup of " + $db + " on " + $hostname + " has completed successfully",$infoevent,2)
                }
                catch{
        	        $type = $_.Exception.getType().FullName
	                $detail = $_.Exception.ErrorDetails().toString()
	                $evt.WriteEntry("Raven backup has generated an error:"+$type+":"+$detail,$errevent,10)
                    # we only mark the db as false if it not already true
                    $dbs[$db]=$false -or $databases[$db]              
                }
            }
        }
    } catch {
    if ($_.GetType() -eq [System.Management.Automation.MethodInvocationException])
    {
        $type = $_.Exception.getType().FullName
        $detail = '[System.Management.Automation.MethodInvocationException]'
    } else {

        $type = $_.Exception.getType().FullName
	    $detail = $_.Exception.ErrorDetails().toString()
        }
    $evt.WriteEntry("Error enumnerating databases on :"+$hostname+":"+$detail,$errevent,10)
    }
}
$dbs.GetEnumerator() | % { $evt.WriteEntry("Database backup report :"+$_.Key+":"+$_.Value,$infoevent,3)}
