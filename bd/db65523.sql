-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 5.9.179.194
-- Tiempo de generación: 01-11-2026 a las 23:10:28
-- Versión del servidor: 10.11.19-MariaDB-log
-- Versión de PHP: 8.2.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db65523`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_reparacion`
--

CREATE TABLE `detalle_reparacion` (
  `id_Detalle` int(11) NOT NULL,
  `idStock` int(11) DEFAULT NULL,
  `idReparacion` int(11) DEFAULT NULL,
  `Cantidad_Usada` int(11) NOT NULL,
  `Precio_Unitario_Momento` decimal(10,2) NOT NULL,
  `Subtotal` decimal(10,2) NOT NULL,
  `Fecha_Consumo` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `detalle_reparacion`
--

INSERT INTO `detalle_reparacion` (`id_Detalle`, `idStock`, `idReparacion`, `Cantidad_Usada`, `Precio_Unitario_Momento`, `Subtotal`, `Fecha_Consumo`) VALUES
(1, 1, NULL, 1, 1200.00, 2200.00, '2026-06-18');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura`
--

CREATE TABLE `factura` (
  `id_Factura` int(11) NOT NULL,
  `idReparacion` int(11) DEFAULT NULL,
  `Total_Factura` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reparacion`
--

CREATE TABLE `reparacion` (
  `id_Reparacion` int(11) NOT NULL,
  `idUsuario` int(11) DEFAULT NULL,
  `idVehiculo` int(11) DEFAULT NULL,
  `Nombre_Cliente` varchar(255) NOT NULL,
  `Fecha_Ingreso` date NOT NULL DEFAULT current_timestamp(),
  `Descripcion_Trabajo_Realizado` varchar(255) NOT NULL,
  `Motivo_Ingreso` varchar(255) NOT NULL,
  `Costo_Mano_De_Obra` decimal(10,2) NOT NULL,
  `Fecha_Creacion` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `stock`
--

CREATE TABLE `stock` (
  `id_Stock` int(11) NOT NULL,
  `Nombre_Pieza` varchar(255) NOT NULL,
  `Cantidad_Stock` int(11) NOT NULL,
  `Precio_Unitario` decimal(10,2) NOT NULL,
  `Fecha_Creacion` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `stock`
--

INSERT INTO `stock` (`id_Stock`, `Nombre_Pieza`, `Cantidad_Stock`, `Precio_Unitario`, `Fecha_Creacion`) VALUES
(1, 'Motor', 2, 1900.00, '2026-07-18'),
(3, 'motor t4', 5, 1200.00, '2026-10-06'),
(4, 'Pez', 2, 1200.00, '2026-10-06');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_Usuario` int(11) NOT NULL,
  `Nombre` varchar(255) NOT NULL,
  `Apellido` varchar(255) NOT NULL,
  `Especializacion` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Clave` varchar(255) NOT NULL,
  `Fecha_Creacion` date DEFAULT current_timestamp(),
  `Rol` int(255) NOT NULL,
  `Avatar` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id_Usuario`, `Nombre`, `Apellido`, `Especializacion`, `Email`, `Clave`, `Fecha_Creacion`, `Rol`, `Avatar`) VALUES
(6, 'admin', 'admin', 'mecanico', 'admin@gmail.com', 'UNrIgIvBc0CAwkAc+nbMWV+iH+G0IrZWaWfXXPz0ctI=', '2026-06-01', 1, 'avatars/default-avatar.png'),
(7, 'test', 'test', 'mecanico2', 'test@gmail.com', 'RLt5n7OacJauXT+gKkNzlp79SNidqvd1/WSdpFual90=', '2026-06-30', 2, 'avatars/usuario_7.jpg'),
(9, 'test  test', 'test test', 'mecanico', 'test3@gmail.com', 'RLt5n7OacJauXT+gKkNzlp79SNidqvd1/WSdpFual90=', '2026-07-13', 2, 'avatars/default-avatar.png');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vehiculo`
--

CREATE TABLE `vehiculo` (
  `id_Vehiculo` int(11) NOT NULL,
  `Matricula` varchar(255) NOT NULL,
  `Modelo` varchar(255) NOT NULL,
  `Fecha_Creacion` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `vehiculo`
--

INSERT INTO `vehiculo` (`id_Vehiculo`, `Matricula`, `Modelo`, `Fecha_Creacion`) VALUES
(2, 'RT', 'D4923', '2026-10-14'),
(3, 'FR4232', 'Polo Track', '2026-10-16'),
(4, 'RTWEWE ', 'Chevrolet Tracker 6AT LT', '2026-10-16');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `detalle_reparacion`
--
ALTER TABLE `detalle_reparacion`
  ADD PRIMARY KEY (`id_Detalle`),
  ADD KEY `id_Stock_Detalle_Reparacion` (`idStock`),
  ADD KEY `id_Reparacion_Detalle_Reparacion` (`idReparacion`);

--
-- Indices de la tabla `factura`
--
ALTER TABLE `factura`
  ADD PRIMARY KEY (`id_Factura`),
  ADD UNIQUE KEY `idReparacion` (`idReparacion`);

--
-- Indices de la tabla `reparacion`
--
ALTER TABLE `reparacion`
  ADD PRIMARY KEY (`id_Reparacion`),
  ADD KEY `fk_id_usuario_id_reperacion` (`idUsuario`),
  ADD KEY `fk_id_vehiculo_idvehiculo` (`idVehiculo`);

--
-- Indices de la tabla `stock`
--
ALTER TABLE `stock`
  ADD PRIMARY KEY (`id_Stock`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_Usuario`),
  ADD UNIQUE KEY `Email` (`Email`);

--
-- Indices de la tabla `vehiculo`
--
ALTER TABLE `vehiculo`
  ADD PRIMARY KEY (`id_Vehiculo`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `detalle_reparacion`
--
ALTER TABLE `detalle_reparacion`
  MODIFY `id_Detalle` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `factura`
--
ALTER TABLE `factura`
  MODIFY `id_Factura` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `reparacion`
--
ALTER TABLE `reparacion`
  MODIFY `id_Reparacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `stock`
--
ALTER TABLE `stock`
  MODIFY `id_Stock` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_Usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `vehiculo`
--
ALTER TABLE `vehiculo`
  MODIFY `id_Vehiculo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detalle_reparacion`
--
ALTER TABLE `detalle_reparacion`
  ADD CONSTRAINT `id_Reparacion_Detalle_Reparacion` FOREIGN KEY (`idReparacion`) REFERENCES `reparacion` (`id_Reparacion`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `id_Stock_Detalle_Reparacion` FOREIGN KEY (`idStock`) REFERENCES `stock` (`id_Stock`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Filtros para la tabla `factura`
--
ALTER TABLE `factura`
  ADD CONSTRAINT `id_Reparacion_idReparacion` FOREIGN KEY (`idReparacion`) REFERENCES `reparacion` (`id_Reparacion`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Filtros para la tabla `reparacion`
--
ALTER TABLE `reparacion`
  ADD CONSTRAINT `fk_id_usuario_id_reperacion` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id_Usuario`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_id_vehiculo_idvehiculo` FOREIGN KEY (`idVehiculo`) REFERENCES `vehiculo` (`id_Vehiculo`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
