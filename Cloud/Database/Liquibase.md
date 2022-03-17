# create ssh tunnel
 ssh -i "C:\bastionhost.pem" -f -N -L 3307:databasecluster-k2iagm4i0po2.cluster-cyfi9mx1urk0.us-east-1.rds.amazonaws.com:3306 ec2-user@1.237.189.153 -v
Alternatively use putty to open tunnel.

# update liquibase.properties
driver=com.mysql.cj.jdbc.Driver
liquibase.hub.mode=off
classpath=./lib/mysql-connector-java-8.0.27.jar
changeLogFile=db-changelog-master.xml
url=jdbc:mysql://127.0.0.1:3307/schema_name?autoReconnect=true&failOverReadOnly=false&maxReconnects=10&useSSL=false

# command to update
liquibase --username=user --password=password123 update
