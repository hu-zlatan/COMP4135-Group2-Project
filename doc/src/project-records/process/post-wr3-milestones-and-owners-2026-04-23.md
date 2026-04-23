# Post-WR3 Milestones and Owners

- Date: 2026-04-23
- Purpose: extend the closed WR3 prototype into a coursework-ready playable demo with complete UX flow and modest gameplay depth

| Milestone | Primary owner | Deliverables | Exit gate |
| --- | --- | --- | --- |
| M7. Front-end flow shell | Hongshuo HU | Title panel, battle shell, result panel, replay/menu routing, project branding | The demo opens on a start screen and always exits battle through a result flow |
| M8. Battle HUD refactor | Hongshuo HU | uGUI HUD wrapper for status, card hand, prompts, unit list, action log, and end-turn controls | The battle loop is readable without relying on prototype-only `OnGUI` layout |
| M9. Moderate gameplay expansion | Hongshuo HU | Expanded starter deck with `Dash`, `Recover`, and longer-range strike options plus reinforcement pressure | Tactical choices are deeper than the original move/strike/guard/push baseline |
| M10. Finish pass and evidence packaging | Hongshuo HU | Updated tests, smoke checklist, and completion records for the post-WR3 demo | The prototype can be handed to a reviewer as a coherent mini-game slice |
| M11. Open-licensed art sourcing and style lock | Hongshuo HU | CC0-first asset register, chosen visual direction, import priority, and license notes for UI, icons, characters, and board dressing | One coherent art direction is selected and every visual bucket has at least one approved source |
| M12. Visual asset integration pass | Hongshuo HU | Imported replacement assets, updated runtime presentation, and before/after smoke evidence | Core prototype placeholders are replaced by a coherent asset pass without regressing readability or interaction clarity |

## Sequence Rules

- M7 and M8 are tightly coupled and should ship together in one coherent runtime flow.
- M9 must preserve the existing turn structure and manager boundaries established in WR2/WR3.
- M10 is responsible for truthfully separating automated coverage from manual smoke-only UX checks.
- M11 should prefer `CC0` sources first and only fall back to attribution-bearing sources when a real coverage gap remains.
- M12 should ship in thin slices: UI and icons first, then characters, then board dressing.
