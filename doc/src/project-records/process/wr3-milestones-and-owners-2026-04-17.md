# WR3 Milestones and Owners

- Date: 2026-04-17
- Purpose: convert the WR3 working plan into report-ready milestones and ownership checkpoints

| Milestone | Primary owner | Deliverables | Exit gate |
| --- | --- | --- | --- |
| M1. WR3 scaffold and evidence baseline | Guangjun HU | WR3 report source tree, baseline tables, requirements list, module list, test inventory | WR3 source compiles and the baseline is frozen truthfully |
| M2. Core logic unit test expansion | Hongshuo HU | Systematic EditMode suites for `TurnManager`, `DeckManager`, `CardResolver`, `GridManager`, `EnemyAI`, `UnitController` | Core rules have direct automated coverage |
| M3. Scene-coupled and integration testing | Zehan CHEN | Focused UI / bootstrap tests plus at least one PlayMode smoke path | The main battle loop has an end-to-end automated checkpoint |
| M4. Metrics and evidence pack | Cheng YE | Coverage setup, execution statistics, matrices, bug summary, workflow figure, chart data | All report tables and figures have traceable raw inputs |
| M5. WR3 report draft | Guangjun HU | Full WR3 draft with heavy table / chart emphasis and appendix test tables | One complete PDF draft exists |
| M6. Final sync and scoring pass | All members, Guangjun HU integrating | Final reruns, truth pass, formatting pass, evidence consistency check | The report is submission-ready and evidence-consistent |

## Sequence and Dependency Rules

- M1 must be frozen before the remaining milestones can claim evidence against a stable baseline.
- M2 and M3 can overlap, but M3 must not claim end-to-end confidence until the relevant rule-level tests from M2 are in place.
- M4 depends on the outputs of M2 and M3 because matrices, execution counts, and defect summaries need real test artefacts.
- M5 depends on M1-M4 because the WR3 report is expected to cite actual evidence rather than placeholders.
- M6 is the only milestone allowed to convert `Implemented` work into `Verified` work after reruns and consistency checks.

## Expected Completion Evidence

| Milestone | Minimum evidence on disk | Repository evidence expectation |
| --- | --- | --- |
| M1 | `doc/src/project-records/testing/wr3-baseline-and-gap-2026-04-17.md`, `wr3-test-inventory-2026-04-17.md` | Initial WR3 branch activity and report scaffold commits |
| M2 | Expanded EditMode suites under `coursework/Assets/Tests/Editor/` | Commits showing rule-focused test additions and any testability-only refactors |
| M3 | UI/bootstrap tests plus PlayMode smoke tests under `coursework/Assets/Tests/` | Commits or PRs showing scene-coupled test work |
| M4 | Updated testing/process records and chart-ready data | Commits touching records, package/tooling, and report inputs |
| M5 | WR3 LaTeX chapter and appendix drafts under `doc/src/wr3-latex/` | Commits producing a full report draft |
| M6 | Final rerun logs, final truth pass updates, and final WR3 PDF | Final integration commits and optional milestone tags |

## Operating Rules

- `test` is the integration branch for WR3 work.
- Milestone completion should be evidenced through branch history, commits, and where appropriate tags such as `wr3-m1` to `wr3-m6`.
- The report must prioritise evidence density and testing traceability over narrative length.
- Use the full working plan in `wr3-execution-plan-2026-04-17.md` as the canonical statement of scope, coverage targets, and acceptance rules.
