# Wiki

 
     
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
 
