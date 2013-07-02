# Raven Database Full Backup Script
# Paddy Carroll
# V 0.1 16/01/2013




if($args.count -lt 1)
{
"Script requires one parameter, the hostname for the target platform"
throw 'error, incorrect number of parameters'
}

# set up event source

if (! [System.Diagnostics.EventLog]::SourceExists("ArchiveHousekeep"))
{
    throw "Administrative script to create event source has not been run: CreateArchiveHousekeepEventSource.ps1" 
}

# set up variables
$hostname = $args[0]
$port=8080
$backup_path = "\\"+$env:USERDOMAIN+"\"+"backup\database\raven\"
$raven_tools = "C:\tools\raven\"
$smuggler=$raven_tools + 'Raven.Smuggler' 

# check host
if(! (test-connection -Computername $hostname -Count 1 ))
{
   throw 'Host '+$hostname+' is not currently contactable'
}
# set time

$dtg=[System.DateTime]::UtcNow

# get databases

$client=new-object System.Net.WebClient
$url='http://'+$hostname+':'+$port+'/Databases'

# for each database

$client.DownloadString($url) | ConvertFrom-Json | % {
$db=$_
$backup_name= $env:userdomain + '_' +$hostname + '_' + $db + '_' + $dtg.Year + '-' + $dtg.Month + '-' + $dtg.day + '_' + $dtg.Hour + '.smuggler.bak'
$backup_media=$backup_path+$backup_name
$smuggler_url='http://'+$hostname+':8080/Databases/one' 
# do backup
& $smuggler out $smuggler_url $backup_media  -incremental

}