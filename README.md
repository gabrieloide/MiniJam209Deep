# One-Way Down ⛏️🌑

**One-Way Down** is a 1-bit retro platformer developed in 72 hours for **Mini Jam 209: Deep**. The game follows a strict technical and creative limitation: **The player only has one life—forever.**

### 🎮 [Play on Itch.io](https://virterex.itch.io/one-way-down)

---

## 📖 The Concept
In a world where the descent is easier than survival, you must master the grappling hook to reach the ultimate treasure at the core of the earth. But be careful: once you fall, your journey ends permanently.

*   **Theme:** Deep (Descent into the unknown).
*   **Limitation:** Only one life (Permanent death across all sessions).

## 🛠️ Technical Highlights
This project was refactored post-jam to demonstrate **Clean Code** principles and a robust, modular architecture in Unity.

### 🏗️ Architecture & Clean Code
*   **Modular Component Design:** Logic is separated into specialized components (`PlayerMovement`, `ZiplineController`, `PlayerDepthTracker`) to adhere to the Single Responsibility Principle.
*   **Event-Driven Communication:** Uses C# Actions and static events in a centralized `GameManager` to decouple UI and gameplay systems.
*   **Security & Scalability:** Implements **ScriptableObjects** (`GameConfig`) to handle sensitive configurations (like Firebase URLs) outside the codebase, following security best practices and protecting against credential leaks.

### ⚙️ Systems & Tools
*   **Physics & Mechanics:** Custom Zipline/Grappling Hook physics and platform attachment logic (handling `Rigidbody2D` interactions without parenting jitter).
*   **Modern Input System:** Fully implemented using the new **Unity Input System** for flexible control schemes.
*   **Tweening & Juice:** Powered by **DOTween** for smooth UI transitions, camera shakes, and environmental feedback (like crumbling platforms).
*   **Aesthetics:** 1-bit Pixel Art style with a focused **4:3 aspect ratio** to evoke a classic handheld feel.

## 🚀 Key Features
*   **12 Challenging Levels:** Each with unique hazards and increasing difficulty.
*   **Grappling Hook Mechanic:** A physics-based movement system that allows for verticality and fast-paced traversal.
*   **Permanent Consequence:** A global death system that tracks player progress and legacy.
*   **Minimalist UI:** Clean, animated interface designed for maximum player feedback.

## 💻 Installation & Usage
1. Clone the repository.
2. Open in **Unity 2022.3** or newer.
3. Ensure **DOTween** and **TextMeshPro** are imported.
4. Create a `GameConfig` asset in `Assets/Secret` to set up your own database backend.

---

*Developed by Gabriel (Virterex) for Mini Jam 209.*
