
$msbuild = 'E:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\bin\MSBuild.exe'
$nuget = 'D:\AlexART\docker\nuget.exe'

$solution = "TodoListAutomation.sln"

&$nuget "restore" $solution

&$msbuild $solution