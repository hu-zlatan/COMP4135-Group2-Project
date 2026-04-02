# WR2 Heuristic Walkthrough

- Session ID: WR2-HW-2026-04-01
- Date: 2026-04-01
- Reviewer(s): local workspace documentation review
- Validation type: internal heuristic walkthrough

## Scope

- Select a player unit from the current board.
- Choose and play a starter card.
- End the turn and observe the enemy response.

## Scenario Results

| Scenario | Expected behaviour | Observation | Severity | Proposed action | Applied? |
| --- | --- | --- | --- | --- | --- |
| Select a player unit | The player should understand the first interaction quickly. | The current UI does show a status prompt, but the board itself does not strongly guide the first click. | Medium | Add a stronger first-turn prompt or highlight. | Planned |
| Play a card on a valid target | Move and attack states should be easy to distinguish. | Tile highlighting is visible, but the colour meaning is not explained anywhere in the current prototype. | Medium | Add a tiny legend or contextual hint when a card is selected. | Planned |
| End turn and read enemy response | Enemy response should be visible and the loop should feel complete. | Enemy actions are summarised in text, but there is no strong battle-end presentation and the enemy phase can feel abrupt. | High | Improve enemy-turn emphasis and finish battle-end state handling. | Not yet |

## Summary

- The prototype validates the intended interaction loop.
- The biggest open issues are onboarding clarity and battle-end presentation.
- WR2 should describe these as remaining gaps instead of overstating them as complete.

## Evidence Status

- This record is still an internal heuristic walkthrough, not a confirmed external playtest.
- No board or canonical repo evidence changes the findings here yet.
- If a stronger team playtest is imported later, extend this record rather than rewriting it as if it had already happened.
