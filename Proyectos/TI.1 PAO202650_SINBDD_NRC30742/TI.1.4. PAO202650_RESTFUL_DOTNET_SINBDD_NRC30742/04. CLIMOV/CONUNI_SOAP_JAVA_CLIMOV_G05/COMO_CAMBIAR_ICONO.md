# 📱 CÓMO CAMBIAR EL ÍCONO DE LA APLICACIÓN

## Pasos para usar tu propia imagen como ícono:

### 1️⃣ Coloca tu imagen
- **Ubicación**: `app/src/main/res/drawable/`
- **Formato**: PNG o JPG
- **Tamaño recomendado**: 512x512 píxeles o más grande (debe ser cuadrada)
- **Nombre sugerido**: `mi_icono.png` (puedes usar cualquier nombre)

### 2️⃣ Opción A: Usar el archivo `icono_app.xml` (MÁS FÁCIL)
1. Abre el archivo: `app/src/main/res/drawable/icono_app.xml`
2. Cambia `mi_icono` por el nombre de tu imagen (SIN la extensión .png o .jpg)
   - Ejemplo: Si tu imagen se llama `logo.png`, cambia `mi_icono` por `logo`
3. Abre: `app/src/main/AndroidManifest.xml`
4. Cambia estas líneas:
   ```xml
   android:icon="@drawable/icono_app"
   android:roundIcon="@drawable/icono_app"
   ```

### 2️⃣ Opción B: Usar directamente tu imagen en el AndroidManifest
1. Coloca tu imagen en `app/src/main/res/drawable/`
2. Abre: `app/src/main/AndroidManifest.xml`
3. Cambia estas líneas (reemplaza `mi_icono` con el nombre de tu imagen):
   ```xml
   android:icon="@drawable/mi_icono"
   android:roundIcon="@drawable/mi_icono"
   ```

### 3️⃣ Recompilar
- Build → Clean Project
- Build → Rebuild Project
- Reinstala la aplicación

## 📝 Notas Importantes:
- La imagen debe ser **cuadrada** (mismo ancho y alto)
- Tamaño mínimo recomendado: **512x512 píxeles**
- Formato: PNG (preferido) o JPG
- Si el ícono se ve borroso, usa una imagen más grande (1024x1024 px)

## 🎨 Ejemplo de estructura:
```
app/src/main/res/drawable/
  ├── mi_icono.png          ← Tu imagen aquí
  ├── icono_app.xml         ← Archivo que usa tu imagen
  └── ...
```

