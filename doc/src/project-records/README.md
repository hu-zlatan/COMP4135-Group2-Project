# WR2 Project Records

This directory keeps the minimum record structure needed to support WR2 now and WR3/final report later.

## Freeze line

- WR2 evidence freeze line: `2026-04-02 12:00`
- After the freeze line, only submission-critical fixes should be described in WR2.
- Any new features added after the freeze line should be moved into WR3 or the final report instead of being backfilled into WR2.

## Subdirectories

- `meetings/`: formal minutes templates and any recovered implementation notes
- `decisions/`: major scope, architecture, UI, and process decisions
- `process/`: canonical repo / board evidence packages or explicit blockers
- `process/canonical-repo/` and `process/task-board/`: deterministic import slots for teacher-visible screenshots or exports, plus explicitly labelled retrospective explanatory artifacts when needed
- `process/discovery/`: command outputs that explain why stronger external evidence is still blocked from this workspace
- `validation/`: walkthroughs, playtest notes, and requirement validation findings
- `testing/`: test prep, bug tracking, retest notes, and WR3 test-plan groundwork

## Key files

- `wr2-todo.md`: explicit WR2 work breakdown with priorities and package suggestions
- `wr2-todo-zh.md`: Chinese version of the WR2 work breakdown for team assignment
- `wr2-asset-register.md`: evidence register, missing items, suggested owners, and due times
- `team-assignment-pr-plan-2026-04-02.md`: current member allocation, per-person TODOs, and recommended PR sequence
- `phase-collaboration-dependency-map-2026-04-02.md`: phase-by-phase parallelization, dependency, and blocking map for team coordination
- `git-pr-collaboration-policy-2026-04-02.md`: the current clean-worktree, PR-only merge flow, and review thresholds for docs vs code
- `prototype-snapshot-verification-2026-04-02.md`: runtime-to-document alignment check for the current playable prototype snapshot
- `pr-submit-cli-workflow.md`: one-command team PR submission workflow using `gh` with profile checks and review-policy gates
- `collaboration-history-2026-04-02.md`: concise note explaining the move from early WeChat/Overleaf coordination to the clean GitHub PR workflow
- `wr2-snapshot-freeze-2026-04-02.md`: the named WR2 snapshot and freeze-scope contract
- `process/project-implementation-timeline-gantt.drawio`: source for the WR2 implementation timeline Gantt chart
- `process/task-board/retrospective-wr2-closeout-board.png`: explanatory reconstruction of the WR2 closeout board from recorded TODO lists and merged PR history
- `validation/wr2-heuristic-walkthrough-2026-04-01.md`: the current WR2 design-validation walkthrough
- `testing/prototype-accuracy-editmode-run-2026-04-02.md`: the initial automated verification note for the battle-end accuracy fix

## Status labels

- `Required now`: still needed to close WR2 from the frozen snapshot
- `Blocked externally`: depends on the canonical repo, team board, or confirmation outside this workspace
- `Deferred later`: useful after WR2, but not part of the minimum Phase 1 closeout

## Local evidence path audit

- Last local path audit: `2026-04-03`
- Verified local evidence roots:
  - `coursework/Assets/Screenshots/`
  - `artifacts/uml/`
  - `doc/src/project-records/`
- Current GitHub repository and PR history are now visible from this workspace.
- Stronger task-board captures and confirmed attendance evidence may still live outside this workspace until explicitly captured.
- Discovery artifacts about the earlier blocked state are stored under `doc/src/project-records/process/discovery/`.

## Usage

- Keep records short and specific.
- Record what actually happened; do not invent missing evidence.
- If a detail must be reconstructed from dated artifacts, mark it as `recovered` and name the source artifacts.
