# WR2 Asset Register

This register tracks reusable WR2 evidence, known gaps, suggested ownership, and the deadline for each item.

Migration note:
This register began as a WR2 closeout baseline and has since been partially refreshed on the clean GitHub `develop` line.
Items below therefore distinguish between evidence that is already present in the repository, evidence now visible through merged GitHub history, and evidence that still depends on external captures.

| Asset | Purpose in WR2 | Source | Current status | Suggested owner | Due | Notes |
| --- | --- | --- | --- | --- | --- | --- |
| WR2 template PDF | Recover correct report structure | `doc/Group-Shared/moodle/wr2/COMP4135_template4cw1RP2.pdf` | Required now - Ready | Document integrator | 2026-04-01 | Confirms chapter names and appendix requirement |
| Coursework specification | Deadlines, weekly report intent, assessment expectations | `doc/src/COMP4135-CW1-Specification.txt` | Required now - Ready | Document integrator | 2026-04-01 | Authority for WR2 scope and WR3 boundary |
| WR1 source report | Extract original requirements and claims | `doc/src/wr1-latex/main.tex` | Required now - Ready | Requirements owner | 2026-04-01 | Use for WR1 to WR2 validation table |
| One-pager | Early scope and intended player experience | `doc/src/onepager_en.md` | Required now - Ready | Requirements owner | 2026-04-01 | Useful for explaining why scope narrowed |
| Architecture constraints | Scope cuts and module choices | `doc/src/demo_architecture_constraints.md` | Required now - Ready | Architecture owner | 2026-04-01 | Basis for W3 and decision log |
| First-pass scripts note | Explain prototype implementation path | `doc/src/demo_first_pass_scripts.md` | Required now - Ready | Architecture owner | 2026-04-01 | Supports W4 and W6 |
| UML source files | Architecture figure source | `artifacts/uml/` | Required now - Ready | Architecture owner | 2026-04-01 | Local path audit confirmed the UML source folder exists |
| Prototype screenshots | High-fidelity evidence | `coursework/Assets/Screenshots/` | Required now - Ready | Prototype owner | 2026-04-01 | Local path audit confirmed the screenshot set exists; chosen images are copied into `wr2-latex/media/` |
| Prototype walkthrough note | WR2 design validation evidence | `doc/src/project-records/validation/wr2-heuristic-walkthrough-2026-04-01.md` | Required now - Ready | Validation owner | 2026-04-02 | Current WR2 validation wording should point here rather than to an implied external playtest |
| Prototype accuracy EditMode run | Minimal automated verification evidence | `doc/src/project-records/testing/prototype-accuracy-editmode-run-2026-04-02.md` | Required now - Ready | Prototype/test owner | 2026-04-02 | Records the 3-pass EditMode run added after the battle-end accuracy fix |
| GitHub PR history | Process evidence for current repo workflow | Canonical team GitHub repo | Required now - Ready | Repo maintainer | 2026-04-03 | Merged PR history on `develop` now proves the cleaned collaboration workflow without needing local-only claims |
| Retrospective closeout coordination board | Explanatory process visual for appendix or discussion | `doc/src/project-records/process/task-board/retrospective-wr2-closeout-board.drawio` | Required now - Ready | Repo maintainer / document integrator | 2026-04-03 | Must remain labelled as a retrospective reconstruction from TODO lists and merged PR history, not as historical board evidence |
| Team repo screenshots | Optional stronger process evidence | External canonical team repo capture | Deferred later - Optional strengthening | Repo maintainer | 2026-04-17 | Import target remains `project-records/process/canonical-repo/`; screenshots help appendix quality but are no longer the only repo evidence path |
| Task board screenshots | Planning and prioritisation evidence | External team board | Blocked externally - Missing locally | Team lead / PM owner | 2026-04-02 12:00 | Import target is `project-records/process/task-board/`; discovery checks are stored under `project-records/process/discovery/` and the blocker note remains in `project-records/process/task-board-evidence-2026-04-02.md` |
| Confirmed implementation meeting attendance | Appendix accuracy | Team chat / calendar | Blocked externally - Missing locally | Team lead / note owner | 2026-04-02 12:00 | No stronger attendance source is currently stored in the process package; keep the recovered note until that changes |
| Playtest participants and notes | Stronger validation evidence | Team validation activity | Deferred later - Optional strengthening | Validation owner | 2026-04-17 | WR2 already has a heuristic walkthrough; a formal playtest strengthens WR3 and the final report |
