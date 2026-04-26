# Post-WR3 M12 M13 Issue Log

- Date: 2026-04-24
- Purpose: capture the concrete problems discovered during the late visual pass and the fixes that closed them before commit

## Scope

- `M12` third slice:
  - tabletop ambience props
  - title and result presentation dressing
- `M13` HUD feedback completion:
  - title overlay cleanup
  - battle-state affordances
  - action-availability feedback
  - layout fixes discovered during manual smoke

## Problems found

### 1. Title overlay content collided

- Symptom:
  - the title summary, feature cards, and start button were competing for the same vertical space
  - the front page looked visually stacked on top of itself during manual smoke
- Root cause:
  - the overlay was themed, but its sections were still effectively laid out as one dense content block
- Fix:
  - split the title flow into `SummaryPanel`, `FeatureStrip`, and a separate `StartButton` region in `Assets/Scripts/Core/GameFlowController.cs`
  - reduce the amount of summary copy so the layout no longer depends on overflow luck
- Validation:
  - hierarchy assertions confirm the three regions exist in Play Mode
  - manual smoke confirmed the overlay is readable again

### 2. Battle HUD top-left prompts overlapped

- Symptom:
  - `Player Turn`, `Energy`, selected-unit text, and prompt copy rendered on top of each other in the top bar
- Root cause:
  - top-bar text rows were anchored into partially overlapping vertical bands
  - the old layout did not reserve a stable button strip width, so prompt text could intrude into the action-button zone
- Fix:
  - introduce explicit layout constants for the runtime HUD in `Assets/Scripts/UI/BattleUI.cs`
  - restack the top bar into four rows:
    - turn and energy
    - current selection
    - prompt title
    - prompt detail
  - reserve a fixed right-side width for `Clear` and `End Turn`
- Validation:
  - `BattleUITests.AttachRuntimeUi_StacksTopBarRowsAndKeepsHandCompact`
  - manual smoke confirmed the overlap no longer reproduces

### 3. Bottom hand dock covered lower units at battle start

- Symptom:
  - the first hand render obscured units near the lower edge of the board
- Root cause:
  - the themed hand dock was too tall for the current camera framing
  - runtime cards were sized more like a showcase panel than an in-battle HUD
- Fix:
  - reduce bottom dock height in `Assets/Scripts/UI/BattleUI.cs`
  - shrink runtime card cell width and height
  - tighten the internal hand label and card-area offsets so the board keeps a bottom safe zone
- Validation:
  - the new layout guard test checks the compact hand dock and runtime card height
  - manual smoke confirmed the starting units remain visible

### 4. Action availability was hidden in the log

- Symptom:
  - players only learned that cards were unusable by reading the action log
  - health state also required scanning text instead of reading the board
- Root cause:
  - the prototype had battle logic, but the themed HUD still lacked direct affordance surfaces
- Fix:
  - add `Energy`, `PromptTitle`, `PromptDetail`, disabled-card overlays, and world-space health bars
  - extend `BattleHudSnapshot` and `CardButtonView` so the HUD can express playable and non-playable states directly
- Validation:
  - EditMode tests cover disabled-card reasons, world health bars, and prompt generation
  - smoke confirmed the feedback is visible without relying on the log

### 5. Debug battle flow bypassed the real HUD path

- Symptom:
  - direct battle testing could skip the front-end flow and miss runtime HUD regressions
- Root cause:
  - `disableFrontEndFlow` previously short-circuited battle startup before the shared flow path built the same UI tree
- Fix:
  - route debug/direct start through the same `GameFlowController` initialization path in `Assets/Scripts/Core/GameRoot.cs`
- Validation:
  - editor tests confirm direct-start mode still builds the runtime HUD before battle begins

### 6. Overlay screenshots were not trustworthy through Unity MCP alone

- Symptom:
  - captured images often showed the world layer but omitted `Screen Space Overlay` UI
- Root cause:
  - the MCP screenshot path was reliable for world rendering but inconsistent for overlay verification in this project setup
- Fix:
  - treat overlay composition as a manual smoke responsibility
  - use automated hierarchy assertions to verify runtime UI structure when screenshots are incomplete
- Validation:
  - smoke records explicitly separate automated evidence from manual visual confirmation

## Files that closed the issues

- Runtime code:
  - `coursework/Assets/Scripts/Core/GameFlowController.cs`
  - `coursework/Assets/Scripts/Core/GameRoot.cs`
  - `coursework/Assets/Scripts/Core/TurnManager.cs`
  - `coursework/Assets/Scripts/Grid/GridManager.cs`
  - `coursework/Assets/Scripts/Presentation/WorldThemeResources.cs`
  - `coursework/Assets/Scripts/UI/BattleHudModels.cs`
  - `coursework/Assets/Scripts/UI/BattleUI.cs`
  - `coursework/Assets/Scripts/UI/CardButtonView.cs`
  - `coursework/Assets/Scripts/UI/UnitHealthBarView.cs`
- Tests:
  - `coursework/Assets/Tests/Editor/BattleUITests.cs`
  - `coursework/Assets/Tests/Editor/CardButtonViewTests.cs`
  - `coursework/Assets/Tests/Editor/GameFlowControllerTests.cs`
  - `coursework/Assets/Tests/Editor/GameRootTests.cs`
  - `coursework/Assets/Tests/Editor/GridManagerTests.cs`
- Supporting records:
  - `doc/src/project-records/testing/post-wr3-ambience-smoke-2026-04-24.md`
  - `doc/src/project-records/testing/post-wr3-hud-feedback-smoke-2026-04-24.md`

## Final verification state

- EditMode: `81/81 passed`
- PlayMode: `1/1 passed`
- Manual smoke:
  - title overlay readable
  - battle HUD stable
  - unit selection and card flow readable
  - lower-board units no longer hidden by the opening hand panel
