
# Publishing SqlServer DataBase Project

- Goto database project folder
  cd C:\Db1
- Run below command
  msbuild /t:Build;Publish /p:SqlPublishProfilePath=Db1.publish.xml Db1.sqlproj
  
  Db1.publish.xml file content
  ```
  <?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetDatabaseName>Db1</TargetDatabaseName>
    <DeployScriptFileName>Db1.sql</DeployScriptFileName>
    <DeployDatabaseInSingleUserMode>False</DeployDatabaseInSingleUserMode>
    <TargetConnectionString>Data Source=localhost;Integrated Security=True;Pooling=False;Connect Timeout=300</TargetConnectionString>
    <ScriptDatabaseCollation>True</ScriptDatabaseCollation>
    <ScriptDatabaseCompatibility>True</ScriptDatabaseCompatibility>
    <ScriptDatabaseOptions>False</ScriptDatabaseOptions>
    <GenerateSmartDefaults>True</GenerateSmartDefaults>
    <IgnorePartitionSchemes>True</IgnorePartitionSchemes>
    <BlockOnPossibleDataLoss>False</BlockOnPossibleDataLoss>
    <DropExtendedPropertiesNotInSource>False</DropExtendedPropertiesNotInSource>
    <DropIndexesNotInSource>False</DropIndexesNotInSource>
    <VerifyDeployment>False</VerifyDeployment>
    <ProfileVersionNumber>1</ProfileVersionNumber>
    <ScriptNewConstraintValidation>False</ScriptNewConstraintValidation>
    <DoNotAlterChangeDataCaptureObjects>False</DoNotAlterChangeDataCaptureObjects>
    <DoNotAlterReplicatedObjects>False</DoNotAlterReplicatedObjects>
  </PropertyGroup>
  <ItemGroup>
    <SqlCmdVariable Include="IncludeSaaSRoles">
      <Value>N</Value>
    </SqlCmdVariable>
    <SqlCmdVariable Include="InstallType">
      <Value>F</Value>
    </SqlCmdVariable>
    <SqlCmdVariable Include="IsReleaseBuild">
      <Value>N</Value>
    </SqlCmdVariable>
    <SqlCmdVariable Include="Partitioned">
      <Value>N</Value>
    </SqlCmdVariable>
    <SqlCmdVariable Include="SQLAlwaysOn">
      <Value>N</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
  
  ```
