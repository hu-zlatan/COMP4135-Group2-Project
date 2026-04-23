# Final Screenshot Shortlist

- Date: 2026-04-24
- Purpose: reduce the current screenshot folder into a smaller set of candidate images for the final report, appendices, and demo support material

## Recommended shortlist

| File | Suggested use | Keep? | Notes |
| --- | --- | --- | --- |
| `coursework/Assets/Screenshots/tactical-cards-scene-check.png` | Early prototype comparison image if the report wants to show growth from the first board state | Optional | Useful only if the final report explicitly contrasts prototype and final build. |
| `coursework/Assets/Screenshots/post-fix-game-view.png` | Battle-state stability evidence after UI initialization fixes | Optional | Good issue-history support image, not essential for the main report. |
| `coursework/Assets/Screenshots/m12-slice2-battle-smoke.png` | Final board and unit presentation pass | Recommended | Good world-layer screenshot for the visual pass. |
| `coursework/Assets/Screenshots/m12-slice3-battle-smoke.png` | Tabletop ambience pass | Recommended | Strongest world-layer battle presentation screenshot currently preserved. |
| `coursework/Assets/Screenshots/m12-slice3-title-smoke.png` | Title/result ambience pass | Recommended with caution | Useful if the image actually shows enough overlay detail for the report. |
| `coursework/Assets/Screenshots/hud-layout-check.png` | HUD layout debug/support evidence | Recommended for appendix only | Better as a technical appendix figure than a main-body image. |
| `coursework/Assets/Screenshots/hud-layout-check-overlay.png` | HUD readability and spacing fix evidence | Recommended for appendix only | Useful when discussing the post-WR3 UI fixes. |

## Images that should probably stay out of the final report

- `battleui-inputsystem-check.png`
- `battleui-play-check.png`
- timestamped `screenshot-*.png` files unless one is needed to prove a specific bug or milestone

These are mainly raw working captures rather than clean report figures.

## Important limitation

Some Unity MCP captures did not reliably include `Screen Space Overlay` UI. This means that battle-board screenshots are often stronger than title-flow or overlay-heavy screenshots. If a figure is meant to prove overlay readability, prefer:

- manual editor captures from the Game view; or
- a figure caption that clearly states the screenshot shows the world layer while the overlay itself was validated separately through smoke and hierarchy checks.

## Recommended next action

Before the final report is frozen:

1. choose 3 to 5 images only;
2. rename or copy the chosen files into a cleaner report-facing folder if needed;
3. avoid mixing raw debugging captures with final appendix figures.
