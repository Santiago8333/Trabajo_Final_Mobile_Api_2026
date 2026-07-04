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
- **Autorización:** Requiere token de rol **Administrador**
- **Respuesta:** `List<UsuarioDto>` / `401 Unauthorized` sin token / `403 Forbidden` si no es Administrador
- **Descripción:** Devuelve todos los usuarios registrados.

### 🔸 Obtener Usuario por Id

- **Método:** GET
- **Ruta:** `/api/Usuario/{id}`
- **Autorización:** Requiere token válido (cualquier rol autenticado)
- **Path Param:**
  - `id`: ID del usuario
- **Respuesta:** `UsuarioDto` / `404 Not Found` si no existe / `401 Unauthorized` sin token
- **Descripción:** Devuelve los datos de un usuario específico.

### 🔸 Crear Usuario

- **Método:** POST
- **Ruta:** `/api/Usuario`
- **Autorización:** Requiere token de rol **Administrador**
- **Tipo de envío:** application/json
- **Cuerpo:** Objeto `Usuario` (JSON)
  - `Nombre`: string
  - `Apellido`: string
  - `Especializacion`: string
  - `Email`: string
  - `Clave`: string
  - `Rol`: int (`1` = Administrador, `2` = Empleado)
- **Respuesta:** `204 No Content` / `409 Conflict` si el email ya está registrado / `401 Unauthorized` sin token / `403 Forbidden` si no es Administrador
- **Descripción:** Registra un nuevo usuario. La clave se guarda hasheada (SHA256 + salt).

---

`UsuarioDto` no incluye el campo `Clave`.

Para acceder a endpoints protegidos con políticas (`Administrador`, `Empleado`), se debe enviar el token obtenido en el login como header:

```
Authorization: Bearer <token>
```
