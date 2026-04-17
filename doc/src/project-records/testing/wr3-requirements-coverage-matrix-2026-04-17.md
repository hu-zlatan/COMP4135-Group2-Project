# WR3 Requirements Coverage Matrix

- Date: 2026-04-17
- Basis: WR1/WR2 single-battle vertical slice, current runtime code, and the archived 2026-04-17 rerun evidence

| Req ID | Requirement / expected behaviour | Primary evidence / test suites | Coverage status | Notes |
| --- | --- | --- | --- | --- |
| R-01 | The battle scene must initialise a single playable tactical battle. | `GameRootTests`, `BattleFlowPlayModeTests` | Verified | Confirmed by both direct bootstrap tests and the PlayMode smoke rerun. |
| R-02 | Units must register to the correct team and occupy valid starting tiles. | `GameRootTests`, `GridManagerTests` | Verified | Direct registration, placement, and occupancy checks all passed in the consolidated rerun. |
| R-03 | The player turn must reset card plays and draw a fresh hand each round. | `TurnManagerTests`, `DeckManagerTests` | Verified | Turn flow and deck-hand lifecycle are both backed by named reruns. |
| R-04 | Move cards must allow valid tile movement only. | `CardResolverTests`, `GridManagerTests` | Verified | Movement range, placement, and execution branches all passed. |
| R-05 | Strike cards must damage adjacent enemies and reject friendly targets. | `CardResolverTests`, `BattleUITests` | Verified | Direct resolver and UI-triggered strike behaviour are both rerun-backed. |
| R-06 | Guard cards must apply a temporary defensive state. | `CardResolverTests`, `UnitControllerTests`, `BattleUITests` | Verified | Guard resolution, guard consumption, and UI guard flow all passed. |
| R-07 | Push cards must damage the target and attempt knock-back. | `CardResolverTests`, `GridManagerTests` | Verified | Damage plus movement constraints are covered by passing rerun evidence. |
| R-08 | Enemy turns must target the nearest alive player and either move or attack. | `EnemyAITests`, `TurnManagerTests` | Verified | AI selection logic and turn-loop orchestration both have passing rerun evidence. |
| R-09 | Battle end must lock the loop and expose a consistent outcome state. | `TurnManagerTests`, `BattleUITests` | Verified | Earlier regression evidence was preserved and the broader rerun passed. |
| R-10 | The UI must prompt the player when unit/card/target selection is invalid. | `BattleUITests`, `CardButtonViewTests` | Verified | Direct UI prompt and button-binding suites both passed in the consolidated rerun. |

## Coverage Position

- `10/10` scoped WR3 requirements now have rerun-backed automated evidence.
- The report can now use `Verified` consistently for the scoped single-battle slice instead of mixing `Discovered` and `Implemented`.
