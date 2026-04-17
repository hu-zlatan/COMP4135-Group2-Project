# WR3 Baseline and Gap Register

- Date: 2026-04-17
- Purpose: freeze the initial WR3 testing position before full validation reruns

## Baseline Facts

| Area | Current fact | Evidence |
| --- | --- | --- |
| WR3 template inputs | `doc/src/wr3-latex` already contains the WR3 template PDF, cleaned sample OCR, reconstructed sample tables, and the local scoring summary. | `doc/src/wr3-latex/COMP4135_template4cw1RP3.pdf`, `doc/src/wr3-latex/sample-report.md`, `doc/src/wr3-latex/sample-tables.md`, `doc/src/wr3-latex/wr3-scoring-summary.md` |
| Existing automated validation record | A narrow EditMode run already exists for the battle-end accuracy fix and passed `3/3` tests. | `doc/src/project-records/testing/prototype-accuracy-editmode-run-2026-04-02.md` |
| Current runtime scope | The current prototype centres on a single battle slice with managers for turn flow, deck handling, card resolution, grid logic, AI, UI, and unit state. | `coursework/Assets/Scripts/**` |
| Test framework | `com.unity.test-framework` is present and `com.unity.testtools.codecoverage` has now been added for WR3. | `coursework/Packages/manifest.json`, `coursework/Packages/packages-lock.json` |
| Existing process evidence | WR2 already imported canonical repo and task-board evidence packages that can be reused in WR3 process chapters. | `doc/src/project-records/process/README.md` and sibling files |

## Gap Register at WR3 Start

| Gap ID | Gap | Why it matters for WR3 | Current action |
| --- | --- | --- | --- |
| WR3-GAP-01 | The committed automated suite was previously only `TurnManagerTests` with `3` cases. | WR3 needs broader module-level evidence and not only a battle-end fix snapshot. | Add systematic EditMode suites and at least one PlayMode smoke path. |
| WR3-GAP-02 | No line coverage package was configured in the original project state. | The sample report gains marks by quantifying test execution and coverage evidence. | Install `com.unity.testtools.codecoverage` and wire its outputs into the report. |
| WR3-GAP-03 | Requirements-to-tests traceability did not exist in one place. | WR3 needs a clear matrix from feature expectations to concrete test suites. | Build a requirements coverage matrix and a module/class coverage matrix. |
| WR3-GAP-04 | Bug tracking was only a template plus a few WR2 validation notes. | WR3 expects issue documentation, retest history, and follow-up actions. | Expand the bug log into a numbered WR3 defect register. |
| WR3-GAP-05 | Unity currently shows a partial test-discovery checkpoint for the expanded suite. | The report must not overstate execution evidence beyond what was actually rerun. | Mark authored tests separately from fully rerun tests until the editor refresh state is cleared. |

## Freeze Point

The WR3 baseline used in the report is:

- previously verified run: `3` EditMode tests, all in `TurnManagerTests`, `3/3` passed;
- current authored expansion: `11` suite files and `44` test methods across EditMode and PlayMode;
- current editor discovery checkpoint: `19` EditMode test methods visible through Unity MCP, plus one PlayMode assembly entry;
- current reporting rule: authored tests may be claimed as implemented, but only rerun/discovered tests may be claimed as execution-confirmed.
