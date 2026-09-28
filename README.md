# Blockson (3D Platformer & Shooter)

A 3D platformer and first/third-person shooter developed in Unity and C#. The player navigates obstacle courses and various types of terrain, engages multiple enemy archetypes, and must reach the exit portal to progress.

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

## Technical Architecture & Core Systems

- **Enemy Finite State Machine (FSM) & Targeting:**
  - Rule-based state controller managing `Idle`, `Shooting`, and `LostSight` transitions.
  - Multi-point raycast checks for target detection and occlusion testing.
  - Position memory caching (`lastKnownLocation`) to drive search routines upon broken sightlines.
  - Local-to-world coordinate transformations (`InverseTransformPoint`/`TransformPoint`) to constrain pitch and yaw for weapon tracking.
- **Custom Player Kinematics & Physics:**
  - Multi-point raycast sampling matrix around character bounds for ground detection.
  - Moving platform velocity vector inheritance evaluated in `FixedUpdate` to eliminate frame latency and physics desync.
  - Dynamic surface friction and acceleration scaling simulating momentum and low-drag icy terrain.
- **Raycast-Driven Aiming & Dual Perspective:**
  - Dynamic switching between First-Person and Third-Person camera views.
  - Viewport-to-world raycasting aligning projectile spawn vectors directly with screen-space crosshair placement.
- **Hazard Volumes & State Progression:**
  - Heart-based health tracking synced with damage triggers from spikes, enemies, lava.
  - Level transitions through a portal at the end of each level.

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
