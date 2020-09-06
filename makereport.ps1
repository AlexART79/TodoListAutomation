
$cmd = "allure.bat"
$results = ".\allure-results"
$report = ".\allure-report"

&$cmd "generate" "--clean" $results "-o" $report