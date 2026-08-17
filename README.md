# 🧱 Data Persistence Breakout Project

This repository contains a simple Breakout-style game developed in Unity. The primary focus of this project is to demonstrate **scene flow management** and **data persistence** (both between scenes and between application sessions) as part of the [Unity Junior Programmer Pathway](https://learn.unity.com/pathway/junior-programmer).

## ✨ Key Features

* **Start Menu Scene:** A dedicated menu scene where the player can input their name before starting the game.
* **Data Persistence Between Scenes:** The player's name is preserved and transferred from the Start Menu to the Main Game scene using a static `GameData` class.
* **Data Persistence Between Sessions:** The High Score and the name of the record holder are saved to a local JSON file (`savefile.json`) using Unity's `JsonUtility`. The data survives application restarts.
* **Dynamic UI Updates:** 
  * The current score and player name are displayed together during gameplay (e.g., `Score : 15 | Alex`).
  * If the high score is beaten during a session, the UI immediately updates to reflect the new record and the current player's name.
* **New Input System Integration:** The project has been migrated to use Unity's New Input System (`InputAction`) for handling player inputs (e.g., launching the ball with the Spacebar).

## 🛠️ Technical Implementation

* **Scene Flow:** Managed via `UnityEngine.SceneManagement`. The `StartMenu` captures user input, stores it in a static variable, and loads the `Main` scene.
* **Session Saving:** The `MainManager` script utilizes a `[System.Serializable] SaveData` class. On `GameOver()`, if the current score exceeds the saved high score, the data is serialized to JSON and written to `Application.persistentDataPath`.
* **Singleton / Static Patterns:** Used appropriately to ensure data survives scene transitions without relying on `DontDestroyOnLoad` for simple variables.

## 🎮 How to Play

1. **Start:** Enter your name in the Start Menu and click the "Start" button.
2. **Launch:** Press `Space` to launch the ball.
3. **Move:** Use the `Left` and `Right` arrow keys (or your configured input) to move the paddle.
4. **Objective:** Break all the bricks to earn points. Don't let the ball fall!
5. **High Score:** If you beat the current Best Score, your name and score will be saved as the new record for future sessions.

## 📂 Project Structure

* `Assets/Scenes/StartMenu.unity` - The initial scene for player name input.
* `Assets/Scenes/Main.unity` - The core gameplay scene.
* `Assets/Scripts/MainManager.cs` - Handles game logic, brick spawning, scoring, and JSON save/load operations.
* `Assets/Scripts/MenuUI.cs` - Handles Start Menu UI interactions and scene loading.
* `Assets/Scripts/GameData.cs` - A static class used to pass the player's name between scenes.

## 🚀 How to Run

1. Clone this repository to your local machine.
2. Open the project folder using **Unity Hub** (Unity version 6000.0 LTS or newer is recommended, as the project uses the New Input System).
3. Open the `StartMenu` scene located in `Assets/Scenes/`.
4. Press the **Play** button in the Unity Editor.

---
*Project created as a submission for the "Manage Scene Flow and Data" unit in the Unity Junior Programmer Pathway.*
