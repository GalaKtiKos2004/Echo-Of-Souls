$code = @'
param(
    [string]$Platform = "EditMode"
)

$unity = "C:\Program Files\Unity\Hub\Editor\6000.3.21f1\Editor\Unity.exe"
$project = "C:\Users\KKiri\Documents\Github\Echo-Of-Souls"
$results = Join-Path $project "results.xml"
$log = Join-Path $project "test.log"

if (Test-Path $results) { Remove-Item $results }

if (-not (Test-Path $unity)) {
    Write-Host "Unity.exe not found at: $unity" -ForegroundColor Red
    exit 1
}

$args = @(
    "-batchmode",
    "-projectPath", $project,
    "-runTests",
    "-testPlatform", $Platform,
    "-testResults", $results,
    "-logFile", $log
)

$proc = Start-Process -FilePath $unity -ArgumentList $args -Wait -PassThru
$exitCode = $proc.ExitCode

Write-Host ""
Write-Host "Exit code: $exitCode"

if (Test-Path $results) {
    [xml]$r = Get-Content $results
    $run = $r.'test-run'
    Write-Host "Total: $($run.total)  Passed: $($run.passed)  Failed: $($run.failed)"
    if ([int]$run.failed -gt 0) {
        Write-Host ""
        Write-Host "FAILED TESTS:" -ForegroundColor Red
        $r.SelectNodes("//test-case[@result='Failed']") | ForEach-Object {
            Write-Host ("  - " + $_.fullname) -ForegroundColor Red
            Write-Host ("    " + $_.failure.message.'#cdata-section') -ForegroundColor DarkRed
        }
    }
} else {
    Write-Host "results.xml not created, check test.log" -ForegroundColor Red
}
'@

Set-Content -Path .\run-tests.ps1 -Value $code -Encoding UTF8