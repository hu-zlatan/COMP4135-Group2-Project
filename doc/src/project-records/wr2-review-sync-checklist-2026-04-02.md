# WR2 Review Sync Checklist

- Checklist ID: WR2-REVIEW-SYNC-2026-04-02
- Prepared: 2026-04-02
- Target branch: `review/coursework`
- Source branch: `dev`

## Mirror exactly

- `doc/src/wr2-latex/main.tex`
- `doc/src/wr2-latex/sections/01-specification-design.tex`
- `doc/src/wr2-latex/sections/02-prototyping-process.tex`
- `doc/src/wr2-latex/sections/03-implementation-strategy.tex`
- `doc/src/wr2-latex/sections/04-summary-reflections.tex`
- `doc/src/wr2-latex/appendices/appendix.tex`
- `doc/src/wr2-latex/references.bib`
- `doc/src/wr2-latex/media/`
- `doc/src/wr2-latex/figures/`
- `doc/src/project-records/README.md`
- `doc/src/project-records/wr2-asset-register.md`
- `doc/src/project-records/wr2-snapshot-freeze-2026-04-02.md`
- `doc/src/project-records/wr2-review-sync-checklist-2026-04-02.md`
- `doc/src/project-records/process/`
- `doc/src/project-records/process/canonical-repo/`
- `doc/src/project-records/process/task-board/`
- `doc/src/project-records/process/discovery/`
- `doc/src/project-records/meetings/`
- `doc/src/project-records/decisions/`
- `doc/src/project-records/validation/`
- `doc/src/project-records/testing/`
- `artifacts/uml/`

## Keep out of the review snapshot

- `doc/Group-Shared/`
- external tool directories such as `CLI-Anything/`, `drawio-desktop/`, `references/`, `local-skills/`
- LaTeX intermediate files such as `.aux`, `.bbl`, `.blg`, `.log`, `.out`, `.toc`

## Sync checks

- Confirm the review branch still contains the same WR2 source hierarchy as `dev`.
- Confirm the review branch includes the process evidence package, even if some entries are blocked.
- Confirm the review branch includes the same appendix-facing records cited by WR2.
- Confirm the review branch still includes `artifacts/uml/` and the Unity project under `coursework/`.
