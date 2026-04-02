---
title: Tactical Cards Demo Architecture Constraints
date: 2026-03-15
---

# Tactical Cards Demo Architecture Constraints

## Goal

Build a playable vertical-slice demo as fast as possible.

The demo only needs to prove:

- grid-based tactical positioning
- card-driven actions
- turn flow
- basic enemy response
- clear visual feedback

This document is not a long-term production architecture. It is the minimum framework for a stable demo.

## Hard Constraints

- Use the existing Unity project at `F:/unity/Unity-Project/coursework`.
- Do not import whole reference projects into `coursework`.
- Do not copy `ProjectSettings`, `Packages`, full scenes, or framework-level tooling from reference repos.
- Prefer explicit code over generic abstraction.
- Prefer one battle scene over reusable campaign systems.
- Prefer button/click card interaction over drag-and-drop.
- Prefer one shared `UnitController` over separate player/enemy inheritance trees.
- Prefer enums and straightforward switch logic over card-effect class hierarchies.

## Demo Scope

### Included

- 1 battle scene
- 1 square grid map, `6x6` or `8x8`
- 2 player units
- 1 to 2 enemy types
- 4 cards for the first playable version:
  - `Move`
  - `Strike`
  - `Guard`
  - `Push`
- draw 3 cards per player turn
- play at most 2 cards per player turn
- player turn, enemy turn, win/lose check
- movement highlight
- attack range highlight
- movement animation
- attack feedback
- damage feedback

### Excluded For Now

- deckbuilding
- drag-and-drop hand interaction
- procedural maps
- multiple scenes or campaign flow
- progression systems
- status effect framework
- terrain effect framework
- large card pool
- generic ability system
- save/load
- multiplayer

## Architecture Principles

### 1. Single-Battle Bias

Everything is optimized for one self-contained battle scene.

### 2. Shallow Object Model

Avoid deep inheritance and avoid “future-proof” systems that the demo will not use.

### 3. Data-Light, Flow-Heavy

Keep data small and readable. Keep execution flow obvious and easy to debug.

### 4. One Responsibility Per Core Script

Each script should be understandable in one pass.

### 5. Presentation Separate From Rules

Grid logic, turn logic, and card resolution should not depend on animation timing or UI layout.

## Core Runtime Modules

### `GameRoot`

Responsibilities:

- scene bootstrapping
- wiring references between managers
- starting battle flow

Should not contain:

- card rules
- pathfinding
- AI decisions

### `GridManager`

Responsibilities:

- generate or register grid tiles
- convert grid coordinates to world positions
- return neighbors
- track occupancy
- validate movement and targetable tiles

Reference source:

- `TurnBasedStrategyGame/tileMapScript.cs`
- `TacticalBattleChess/.../Pathfinder.cs`

### `TileView`

Responsibilities:

- visual for a single tile
- selected state
- move highlight state
- attack highlight state

Should stay visual-only.

### `UnitController`

Responsibilities:

- position on grid
- team ownership
- current HP
- base attack
- move range
- guard state
- move animation
- take damage
- death handling

Use one controller for both player and enemy units.

### `TurnManager`

Responsibilities:

- start player turn
- request card draw
- track remaining playable cards
- end player turn
- run enemy turn
- trigger win/lose checks

Should not know card-specific logic beyond “a card was consumed”.

### `DeckManager`

Responsibilities:

- draw pile
- hand
- discard pile
- reshuffle discard into draw pile when needed
- draw cards
- consume cards

Should not execute card effects.

### `CardResolver`

Responsibilities:

- validate whether the selected card can be used
- determine valid targets
- execute the selected card effect

This is the only system that interprets `CardType`.

### `EnemyAI`

Responsibilities:

- choose nearest player unit
- move toward target if not in attack range
- basic attack if adjacent

Keep it deterministic and minimal.

### `BattleUI`

Responsibilities:

- show hand cards
- show current turn
- show remaining card plays
- show unit HP
- show end turn control

Should call manager methods, not own gameplay state.

## Minimal Data Model

### `CardType`

Use an enum:

- `Move`
- `Strike`
- `Guard`
- `Push`

### `TargetType`

Use an enum:

- `Self`
- `Tile`
- `EnemyUnit`

### `TeamType`

Use an enum:

- `Player`
- `Enemy`

### `CardData`

Use ScriptableObject or plain serializable data with only:

- `id`
- `cardName`
- `cardType`
- `targetType`
- `range`
- `power`
- `moveDistance`
- `description`

Do not add generic effect parameter bags.

### `UnitStats`

Keep only:

- `displayName`
- `team`
- `maxHp`
- `attack`
- `moveRange`

Runtime-only state belongs on `UnitController`.

## Card Resolution Rules

### `Move`

- target: reachable tile
- effect: move selected unit up to `moveDistance`

### `Strike`

- target: enemy unit within range
- effect: deal `power` damage

### `Guard`

- target: self
- effect: mark unit as guarding until next incoming hit or end of round

### `Push`

- target: adjacent enemy unit
- effect: deal small damage or zero damage, then push target back one tile if free

## Scene Composition

Create one battle scene with:

- `GameRoot`
- `GridManager`
- `TurnManager`
- `DeckManager`
- `CardResolver`
- `EnemyAI`
- `BattleUI`
- tile prefab
- unit prefabs

No service locator. No event bus unless a very small local event is clearly justified.

## Input Model

Use this sequence:

1. select unit
2. click card in hand
3. highlight legal targets
4. click tile or enemy
5. resolve card

Do not build drag-and-drop before the playable loop is stable.

## Recommended Folder Layout

Under `Assets/Scripts`:

- `Core/`
  - `GameRoot.cs`
  - `TurnManager.cs`
- `Grid/`
  - `GridManager.cs`
  - `TileView.cs`
- `Units/`
  - `UnitController.cs`
  - `EnemyAI.cs`
- `Cards/`
  - `CardData.cs`
  - `DeckManager.cs`
  - `CardResolver.cs`
- `UI/`
  - `BattleUI.cs`
  - `CardButtonView.cs`

Keep the folder structure shallow.

## Reference Repo Usage Rules

### Use From `TurnBasedStrategyGame`

- grid setup ideas
- node graph setup
- move/attack loop
- simple combat presentation

### Use From `TacticalBattleChess`

- path/range search ideas
- indicator refresh ideas

### Use From `CardHouse`

- only deck/hand/discard naming and setup ideas
- not for first implementation

### Use From `EmblemForge`

- only top-level module naming if useful

## Anti-Patterns

Do not do these in the demo phase:

- `IAction`, `ICardEffect`, `ITargetRule`, `IStatusSource` style abstractions
- generic command buses
- multi-scene bootstrap systems
- custom editor workflows
- object pooling before performance is proven to matter
- trying to make the card system reusable for future genres

## First Implementation Order

1. build grid and tile occupancy
2. place units and support click selection
3. implement turn flow
4. implement deck draw and hand display
5. implement `Move`
6. implement `Strike`
7. implement enemy turn
8. implement `Guard`
9. implement `Push`
10. add visual polish and demo-friendly feedback

## Success Condition

The architecture is good enough if:

- a new reader can understand the control flow quickly
- one battle can be played from start to finish
- adding one new card does not require redesigning core systems
- debugging a broken turn or card can be done from a small number of scripts

If those conditions are met, stop architecting and keep building.
