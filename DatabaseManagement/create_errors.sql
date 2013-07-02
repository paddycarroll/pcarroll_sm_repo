/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.1, 03/06/2012                           */
/* Creates the errors associated with DMC stuff      */
/* Dependencies: none                                */
/* V 0.1 Initial Version 03/06/2012                  */
/*****************************************************/
use master
go
exec sp_addmessage @msgnum=50001,@severity=1,@msgtext=N'Backup %s supressed for mirror database.', @with_log='true', @replace='replace', @lang='us_english'
exec sp_addmessage @msgnum=50002,@severity=16,@msgtext=N'Backup %s has failed. examine sql server logs for cause',@with_log='true', @replace='replace', @lang='us_english'
