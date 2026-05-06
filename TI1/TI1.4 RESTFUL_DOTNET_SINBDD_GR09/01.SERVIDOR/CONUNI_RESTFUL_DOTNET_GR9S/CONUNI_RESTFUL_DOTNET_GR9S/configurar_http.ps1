# Script para configurar HTTP en IIS Express
# Ejecutar como Administrador: PowerShell -ExecutionPolicy Bypass -File configurar_http.ps1

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Configurando IIS Express para HTTP en red" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Obtener IP local
$ipAddress = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object {$_.IPAddress -like "192.168.*"} | Select-Object -First 1).IPAddress
if (-not $ipAddress) {
    $ipAddress = "192.168.100.6"
    Write-Host "No se pudo detectar IP automáticamente. Usando: $ipAddress" -ForegroundColor Yellow
} else {
    Write-Host "IP detectada: $ipAddress" -ForegroundColor Green
}
Write-Host ""

# Buscar applicationhost.config
$possiblePaths = @(
    "$env:USERPROFILE\Documents\IISExpress\config\applicationhost.config",
    "$PSScriptRoot\.vs\config\applicationhost.config",
    "$PSScriptRoot\..\.vs\config\applicationhost.config"
)

$configPath = $null
foreach ($path in $possiblePaths) {
    if (Test-Path $path) {
        $configPath = $path
        Write-Host "Archivo encontrado: $configPath" -ForegroundColor Green
        break
    }
}

if (-not $configPath) {
    Write-Host "ERROR: No se encontró applicationhost.config" -ForegroundColor Red
    Write-Host "Busca manualmente en:" -ForegroundColor Yellow
    foreach ($path in $possiblePaths) {
        Write-Host "  - $path" -ForegroundColor Yellow
    }
    Write-Host ""
    Write-Host "El archivo se crea cuando ejecutas el proyecto por primera vez en Visual Studio" -ForegroundColor Yellow
    pause
    exit 1
}

Write-Host ""
Write-Host "Haciendo backup del archivo..." -ForegroundColor Yellow
Copy-Item $configPath "$configPath.backup" -Force
Write-Host "Backup creado: $configPath.backup" -ForegroundColor Green

# Leer el archivo
[xml]$config = Get-Content $configPath

# Buscar el sitio
$siteName = "CONUNI_RESTFUL_DOTNET_GR9S"
$site = $config.configuration.'system.applicationHost'.sites.site | Where-Object { $_.name -eq $siteName }

if (-not $site) {
    Write-Host "ADVERTENCIA: No se encontró el sitio '$siteName'" -ForegroundColor Yellow
    Write-Host "Sitios disponibles:" -ForegroundColor Yellow
    $config.configuration.'system.applicationHost'.sites.site | ForEach-Object { Write-Host "  - $($_.name)" -ForegroundColor Yellow }
    Write-Host ""
    Write-Host "Intentando agregar binding al primer sitio encontrado..." -ForegroundColor Yellow
    $site = $config.configuration.'system.applicationHost'.sites.site[0]
}

if ($site) {
    Write-Host "Sitio encontrado: $($site.name)" -ForegroundColor Green
    
    # Verificar bindings existentes
    $httpPort = 44385
    $httpsPort = 44384
    
    $httpBinding = $site.bindings.binding | Where-Object { $_.protocol -eq "http" -and $_.bindingInformation -like "*:$httpPort:*" }
    $httpsBinding = $site.bindings.binding | Where-Object { $_.protocol -eq "https" -and $_.bindingInformation -like "*:$httpsPort:*" }
    
    # Agregar binding HTTP para la IP de red
    if (-not ($site.bindings.binding | Where-Object { $_.protocol -eq "http" -and $_.bindingInformation -eq "*:$httpPort:$ipAddress" })) {
        $newBinding = $config.CreateElement("binding")
        $newBinding.SetAttribute("protocol", "http")
        $newBinding.SetAttribute("bindingInformation", "*:$httpPort:$ipAddress")
        $site.bindings.AppendChild($newBinding) | Out-Null
        Write-Host "Binding HTTP agregado: *:$httpPort:$ipAddress" -ForegroundColor Green
    } else {
        Write-Host "Binding HTTP ya existe: *:$httpPort:$ipAddress" -ForegroundColor Yellow
    }
    
    # Agregar binding HTTP para todas las interfaces (0.0.0.0)
    if (-not ($site.bindings.binding | Where-Object { $_.protocol -eq "http" -and $_.bindingInformation -eq "*:$httpPort:*" })) {
        $newBinding = $config.CreateElement("binding")
        $newBinding.SetAttribute("protocol", "http")
        $newBinding.SetAttribute("bindingInformation", "*:$httpPort:*")
        $site.bindings.AppendChild($newBinding) | Out-Null
        Write-Host "Binding HTTP agregado: *:$httpPort:* (todas las interfaces)" -ForegroundColor Green
    } else {
        Write-Host "Binding HTTP ya existe: *:$httpPort:*" -ForegroundColor Yellow
    }
    
    # Guardar el archivo
    $config.Save($configPath)
    Write-Host ""
    Write-Host "Archivo guardado correctamente!" -ForegroundColor Green
} else {
    Write-Host "ERROR: No se pudo encontrar ningún sitio en el archivo" -ForegroundColor Red
    pause
    exit 1
}

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Configurando URL ACL y Firewall..." -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Configurar URL ACL
Write-Host "Configurando URL ACL para HTTP..." -ForegroundColor Yellow
try {
    $aclCmd = "netsh http add urlacl url=http://$ipAddress`:$httpPort/ user=Everyone"
    $result = Invoke-Expression $aclCmd 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "URL ACL agregada correctamente" -ForegroundColor Green
    } else {
        Write-Host "URL ACL ya existe o hay un error: $result" -ForegroundColor Yellow
    }
} catch {
    Write-Host "Error al agregar URL ACL: $_" -ForegroundColor Yellow
}

# Agregar regla de firewall
Write-Host "Agregando regla de firewall..." -ForegroundColor Yellow
try {
    $ruleName = "IIS Express HTTP RESTful"
    # Eliminar regla si existe
    Remove-NetFirewallRule -Name $ruleName -ErrorAction SilentlyContinue
    # Agregar nueva regla
    New-NetFirewallRule -Name $ruleName -DisplayName $ruleName -Direction Inbound -Protocol TCP -LocalPort $httpPort -Action Allow | Out-Null
    Write-Host "Regla de firewall agregada correctamente" -ForegroundColor Green
} catch {
    Write-Host "Error al agregar regla de firewall: $_" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Configuración completada!" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "El servidor debería estar accesible en:" -ForegroundColor Green
Write-Host "  - HTTP Localhost: http://localhost:$httpPort" -ForegroundColor White
Write-Host "  - HTTP Red local: http://$ipAddress`:$httpPort" -ForegroundColor White
Write-Host ""
Write-Host "IMPORTANTE:" -ForegroundColor Yellow
Write-Host "1. Cierra Visual Studio completamente" -ForegroundColor White
Write-Host "2. Abre el proyecto nuevamente" -ForegroundColor White
Write-Host "3. Ejecuta el proyecto" -ForegroundColor White
Write-Host "4. Verifica que escuche en el puerto: netstat -an | findstr :$httpPort" -ForegroundColor White
Write-Host ""
pause


