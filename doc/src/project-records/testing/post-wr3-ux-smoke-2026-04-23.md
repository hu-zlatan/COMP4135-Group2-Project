# Post-WR3 UX Smoke Checklist

- Date: 2026-04-23
- Purpose: record the manual checks that complement TDD-driven flow and logic coverage

## Manual smoke items

- Launch the project and confirm the title screen appears before battle interaction starts.
- Start a battle and verify the HUD shows turn info, selected unit/card, hand, unit list, and action log.
- Play several turns and confirm hand buttons remain responsive while world clicks still target tiles and units.
- Reach victory and verify the result screen shows replay and menu actions.
- Reach defeat and verify the result screen copy updates accordingly.
- Use `Dash`, `Recover`, and the longer-range strike at least once in play.
- Return to menu, start again, and confirm the battle state resets cleanly.

## Automation boundary

- Flow state, battle start, result generation, replay/menu routing, and new card behaviour are intended for automated coverage.
- Layout, readability, alignment, visual polish, and input feel remain smoke-only unless Unity test infrastructure becomes stable enough for additional UI assertions.
