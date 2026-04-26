# WR3 Test Inventory

- Date: 2026-04-17
- Purpose: enumerate the final rerun-backed WR3 suite by file, module, and test count after the 2026-04-17 evidence freeze

## Suite Inventory

| Suite file | Mode | Primary module / flow | Test count | Checkpoint status |
| --- | --- | --- | --- | --- |
| `TurnManagerTests.cs` | EditMode | `TurnManager` | 3 | Verified by named rerun evidence |
| `DeckManagerTests.cs` | EditMode | `DeckManager` | 6 | Verified by named rerun evidence |
| `EnemyAITests.cs` | EditMode | `EnemyAI` | 4 | Verified by named rerun evidence |
| `UnitControllerTests.cs` | EditMode | `UnitController` | 6 | Verified by named rerun evidence |
| `GridManagerTests.cs` | EditMode | `GridManager` | 7 | Verified by named rerun evidence |
| `CardResolverTests.cs` | EditMode | `CardResolver` | 6 | Verified by named rerun evidence |
| `BattleUITests.cs` | EditMode | `BattleUI` scene/UI prompts, summaries, helper branches | 19 | Verified by named rerun evidence |
| `GameRootTests.cs` | EditMode | `GameRoot` scene bootstrap | 2 | Verified by named rerun evidence |
| `TileViewTests.cs` | EditMode | `TileView` | 3 | Verified by named rerun evidence |
| `CardButtonViewTests.cs` | EditMode | `CardButtonView` | 1 | Verified by named rerun evidence |
| `BattleFlowPlayModeTests.cs` | PlayMode | Battle start smoke path | 1 | Verified by named rerun evidence |

## Totals

| Metric | Value |
| --- | --- |
| EditMode suite files | 10 |
| PlayMode suite files | 1 |
| Total suite files | 11 |
| Total test methods | 58 |
| Verified test methods | 58 |
| Directly targeted runtime classes | 10 |
| Smoke / integration paths | 1 |

## Target Runtime Coverage

| Runtime class | Direct suite file | Covered at checkpoint |
| --- | --- | --- |
| `TurnManager` | `TurnManagerTests.cs` | Yes |
| `DeckManager` | `DeckManagerTests.cs` | Yes |
| `CardResolver` | `CardResolverTests.cs` | Yes |
| `GridManager` | `GridManagerTests.cs` | Yes |
| `EnemyAI` | `EnemyAITests.cs` | Yes |
| `UnitController` | `UnitControllerTests.cs` | Yes |
| `BattleUI` | `BattleUITests.cs` | Yes |
| `GameRoot` | `GameRootTests.cs` | Yes |
| `TileView` | `TileViewTests.cs` | Yes |
| `CardButtonView` | `CardButtonViewTests.cs` | Yes |

The scoped WR3 suite now has at least one direct verified test file for every targeted runtime class in the single-battle prototype, and `BattleUI` has been expanded to a 19-test direct suite rather than remaining a lightly covered outlier.
