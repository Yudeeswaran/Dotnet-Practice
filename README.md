# 🎮 Game Character Ability System

## Project Overview
The **Game Character Ability System** is a console-based, turn-based game simulation built using **C# language fundamentals and Object-Oriented Programming (OOP) principles**.

---

## 🕹️ Project Description
This project simulates a **turn-based battle** between a player-controlled character and an automated enemy character.

The player can choose from multiple character types, each with:
- Unique stats
- Different attack styles
- A special ability

The enemy uses a **simple rule-based AI** to decide its actions based on health and available resources.

---

## Game Flow (Turn-Based)
1. Player selects a character type (Warrior / Mage / Archer)
2. Game enters a turn-based loop:
   - Player takes an action
   - Enemy responds automatically
3. Health and resources are updated after each turn
4. Game continues until one character is defeated

---

## Character Types

### Warrior
- High health
- Melee attacks
- Uses stamina-based special ability

### Mage
- Low health, high damage
- Uses mana
- Powerful magic-based special ability

### Archer
- Balanced stats
- Ranged attacks
- Limited arrows with multi-shot ability

---

## Enemy System
- Enemy characters use the same base structure as player characters
- Actions are decided using **simple conditional logic**
- No randomness-heavy or complex AI logic
- Designed for clarity and debuggability

---

## 📁 Project Structure

```text
GameCharacterAbilitySystem/
├── Program.cs
├── Core/
│   ├── GameEngine.cs
│   └── GameState.cs
├── Characters/
│   ├── GameCharacter.cs
│   ├── PlayerCharacter.cs
│   └── EnemyCharacter.cs
├── CharacterTypes/
│   ├── Warrior.cs
│   ├── Mage.cs
│   └── Archer.cs
├── Interfaces/
│   ├── ISpecialAbility.cs
│   └── IEnemyBehavior.cs
├── EnemyAI/
│   └── BasicEnemyAI.cs
├── Combat/
│   ├── AttackManager.cs
│   └── DamageCalculator.cs
├── Utilities/
│   ├── InputHandler.cs
│   └── DisplayManager.cs
└── Enums/
    ├── ActionType.cs
    └── DifficultyLevel.cs
```




