<div align="center">

# 🌬️ Windmill Breath VR

### VR-Guided Breathing Experience

**Find your calm. Breathe with the windmill.**

<p align="center">
  <img src="https://img.shields.io/badge/Unity-2022.3-black?style=for-the-badge&logo=unity" alt="Unity">
  <img src="https://img.shields.io/badge/C%23-Game%20Logic-239120?style=for-the-badge&logo=csharp" alt="C#">
  <img src="https://img.shields.io/badge/VR-Meta%20Quest-blue?style=for-the-badge&logo=meta" alt="Meta Quest">
  <img src="https://img.shields.io/badge/Animation-Coroutines-purple?style=for-the-badge" alt="Coroutines">
</p>

</div>

---

## Table of Contents

- [About](#about)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [How It Works](#how-it-works)
- [Customization](#customization)
- [Key Scripts](#key-scripts)

---

## About

**Windmill Breath VR** is a mindfulness and meditation experience built for VR. The project features an animated 3D windmill that synchronizes with guided breathing exercises, creating a calming visual aid for breath control practice.

I built this for a client who needed a VR-based breathing exercise application. The windmill spins progressively faster during exhalation and gradually slows during relaxation phases, providing intuitive visual feedback that helps users maintain proper breathing rhythm.

---

## Features

| Feature | Description |
|---------|-------------|
| **🧘 Guided Breathing** | 4-phase breathing cycle: Inhale → Hold → Exhale → Relax |
| **🌪️ Animated Windmill** | Smooth rotation synchronized with breathing phases |
| **⏱️ Countdown Timer** | Visual countdown for each phase |
| **🔢 Round Tracking** | Track progress through multiple breathing rounds |
| **✨ Smooth UI** | Fade in/out text transitions for immersive experience |
| **⚙️ Customizable** | Adjustable durations for each breathing phase |
| **🎮 VR Ready** | Built for Meta Quest headsets |

---

## Tech Stack

| Component | Technology |
|-----------|------------|
| **Game Engine** | Unity 2022.3 LTS |
| **Programming** | C# |
| **VR Platform** | Meta Quest 2/3/Pro |
| **UI Framework** | Unity UI (uGUI) |
| **Animations** | Coroutine-based state machine |
| **3D Models** | Unity Primitives + Custom Materials |

---

## Project Structure

```
ProjectWindmill/
├── Assets/
│   ├── Scripts/
│   │   ├── BreathControl.cs      # Main breathing cycle controller
│   │   ├── Windmill.cs           # Windmill rotation logic
│   │   └── TextFader.cs          # UI text fade animations
│   ├── Materials/
│   │   └── WindmillTiles03_4K_BaseColor.mat
│   ├── Ground textures pack/     # Environment textures
│   └── Scenes/
│       └── SampleScene.unity
├── Packages/
│   └── manifest.json
└── ProjectSettings/
```

---

## Installation

### Prerequisites

- Unity 2022.3 LTS or later
- Meta Quest 2/3/Pro or compatible VR headset
- Oculus Desktop App

### Quick Start

```bash
# Clone the repository
git clone https://github.com/Mohit-Bagri/windmill-vr.git
cd windmill-vr/ProjectWindmill

# Open in Unity
# 1. Open Unity Hub
# 2. Click "Add Project"
# 3. Select the ProjectWindmill folder
```

### Build for VR

1. **Switch Platform:**
   - File → Build Settings → Android
   - Click "Switch Platform"

2. **XR Settings:**
   - Edit → Project Settings → XR Plug-in Management
   - Enable Oculus

3. **Build & Run:**
   - Connect Meta Quest via USB
   - Build Settings → Build and Run

---

## How It Works

The breathing exercise follows a structured 4-phase cycle:

### Breathing Phases

| Phase | Duration | Windmill Behavior |
|-------|----------|-------------------|
| **🫁 Inhale** | 3 seconds | Static - Prepare phase |
| **⏸️ Hold** | 2 seconds | Static - Breath retention |
| **💨 Exhale** | 4 seconds | **Accelerates** from 0 to max speed |
| **😌 Relax** | 2 seconds | **Decelerates** back to stop |

### Session Flow

1. **Get Ready** - 5-second countdown before session starts
2. **Breathing Rounds** - Complete customizable number of rounds (default: 5)
3. **Session Complete** - Final message displayed

### Visual Feedback

The windmill rotation speed directly corresponds to the exhalation phase:
- **Inhale/Hold** - Windmill is stationary
- **Exhale** - Windmill gradually speeds up (acceleration over 2s)
- **Relax** - Windmill smoothly slows down (deceleration over 2s)

This creates a calming visual metaphor: breathing out "powers" the windmill, encouraging complete and controlled exhalation.

---

## Customization

All breathing parameters can be adjusted via the Unity Inspector:

| Parameter | Default | Description |
|-----------|---------|-------------|
| `totalRounds` | 5 | Number of breathing cycles |
| `startingCountdown` | 5s | Preparation countdown before start |
| `inhaleDuration` | 3s | Duration of inhale phase |
| `holdDuration` | 2s | Duration of breath hold |
| `exhaleDuration` | 4s | Duration of exhale phase |
| `relaxDuration` | 2s | Duration of relaxation phase |

**Windmill Settings:**
| Parameter | Default | Description |
|-----------|---------|-------------|
| `maxSpeed` | 200 | Maximum rotation speed during exhale |
| `accelerationTime` | 2s | Time to reach max speed |

---

## Key Scripts

### [`BreathControl.cs`](ProjectWindmill/Assets/Scripts/BreathControl.cs)

The main controller that manages the breathing exercise flow using Unity coroutines for smooth state transitions.

```csharp
// Core breathing cycle coroutine
IEnumerator BreathCycle()
{
    while (currentRound < totalRounds)
    {
        currentRound++;
        
        // Inhale Phase
        yield return StartCoroutine(UpdateBreathPhase("Inhale", inhaleDuration));
        
        // Hold Phase  
        yield return StartCoroutine(UpdateBreathPhase("Hold", holdDuration));
        
        // Exhale Phase - Start windmill
        windmill.StartWindmill();
        yield return StartCoroutine(UpdateBreathPhase("Exhale", exhaleDuration));
        
        // Stop windmill gradually
        windmill.StopWindmill();
        
        // Relax Phase
        yield return StartCoroutine(UpdateBreathPhase("Relax", relaxDuration));
    }
}
```

### [`Windmill.cs`](ProjectWindmill/Assets/Scripts/Windmill.cs)

Handles smooth rotation with acceleration and deceleration curves.

```csharp
void Update()
{
    if (isExhaling)
    {
        // Gradually increase speed
        if (currentSpeed < maxSpeed)
            currentSpeed += (maxSpeed / accelerationTime) * Time.deltaTime;
    }
    else
    {
        // Gradually slow down
        if (currentSpeed > 0)
            currentSpeed -= (maxSpeed / accelerationTime) * Time.deltaTime;
    }
    
    // Apply rotation
    rotator.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
}
```

### [`TextFader.cs`](ProjectWindmill/Assets/Scripts/TextFader.cs)

Manages smooth UI text fade transitions using alpha interpolation.

```csharp
private IEnumerator FadeTextAlpha(float startAlpha, float endAlpha)
{
    float elapsedTime = 0f;
    while (elapsedTime < fadeDuration)
    {
        elapsedTime += Time.deltaTime;
        float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
        breathInstruction.canvasRenderer.SetAlpha(newAlpha);
        yield return null;
    }
}
```

---

<div align="center">

Made in 🇮🇳 with ❤️ by [MOHIT BAGRI](https://mohitbagri-portfolio.vercel.app)

⭐ **Star this repo if you found it helpful!** ⭐

</div>
