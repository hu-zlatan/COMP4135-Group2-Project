# WR2 Todo List

This is the explicit work breakdown for WR2 and the minimum documentation foundation needed for WR3/final report.

Migration note:
This TODO is imported from the WR2 closeout workspace.
Checked items represent the recorded WR2 closeout status from that snapshot, not a claim that every referenced WR2 packaging file is already present in this clean `develop` line.

## Priority Legend

- `P0`: blocks WR2 submission
- `P1`: strongly recommended before submission
- `P2`: can continue after WR2 if time is tight

## Status Labels

- `Required now`: needed to close WR2 from the frozen snapshot
- `Blocked externally`: depends on evidence or confirmation outside this workspace
- `Deferred later`: useful later, but not part of the minimum WR2 closeout

## W0 Control and Freeze

### `P0` Evidence control

- [x] Create WR2 source tree under `doc/src/wr2-latex/`
- [x] Create project record structure under `doc/src/project-records/`
- [x] Create WR2 asset register
- [x] Set WR2 evidence freeze line to `2026-04-02 12:00`
- [ ] [Blocked externally] Collect missing external evidence from the canonical team repo
- [ ] [Blocked externally] Collect missing external evidence from the team board / kanban

### `P1` Team sync

- [ ] [Required now] Run a 15-minute sync to confirm which claims in WR2 are fully evidenced
- [ ] [Required now] Assign owners for the remaining missing evidence items
- [x] Confirm which prototype snapshot is the final WR2 snapshot

## W1 WR2 Source Rebuild

### `P0` Document skeleton

- [x] Replace the incorrect WR1-style WR2 source with a real WR2 LaTeX structure
- [x] Create proper sections for:
  - [x] Specification and Design Decisions
  - [x] Prototyping Process
  - [x] Implementation Strategy
  - [x] Summary and Reflections
  - [x] Reference
  - [x] Meeting Minutes / Appendix
- [x] Add stable `media/`, `figures/`, `appendices/`, and `references.bib`
- [x] Compile the WR2 PDF successfully

### `P1` Final polish

- [ ] [Required now] Do one final content proofread by someone who did not write the section
- [ ] [Required now] Confirm all names, dates, and module references match the team's final wording

## W2 Requirements Validation

### `P0` Scope validation

- [x] Extract core WR1 promises from the existing WR1 report
- [x] Summarise WR1 to WR2 requirement validation
- [x] Record which features are retained
- [x] Record which features are simplified
- [x] Record which features are deferred

### `P1` Evidence tightening

- [ ] [Blocked externally] Replace any weak wording if the team has stronger repo or playtest evidence
- [ ] [Required now] Confirm that no WR2 paragraph claims a feature that is not present in the chosen prototype snapshot

## W3 Architecture and System Diagrams

### `P0` Required figures

- [x] Add an updated architecture diagram matching the current code modules
- [x] Add a simplified module/class responsibility diagram
- [x] Ensure the diagrams match:
  - [x] `GameRoot`
  - [x] `TurnManager`
  - [x] `GridManager`
  - [x] `DeckManager`
  - [x] `CardResolver`
  - [x] `UnitController`
  - [x] `EnemyAI`
  - [x] `BattleUI`

### `P1` Cross-check

- [ ] [Required now] Have the main Unity implementer confirm the diagrams still match the current scene/code

## W4 Prototype Evidence

### `P0` High-fidelity evidence

- [x] Collect current battle UI screenshot
- [x] Collect current board screenshot
- [x] Include prototype screenshots in WR2
- [x] Write the prototype formation narrative

### `P0` Low-fidelity evidence

- [x] Add a retrospective low-fidelity wireframe
- [x] Mark it clearly as retrospective rather than original archived evidence

### `P1` Feature-state clarity

- [x] Summarise implemented / partial / deferred prototype features
- [ ] [Deferred later] If the team has a better screenshot set, replace the current images before final export

## W5 Design Validation

### `P0` Baseline validation

- [x] Create a validation log template
- [x] Record a WR2 heuristic walkthrough
- [x] Cover at least these scenarios:
  - [x] first interaction and unit selection
  - [x] play a card and confirm target/feedback
  - [x] end turn and observe enemy response
- [x] Extract at least 3 real findings

### `P1` Stronger validation

- [ ] [Deferred later] Run a real team playtest and append it to the validation log
- [ ] [Deferred later] Replace any heuristic-only claim with user-backed evidence if available

## W6 Implementation Strategy

### `P0` Code structure

- [x] Add the current script directory tree
- [x] Explain folder responsibilities
- [x] Add the simplified dependency/responsibility figure

### `P0` Key decisions

- [x] Document the single-battle vertical slice decision
- [x] Document the shared `UnitController` decision
- [x] Document the `enum + CardResolver` decision
- [x] Document the deterministic `EnemyAI` decision
- [x] Document the current UI/input implementation choice
- [x] Document the need for external repo/board evidence

### `P1` External proof

- [ ] [Blocked externally] Insert actual canonical repo screenshots if you want stronger process marks
- [ ] [Blocked externally] Insert task-board screenshots if you want stronger planning/kanban evidence

## W7 Summary, Appendix, and WR3 Foundation

### `P0` Report closing sections

- [x] Update implementation-phase division of labour
- [x] Write the completed process summary
- [x] Write the future plan aimed at WR3
- [x] Add appendix content instead of leaving the WR2 appendix empty

### `P0` Records foundation

- [x] Add `meeting minutes` template
- [x] Add `decision log` template
- [x] Add `validation log` template
- [x] Add `test / bug log` template
- [x] Add a recovered implementation checkpoint note
- [x] Add a WR3 prep testing note

### `P1` Replace recovered evidence where possible

- [ ] [Blocked externally] Replace recovered implementation checkpoint with confirmed team attendance if available
- [ ] [Blocked externally] Add at least one formal implementation-stage meeting record if the team has raw notes or chat history

## W8 Final Review and Submission

### `P0` Submission checks

- [x] Build the WR2 PDF
- [x] Run BibTeX and rebuild references
- [x] Confirm the title is Weekly Report 2, not Weekly Report 1
- [x] Confirm the chapter names match the WR2 template structure
- [x] Confirm bibliography is not empty
- [x] Confirm appendix exists

### `P1` Pre-submission checks

- [ ] [Required now] Review all claims against evidence one more time
- [ ] [Required now] Confirm all figure captions and section wording are final
- [ ] [Required now] Export and archive the final PDF that will actually be submitted
- [ ] [Deferred later] Keep the LaTeX source and the supporting record files together for later reuse

## Recommended Team Packages

### Package A: document integration

- [ ] [Required now] Final wording pass on `wr2-latex`
- [ ] [Required now] Final PDF export check
- [ ] [Required now] Final evidence consistency check

### Package B: requirements and project management

- [ ] [Required now] Confirm WR1 to WR2 validation wording
- [ ] [Required now] Confirm division of labour wording
- [ ] [Blocked externally] Replace recovered meeting details if evidence exists

### Package C: architecture and implementation

- [ ] [Required now] Verify the architecture and module diagrams
- [ ] [Required now] Verify implementation strategy claims against the current code
- [x] Confirm the chosen prototype snapshot

### Package D: validation and process evidence

- [ ] [Deferred later] Run one more focused playtest if possible
- [ ] [Blocked externally] Gather canonical repo screenshots
- [ ] [Blocked externally] Gather task-board screenshots
- [ ] [Deferred later] Update validation/testing logs with any late findings

## Where To Look

- Main WR2 source: `doc/src/wr2-latex/`
- Main WR2 PDF: `doc/src/wr2-latex/main.pdf`
- Asset register: `doc/src/project-records/wr2-asset-register.md`
- This todo list: `doc/src/project-records/wr2-todo.md`
