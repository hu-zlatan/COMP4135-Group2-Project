# WR2 Process Evidence Package

This folder stores teacher-facing process evidence for WR2 and the final submission.

## Expected evidence types

- Canonical repo identity and branch evidence
- Commit / PR / review evidence from the real team remote
- Team board or kanban snapshots with representative task flow
- Notes explaining what is still blocked when the evidence is not available in this workspace
- Discovery artifacts showing what was checked from this workspace before escalation

## Rules

- If the local workspace cannot truthfully access a source, write a blocker note instead of inventing completeness.
- Name the expected owner and source system for every missing item.
- Keep the process package aligned with `wr2-asset-register.md`.
- Store imported captures under deterministic subdirectories so later review sync is mechanical.

## Current package

- `canonical-repo-evidence-2026-04-02.md`
- `task-board-evidence-2026-04-02.md`
- `canonical-repo/`
- `task-board/`
- `discovery/`

## Subdirectories

- `canonical-repo/`: imported screenshots or exports from the real team remote
- `task-board/`: imported screenshots or exports from the real team board
- `discovery/`: command outputs and source checks that explain why a stronger import is still blocked
