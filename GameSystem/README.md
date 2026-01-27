# 🎮 Turn-Based Two Player Game (C# / .NET)

This is a **console-based, turn-based two-player game** built using **C# and .NET**.  
The project is designed to practice **C# fundamentals and Object-Oriented Programming (OOP)** concepts in a simple and structured way.

---

## 🕹️ Project Overview
- Two-player (Player vs Player)
- Console-based application
- No AI or enemies
- Focused on learning **design, structure, and OOP concepts**

---

## 📁 Project Structure & Responsibility

GameSystem/  
├── Program.cs  
├── Core/  
│   ├── GameEngine.cs  
│   └── ICombatant.cs  
└── Characters/  
&nbsp;&nbsp;&nbsp;&nbsp;├── GameCharacter.cs  
&nbsp;&nbsp;&nbsp;&nbsp;└── PlayerCharacter.cs  

---

### 📌 Program.cs
- Entry point of the application  
- Creates the `GameEngine` object  
- Starts the game  

### 📌 GameEngine.cs
- Controls the overall game flow  
- Manages turn-based logic  
- Handles player actions (Attack, Heal, View Stats, Give Up)  
- Determines win conditions  

### 📌 ICombatant.cs
- Defines the combat behavior contract  
- Ensures any combat participant exposes `Attack` and `Health`  
- Helps decouple the game engine from concrete implementations  

### 📌 GameCharacter.cs (Abstract Class)
- Acts as a base template for all characters  
- Stores shared data such as name, health, and attack power  
- Implements common logic like attacking and taking damage  
- Cannot be instantiated directly  

### 📌 PlayerCharacter.cs
- Inherits from `GameCharacter`  
- Represents a playable character  
- Implements healing logic with limited usage  
- Provides concrete behavior for players  

---

## ▶️ How to Run
1. Open the project in Visual Studio  
2. Build the solution  
3. Run the application from `Program.cs`  
4. Follow the console instructions to play  
