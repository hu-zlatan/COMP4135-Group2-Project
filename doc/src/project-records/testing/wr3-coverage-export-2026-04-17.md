# WR3 Coverage Export Record

- Date: 2026-04-17
- Purpose: capture the clean archived WR3 code-coverage export and the exact evidence paths used by the report

## Execution Setup

- The main Unity editor session already had the coursework project open, so an official batchmode coverage export could not target the same project path directly.
- To keep the coverage run on the same code and test state while using the official Unity coverage CLI, a mirrored workspace was created at `F:/unity/Unity-Project-clean-init-wr3-coverage/coursework`.
- All coverage artifacts were written back into the canonical repository under `doc/src/project-records/testing/coverage/2026-04-17/`.

## Archived Artifact Groups

| Artifact group | Path | Purpose |
| --- | --- | --- |
| EditMode rerun result | `doc/src/project-records/testing/coverage/2026-04-17/editmode-test-results.xml` | Named pass/fail record for the full 57-test EditMode suite |
| PlayMode rerun result | `doc/src/project-records/testing/coverage/2026-04-17/playmode-test-results.xml` | Named pass/fail record for the PlayMode smoke suite |
| Raw OpenCover XML | `doc/src/project-records/testing/coverage/2026-04-17/coursework-opencov/` | Per-test raw coverage results for EditMode and PlayMode |
| HTML coverage report | `doc/src/project-records/testing/coverage/2026-04-17/Report/index.html` | Human-readable report used for WR3 tables and figures |
| Summary exports | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md`, `Summary.json`, `Summary.xml` | Compact numeric summary for the report body |
| Additional report formats | `doc/src/project-records/testing/coverage/2026-04-17/Report/lcov.info`, `Cobertura.xml`, `SonarQube.xml` | Secondary machine-readable evidence exports |
| Coverage badges | `doc/src/project-records/testing/coverage/2026-04-17/Report/badge_*.svg`, `badge_*.png` | Visual summary assets for appendix / screenshots |
| Report history | `doc/src/project-records/testing/coverage/2026-04-17/Report-history/` | Historic entry created by the final combined report run |
| Batch logs | `doc/src/project-records/testing/coverage/2026-04-17/*coverage.log` | Command-line evidence for the three batchmode runs |

## Coverage Summary

| Metric | Value |
| --- | --- |
| Assemblies | 1 (`Assembly-CSharp`) |
| Classes | 11 |
| Files | 11 |
| Covered lines | 868 |
| Uncovered lines | 301 |
| Coverable lines | 1169 |
| Line coverage | 74.2% |
| Covered methods | 146 |
| Total methods | 158 |
| Method coverage | 92.4% |

## Class-Level Line Coverage

| Class | Line coverage | Covered / Coverable |
| --- | --- | --- |
| `TacticalCards.BattleUI` | 56.2% | 291 / 517 |
| `TacticalCards.CardButtonView` | 100% | 13 / 13 |
| `TacticalCards.CardData` | 95.6% | 22 / 23 |
| `TacticalCards.CardResolver` | 77.7% | 49 / 63 |
| `TacticalCards.DeckManager` | 100% | 73 / 73 |
| `TacticalCards.EnemyAI` | 100% | 27 / 27 |
| `TacticalCards.GameRoot` | 93.1% | 27 / 29 |
| `TacticalCards.GridManager` | 85.9% | 147 / 171 |
| `TacticalCards.TileView` | 93.3% | 70 / 75 |
| `TacticalCards.TurnManager` | 80.1% | 113 / 141 |
| `TacticalCards.UnitController` | 97.2% | 36 / 37 |

## Interpretation Used in WR3

- The coverage export confirms that WR3 is no longer arguing from test inventory alone; it now has archived quantitative execution evidence.
- Coverage is strongest on `DeckManager`, `EnemyAI`, `CardButtonView`, `GameRoot`, `TileView`, and `UnitController`, which aligns with the direct focused suites.
- `BattleUI` remains the lowest-covered runtime class at 56.2%, but it is no longer a marginally covered outlier: the class is now backed by 19 direct tests, clearer prompt/summary behaviour, and the PlayMode smoke path.
