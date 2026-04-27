# ⛏️ One-Way Down: TODO List

## 🏃 Fase 1: Gameplay Core (Completado)
- [x] **Movimiento Básico**: Caminar, saltar y detección de suelo.
- [x] **Mecánica de Zipline**: Raycast direccional, restricción en aire y modo "Aim".
- [x] **Sistema de Peligros**: 
    - [x] `Hazard.cs` (Base).
    - [x] `MovingHazard.cs` (Sierras/Taladros).
    - [x] `TemporalGround.cs` (Suelos que caen).
- [x] **Cámara**: Sistema de pantallas estáticas (estilo Celeste).

## 🌐 Fase 2: Conectividad y Persistencia (Completado)
- [x] **Firebase REST**: Envío de muertes (Post) y descarga de tumbas (Get).
- [x] **Analíticas**: Registro de nombre, causa de muerte, profundidad y fecha.
- [x] **Tumbas Dinámicas**: Spawneo de tumbas con datos de otros jugadores.
- [x] **Only One Life**: Bloqueo de `PlayerPrefs` tras la muerte.

## 🧱 Fase 3: Diseño de Niveles (Estructura de 12 Pantallas)
- [ ] **Bioma 1: Tierra/Minas** (Pantallas 1-4)
    - Introducción al Zipline y peligros básicos.
- [ ] **Bioma 2: Cristales** (Pantallas 5-8)
    - Combinaciones de sierras y suelos temporales.
- [ ] **Bioma 3: Magma** (Pantallas 9-11)
    - El reto final de precisión y timing.
- [ ] **Pantalla 12: El Oasis del Fondo** (Victoria)
    - Zona de paz, música relajante y cierre del juego.

## 🎨 Arte y Pulido
- [ ] **HUD**: Mejorar el diseño visual del contador de metros.
- [ ] **Game Over**: Pantalla final con resumen del descenso.
- [ ] **Sonido**: Música ambiental y SFX básicos.
- [ ] **Partículas**: Polvo al caer y chispas al usar el zipline.

## 📦 Exportación
- [ ] Build para WebGL y testeo en navegador.
