$basePath = "C:\Users\zakkc\.gemini\antigravity\scratch\GestionHotelera"

# 1. Reemplazar texto en todos los archivos .cs
$csFiles = Get-ChildItem -Path $basePath -Recurse -Filter "*.cs"
foreach ($file in $csFiles) {
    if ($file.FullName -match "\\obj\\" -or $file.FullName -match "\\bin\\") { continue }
    $content = Get-Content $file.FullName -Raw
    $newContent = $content -replace "GestionHotelera\.Core", "GestionHotelera"
    $newContent = $newContent -replace "GestionHotelera\.Consola", "GestionHotelera"
    if ($content -ne $newContent) {
        Set-Content -Path $file.FullName -Value $newContent -NoNewline
    }
}

# 2. Reemplazar en los .csproj
$csprojFiles = Get-ChildItem -Path $basePath -Recurse -Filter "*.csproj"
foreach ($file in $csprojFiles) {
    $content = Get-Content $file.FullName -Raw
    $newContent = $content -replace "GestionHotelera\.Core", "Biblioteca"
    $newContent = $newContent -replace "GestionHotelera\.Consola", "Consola"
    $newContent = $newContent -replace "<RootNamespace>Biblioteca</RootNamespace>", "<RootNamespace>GestionHotelera</RootNamespace>"
    $newContent = $newContent -replace "<RootNamespace>Consola</RootNamespace>", "<RootNamespace>GestionHotelera</RootNamespace>"
    if ($content -ne $newContent) {
        Set-Content -Path $file.FullName -Value $newContent -NoNewline
    }
}

# 3. Renombrar carpetas y archivos csproj
Rename-Item -Path "$basePath\GestionHotelera.Core\GestionHotelera.Core.csproj" -NewName "Biblioteca.csproj"
Rename-Item -Path "$basePath\GestionHotelera.Consola\GestionHotelera.Consola.csproj" -NewName "Consola.csproj"

Rename-Item -Path "$basePath\GestionHotelera.Core" -NewName "Biblioteca"
Rename-Item -Path "$basePath\GestionHotelera.Consola" -NewName "Consola"
