
# Game Design One-Pager

## Project Title

**Tactical Cards**

## Genre

**Lightweight Tactical Grid-Based Strategy Game with Command Cards**

## One-Sentence Pitch

A lightweight turn-based tactics game where the player controls a small team on a grid map and uses a limited number of command cards each turn to move, attack, defend, and outmaneuver enemies.

## Target Audience

Teenagers and casual strategy players who enjoy tactical decision-making but prefer simple, readable rules over heavy and complex systems.

## Core Idea

The game combines elements of tactical grid combat and card-driven decision-making.
Unlike traditional card games, strategy does not rely on a large deck or complex card synergies.
Unlike traditional tactical RPGs, the game avoids heavy character classes, large maps, and complicated progression systems.

Instead, the player makes meaningful decisions each turn through:

* positioning on a small grid map
* selecting the right unit
* choosing which limited cards to use that turn
* managing timing, range, and enemy threats

## Core Design Goal

Create a strategy game that is:

* easy to understand
* small enough to complete within coursework scope
* visually and mechanically suited to Unity
* deep enough to show tactical decision-making without requiring a large amount of content

## Why This Project Fits Unity

Unity is used not just for UI, but for:

* grid-based map and tile interaction
* unit selection and movement visualization
* attack and skill animations
* hit effects and action feedback
* scene-based combat presentation
* integrating card UI with in-world character actions

This makes the project meaningfully different from a purely browser-based card game.

## Core Gameplay Loop

1. Start player turn
2. Draw cards from the deck
3. Select a unit on the battlefield
4. Play a limited number of command cards
5. Units perform movement, attacks, or special actions
6. End turn
7. Enemies act
8. Check victory or defeat condition
9. Repeat until battle ends

## Core Mechanics

### 1. Tactical Grid Combat

* The battlefield is a small square grid
* Units occupy tiles
* Positioning matters for attack range, safety, and tactical advantage

### 2. Limited Card Usage Per Turn

* The player draws a small number of cards each turn
* Instead of using an energy system, the player can only play a fixed number of cards per turn
* This keeps turns concise and readable

### 3. Command Cards

Cards represent tactical actions rather than a large collectible deck.
Examples include:

* Move
* Dash
* Strike
* Guard
* Push
* Heal
* Area Skill

### 4. Small Team Control

* The player controls 2–3 units
* Each unit has a basic role or specialty
* Enemy units have simple but distinct behaviors

## MVP Scope

The minimum playable version will include:

### Battlefield

* 1 small map, such as 6x6 or 8x8 grid

### Units

* 2 player-controlled units
* 2–3 enemy types

### Card System

* deck
* draw pile
* hand
* discard pile
* fixed number of playable cards per turn

### Card Content

* 8–12 cards total
* basic attack, defense, movement, and one or two special effects

### Combat Systems

* turn management
* movement range display
* attack range display
* basic enemy AI
* win/lose condition

### Feedback

* tile highlight
* card hover/selection
* movement animation
* attack animation
* damage feedback

## Intended Player Experience

The player should feel that:

* every turn offers a meaningful tactical choice
* actions are easy to understand
* the battlefield state is readable
* card use feels impactful because it directly drives character actions

## Features Deliberately Excluded

To keep the scope realistic, the project will not initially include:

* large maps
* procedural generation
* complex class systems
* large card pools
* relic systems
* branching campaigns
* multiplayer
* deep progression systems

## Example Starter Units

### Vanguard

* durable front-line unit
* good at melee attacks and protecting space

### Ranger

* ranged attacker
* better positioning and support options

## Example Starter Cards

* **Move**: move a selected unit by one tile range
* **Strike**: deal damage to an adjacent enemy
* **Guard**: reduce incoming damage for one turn
* **Dash**: reposition quickly
* **Push**: force an enemy back by one tile
* **Heal**: restore a small amount of HP

## Technical Structure Overview

The game is expected to include systems such as:

* `GameManager`
* `TurnManager`
* `GridManager`
* `Tile`
* `Unit`
* `PlayerUnit`
* `EnemyUnit`
* `DeckManager`
* `HandManager`
* `CardData`
* `CardEffect`
* `UIManager`

## Open-Source Reference Strategy

The project will reference existing open-source projects selectively:

* tactical/grid projects for map, tile, and turn structure
* card projects for hand display, drag interaction, and card data organization

However, the gameplay rules, project scope, combat flow, and command-card system will be independently designed for this project.

## Success Criteria

The project will be considered successful if it achieves:

* a complete playable battle loop
* clear tactical decision-making
* working card draw and limited-use card turns
* visible character movement and combat feedback
* a stable prototype suitable for testing, reporting, and demonstration

