# WR3 Sample Reconstructed Tables

This file contains the table-heavy parts of the WR3 sample reorganized into Markdown tables.
It is based on OCR from the photos plus manual cleanup where the table structure was still clear.
Cells marked `[unclear]` could not be recovered confidently from the source photos.

## Table 4.1 — Requirements Coverage Matrix

| Req. ID | Description | Test Case(s) | Status |
| --- | --- | --- | --- |
| R-1 | Food processing logic | `FakeFoodTests`, `FoodTests` | Covered (80%) |
| R-2 | Instruction menu functionality | `InstructionMenuTests` | Covered (100%) |
| R-3 | Pause & Exit manager functionality | `PauseExitManagerTests`, `TestPauseExitManager` | Partially covered (50%) |
| R-4 | Pause manager functionality | `PauseManagerTests` | Partially covered (71.2%) |
| R-5 | Score manager functionality | `ScoreManagerTests` | Covered (100%) |
| R-6 | Snake gameplay mechanics | `SnakeTests` | Covered (100%) |
| R-7 | Start menu navigation | `StartMenuManagerTests` | Covered (100%) |
| R-8 | Welcome menu functionality | `WelcomeMenuTests` | Covered (100%) |
| R-9 | Overall game integration scenarios | `GameIntegrationTests`, `MenuIntegrationTest`, `PauseManagerIntegrationTest` | Covered (100%) |

## Table 5.2 — Milestones and Deadlines

| Milestone | Deadline |
| --- | --- |
| Weekly Report 1 / Requirement Specification | `[unclear]` March 2025, 5 p.m. |
| Team Project Website | 21st of April 2025, 5 p.m. |
| Final Software Submission | 21st of April 2025, 5 p.m. |
| Final Group Report | 21st of April 2025, 5 p.m. |
| Demonstration Video | 21st of April 2025, 5 p.m. |
| Presentation | 22nd of April 2025, 5 p.m. |

## Appendix Test Case Tables — Food / FakeFood / Integration

| Test | Inputs | Expected Outcome | Result | Notes |
| --- | --- | --- | --- | --- |
| `TestRandomizePositionAndColor_UpdatesPositionColorAndText` | `N/A` | Food color should update and remain red or green | Fail | OCR shows `false`; row appears across pages 37 and 40 |
| `TestStart_WithValidDependencies_CallsRandomizePositionAndColor` | `N/A` | `SpriteRenderer` and `AudioSource` initialize correctly; color update is triggered | Fail | OCR shows code snippet and failing color validation on page 38 |
| `OnTriggerEnter2D_NonPlayerCollision_DoesNothing` | Non-player collider hits `fakeFood` | Error panel stays inactive; score remains unchanged | `[unclear]` | Page 42 preserves the row, but the OCR result field is noisy |

## Appendix Test Case Tables — Snake

| Test | Inputs | Expected Outcome | Result | Notes |
| --- | --- | --- | --- | --- |
| `Snake_Grow_AddsSegment` | Grow once | Segment count increases by 1 | Pass | Page 43 |
| `Snake_Grow_AddsSegment_AtCorrectPosition` | Grow once at tail | New segment is placed at the previous tail position `(5, 5, 0)` | Pass | Page 44 |
| `TestResetState_ClearsSegmentsAndResetsPosition` | Call `ResetState()` | Only the head remains and its position resets to `(0, 0, 0)` | Pass | Page 44 |
| `TestOnTriggerEnter2D_Food_CallsGrow` | Collision with food | Snake grows by 1 segment | Pass | Page 44 |
| `TestOnTriggerEnter2D_Obstacle_CallsResetState` | Collision with obstacle | Snake resets to initial state | `[unclear]` | Mentioned at page 44 but the outcome cell is cut off in the photo |

## Appendix Test Case Tables — Instruction / Menu

| Test | Inputs | Expected Outcome | Result | Notes |
| --- | --- | --- | --- | --- |
| `Instruction.TestInitialState` | Initial launch | `startMenuPanel` active; `gameParent` inactive | Pass | Page 45 |
| `Instruction.TestStartButtonClick` | Click Start button | `startMenuPanel` becomes inactive; `gameParent` becomes active | Pass | Page 45 |
| `StartMenuManager.TestInitialState` | Initial launch | Start menu visible and game UI hidden | Pass | Page 45 |
| `TeststartMenuPanel` | Click through start menu flow | `startMenuPanel` hidden and `gameParent` active | Pass | Page 46 |
