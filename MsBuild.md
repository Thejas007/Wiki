 # Directory.Build.props 
  -Directory.Build.props and Directory.Build.targets
  
 Prior to MSBuild version 15, if you wanted to provide a new, custom property to projects in your solution, you had to manually add a reference to that property to every project file in the solution. Or, you had to define the property in a .props file and then explicitly import the .props file in every project in the solution, among other things.

However, now you can add a new property to every project in one step by defining it in a single file called Directory.Build.props in the root folder that contains your source. When MSBuild runs, Microsoft.Common.props searches your directory structure for the Directory.Build.props file (and Microsoft.Common.targets looks for Directory.Build.targets). If it finds one, it imports the property. Directory.Build.props is a user-defined file that provides customizations to projects under a directory.

https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019


Sample: 
`
<Project>
  <PropertyGroup>
    <!-- Create a property pointing to the root of the repository; ends with backslash. -->
    <RepositoryRootPath>$(MSBuildThisFileDirectory)</RepositoryRootPath>
    <!-- Define code analysis rules for all projects in the repository. -->
    <CodeAnalysisRuleSet>$(RepositoryRootPath)Shared\MinimumRequired.ruleset</CodeAnalysisRuleSet>
	<StyleCopTreatErrorsAsWarnings>true</StyleCopTreatErrorsAsWarnings>
  </PropertyGroup>
  <ItemGroup>
    <AdditionalFiles Include="$(RepositoryRootPath)Shared\StyleCop.json" />
  </ItemGroup>
</Project>
`
