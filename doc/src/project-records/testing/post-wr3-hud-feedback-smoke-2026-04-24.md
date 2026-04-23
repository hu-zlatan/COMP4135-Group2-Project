# Post-WR3 HUD Feedback Smoke

- Date: 2026-04-24
- Purpose: record the HUD feedback completion pass that fixes the title overlay layout and adds explicit battle affordances

## Automated evidence

- EditMode: `81/81 passed`
- PlayMode: `1/1 passed`
- Coverage focus:
  - title flow builds a structured summary panel and non-overlapping feature strip
  - battle HUD exposes `Energy`, prompt text, disabled-card reasons, and runtime world health bars
  - direct battle debug mode now uses the same runtime HUD path as the normal title-flow path
  - runtime HUD layout guards against overlapping top-bar rows and an oversized hand dock

## Runtime verification

- Title-flow hierarchy verification in Play Mode:
  - `RuntimeCanvas`
  - `TitlePanel`
  - `TitleContent`
  - `SummaryPanel`
  - `FeatureStrip`
- Battle-flow hierarchy verification in Play Mode:
  - `EnergyLabel`
  - `PromptTitle`
  - `UnitHealthBar` x4
  - `DisabledOverlay` created on runtime card buttons
  - compact `BottomPanel` and resized runtime `CardButton` entries are present after the layout fix

## Manual smoke note

- Unity MCP capture still did not reliably return `Screen Space Overlay` pixels, so overlay composition should still be manually smoked in the editor Game view.
- The user-reported title overlap was resolved by separating the summary copy, feature strip, and CTA button into distinct vertical regions.
- The user-reported top-left prompt overlap and bottom hand-panel occlusion were resolved by stacking top-bar rows explicitly and compacting the hand dock and runtime card size.
