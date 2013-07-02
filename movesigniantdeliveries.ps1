# MoveSigniantDeliveries.ps1
# File move of Signiant Deliveries from Europe to Stage for ABC
# executed on windows schedule  
# 
# version 0.2
# Paddy Carroll
# version 0.1 initial 03/08/2012
# version 0.2 file name checks 04/08/2012

# check for arguments

if($args.count -lt 3)
{
"Script requires three parameters, an input path, and an output path and an output share name"
throw 'error'
}

# set the file name test function

function FileRule ($name)
{
	switch -regex ($name)
	{
		"(?i)^#.*.MXF$" {$false; break}
		"(?i)^.*.MXF$" {$true; break}
		default {$false}
	}
}

# set up variables

$input_path = $args[0]
$output_path = $args[1]
$output_share_name = $args[2]
$copy_user = "copy"
$copy_password = "Xfer123"

# Set up the share


net use $output_share_name  $copy_password /user:stage\$copy_user

if (!($?)) {
"attemt to establish output_share failed!"
throw 'error'
}


# check paths
if (!(test-path $input_path)) {
	"Input Path unavailable"
	throw 'error'
}

if (!(test-path $output_path)) {
	"Output Path unavailable"
	throw 'error'
}

# set up the log file

$logfilename = $output_path+"\signiant_copy_" + (get-date -format dd-MMM-yyyy-hh-mm-ss) + ".log"


# go through the files
"starting" >> $logfilename
gci $input_path | where-object {!$_.PsIsContainer} | foreach {
# check the name match is ok
   if ( FileRule $_.name )
   {
      try{
         $file = $_.FullName
         echo moving $file to $output_path >> $logfilename
         mv $file "${output_path}" 
         }catch{
            $type = $_.Exception.getType().FullName
            $detail = $_.Exception.ErrorDetails().toString()
            $file + " fails to move to " + $output_path >>  $logfilename
            $type >> $logfilename
            $detail >> $logfilename
         }
      }else{
         echo skipping $_.name >> $logfilename
   }
}

"finished" >> $logfilename

net use $output_share_name  /d 





 