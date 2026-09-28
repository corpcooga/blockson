# Blockson (3D Platformer & Shooter)

A 3D platformer and first/third-person shooter developed in Unity and C#. The player navigates obstacle courses and various types of terrain, engages multiple enemy archetypes, and must reach the exit portal to progress.

🔗 **[Play / Download on itch.io](https://corpcooga.itch.io/blockson)**

---

## Gameplay Preview


<img width="49%" alt="1" src="https://github.com/user-attachments/assets/f7433f53-111b-4715-855a-fc017b2417bb" />
<img width="49%" alt="2" src="https://github.com/user-attachments/assets/42faf8f2-af41-4c36-863a-197d65c33340" />
<img width="49%" alt="3" src="https://github.com/user-attachments/assets/d81f9109-c539-46df-b39c-5f1b7553ec93" />
<img width="49%" alt="4" src="https://github.com/user-attachments/assets/e8f3ccec-8647-474f-8005-c6156c11d642" />

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
