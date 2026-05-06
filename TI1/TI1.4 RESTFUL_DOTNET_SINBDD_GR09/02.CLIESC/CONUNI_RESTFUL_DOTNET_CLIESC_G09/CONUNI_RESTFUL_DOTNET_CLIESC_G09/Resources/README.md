# 📸 INSTRUCCIONES PARA LA IMAGEN DEL LOGO

## 📁 Dónde colocar la imagen:

Coloca tu imagen del logo en esta carpeta con el nombre:

**`logo.png`**

Ruta completa:
```
CONUNI_RESTFUL_DOTNET_CLIESC_G09\Resources\logo.png
```

## ✅ Formatos soportados:
- PNG (recomendado)
- JPG/JPEG
- BMP

## 📐 Tamaño recomendado:
- 200x200 píxeles o más grande
- La imagen se ajustará automáticamente

## 🔧 Si quieres usar otra ruta:

Si prefieres poner la imagen en otra ubicación, modifica el código en:
`vista\VentanaLogin.cs` línea 97

Cambia la ruta:
```csharp
string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "logo.png");
```

## 📝 Nota:
Si no colocas la imagen, se mostrará el texto "LOGO" temporalmente.


