# Trabajo Final Mobile Api 2026

API REST en ASP.NET Core (.NET 8) con Entity Framework Core (Pomelo MySQL).

## 📘 API REST - Documentación de Endpoints

**Base URL:**
`http://localhost:5064/`

---

## 🔐 Autenticación

### 🔸 Login

- **Método:** POST
- **Ruta:** `/api/Usuario/login`
- **Tipo de envío:** application/json
- **Cuerpo:**
  - `Email`: string (`admin@gmail.com`)
  - `Clave`: string (`admin`)
- **Respuesta:** `{ "token": "string", "usuario": UsuarioDto }`
- **Descripción:** Autentica al usuario por email y clave, y devuelve un token JWT junto con sus datos.

---

## 👤 Usuario

### 🔸 Obtener Todos los Usuarios

- **Método:** GET
- **Ruta:** `/api/Usuario`
- **Respuesta:** `List<UsuarioDto>`
- **Descripción:** Devuelve todos los usuarios registrados.

### 🔸 Obtener Usuario por Id

- **Método:** GET
- **Ruta:** `/api/Usuario/{id}`
- **Path Param:**
  - `id`: ID del usuario
- **Respuesta:** `UsuarioDto` / `404 Not Found` si no existe
- **Descripción:** Devuelve los datos de un usuario específico.

### 🔸 Crear Usuario

- **Método:** POST
- **Ruta:** `/api/Usuario`
- **Tipo de envío:** application/json
- **Cuerpo:** Objeto `Usuario` (JSON)
  - `Nombre`: string
  - `Apellido`: string
  - `Especializacion`: string
  - `Email`: string
  - `Clave`: string
  - `Rol`: int (`1` = Administrador, `2` = Empleado)
- **Respuesta:** `204 No Content`
- **Descripción:** Registra un nuevo usuario. La clave se guarda hasheada (SHA256 + salt).

---

`UsuarioDto` no incluye el campo `Clave`.

Para acceder a endpoints protegidos con políticas (`Administrador`, `Empleado`), se debe enviar el token obtenido en el login como header:

```
Authorization: Bearer <token>
```
