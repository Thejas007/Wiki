
# Publishing SqlServer DataBase Project

- Goto database project folder
  cd C:\Db1
- Run below command
  msbuild /t:Build;Publish /p:SqlPublishProfilePath=Db1.publish.xml Db1.sqlproj
  
  Db1.publish.xml file content
 
