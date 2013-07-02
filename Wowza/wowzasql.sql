#!/bin/bash
export _NAME=$1
export NAME=\"`echo ${_NAME} | sed -e "s/_/ /g"`\"

sudo -u postgres psql -d deluxe -c "insert into streams (id,name,urlflash, urlhls,aspect) values (nextval('streams_seq'),'${NAME}','${_NAME}.stream',${_NAME}.smil','16:9');"
export stream_id=`sudo -u postgres psql -s -d deluxe -c "select id from streams where name= '${NAME}';"` 
export org_id=`sudo -u postgres psql -s -d deluxe -c "select id from organsiations where name= '${ORG}';"`
sudo -u postgres psql -d deluxe -c "insert into organisationandstream (stream_id,organsiation_id) values (${stream_id},${organisation_id});"
sudo -u postgres psql -d deluxe -c "insert into organisationandstream (stream_id,organsiation_id) values (${stream_id},1);"
