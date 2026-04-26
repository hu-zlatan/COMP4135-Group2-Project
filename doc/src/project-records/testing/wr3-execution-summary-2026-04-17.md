# WR3 Execution Summary

- Date: 2026-04-17
- Purpose: archive the rerun-backed execution evidence used by the final WR3 validation chapter after the clean 2026-04-17 evidence freeze

## Confirmed Execution Records

| Run date | Mode | Scope | Total | Passed | Failed | Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| 2026-04-02 | EditMode | Narrow `TurnManagerTests` battle-end regression run | 3 | 3 | 0 | `doc/src/project-records/testing/prototype-accuracy-editmode-run-2026-04-02.md` |
| 2026-04-17 | EditMode | Consolidated coverage-backed rerun on mirrored workspace | 57 | 57 | 0 | `doc/src/project-records/testing/coverage/2026-04-17/editmode-test-results.xml`, `doc/src/project-records/testing/coverage/2026-04-17/editmode-coverage.log` |
| 2026-04-17 | PlayMode | Coverage-backed smoke rerun on mirrored workspace | 1 | 1 | 0 | `doc/src/project-records/testing/coverage/2026-04-17/playmode-test-results.xml`, `doc/src/project-records/testing/coverage/2026-04-17/playmode-coverage.log` |
| 2026-04-17 | Coverage export | Combined EditMode + PlayMode report generation | 58 | 58 | 0 | `doc/src/project-records/testing/coverage/2026-04-17/coverage-report-generation.log`, `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |

## Coverage Export Summary

| Metric | Value | Evidence |
| --- | --- | --- |
| Runtime classes in report | 11 | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.json` |
| Covered lines | 868 | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |
| Coverable lines | 1169 | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |
| Line coverage | 74.2% | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |
| Covered methods | 146 | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |
| Total methods | 158 | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |
| Method coverage | 92.4% | `doc/src/project-records/testing/coverage/2026-04-17/Report/Summary.md` |

## Current Execution Position

| Category | Count | Notes |
| --- | --- | --- |
| Total suite files | 11 | 10 EditMode suites plus 1 PlayMode smoke suite |
| Total authored test methods | 58 | Matches the current inventory under `coursework/Assets/Tests/` |
| Verified by named rerun | 58 | All authored test methods now have rerun-backed pass evidence |
| Suite files still only discovered | 0 | No core suite remains in a discovery-only state |
| Suite files still only implemented | 0 | No core suite remains in an implemented-only state |

## Reporting Rule

- `Verified` means a suite or requirement is backed by a named rerun record on disk.
- `Archived coverage` means the raw OpenCover XML, generated report, badges, and summary files all exist under `doc/src/project-records/testing/coverage/2026-04-17/`.
- No unresolved blocker defect remains in the scoped WR3 validation set; remaining follow-up items are presentation-strengthening or future-regression checks rather than missing execution evidence.
