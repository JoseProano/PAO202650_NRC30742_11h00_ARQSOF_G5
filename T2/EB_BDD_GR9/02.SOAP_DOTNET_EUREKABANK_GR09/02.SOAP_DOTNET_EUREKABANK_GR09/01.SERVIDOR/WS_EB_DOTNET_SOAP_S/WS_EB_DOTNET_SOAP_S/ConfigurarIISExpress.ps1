# Script completo para configurar IIS Express para escuchar en localhost y en IP 10.183.38.246
# Ejecutar como Administrador ANTES de abrir Visual Studio

$port = 51641
$ipAddress = "10.183.38.246"
$projectPath = "D:\Darwin\Documents\PEPAS MONSTER G09\01.SOA_BDD_GR09\02.SOAP_DOTNET_EUREKABANK_GR09\01.SERVIDOR\WS_EB_DOTNET_SOAP_S"
$vsConfigPath = "$projectPath\.vs\WS_EB_DOTNET_SOAP_S\config\applicationhost.config"

Write-Host "=== Configurando IIS Express para multiples direcciones ===" -ForegroundColor Cyan
Write-Host ""

# Paso 1: Configurar reservas de URL con netsh
Write-Host "Paso 1: Configurando reservas de URL..." -ForegroundColor Yellow

# Eliminar reservas existentes si existen
netsh http delete urlacl "http://${ipAddress}:${port}/" 2>$null | Out-Null
netsh http delete urlacl "http://*:${port}/" 2>$null | Out-Null

# Agregar reserva para todas las interfaces (permite localhost y cualquier IP)
Write-Host "  Agregando reserva para todas las interfaces en puerto $port..." -ForegroundColor Gray
$result = netsh http add urlacl url="http://*:${port}/" user="Everyone"
if ($LASTEXITCODE -eq 0) {
    Write-Host "  Reserva agregada exitosamente" -ForegroundColor Green
} else {
    $resultText = $result -join " "
    if ($resultText -match "ya existe" -or $resultText -match "already exists") {
        Write-Host "  La reserva ya existe" -ForegroundColor Yellow
    } else {
        Write-Host "  Error al agregar reserva" -ForegroundColor Red
    }
}

Write-Host ""

# Paso 2: Modificar applicationhost.config cuando Visual Studio lo cree
Write-Host "Paso 2: Configurando applicationhost.config..." -ForegroundColor Yellow

if (Test-Path $vsConfigPath) {
    Write-Host "  Archivo applicationhost.config encontrado" -ForegroundColor Gray
    
    try {
        [xml]$config = Get-Content $vsConfigPath
        
        # Buscar el sitio
        $sites = $config.configuration.'system.applicationHost'.sites.site
        $site = $null
        
        foreach ($s in $sites) {
            $bindings = $s.bindings.binding
            if ($bindings) {
                foreach ($b in $bindings) {
                    if ($b.bindingInformation -like "*:${port}:*") {
                        $site = $s
                        break
                    }
                }
            }
            if ($site) { break }
        }
        
        if ($site) {
            Write-Host "  Sitio encontrado, agregando binding para IP $ipAddress..." -ForegroundColor Gray
            
            # Verificar si ya existe el binding para la IP
            $existingBinding = $null
            foreach ($b in $site.bindings.binding) {
                if ($b.bindingInformation -eq "${ipAddress}:${port}:") {
                    $existingBinding = $b
                    break
                }
            }
            
            if (-not $existingBinding) {
                # Crear nuevo binding
                $newBinding = $config.CreateElement("binding")
                $newBinding.SetAttribute("protocol", "http")
                $newBinding.SetAttribute("bindingInformation", "${ipAddress}:${port}:")
                $site.bindings.AppendChild($newBinding) | Out-Null
                
                # Guardar el archivo
                $config.Save($vsConfigPath)
                Write-Host "  Binding agregado para IP $ipAddress" -ForegroundColor Green
            } else {
                Write-Host "  El binding para la IP ya existe" -ForegroundColor Yellow
            }
            
            # Asegurar que el binding para todas las interfaces también existe
            $allInterfacesBinding = $null
            foreach ($b in $site.bindings.binding) {
                if ($b.bindingInformation -eq "*:${port}:") {
                    $allInterfacesBinding = $b
                    break
                }
            }
            
            if (-not $allInterfacesBinding) {
                $newBinding = $config.CreateElement("binding")
                $newBinding.SetAttribute("protocol", "http")
                $newBinding.SetAttribute("bindingInformation", "*:${port}:")
                $site.bindings.AppendChild($newBinding) | Out-Null
                $config.Save($vsConfigPath)
                Write-Host "  Binding agregado para todas las interfaces" -ForegroundColor Green
            }
        } else {
            Write-Host "  Sitio no encontrado en applicationhost.config" -ForegroundColor Yellow
            Write-Host "    (Esto es normal si Visual Studio aun no ha creado el archivo)" -ForegroundColor Gray
        }
    } catch {
        Write-Host "  Error al procesar applicationhost.config: $_" -ForegroundColor Red
    }
} else {
    Write-Host "  Archivo applicationhost.config no existe aun" -ForegroundColor Yellow
    Write-Host "    Se creara automaticamente cuando abras el proyecto en Visual Studio" -ForegroundColor Gray
    Write-Host "    Ejecuta este script nuevamente despues de abrir el proyecto" -ForegroundColor Gray
}

Write-Host ""
Write-Host "=== Configuracion completada ===" -ForegroundColor Green
Write-Host ""
Write-Host "El servicio estara disponible en:" -ForegroundColor Cyan
Write-Host "  http://localhost:${port}/" -ForegroundColor White
Write-Host "  http://${ipAddress}:${port}/" -ForegroundColor White
Write-Host "  http://localhost:${port}/WSEureka.svc" -ForegroundColor White
Write-Host "  http://${ipAddress}:${port}/WSEureka.svc" -ForegroundColor White
Write-Host ""
Write-Host "Nota: Si el archivo applicationhost.config no existia, abre el proyecto en Visual Studio" -ForegroundColor Yellow
Write-Host "      y luego ejecuta este script nuevamente para agregar el binding de la IP." -ForegroundColor Yellow
