param([string]$Folder = 'competitive-check')
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Runtime.WindowsRuntime
$taskMethod = [System.WindowsRuntimeSystemExtensions].GetMethods() | Where-Object {
    $_.Name -eq 'AsTask' -and $_.IsGenericMethodDefinition -and $_.GetParameters().Count -eq 1 -and $_.GetParameters()[0].ParameterType.Name -eq 'IAsyncOperation`1'
} | Select-Object -First 1
$actionMethod = [System.WindowsRuntimeSystemExtensions].GetMethods() | Where-Object {
    $_.Name -eq 'AsTask' -and -not $_.IsGenericMethod -and $_.GetParameters().Count -eq 1 -and $_.GetParameters()[0].ParameterType.Name -eq 'IAsyncAction'
} | Select-Object -First 1
function Wait-WinRt($operation, [Type]$resultType) {
    $task = $taskMethod.MakeGenericMethod($resultType).Invoke($null, @($operation))
    $task.Wait()
    return $task.Result
}
$fileType = [Windows.Storage.StorageFile,Windows.Storage,ContentType=WindowsRuntime]
$pdfType = [Windows.Data.Pdf.PdfDocument,Windows.Data.Pdf,ContentType=WindowsRuntime]
$outDir = Join-Path $PSScriptRoot ('rpt_reports/' + $Folder)
Get-ChildItem -LiteralPath $outDir -Filter '*.pdf' -File | ForEach-Object {
    $pdfFile = Wait-WinRt ($fileType::GetFileFromPathAsync($_.FullName)) $fileType
    $pdfDocument = Wait-WinRt ($pdfType::LoadFromFileAsync($pdfFile)) $pdfType
    Write-Output ($_.Name + ': ' + $pdfDocument.PageCount + ' pages, ' + $pdfDocument.GetPage(0).Size)
    for ($pageIndex = 0; $pageIndex -lt $pdfDocument.PageCount; $pageIndex++) {
        $page = $pdfDocument.GetPage($pageIndex)
        $stream = New-Object Windows.Storage.Streams.InMemoryRandomAccessStream
        $options = New-Object Windows.Data.Pdf.PdfPageRenderOptions
        $options.DestinationWidth = 1300
        $actionMethod.Invoke($null, @($page.RenderToStreamAsync($stream, $options))).Wait()
        $reader = New-Object Windows.Storage.Streams.DataReader($stream.GetInputStreamAt(0))
        Wait-WinRt ($reader.LoadAsync([uint32]$stream.Size)) ([uint32]) | Out-Null
        $bytes = New-Object byte[] ([int]$stream.Size)
        $reader.ReadBytes($bytes)
        $dest = Join-Path $outDir ($_.BaseName + '-page-' + ($pageIndex + 1) + '.png')
        [IO.File]::WriteAllBytes($dest, $bytes)
        $reader.Dispose(); $stream.Dispose(); $page.Dispose()
    }
}
