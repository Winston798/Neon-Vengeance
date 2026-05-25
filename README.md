# Neon Vengeance

Neon Vengeance is a Unity 2D cyberpunk action-RPG prototype. The project includes multi-scene progression, player combat, enemy state machines, dialogue, shop, task, equipment, and UI systems built with C# and Unity 2022 LTS.

## Features

- 2D action platforming with movement, jumping, dashing, and melee attacks
- Player finite-state machine for idle, run, jump, dash, attack, hurt, pause, and death states
- Multiple enemy types with shared enemy-state infrastructure and specialized behaviors
- Scene flow for start, town, and level progression
- Dialogue, task, shop, bag, equipment, skill, and pause UI panels
- ScriptableObject data assets for enemies, tasks, equipment, and dialogue
- Cyberpunk-inspired sprite, audio, tilemap, animation, prefab, and UI resources

## Built With

- Unity `2022.3.62f2`
- C#
- Unity 2D toolchain
- Cinemachine
- TextMeshPro
- Unity UI
- Unity Visual Scripting package installed in the project manifest

## Controls

| Action | Input |
| --- | --- |
| Move | Horizontal axis / keyboard movement |
| Jump | `Space` |
| Attack | `J` |
| Heavy attack / skill action | `K` |
| Dash | `Left Shift` |
| Bag / inventory action | `B` |
| Skill panel / skill action | `S` |
| Interact | `R` |
| Pause | `Escape` |
| Advance dialogue | Left mouse click |

## Repository Structure

```text
.
|-- Assets/
|   |-- 00Scenes/        # Start, Town, and level scenes
|   |-- 01Scripts/       # Gameplay, services, systems, UI, player, enemy, task, and equipment scripts
|   |-- 02Animator/      # Animator controllers and animation assets
|   |-- 03Data/          # ScriptableObject gameplay data
|   |-- Resources/       # Runtime-loaded prefabs, UI, effects, music, and enemies
|   |-- CyberPunk/       # Visual and audio art resources
|   |-- 998TileMap/      # Tilemap assets
|   `-- 999素材/          # Additional art and font resources
|-- Packages/            # Unity package manifest and lock file
|-- ProjectSettings/     # Unity project settings
`-- .gitignore           # Unity-focused ignore rules
```

## Getting Started

1. Install Unity Hub and Unity `2022.3.62f2` or another compatible Unity 2022.3 LTS editor.
2. Clone the repository.
3. In Unity Hub, choose **Open** and select the repository root.
4. Let Unity restore packages from `Packages/manifest.json`.
5. Open `Assets/00Scenes/Start.unity`.
6. Press Play in the Unity editor.

## Development Notes

- The project relies on Unity `.meta` files, so keep them committed with their assets.
- `Library/`, `Temp/`, `Obj/`, `Build/`, `Builds/`, `Logs/`, and user-specific IDE files are ignored.
- No compiled build is included in this repository. Create platform builds from Unity when needed.
- The product name in Unity project settings is currently `FireGame`; the repository name is `Neon Vengeance`.

## License

No license has been specified yet.
