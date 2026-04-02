# Task Board Evidence

- Record ID: PROCESS-BOARD-2026-04-02
- Date: 2026-04-02
- Status: blocked externally after discovery
- Expected owner: team lead / PM owner
- Expected source system: canonical team task board or kanban tool
- Import folder: `process/task-board/`

## What this workspace can confirm

- The repo contains WR2 work breakdown records in `doc/src/project-records/`.
- The repo contains bilingual TODO lists and an evidence register that describe intended planning structure.
- No board screenshots, exports, or task-card history are currently stored in this workspace.
- `gh project list --owner aqure5418ly` could not inspect GitHub Projects from this environment because the token is missing the `read:project` scope.

## Discovery artifacts captured in-repo

- `process/discovery/github-project-scope-check-2026-04-02.txt`

This file records the current GitHub Projects access limitation from this workspace. It does not prove that the team board is on GitHub Projects; it only records one checked path that remains unavailable here.

## Missing teacher-facing artifacts

- At least one screenshot or export of the real team board
- 2 to 4 representative tasks showing progression or state movement
- Date context proving the board reflects the active implementation period

## Why this is blocked here

The local workspace preserves planning outputs, but it does not include direct captures from the board the team actually used. That means WR2 can describe the intended task structure, but cannot claim stronger board evidence until the real captures are imported.

## Important guardrail

The team should not create a brand-new GitHub Project now and present it as if it were the historical WR2 task board. That would only prove that a board exists after closeout. If a retrospective visual is needed for explanation, it should be labelled clearly as reconstructed from the recorded TODO list and merged PR history, not as original teacher-facing board evidence.

## Current explanatory artifact

A retrospective coordination board has now been reconstructed and stored as:

- `process/task-board/retrospective-wr2-closeout-board.drawio`
- `process/task-board/retrospective-wr2-closeout-board.png`

This artifact is suitable for explaining how the closeout work was grouped after the move to GitHub PR workflow, but it must remain labelled as a retrospective reconstruction rather than historical task-board evidence.

## Next collection action

1. Identify the actual board or kanban system the team used.
2. Capture one board-wide snapshot and 2 to 4 representative tasks.
3. Import the captures into `process/task-board/` using dated filenames.
4. Update this note and `wr2-asset-register.md` from blocked to ready if the evidence is imported.
