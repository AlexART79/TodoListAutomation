# test runner from packages
$nunit_console_path = "packages\NUnit.ConsoleRunner.3.11.1\tools\nunit3-console.exe"

# test assemblies (UI, DB and REST)
$ui_test_assembly = ".\bin\Debug\TodoListAutomation.dll" 
$db_test_assemply = ".\bin\Debug\TodoListDbAutomation.dll" 
$rest_test_assembly = ".\bin\Debug\TodoListRestAutomation.dll"

# tests condition
$where = ''
$cat = ''

if ($cat -ne '') {
    $where = "--where:cat="+$cat
}

$opts = "--result:TestResult.xml;format=nunit3"

# execute tests
&$nunit_console_path $ui_test_assembly $db_test_assemply $rest_test_assembly $where $opts