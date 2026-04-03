# Tactical Cards Demo First Pass Scripts

## Purpose

This file translates `demo_architecture_constraints.md` into the first batch of scripts to actually create.

The first pass is not a full implementation. It is the smallest compilable gameplay skeleton that supports:

- grid ownership and occupancy
- unit placement and selection
- turn flow
- deck and hand state
- resolving four starter cards
- minimal enemy turn hooks

## First Pass Script List

### `Assets/Scripts/Core/GameRoot.cs`

Owns cross-system references and wires the battle scene together.

### `Assets/Scripts/Core/TurnManager.cs`

Owns turn state, remaining card plays, player/enemy turn transitions, and defeated-unit cleanup.

### `Assets/Scripts/Grid/GridManager.cs`

Owns tile registration, coordinate lookup, occupancy, neighbor queries, and tile validation.

### `Assets/Scripts/Grid/TileView.cs`

Owns one tile's coordinate and highlight state.

### `Assets/Scripts/Units/UnitController.cs`

Owns unit runtime state: team, HP, move range, attack, guarding, and tile occupancy.

### `Assets/Scripts/Units/EnemyAI.cs`

Provides a minimal deterministic enemy decision helper for the current unit.

### `Assets/Scripts/Cards/CardData.cs`

Defines card data as a small ScriptableObject with fixed starter fields.

### `Assets/Scripts/Cards/DeckManager.cs`

Owns draw pile, hand, discard pile, drawing, discarding, and reshuffling.

### `Assets/Scripts/Cards/CardResolver.cs`

Owns validation and execution for `Move`, `Strike`, `Guard`, and `Push`.

### `Assets/Scripts/UI/BattleUI.cs`

Acts as a very thin bridge between runtime state and future UI widgets.

### `Assets/Scripts/UI/CardButtonView.cs`

Represents one hand card button and forwards click events to `BattleUI`.

### `Assets/Scripts/Common/GameEnums.cs`

Stores `TeamType`, `CardType`, and `TargetType`.

## Call Flow

### Battle Setup

1. `GameRoot` finds `GridManager`, `TurnManager`, `DeckManager`, `CardResolver`, and `BattleUI`
2. `GameRoot` injects references
3. `TurnManager` starts the player turn
4. `DeckManager` draws opening cards
5. `BattleUI` refreshes the visible state

### Player Action

1. Player selects a `UnitController`
2. Player clicks a `CardButtonView`
3. `BattleUI` forwards selected card to `CardResolver`
4. `CardResolver` asks `GridManager` for valid targets
5. Player chooses a tile or enemy
6. `CardResolver` executes effect
7. `TurnManager` consumes one card play
8. `BattleUI` refreshes

### Enemy Action

1. `TurnManager` enters enemy phase
2. For each enemy unit, `EnemyAI` chooses move/attack
3. `CardResolver` or direct `UnitController` actions apply the result
4. `TurnManager` removes defeated units from the active lists
5. Control returns to player phase unless a later layer adds explicit end-state gating

## Intentional Omissions

The first pass should not include:

- map generation tools
- animation timelines
- full UI prefabs
- drag-and-drop hand interaction
- generic card effect framework
- status effect system
- save/load

## Definition Of Done For First Pass

The first pass is done when:

- all scripts compile
- a scene can host the required managers and unit prefabs
- the data model is stable enough to start wiring scene objects
- no major architecture redesign is needed to implement the starter cards

The current first pass does not yet require a polished victory or defeat screen.
