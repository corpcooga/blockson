# Blockson (3D Platformer & Shooter)

A 3D platformer and first/third-person shooter developed in Unity and C#. The player navigates obstacle courses and specialized terrain, engages various enemy archetypes, and must reach the exit portal to progress.

🔗 **[Play / Download on itch.io](https://corpcooga.itch.io/blockson)**

---

## Gameplay Preview

<p align="center">
  <img width="49%" alt="Screenshot 2026-09-27 at 5 01 35 PM" src="https://github.com/user-attachments/assets/3460cc3f-9461-49c4-a060-6df4d58084cb" />
  <img width="49%" alt="Screenshot 2026-09-27 at 5 01 12 PM" src="https://github.com/user-attachments/assets/872c3dbd-15d7-47f0-8b2e-ed614f5852fe" />
</p>
<p align="center">
  <img width="49%" alt="Screenshot 2026-09-27 at 5 00 56 PM" src="https://github.com/user-attachments/assets/73604106-71f3-4bba-8f28-18c3fdf1499d" />
  <img width="49%" alt="Screenshot 2026-09-27 at 5 00 14 PM" src="https://github.com/user-attachments/assets/23474058-a96b-422c-a926-b081d52b0c59" />
</p>

---

## Core Gameplay & Systems

- **Weapon System:** Projectiles eliminate enemies.
- **Enemy Archetypes:**
  - *Standard Enemies:* Patrol and deal damage on player contact.
  - *Heavy Enemies:* Require 2 shots to destroy.
  - *Ranged Enemies:* Armed with weapons that fire projectiles at the player.
- **Special Surfaces:**
  - *Ice Surfaces:* Reduce surface drag to build up high-speed movement momentum.
  - *Jump Pads:* Launch the player vertically to reach elevated platforms.
- **Health System:**
  - 3-heart player health system.
  - *Spikes:* Deal 1 heart per hit.
  - *Lava Volumes:* Instant-death upon contact and level restarts.
- **Camera Controller:**
  - Dynamic runtime switching between First-Person and Third-Person via keybind.
  - Projectile path updates to accurately reflect camera angle and crosshair placement.
- **Objective Progression:** Navigational flow requiring players to eliminate threats and reach portals to advance.

---

## Tech Stack

- **Engine:** Unity
- **Language:** C#
- **Physics & Audio:** Unity Physics Engine, Rigidbodies, Raycasting, Particle Systems
- **Platform:** macOS Standalone (distributed via itch.io)

---

## Project Structure

- `Assets/Scripts`: C# scripts managing player controller, weapon shooting, enemy health/AI, and hazard logic.
- `Assets/Scenes`: Level layouts, hazard geometry, and portal spawn locations.
- `ProjectSettings/`: Physics collision matrix, input mappings, and tag/layer configurations.

---

## Running Locally

1. Clone the repository:
   ```bash
   git clone https://github.com/corpcooga/blockson.git
