# WR3 Defect and Retest Register

- Date: 2026-04-17
- Purpose: carry WR2 quality issues forward into WR3 and record which WR3 and inherited blockers were closed by the final rerun-backed evidence freeze

| ID | Area | Severity | Symptom / finding | Source | Current status | Next action |
| --- | --- | --- | --- | --- | --- | --- |
| WR2-BUG-01 | Onboarding feedback | Medium | New players could miss the required "select a unit before selecting a card" order until the status prompts and next-step messages were strengthened. | WR2 heuristic walkthrough, final `BattleUI` rerun | Fixed and verified | Keep the current next-step prompt flow as a regression-backed behaviour and reretest after any wording or highlight change. |
| WR2-BUG-02 | Enemy turn readability | Medium | Enemy actions were functionally correct, but the phase summary previously lacked strong enough emphasis. | WR2 heuristic walkthrough, final `BattleUI` and `TurnManager` reruns | Fixed and verified | Rerun the summary/message checks after any later wording, log, or transition-layout change. |
| WR2-BUG-03 | Battle-end handling | High | The loop previously risked continuing after elimination. | WR2 prep log | Fixed and verified | Preserved as a regression case and reconfirmed in the expanded WR3 rerun context. |
| WR3-BUG-01 | Test execution pipeline | Medium | Early live-editor discovery exposed only part of the authored suite, creating a truth gap between inventory and rerun evidence. | 2026-04-17 WR3 checkpoint | Fixed and verified | Keep named rerun records as the only basis for `Verified` labels. |
| WR3-BUG-02 | Coverage evidence gap | Low | Code Coverage was installed, but the first archived export did not yet exist. | 2026-04-17 WR3 checkpoint | Fixed and verified | Use the archived `coverage/2026-04-17/` report set as the baseline for any later WR3 updates. |

## Retest Policy

- A defect moves to `Fixed and verified` only when a named automated rerun or archived coverage-backed execution record exists.
- UI/readability defects can also be marked `Fixed and verified` in WR3 when the improved prompts, summaries, or outcome messaging are present in the runtime code and backed by named rerun evidence.
