# WR3 Execution Plan

- Date: 2026-04-17
- Purpose: freeze the active WR3 plan before further test expansion and report drafting
- Scope: testing, evidence packaging, and WR3 report production for the existing single-battle prototype

## Baseline

- WR3 starts from a real but narrow verified baseline: the existing EditMode rerun recorded in `prototype-accuracy-editmode-run-2026-04-02.md` passed `3/3` tests, all within `TurnManagerTests`.
- Additional test suites are being authored, but they must not be described as verified until Unity discovers and reruns them successfully.
- `doc/src/wr3-latex/` already contains the official template PDF, OCR/sample extracts, and the local scoring summary used to shape a table-heavy report.
- `doc/src/project-records/testing/` and `doc/src/project-records/process/` already provide the record structure required for WR3 evidence packaging.
- `com.unity.testtools.codecoverage` is now part of the Unity package manifest, but no final coverage export should be claimed until a successful rerun produces a usable artifact.

## WR3 Objective

WR3 is a testing and evidence cycle, not a feature-expansion cycle. The working objective is to convert the current gameplay vertical slice into a submission state that is:

1. directly tested across the main rules and manager classes;
2. traceable through execution logs, defect records, and coverage or coverage-like matrices;
3. reportable through dense tables and charts instead of long descriptive prose.

## Milestone Plan

| Milestone | Primary owner | Deliverables | Exit gate |
| --- | --- | --- | --- |
| M1. WR3 scaffold and evidence baseline | Guangjun HU | WR3 source tree, section skeleton, baseline records, requirements list, module list, test inventory | WR3 writing structure exists and the starting facts are frozen truthfully |
| M2. Core logic unit test expansion | Hongshuo HU | EditMode suites for `TurnManager`, `DeckManager`, `CardResolver`, `GridManager`, `EnemyAI`, and `UnitController` | Core rules have direct automated coverage |
| M3. Scene-coupled and integration testing | Zehan CHEN | Focused tests for UI/bootstrap classes plus at least one PlayMode smoke path | The main battle loop has an end-to-end automated checkpoint |
| M4. Metrics and evidence pack | Cheng YE | Coverage setup, execution statistics, matrices, chart data, defect summaries, and workflow evidence | Every major WR3 table and figure has a traceable raw source |
| M5. WR3 report draft | Guangjun HU | Full WR3 draft with charts, matrices, execution tables, and appendix evidence index | A complete WR3 PDF draft exists |
| M6. Final sync and scoring pass | All members, integrated by Guangjun HU | Final reruns, truth pass, formatting pass, reference check, and evidence consistency review | Report, appendix, and raw evidence are aligned and submission-ready |

## Scope Rules

- Keep gameplay scope fixed at the current single-battle vertical slice unless a defect fix requires a behavioural correction.
- Prefer tests and evidence work over new prototype features.
- Distinguish `Verified`, `Discovered`, and `Implemented` states explicitly in records and in the report.
- Only make code changes that improve testability when those changes do not alter gameplay semantics.
- Use `test` as the WR3 integration branch so report evidence and branch history remain easy to trace.

## Test Implementation Plan

### Direct coverage targets

- `TurnManager`
  - battle start with one side missing
  - card-play quota reset and consume rules
  - end-turn transition
  - enemy-turn skip / action summary
  - battle-end lock
- `DeckManager`
  - fallback deck initialisation
  - draw count
  - discard from hand
  - discard hand
  - reshuffle discard to draw
  - empty deck behaviour
- `CardResolver`
  - valid tile range by card type
  - move resolve
  - strike resolve
  - guard resolve
  - push resolve
  - dead-target occupancy clear
  - invalid/friendly target rejection
- `GridManager`
  - grid generation size
  - bounds checks
  - place/move/push rules
  - occupancy updates
  - neighbour lookup
  - breadth-first range results
  - highlight set and clear
- `EnemyAI`
  - nearest target selection
  - Manhattan distance
  - step direction priority
  - null/dead target skipping
- `UnitController`
  - initial HP
  - damage
  - guarding reduction
  - heal clamp
  - death deactivation
  - grid/world position sync
- `BattleUI`
  - select card without selected unit
  - guard self auto-resolve
  - invalid tile/unit handling
  - card-play consumption
  - post-battle lock
  - status/log updates
- `GameRoot` and smoke path
  - battle scene initialisation
  - manager wiring
  - unit registration
  - first playable turn start

### Coverage strategy

- Preferred path: generate line-coverage evidence through the installed Unity coverage package.
- Mandatory path even if coverage export fails:
  - requirements coverage matrix;
  - module/class test coverage matrix;
  - execution summary table with explicit status labels;
  - defect and retest records.

## WR3 Report Outputs

### Main-body tables and figures

- testing workflow / branch-review flow figure
- milestone and deliverable table
- test strategy matrix
- requirements coverage matrix
- module/class coverage matrix
- execution summary table
- line coverage chart or equivalent quantitative chart
- defect summary table
- root cause / fix / prevention table

### Appendix outputs

- representative test case tables by module
- test run records
- defect and retest log snapshots
- branch / PR / tag / repo workflow evidence references

## Acceptance Gates

- All committed automated tests must pass before final WR3 closeout.
- Every core runtime script must either have direct automated tests or an explicit integration/smoke coverage note.
- `Validation Results` must stay evidence-driven; unverified claims are not acceptable.
- The final WR3 report must be more table-heavy, chart-heavy, and data-heavy than WR2.

## Assumptions and Defaults

- No fixed course date is written here; milestones are relative checkpoints until the final deadline mapping is confirmed.
- Sample material under `doc/src/wr3-latex/` is used as a structure reference only, not as project content.
- Process evidence should be truthful even when incomplete; blocked or weak evidence must be labelled as such instead of being normalised into certainty.
