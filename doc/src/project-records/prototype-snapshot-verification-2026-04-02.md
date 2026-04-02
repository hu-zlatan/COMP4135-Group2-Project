# Prototype Snapshot Verification

**Date:** `2026-04-02`  
**Last updated:** `2026-04-03`  
**Owner:** `Hongshuo HU`  
**Suggested branch:** `docs/prototype-snapshot-verification`  
**Baseline checked against:** clean GitHub `develop` line after the prototype-accuracy fix and WR2 closeout sync

## Purpose

This note records which WR2 prototype claims are directly supported by the current Unity scene and runtime code, and which claims still need restrained wording in later report-closeout work.

## Snapshot scope checked

- scene wiring and startup flow
- grid generation and occupancy
- shared unit runtime model
- turn flow
- fallback deck and starter cards
- enemy response
- current UI feedback
- screenshot and UML assets selected to represent the snapshot

## Runtime alignment summary

### Architecture modules confirmed in code

- `GameRoot`
  - scene bootstrap, manager lookup, unit registration, battle startup
- `GridManager`
  - grid generation, coord-to-world conversion, occupancy, range lookup, tile highlighting
- `UnitController`
  - shared player/enemy runtime model using `TeamType`
- `TurnManager`
  - player turn start, draw, remaining card plays, enemy turn execution, defeated-unit cleanup
- `DeckManager`
  - draw pile, hand, discard pile, reshuffle, fallback starter deck
- `CardResolver`
  - explicit resolver for `Move`, `Strike`, `Guard`, `Push`
- `EnemyAI`
  - nearest-target selection, Manhattan distance, single-step movement
- `BattleUI`
  - unit selection, card selection, turn status, action log, hand display, move/attack highlights

### Gameplay claims checked

| Claim | Status | Evidence |
| --- | --- | --- |
| `Move` card works | confirmed | `CardResolver.ResolveTileCard`, `GridManager.TryMoveUnit` |
| `Strike` card works | confirmed | `CardResolver.ResolveUnitCard`, `ResolveStrike` |
| `Guard` card works | confirmed | `CardResolver.ResolveTileCard`, `UnitController.TakeDamage` |
| `Push` card works | confirmed | `CardResolver.ResolvePush`, `GridManager.TryPushUnit` |
| player/enemy turn loop exists | confirmed | `TurnManager.StartPlayerTurn`, `EndPlayerTurn`, `RunEnemyTurn` |
| enemy response exists | confirmed | `EnemyAI.FindNearestPlayer`, adjacent hit or one-step move |
| lightweight UI feedback exists | confirmed | `BattleUI` hand panel, top bar, log panel, highlight refresh |
| explicit victory/defeat end-state flow exists | confirmed in minimal form | `TurnManager` now stops the battle loop when one side is eliminated, and `BattleUI` surfaces top-bar/log outcome feedback |

## Wording guardrails for later WR2 closeout

- Safe to say the prototype demonstrates:
  - grid-based movement and range highlighting
  - a card-driven action loop with `Move / Strike / Guard / Push`
  - deterministic enemy response
  - lightweight runtime UI feedback for selection, hand state, and action logging
  - truthful battle-end termination with textual victory/defeat feedback
  - initial automated verification around the corrected battle-end flow
- Do not overclaim:
  - a polished victory screen
  - a full defeat/restart flow
  - a finished production UI stack
  - a content-complete combat system beyond the current starter-card snapshot

## Screenshot selection for this snapshot

- `coursework/Assets/Screenshots/tactical-cards-scene-inline.png`
  - best general board snapshot
- `coursework/Assets/Screenshots/battleui-play-check.png`
  - best hand and runtime UI snapshot
- `coursework/Assets/Screenshots/battleui-inputsystem-check.png`
  - useful supporting screenshot for runtime integration proof

If later WR2 packaging uses screenshots outside this set, they should be rechecked against the same code baseline before being described as the frozen prototype snapshot.

## UML and diagram assets to keep aligned

- `artifacts/uml/tactical_cards_demo_architecture.drawio`
- `artifacts/uml/tactical_cards_demo_architecture.svg`
- `artifacts/uml/tactical_cards_gameplay_loop.drawio`
- `artifacts/uml/tactical_cards_gameplay_loop.svg`
- `artifacts/uml/tactical_cards_unit_state_machine.drawio`
- `artifacts/uml/tactical_cards_unit_state_machine.svg`

These diagrams are still usable for WR2, but later report wording should reflect the current runtime truth:

- one shared `UnitController`
- explicit `CardResolver`
- deterministic `EnemyAI`
- lightweight `BattleUI`
- dedicated battle-end feedback now exists in `BattleUI`, but only as top-bar/log text rather than a separate result screen

## Outcome

- `docs/prototype-snapshot-verification` should stay a docs-only PR.
- The earlier battle-end accuracy gap was fixed in the merged `feat/prototype-accuracy-fixes` work, and WR2 wording should now describe that behaviour as implemented in minimal form rather than deferred.
- The remaining reporting guardrail is about scope and polish, not correctness: WR2 should describe battle-end handling as functional but lightweight, and should keep stronger claims for richer presentation, onboarding, and broader testing out of the current snapshot.
