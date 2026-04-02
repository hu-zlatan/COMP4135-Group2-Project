# WR2 Implementation Decisions

## D-01 Single-Battle Vertical Slice

- Date: 2026-03-15
- Status: accepted
- Problem: the original concept allowed the scope to grow into multiple scenes and more content than WR2 could support.
- Chosen option: implement one self-contained battle scene for WR2.
- Why: a single scene is enough to prove the board, cards, turn loop, and enemy response.
- Trade-off: campaign flow and progression remain deferred.

## D-02 Shared Unit Runtime Model

- Date: 2026-03-15
- Status: accepted
- Problem: separate player/enemy controller hierarchies would add complexity before the core loop was stable.
- Chosen option: use one shared `UnitController` with a `TeamType` enum.
- Why: health, guarding, movement, and grid state stay easy to trace.
- Trade-off: specialised behaviour lives outside the unit class for now.

## D-03 Explicit Card Resolver

- Date: 2026-03-15
- Status: accepted
- Problem: a generic card-effect hierarchy would take longer to debug and explain than the starter card set justified.
- Chosen option: keep `CardType` explicit and resolve effects in one `CardResolver`.
- Why: the WR2 prototype only needs a small starter set and benefits from readable switch-based logic.
- Trade-off: larger future card sets would need refactoring.

## D-04 Deterministic Enemy AI

- Date: 2026-03-15
- Status: accepted
- Problem: a smarter AI would slow implementation and complicate debugging.
- Chosen option: nearest-target movement and adjacent attack only.
- Why: this validates enemy response with minimal rules.
- Trade-off: the enemy behaviour is strategically shallow.

## D-05 Prototype-First UI

- Date: 2026-03-15
- Status: accepted
- Problem: a polished UI stack was not necessary to prove the prototype loop for WR2.
- Chosen option: use a lightweight battle panel and board click flow that prioritises clarity over polish.
- Why: this reduced integration time and kept the current prototype easy to demonstrate.
- Trade-off: onboarding and presentation still need improvement before final submission.

## D-06 External Process Evidence Required

- Date: 2026-04-01
- Status: accepted
- Problem: the local workspace does not contain the team repository history or task board evidence needed for process claims.
- Chosen option: document the intended workflow in WR2, but require screenshots from the canonical team repo and board before final submission.
- Why: this keeps WR2 honest and prevents the report from overstating local evidence.
- Trade-off: one more evidence-collection step remains outside this workspace.
