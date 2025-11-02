# 🎮 Moving Cubes Game

Welcome to **Moving Cubes** - a fast-paced spatial awareness and quick response game built with Unity!

![Unity](https://img.shields.io/badge/Unity-6000.0.23f1-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-Programming-purple?logo=csharp)
![License](https://img.shields.io/badge/License-MIT-green)

## 📖 About The Game

Moving Cubes is an exciting reaction-based game that challenges your:
- 🧠 **Quick Thinking**
- 👁️ **Spatial Vision**
- ⚡ **Fast Response**
- 🎯 **Focus and Concentration**

## 🎯 How To Play

### Objective
Press all the moving cubes **in the correct order** (from 1 to the last number) before the timer runs out!

### Game Rules
1. **Choose Your Level** - Select from different difficulty levels with varying numbers of cubes and time limits
2. **Press The Right Cube** - Click on cubes in numerical order (1, 2, 3, etc.)
3. **Beat The Clock** - Complete all cubes before time expires
4. **Warning System** - If you don't press a cube within 5 seconds, it will grow bigger as a visual cue!

### Scoring System
- ✅ **1 Point** per correctly pressed cube
- ⏱️ **1 Point** for every second remaining when you finish
- 🏆 Higher scores for faster completion times!

## 🎲 Game Tips

💡 **Quick Response** - The cubes move fast, stay alert!

🎯 **Stay Focused** - Maintain concentration throughout the game

👀 **Open Vision** - Keep your eyes on the entire game board at all times

🧠 **Keep Open Mind** - Be ready to adapt as cubes move around

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3 or higher (tested with Unity 6000.0.23f1)
- Git

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/Yaara40/MovingCubes.git
```

2. **Open the project in Unity**
   - Open Unity Hub
   - Click "Add" and select the cloned folder
   - Open with Unity 2022.3 or newer

3. **Open the main game scene**
   - Navigate to: `Assets/MovingCubes/`
   - Open: **`MovingCubesGame.unity`**

4. **Press Play** ▶️ in Unity Editor to start the game

## 📁 Project Structure

```
MovingCubes/
├── Assets/
│   ├── MovingCubes/                    # 🎮 COMPLETE GAME FOLDER
│   │   ├── Audio/                      # Sound effects
│   │   ├── Buttons/                    # UI button assets
│   │   │   ├── PNG/                    # Button images
│   │   │   ├── Scenes/                 # Button demo scenes
│   │   │   └── maanetorn/              # Farm Game UI Pack
│   │   ├── Materials/                  # Cube materials and colors
│   │   ├── Prefabs/                    # Game object prefabs
│   │   ├── Scripts/                    # All game code
│   │   │   └── Cube.cs                 # Main cube behavior
│   │   └── MovingCubesGame.unity       # ⭐ MAIN GAME SCENE
│   │
│   ├── Settings/                       # Unity render pipeline settings
│   ├── TextMesh Pro/                   # Text rendering system
│   └── TutorialInfo/                   # Unity tutorial files
│
├── ProjectSettings/                    # Unity configuration
├── Packages/                           # Unity packages
├── .gitignore                          # Git ignore rules
└── README.md                           # This file
```

### 🗂️ Key Directories Explained

**`Assets/MovingCubes/`** - ⭐ **Everything for the game is here!**
- **`MovingCubesGame.unity`** - Main game scene (START HERE!)
- **`Scripts/`** - All game logic and code
  - `Cube.cs` - Controls cube behavior, movement, and interactions
- **`Buttons/`** - UI button assets from Unity Asset Store
  - `PNG/` - Button image files
  - `maanetorn/` - Farm Game UI pack for interface elements
  - `Scenes/` - Demo scenes for buttons (not game scenes)
- **`Materials/`** - Visual appearance and colors of game objects
- **`Prefabs/`** - Reusable game objects
- **`Audio/`** - Sound effects

**`Assets/Settings/`** - Unity Universal Render Pipeline settings

**`Assets/TextMesh Pro/`** - Advanced text rendering for UI

### 🎯 Quick Navigation Guide

| What you're looking for | Where to find it |
|------------------------|------------------|
| **🎬 Main game scene** | `Assets/MovingCubes/MovingCubesGame.unity` |
| **💻 Game code** | `Assets/MovingCubes/Scripts/Cube.cs` |
| **🎨 UI buttons** | `Assets/MovingCubes/Buttons/PNG/` |
| **🎨 Farm UI elements** | `Assets/MovingCubes/Buttons/maanetorn/Farm Game UI - Simple 2D UI/` |
| **🎨 Cube visuals** | `Assets/MovingCubes/Materials/` |
| **🔧 Game objects** | `Assets/MovingCubes/Prefabs/` |
| **🔊 Sound effects** | `Assets/MovingCubes/Audio/` |

## 🎮 Game Features

- 🎚️ **Multiple Difficulty Levels** - Choose your challenge
- ⏱️ **Dynamic Timer System** - Race against time
- 🔢 **Sequential Gameplay** - Press cubes in numerical order
- 📊 **Real-time Score Tracking** - See your points as you play
- ⚠️ **Visual Feedback** - Cubes grow when time is running out
- 🎨 **Clean UI** - Easy-to-use interface with custom buttons
- 🔊 **Audio Feedback** - Sound effects for interactions

## 🛠️ Built With

- **Unity Engine 6000.0.23f1** - Game development platform
- **C# (.NET Standard 2.1)** - Programming language
- **TextMeshPro** - UI text rendering
- **Unity Asset Store Assets**:
  - Custom UI Buttons pack
  - Farm Game UI - Simple 2D UI pack

## 🎨 Third-Party Assets

This project uses the following assets from the Unity Asset Store:

### UI Button Pack
- **Location**: `Assets/MovingCubes/Buttons/PNG/`
- **Usage**: Game interface buttons
- **License**: Unity Asset Store Standard License

### Farm Game UI - Simple 2D UI
- **Location**: `Assets/MovingCubes/Buttons/maanetorn/Farm Game UI - Simple 2D UI/`
- **Usage**: UI panels, buttons, and interface elements
- **License**: Unity Asset Store Standard License
- **Includes**: 
  - Button sprites (128x128 and 256x256)
  - Custom fonts (Lilita One)
  - UI prefabs
  - Demo scene

All third-party assets are used in accordance with Unity Asset Store terms of use.

## 👥 Creators

Created with ❤️ by:
- **Yaara Yizchaki** - [@Yaara40](https://github.com/Yaara40)
- **Omer Cohen** - [@omercohen4](https://github.com/omercohen4)

## 🔧 For Developers

### Opening the Project

1. Clone this repository
2. Open Unity Hub
3. Click "Add" → Select the project folder
4. Open with Unity 2022.3 or newer
5. Wait for Unity to import all assets
6. Navigate to `Assets/MovingCubes/` 
7. Open **`MovingCubesGame.unity`** and press Play ▶️

### Main Script Overview

**`Cube.cs`** (`Assets/MovingCubes/Scripts/Cube.cs`)

The core game logic script that handles:
- Cube spawning and positioning
- Movement behavior
- Click event handling
- Number sequencing validation
- Visual feedback (scaling when time running out)
- Score calculation

### Project Organization

The project follows a clean structure with all game-related files organized within the `Assets/MovingCubes/` folder:

```
MovingCubes/
├── Audio/              → Sound effects
├── Buttons/            → UI assets
│   ├── PNG/           → Button images
│   ├── Scenes/        → Button demo scenes
│   └── maanetorn/     → Farm UI pack
├── Materials/          → Visual properties
├── Prefabs/            → Reusable objects
├── Scripts/            → Game logic
└── MovingCubesGame.unity → Main scene ⭐
```

This organization makes it easy to:
- ✅ Find any game component quickly
- ✅ Maintain and update the project
- ✅ Add new features without confusion
- ✅ Collaborate with team members

### Modifying the Game

**To change cube behavior:**
```
Edit: Assets/MovingCubes/Scripts/Cube.cs
```

**To adjust UI:**
```
Modify: Assets/MovingCubes/Buttons/ (prefabs and sprites)
```

**To edit the main scene:**
```
Open: Assets/MovingCubes/MovingCubesGame.unity
```

**To add new features:**
```
Create scripts in: Assets/MovingCubes/Scripts/
```

**To change visuals:**
```
Edit materials in: Assets/MovingCubes/Materials/
```

## 🔮 Future Features

If we had more time, we would add:
- ⚙️ **Settings Menu** - Adjust volume, difficulty, and controls
- 🏆 **High Score Table** - Track and display best scores
- 🎛️ **Custom Game Mode** - Manually define duration, cube count, and speed
- 📈 **Statistics Dashboard** - View detailed performance metrics
- 🎵 **Background Music** - Immersive audio experience
- 🌐 **Localization** - Support for multiple languages (Hebrew, English, etc.)
- 🎨 **Theme System** - Different visual styles to choose from
- 💾 **Cloud Save** - Save progress across devices
- 🎯 **Achievement System** - Unlock rewards for milestones
- 👥 **Multiplayer Mode** - Compete with friends

## 🎬 Demo & Presentation

Check out our game presentation: [Moving Cubes Game Presentation](Moving_cubes_game.pptx)

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

**Important**: Third-party assets from Unity Asset Store are subject to their respective licenses and are **not** included in this MIT license. These assets are for use within this project only.

## 🤝 Contributing

We welcome contributions! Here's how you can help:

1. 🍴 Fork the repository
2. 🌿 Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. 💾 Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. 📤 Push to the branch (`git push origin feature/AmazingFeature`)
5. 🎉 Open a Pull Request

**Contribution Ideas:**
- 🐛 Bug fixes
- ✨ New game features
- 🎨 UI/UX improvements
- ⚡ Performance optimizations
- 📚 Documentation improvements
- 🌐 Translations

## 📞 Contact & Support

- **Issues**: [GitHub Issues](https://github.com/Yaara40/MovingCubes/issues)
- **Creator**: [@Yaara40](https://github.com/Yaara40)

Have questions, found a bug, or have a suggestion? Feel free to open an issue!

## 🎉 Acknowledgments

Thank you for playing Moving Cubes! We hope you had fun! 🎮

**Special Thanks:**
- 🎮 Unity Technologies - For the amazing game engine
- 🎨 Unity Asset Store Creators - For the beautiful UI assets
- 🧪 All our playtesters - For valuable feedback
- 💖 Open source community - For inspiration and support

---

<div align="center">

### **Ready, Set, GO!** 🚀

**Play • Learn • Have Fun!**

[![GitHub stars](https://img.shields.io/github/stars/Yaara40/MovingCubes?style=social)](https://github.com/Yaara40/MovingCubes/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/Yaara40/MovingCubes?style=social)](https://github.com/Yaara40/MovingCubes/network/members)

</div>

---

### 📋 Quick Start Checklist

- [ ] Clone the repository
- [ ] Open in Unity Hub (Unity 2022.3+)
- [ ] Navigate to `Assets/MovingCubes/`
- [ ] Open `MovingCubesGame.unity`
- [ ] Press Play ▶️
- [ ] Have fun and challenge yourself! 🎮

---

**Made with 💙 by Yaara & Omer**
