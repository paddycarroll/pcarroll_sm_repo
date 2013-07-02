#!/bin/bash
export psql=/Library/PostgreSQL/9.1/bin/psql
export _NAME=$1
export ORG=$2
export NAME=`echo ${_NAME} | sed -e "s/_/ /g"`

sudo -u postgres $psql -d deluxe -c "insert into streams (id,name,urlflash, urlhls,aspect) values (nextval('streams_id_seq'),'${NAME}','${_NAME}.stream','${_NAME}.smil','16:9');"
export stream_id=`sudo -u postgres $psql -q -t -d deluxe -c "select id from streams where name = '${NAME}';"` 
export org_id=`sudo -u postgres $psql -q -t -d deluxe -c "select id from organisations where name = '${ORG}';"`
echo $stream_id $org_id
sudo -u postgres $psql -d deluxe -c "insert into organisationandstream (stream_id,organisation_id) values (${stream_id},${org_id});"
sudo -u postgres $psql -d deluxe -c "insert into organisationandstream (stream_id,organisation_id) values (${stream_id},1);"
