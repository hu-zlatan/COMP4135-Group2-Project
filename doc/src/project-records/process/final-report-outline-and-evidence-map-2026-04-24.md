# Final Report Outline And Evidence Map

- Date: 2026-04-24
- Purpose: convert the CW1 specification, final-report template, and current Tactical Cards evidence into a practical drafting map for the final group report
- Primary governing sources:
  - `doc/src/course/COMP4135-CW1-Specification.pdf`
  - `doc/src/course/report_template.pdf`
  - `doc/src/course/COMP4135_week9_seminar.pdf`

## 1. Hard Constraints

The final report and final submission package should be built against the following non-negotiable rules.

### Final report constraints from the CW1 specification

- The final group report must be at least `5,000` words.
- The main body should be approximately `20-25` pages, excluding appendices.
- Each team submits one collaboratively written report.
- The report must be self-contained and must not assume the reader has already read the weekly reports.
- The report must include:
  - an updated system design and user-interface design;
  - a discussion of implementation and testing;
  - a list of all major system components;
  - clear identification of which components were written by the group and which were sourced externally;
  - an overview of the source-code hierarchy;
  - a summary of achievements traced back to stated requirements;
  - reflection from both technical and project-management / teamwork perspectives;
  - a testing appendix;
  - minutes from all formal meetings as an appendix;
  - a user manual as an appendix.

### Software and submission constraints from the CW1 specification

- Software is assessed separately from the report and must be submitted at the same time.
- The source package must be submitted as a `.zip` or `.tar.gz`.
- All source code must be properly attributed.
- Adapted code and third-party components must be clearly identified.
- Third-party licenses must be followed and must be compatible with the project.
- The submission should include enough documentation for another person to run, understand, and maintain the project.

### Weighting that matters for report preparation

| Component | Weight |
| --- | --- |
| Team Project Website | 2% |
| Weekly Report | 18% |
| Software | 20% |
| Teamwork | 10% |
| Final Group Report | 15% |
| Video | 10% |
| Presentation | 15% |

### Additional seminar reminders

- The website URL should appear in the final report.
- The repository home page should expose the main links clearly.
- Group-wide deliverables are submitted once by the team leader.
- Peer assessment is submitted individually by each member.

## 2. Tactical Cards Final Report Structure

Use `doc/src/course/report_template.pdf` as the top-level chapter skeleton. Do not reuse the WR3 chapter order directly. WR3 and post-WR3 records should be mined into the new structure below.

| Final report chapter | Tactical Cards writing focus | Main on-disk evidence | Current position / required action |
| --- | --- | --- | --- |
| `Abstract` | One-paragraph summary of the project, final playable scope, technical approach, and major outcomes. | `doc/src/onepager_en.md`, `doc/src/project-records/process/post-wr3-milestone-status-2026-04-23.md`, `README.md` | Not written yet. Easy to draft after the main body is stable. |
| `1 Introduction` | Motivation, target users, coursework context, aims, and final project scope. Explain why the team chose a lightweight tactical card battle instead of a larger RPG or pure card game. | `doc/src/onepager_en.md`, `doc/src/wr1-latex/main.tex`, `doc/src/wr2-latex/main.tex`, `doc/src/project-records/meetings/meeting-2026-03-17-wr1-kickoff.md` | Strong conceptual source material exists. Needs consolidation and rewriting into self-contained prose. |
| `2 Background and Related Work` | Briefly position the project against tactical grid games, card-driven tactics, and Unity as the chosen implementation platform. Also explain reference-mining strategy without overstating borrowed work. | `doc/src/onepager_en.md`, `doc/src/project-records/post-wr3-open-asset-register-2026-04-23.md`, local reference strategy records if cited | Weakest current chapter. The repo has design intent, but not yet a curated related-work section with citations. |
| `3 Requirements Engineering` | Reconstruct the final functional and non-functional requirements, then show how the project evolved from WR1 scope into the delivered demo. Include target audience and usability requirements. | `doc/src/wr1-latex/main.tex`, `doc/src/onepager_en.md`, `doc/src/project-records/validation/wr2-heuristic-walkthrough-2026-04-01.md`, `doc/src/project-records/testing/wr3-requirements-coverage-matrix-2026-04-17.md` | Good raw material exists. Needs one final requirement set that reflects post-WR3 features rather than the original WR1-only starter slice. |
| `4 Development` | Explain architecture, implementation strategy, simulation/battle design, and UI design. This chapter should describe the actual runtime structure now in the repo, not the earlier prototype-only view. | `coursework/Assets/Scripts/**`, `doc/src/project-records/decisions/wr2-implementation-decisions.md`, `doc/src/project-records/process/post-wr3-milestone-status-2026-04-23.md`, `doc/src/project-records/process/post-wr3-m12-m13-issue-log-2026-04-24.md`, `coursework/Assets/ThirdParty/Kenney/NOTICE.md` | Strongest current chapter candidate. The runtime codebase is now stable enough to present as a coherent system. |
| `4.1 System Design` | Present the final module breakdown and interaction flow: flow shell, battle loop, cards, units, grid, UI, presentation layer. | `coursework/Assets/Scripts/Core/*.cs`, `Cards/*.cs`, `Grid/*.cs`, `Units/*.cs`, `UI/*.cs`, `Presentation/*.cs` | Needs diagrams or cleaned tables. Raw component evidence is strong. |
| `4.2 Implementation Strategics` | Explain why the project used a scoped vertical slice, TDD-heavy validation, and iterative refactoring rather than broad content expansion. | `doc/src/project-records/decisions/wr2-implementation-decisions.md`, `doc/src/project-records/process/wr3-execution-plan-2026-04-17.md`, `doc/src/project-records/process/post-wr3-milestones-and-owners-2026-04-23.md` | Good evidence exists. The chapter title in the template is awkward, but keep it and explain the actual implementation strategy clearly. |
| `4.3 Simulation Design` | Describe the battle rules: grid, turn loop, hand flow, card resolution, enemy AI, win/lose flow, reinforcement pressure, and the post-WR3 additions. | `doc/src/onepager_en.md`, `coursework/Assets/Scripts/Core/TurnManager.cs`, `coursework/Assets/Scripts/Cards/CardResolver.cs`, `coursework/Assets/Scripts/Cards/DeckManager.cs`, `coursework/Assets/Scripts/Units/EnemyAI.cs` | Good source material exists in code and earlier design docs. |
| `4.4 UI Design` | Show the movement from prototype UI to title / battle / result flow, themed HUD, health bars, disabled-card feedback, and readability fixes. | `coursework/Assets/Scripts/UI/BattleUI.cs`, `coursework/Assets/Scripts/Core/GameFlowController.cs`, `doc/src/project-records/testing/post-wr3-ux-smoke-2026-04-23.md`, `doc/src/project-records/testing/post-wr3-ambience-smoke-2026-04-24.md`, `doc/src/project-records/testing/post-wr3-hud-feedback-smoke-2026-04-24.md`, `doc/src/project-records/process/post-wr3-m12-m13-issue-log-2026-04-24.md` | Very strong evidence exists. This should become one of the report's best chapters. |
| `5 Test & System Evaluation` | Merge the WR3 validation pack with the post-WR3 regression and smoke work. Show not just test counts, but what the tests actually prove about the delivered game. | `doc/src/project-records/testing/wr3-test-inventory-2026-04-17.md`, `doc/src/project-records/testing/wr3-execution-summary-2026-04-17.md`, `doc/src/project-records/testing/wr3-requirements-coverage-matrix-2026-04-17.md`, `doc/src/project-records/testing/wr3-defect-log-2026-04-17.md`, `doc/src/project-records/testing/post-wr3-hud-feedback-smoke-2026-04-24.md`, `doc/src/project-records/testing/post-wr3-ambience-smoke-2026-04-24.md`, `doc/src/project-records/testing/post-wr3-visual-smoke-2026-04-24.md` | Strong material exists, but it is split across WR3 and post-WR3 records. It needs one unified final narrative. |
| `5.1 Test Strategy` | Explain the TDD-first rule for logic and flow, plus smoke-first validation for aesthetics and layout. | `doc/src/project-records/testing/wr3-baseline-and-gap-2026-04-17.md`, `doc/src/project-records/testing/post-wr3-hud-feedback-smoke-2026-04-24.md` | Ready to draft. |
| `5.2 Test Methods` | Separate EditMode, PlayMode, targeted regression reruns, and manual smoke. | `coursework/Assets/Tests/**`, `doc/src/project-records/testing/*.md` | Ready to draft. |
| `5.3 Code Coverage` | Present the archived coverage export and explain its scope and limitations. | `doc/src/project-records/testing/wr3-coverage-export-2026-04-17.md`, `doc/src/project-records/testing/coverage/2026-04-17/` | Risky: the worktree currently shows deleted files inside the coverage report folder. Restore or regenerate before citing it in the final report. |
| `5.4 Evaluation` | Evaluate whether the final build meets the requirements, where manual smoke was required, and what residual limitations remain. | `doc/src/project-records/testing/wr3-requirements-coverage-matrix-2026-04-17.md`, `doc/src/project-records/process/post-wr3-m12-m13-issue-log-2026-04-24.md`, `doc/src/project-records/validation/wr2-heuristic-walkthrough-2026-04-01.md` | Needs one final requirements traceability table that covers post-WR3 UI/UX work too. |
| `6 Evolution & Maintenance` | Explain deferred features, maintainability choices, testability, code organization, third-party asset controls, and how the project could be extended after the coursework. | `doc/src/project-records/decisions/wr2-implementation-decisions.md`, `doc/src/project-records/process/post-wr3-milestone-status-2026-04-23.md`, `coursework/Assets/Scripts/TacticalCards.asmdef`, `coursework/Assets/ThirdParty/Kenney/NOTICE.md` | Chapter not prewritten anywhere. Needs deliberate synthesis. |
| `7 Problem & Reflections` | Present the real issues encountered: scope control, toolchain instability, UI regressions, meeting/process gaps, and what the team changed. | `doc/src/project-records/process/post-wr3-m12-m13-issue-log-2026-04-24.md`, `doc/src/project-records/collaboration-history-2026-04-02.md`, `doc/src/project-records/process/task-board-evidence-2026-04-02.md`, `doc/src/project-records/testing/wr3-defect-log-2026-04-17.md` | Strong chapter candidate if written honestly. Avoid generic reflection language. |
| `8 Conclusion` | Final outcome, what was successfully delivered, what remains intentionally small, and why the result is still valid coursework scope. | All milestone and testing records above | Easy to write after the rest is stable. |

## 3. Major System Components For The Final Report

The CW1 specification explicitly requires a major-component list and a source-code hierarchy overview. The current runtime structure is clean enough to present directly.

### Runtime code hierarchy

- `coursework/Assets/Scripts/Core/`
  - `GameRoot.cs`
  - `GameFlowController.cs`
  - `GameFlowState.cs`
  - `TurnManager.cs`
- `coursework/Assets/Scripts/Cards/`
  - `CardData.cs`
  - `CardResolver.cs`
  - `DeckManager.cs`
- `coursework/Assets/Scripts/Grid/`
  - `GridManager.cs`
  - `TileView.cs`
- `coursework/Assets/Scripts/Units/`
  - `UnitController.cs`
  - `EnemyAI.cs`
- `coursework/Assets/Scripts/UI/`
  - `BattleHudModels.cs`
  - `BattleUI.cs`
  - `CardButtonView.cs`
  - `UiThemeResources.cs`
  - `UnitHealthBarView.cs`
- `coursework/Assets/Scripts/Presentation/`
  - `UnitVisualShell.cs`
  - `WorldThemeResources.cs`
- `coursework/Assets/Scripts/Common/`
  - `GameEnums.cs`

### Test hierarchy

- `coursework/Assets/Tests/Editor/`
  - `BattleUITests.cs`
  - `CardButtonViewTests.cs`
  - `CardResolverTests.cs`
  - `DeckManagerTests.cs`
  - `EnemyAITests.cs`
  - `GameFlowControllerTests.cs`
  - `GameRootTests.cs`
  - `GridManagerTests.cs`
  - `TileViewTests.cs`
  - `TurnManagerTests.cs`
  - `UnitControllerTests.cs`
- `coursework/Assets/Tests/PlayMode/`
  - `BattleFlowPlayModeTests.cs`

### External and third-party component evidence

- Open-licensed runtime UI art:
  - `coursework/Assets/ThirdParty/Kenney/NOTICE.md`
  - `doc/src/project-records/post-wr3-open-asset-register-2026-04-23.md`
- Unity packages and editor dependencies:
  - `coursework/Packages/manifest.json`
  - `coursework/Packages/packages-lock.json`

## 4. Appendix Mapping

The template appendices should not be treated as placeholders. They map cleanly onto existing repo material, but several still need consolidation.

| Appendix | What should go in it | Current evidence | Gap status |
| --- | --- | --- | --- |
| `A User-manual` | How to open the project, load the scene, start a battle, read the HUD, use cards, understand victory/defeat, and replay. | `README.md` contains only a minimal run note. | Missing as a standalone appendix source. Needs to be written. |
| `B Meeting Minutes` | All formal meetings, not just selected highlights. | `doc/src/project-records/meetings/meeting-2026-03-17-wr1-kickoff.md`, `meeting-2026-03-18-wr1-alignment.md`, `implementation-checkpoint-recovered-2026-03-15.md`, plus the WR3 testing meeting embedded in `doc/src/wr3-latex/appendices/appendix.tex` | Incomplete. These need to be consolidated, normalized, and probably expanded with any missing later formal meetings. |
| `C Test Case` | Representative cases, coverage data, test results, smoke evidence. | `doc/src/wr3-latex/appendices/appendix.tex`, `doc/src/project-records/testing/*.md`, `doc/src/project-records/testing/coverage/2026-04-17/` | Strong, but coverage evidence currently needs restoration or regeneration. |
| `D Development Planning` | Milestones, owners, timelines, scope control, and any Gantt or backlog visuals. | `doc/src/project-records/process/wr3-execution-plan-2026-04-17.md`, `wr3-milestones-and-owners-2026-04-17.md`, `post-wr3-milestones-and-owners-2026-04-23.md`, `post-wr3-milestone-status-2026-04-23.md`, `process/project-implementation-timeline-gantt.drawio`, `doc/src/project-records/team-assignment-pr-plan-2026-04-02.md` | Strong. Mostly a curation job. |
| `E Version Control` | Branch workflow, repository identity, PR/review rules, and selected repository evidence. | `doc/src/project-records/process/canonical-repo-evidence-2026-04-02.md`, `doc/src/project-records/git-pr-collaboration-policy-2026-04-02.md`, `doc/src/project-records/collaboration-history-2026-04-02.md`, `doc/src/project-records/pr-submit-cli-workflow.md` | Good narrative evidence exists, but README needs updating and optional screenshots or exports would strengthen presentation. |
| `F Docs` | Supporting records, asset register, issue logs, smoke notes, and any material too detailed for the main body. | `doc/src/project-records/**` broadly, especially `post-wr3-open-asset-register-2026-04-23.md` and `post-wr3-m12-m13-issue-log-2026-04-24.md` | Strong. Needs indexing, not invention. |

## 5. Known Gaps Before Final Report Drafting

These are the main evidence or compliance gaps still visible from the current workspace.

### Report-content gaps

1. `Background and Related Work` is not yet assembled.
   - The repo contains design intent and selective reference strategy, but not a polished related-work section with citations.
2. The final requirement set is fragmented.
   - WR1 requirements, WR3 coverage requirements, and post-WR3 UI/UX work need to be merged into one final requirement table.
3. The report does not yet have a final user manual source.
4. Meeting minutes are incomplete as a final-report appendix set.

### Submission-package gaps

1. `README.md` is outdated.
   - It still describes the project as a WR2 snapshot.
   - It says `develop` is the main integration branch, while the current working branch is `test`.
   - It does not currently expose the final website/report links required for final submission presentation.
2. Code attribution is weak.
   - Current runtime `.cs` files do not carry authorship or source headers.
   - The asset side has a `NOTICE.md`, but code-level attribution still needs an audit against the CW1 specification.
3. Coverage evidence is currently dirty in the worktree.
   - The archive under `doc/src/project-records/testing/coverage/2026-04-17/Report/` currently appears partially deleted in `git status`.
   - Final report drafting should not rely on that archive until it is restored or regenerated.
4. The website URL that must appear in the final report is not yet captured in the final-report prep materials.
5. Appendix-ready screenshots exist, but several remain untracked under `coursework/Assets/Screenshots/` and have not been curated into a final docs pack.

### Evidence-strength gaps

1. Task-board evidence is still weaker than repo evidence.
   - `doc/src/project-records/process/task-board-evidence-2026-04-02.md` still labels the real board evidence as externally blocked.
2. Historical collaboration evidence is truthful but partly reconstructed.
   - This is acceptable if written honestly, but it should not be overstated as if every historical artifact were directly archived.

## 6. Recommended Writing Order

Use the following drafting order. It minimizes rework and keeps the self-contained requirement under control.

1. Build the final requirement table.
   - This is the anchor for Chapters 3, 5, and 8.
2. Write Chapter 4 `Development`.
   - The architecture and implemented systems are already the clearest part of the project.
3. Write Chapter 5 `Test & System Evaluation`.
   - Reuse WR3 and post-WR3 evidence, but collapse it into one final validation story.
4. Write Chapter 7 `Problem & Reflections`.
   - Do this while the late-stage issue logs are still fresh.
5. Write Chapters 1 and 2.
   - Once the final scope is stable, introduction and background can be kept concise and accurate.
6. Write Chapter 6 `Evolution & Maintenance`.
   - This should explain what was intentionally deferred and how the current structure supports future work.
7. Finish the appendices.
   - User manual and meeting-minutes consolidation are the two biggest remaining manual tasks.
8. Write the abstract and conclusion last.

## 7. Recommended Next Actions

The next practical steps should be:

1. Create a new final-report source tree, preferably `doc/src/final-report-latex/`, based on `doc/src/course/report_template.pdf`.
2. Draft the final requirements table by merging:
   - `doc/src/wr1-latex/main.tex`
   - `doc/src/onepager_en.md`
   - `doc/src/project-records/testing/wr3-requirements-coverage-matrix-2026-04-17.md`
   - the post-WR3 M7-M13 feature records.
3. Restore or regenerate the coverage archive before any final-report figure or table cites it.
4. Write a standalone user manual source file.
5. Consolidate all formal meeting minutes into one appendix-ready set.
6. Update `README.md` so the repository landing page matches the final submission state.

## 8. Bottom Line

The repo already contains enough material to produce a strong final report, but it is not a simple copy-paste of WR3. The final report should be built as:

- WR1 and the one-pager for project intent and original requirements;
- WR2 decisions for scope and architecture rationale;
- WR3 for formal testing and validation structure;
- post-WR3 records for the actual final delivered UI, UX, and presentation quality.

The biggest remaining risks are not missing gameplay code. They are missing consolidation work: final requirement unification, appendix preparation, coverage evidence hygiene, README / website alignment, and code-attribution compliance.
