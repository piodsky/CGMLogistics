param([int]$Port = 9234)
$ErrorActionPreference = 'Stop'
$outDir = Join-Path $PSScriptRoot 'rpt_reports/competitive-check'
$previewPath = (Resolve-Path -LiteralPath (Join-Path $outDir 'preview.html')).Path
$profilePath = Join-Path $outDir 'browser-profile'
$chromePath = 'C:\Program Files\Google\Chrome\Application\chrome.exe'
$script:cbCommandId = 0
$socket = $null
$browser = $null

function Invoke-Cdp([string]$method, $parameters) {
    $script:cbCommandId++
    $requestId = $script:cbCommandId
    $request = @{ id = $requestId; method = $method; params = $parameters } | ConvertTo-Json -Depth 30 -Compress
    $requestBytes = [Text.Encoding]::UTF8.GetBytes($request)
    $tokenSource = New-Object Threading.CancellationTokenSource(60000)
    try {
        $socket.SendAsync([ArraySegment[byte]]::new($requestBytes), [Net.WebSockets.WebSocketMessageType]::Text, $true, $tokenSource.Token).GetAwaiter().GetResult() | Out-Null
        do {
            $messageStream = New-Object IO.MemoryStream
            try {
                do {
                    $buffer = New-Object byte[] 262144
                    $part = $socket.ReceiveAsync([ArraySegment[byte]]::new($buffer), $tokenSource.Token).GetAwaiter().GetResult()
                    $messageStream.Write($buffer, 0, $part.Count)
                } while (-not $part.EndOfMessage)
                $message = [Text.Encoding]::UTF8.GetString($messageStream.ToArray()) | ConvertFrom-Json
            } finally { $messageStream.Dispose() }
        } while ($message.id -ne $requestId)
        if ($message.error) { throw ($message.error | ConvertTo-Json -Compress) }
        if ($message.result.exceptionDetails) { throw ($message.result.exceptionDetails | ConvertTo-Json -Depth 10 -Compress) }
        return $message.result
    } finally { $tokenSource.Dispose() }
}

try {
    $arguments = @('--headless=new', '--disable-gpu', '--no-first-run', '--no-default-browser-check',
        '--disable-background-networking', '--disable-component-update', '--disable-sync', '--disable-extensions',
        '--allow-file-access-from-files', '--remote-debugging-address=127.0.0.1', "--remote-debugging-port=$Port",
        ('--user-data-dir="' + $profilePath + '"'), 'about:blank')
    $browser = Start-Process -FilePath $chromePath -ArgumentList $arguments -WindowStyle Hidden -PassThru
    $connected = $false
    for ($attempt = 0; $attempt -lt 40; $attempt++) {
        try { $null = Invoke-RestMethod "http://127.0.0.1:$Port/json/version"; $connected = $true; break }
        catch { Start-Sleep -Milliseconds 200 }
    }
    if (-not $connected) { throw 'The local browser did not start.' }
    $previewUrl = ([Uri]$previewPath).AbsoluteUri
    $tab = Invoke-RestMethod -Method Put ("http://127.0.0.1:$Port/json/new?" + [Uri]::EscapeDataString($previewUrl))
    $socket = New-Object Net.WebSockets.ClientWebSocket
    $socket.ConnectAsync([Uri]$tab.webSocketDebuggerUrl, [Threading.CancellationToken]::None).GetAwaiter().GetResult() | Out-Null
    for ($attempt = 0; $attempt -lt 40; $attempt++) {
        $ready = Invoke-Cdp 'Runtime.evaluate' @{ expression = 'typeof window.runReportChecks === "function"'; returnByValue = $true }
        if ($ready.result.value) { break }
        Start-Sleep -Milliseconds 200
    }
    $run = Invoke-Cdp 'Runtime.evaluate' @{ expression = 'window.runReportChecks()'; awaitPromise = $true; returnByValue = $true }
    $run.result.value | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath (Join-Path $outDir 'layout.json') -Encoding UTF8
    $metadata = @($run.result.value)
    for ($index = 0; $index -lt $metadata.Count; $index++) {
        $result = Invoke-Cdp 'Runtime.evaluate' @{ expression = "window.reportResults[$index].data"; returnByValue = $true }
        $fileName = $metadata[$index].fixture + '-' + $metadata[$index].type + '.pdf'
        [IO.File]::WriteAllBytes((Join-Path $outDir $fileName), [Convert]::FromBase64String($result.result.value))
        Write-Output ($fileName + ': width=' + $metadata[$index].width + ', height=' + $metadata[$index].beforeHeight + ', font=' + $metadata[$index].fontSize + ', pages=' + $metadata[$index].pageCountText)
    }
} finally {
    if ($socket) {
        try { Invoke-Cdp 'Browser.close' @{} | Out-Null } catch {}
        $socket.Dispose()
    }
    if ($browser -and -not $browser.HasExited) { Stop-Process -Id $browser.Id -ErrorAction SilentlyContinue }
}
