# Wiki

# Combining the commits
To squash the last 3 commits into one:

Method 1

git reset --soft HEAD~3

git commit -m "New message for the combined commit"

Pushing the squashed commit
If the commits have been pushed to the remote:
git push origin +name-of-branch
The plus sign forces the remote branch to accept your rewritten history, otherwise you will end up with divergent branches

If the commits have NOT yet been pushed to the remote:
git push origin name-of-branch


Method 2

git rebase -i HEAD~3

pick last comit
use squash/fixup for all other commits

Force push to origin

# SYSTIMESTAMP oracle

select systimestamp at time zone 'UTC',sys_extract_utc(systimestamp at time zone 'UTC') ,sys_extract_utc(SYSTIMESTAMP), cast (sys_extract_utc(SYSTIMESTAMP) as date)from dual;

sys_extract_utc(SYSTIMESTAMP) returns TIMESTAMP(6)

# Convert to UTC
 select sysdate,      
      (sysdate+ NUMTODSINTERVAL(-1*EXTRACT(timezone_hour FROM SYSTIMESTAMP),'hour') +  NUMTODSINTERVAL((-1*SIGN(EXTRACT(timezone_hour FROM SYSTIMESTAMP))*EXTRACT(timezone_minute FROM SYSTIMESTAMP)),'minute')) as utc ,
        SYS_EXTRACT_UTC(SYSTIMESTAMP) from (select cast(sysdate as timestamp) t, SYSTIMESTAMP from dual);
        
FUNCTION GetUtcDateTime
  RETURN DATE
  IS
  l_timezoneHour number;
  l_timezoneMinute number;
  utc date;
  BEGIN
  l_timezoneHour:=EXTRACT(timezone_hour FROM SYSTIMESTAMP);
  l_timezoneMinute:=EXTRACT(timezone_minute FROM SYSTIMESTAMP);
  select (sysdate+ NUMTODSINTERVAL(-1*l_timezoneHour,'hour') +  NUMTODSINTERVAL((-1*SIGN(l_timezoneHour)*l_timezoneMinute),'minute')) into utc from dual;
  RETURN utc;
  END GetUtcDateTime;
  
 
     
# Certificate generation

1. openssl genrsa -out rootca.key 8192

2. openssl req -sha512 -new -x509 -days 3650 -key rootca.key -out rootca.crt

3. touch certindex
echo 1000 > certserial
echo 1000 > crlnumber

4. mkdir enduser-certs

5. openssl genrsa -out enduser-certs/enduser-example.com.key 4096

6. openssl req -new -sha512 -key enduser-certs/enduser-example.com.key -out enduser-certs/enduser-example.com.csr

openssl req -new -sha512 -key enduser-certs/dev.key -out enduser-certs/dev.csr

openssl req -new -sha512 -key enduser-certs/dev.key -out enduser-certs/dev.csr


7. openssl ca -batch -config ca.conf -notext -in enduser-certs/enduser-example.com.csr -out enduser-certs/enduser-example.com.crt

openssl ca -batch -config C:\Users\tnanjundaiah\Certs\root\ca.config -notext -in enduser-certs/dev.csr -out enduser-certs/dev.crt

on config error
set OPENSSL_CONF=C:\Users\tnanjundaiah\Certs\root\ca.config
openssl ca -batch -notext -in enduser-certs/dev.csr -out enduser-certs/dev.crt

After that set
set OPENSSL_CONF=C:\OpenSSL-Win64\bin\cnf\openssl.cnf

8. Generate pfx file
cd enduser-certs
 
openssl pkcs12 -export -out dev.pfx -inkey dev.key -in dev.crt


# Microsoft style guide
  
  Link :  https://docs.microsoft.com/en-us/style-guide/welcome/
  
  https://docs.microsoft.com/en-us/style-guide/a-z-word-list-term-collections/i/invalid-not-valid
  
# Chrome srcset for image 

     https://simpl.info/srcsetwvalues/
     
# IEnumberable vs IList
Our contract should have least specific types. Consumer can cast to their usage.

https://stackoverflow.com/questions/3228708/what-should-i-use-an-ienumerable-or-ilist

# Build a .net solution using msbuild

    <SolutionToBuild Include="$(BuildProjectFolderPath)/WebSln.sln">
        <Targets></Targets>
        <Properties>OutDir=$(OutDir)\Webproj\</Properties>
    </SolutionToBuild>
    
    <SolutionToBuild Include="$(BuildProjectFolderPath)/Tools/CertificateUploader/CertificateUploader.csproj">
        <Targets></Targets>
        <Properties>OutDir=$(OutDir)\Tools\CertificateUploader\;Configuration=Release;Platform=AnyCPU</Properties>
    </SolutionToBuild>
    
    
    
    Make sure OutDir is the first argument in the preopties list.
    
    Copying file from out directory
    <Target Name="AfterCompile">
		
		<!--Delete .\artifacts\ directory if exists-->
		<Exec Command="IF EXIST $(ArtifactsRoot) (RMDIR /S /Q $(ArtifactsRoot))" />
		
		<!--Create .\artifacts\ directory-->
		<Exec Command="IF NOT EXIST $(ArtifactsRoot) (MKDIR $(ArtifactsRoot))" />
		
		<!--Copy build output to .\artifacts\ directory-->
		
		<!--Copy websites-->
		
		<Exec Command="XCOPY &quot;$(Binaries)\Release\Webproj\_PublishedWebsites\*.*&quot; &quot;$(ArtifactsRoot)\Websites\*.*&quot; /y /e"/>
			
		<Exec Command="XCOPY &quot;$(Binaries)\Release\Tools\CertificateUploader\*.*&quot; &quot;$(ArtifactsRoot)\Tools\CertificateUploader\*.*&quot; /y /e"/>
		
		
	</Target>
	
	
# Embed all dll into single exe
      Costura is an add-in for Fody
      Embeds dependencies as resources.
     https://github.com/Fody/Costura
 
# Looping a cursor
	DECLARE
	   c1   SYS_REFCURSOR;
	   l    license%rowtype;
	BEGIN
	   OPEN c1 FOR SELECT  * FROM  license;
	   LOOP
	       FETCH c1 INTO l;
	       EXIT WHEN c1%notfound;
	       dbms_output.put_line(l.gemidentifier);
	   END LOOP;
	END;
    
# Execute a store proc
  variable x refcursor;
     variable  o_display_msg VARCHAR2;
    variable o_return_code  NUMBER;
    exec  PKG_API.GetGemAccount_ByExtAccountCode(    4,    'vlt'      ,	null			,    :x                   ,    :o_display_msg              ,   :o_return_code            );
print x;
