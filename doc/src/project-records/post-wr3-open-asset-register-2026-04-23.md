# Post-WR3 Open Asset Register

- Date: 2026-04-23
- Purpose: shortlist low-risk open-licensed art assets for the next visual milestone after the post-WR3 gameplay and UX stabilization pass

## Recommended style direction

- Use a `CC0-first` asset strategy so the coursework demo stays easy to redistribute, document, and credit.
- Keep the visual direction `stylized tabletop fantasy` rather than mixing pixel art, realistic PBR scans, and flat prototype shapes.
- Prefer one coherent runtime stack:
  - `uGUI` HUD styled with flat panels, readable icons, and a restrained fantasy palette.
  - Low-poly or mini-style 3D units and props that can replace the capsule placeholders without changing gameplay code.

## Source-backed shortlist

| Bucket | Recommended source | License | Why it fits the current project | Suggested use now |
| --- | --- | --- | --- | --- |
| HUD panels, buttons, chrome | [Kenney UI Pack](https://kenney.nl/assets/ui-pack) | CC0 | Clean, engine-friendly UI sprites that fit the new title/battle/result flow without adding license friction | Replace prototype button and panel visuals in `BattleUI` and `GameFlowController` |
| Card, status, and action icons | [Kenney Board Game Icons](https://kenney-assets.itch.io/board-game-icons) | CC0 | The icon language already leans tabletop/board-game, which matches the tactical-card framing | Add icons for `Strike`, `Push`, `Dash`, `Recover`, turn state, and result summaries |
| Alternative input/help glyphs | [Kenney Input Prompts](https://kenney.nl/assets/input-prompts) | CC0 | Useful if the HUD gains explicit click/help prompts or a controls strip later | Optional prompt overlays for tutorial or help text |
| Player and enemy character models | [Quaternius Universal Base Characters](https://quaternius.com/packs/universalbasecharacters.html) | CC0 | Humanoid-ready character set with a retargetable rig, which is lower engineering risk than custom animation authoring | Replace player/enemy capsules with one player silhouette and one enemy silhouette first |
| Character animation set | [Quaternius Universal Animation Library](https://quaternius.itch.io/universal-animation-library) | CC0 | Retargetable locomotion and combat-adjacent animations reduce the need for bespoke animation work | Add idle, move, attack, hit, and death or defeat reactions |
| Board dressing and combat props | [Kenney Mini Dungeon](https://kenney.nl/assets/mini-dungeon) | CC0 | Stylistically simple 3D props that can dress the board edges, spawn zones, and menu background without overbuilding the scene | Add board-edge props, markers, and a more intentional combat backdrop |
| Cohesive all-Kenney character fallback | [Kenney Mini Characters 1](https://kenney.nl/assets/mini-characters-1) | CC0 | If Quaternius import/retargeting becomes too expensive, this is the lowest-risk fallback for keeping one visual family | Use as the simplified character replacement path for M12 |
| Optional lighting/material polish | [Poly Haven](https://polyhaven.com/) | CC0 | Good source for a single neutral HDRI or subtle material support if the environment needs quick depth | Use sparingly so the project does not drift into a realism mismatch |
| UI fallback collection | [OpenGameArt CC0 UI collection](https://opengameart.org/content/cc0-ui) | Mixed collection page, intended CC0 subset | Useful only if Kenney leaves an obvious gap such as bars, badges, or decorative flourishes | Treat as fallback and verify each downloaded item before import |
| Icon overflow fallback | [Game-Icons.net](https://game-icons.net/) | CC BY 3.0 | Extremely broad coverage, but requires attribution and should not be the default if CC0 options are enough | Use only if Kenney icon coverage is insufficient and a credits entry is acceptable |

## Recommended selection rule

- Primary path for M12:
  - `Kenney UI Pack`
  - `Kenney Board Game Icons`
  - `Quaternius Universal Base Characters`
  - `Quaternius Universal Animation Library`
  - `Kenney Mini Dungeon`
- Lower-risk fallback path for M12:
  - `Kenney UI Pack`
  - `Kenney Board Game Icons`
  - `Kenney Mini Characters 1`
  - `Kenney Mini Dungeon`

## Integration order

1. Replace HUD/button/panel sprites first so the title, battle, and result flow look intentional even before model swaps.
2. Add icons to card buttons and battle summaries so readability improves before deeper art work lands.
3. Replace the player and enemy placeholders with one coherent character set.
4. Add only a small set of animations: idle, move, attack, hit, and defeat.
5. Dress the board edges and background with lightweight props, then capture new smoke screenshots.

## Imported first slice

- Status: imported on 2026-04-23 and wired into runtime UI code
- Runtime evidence:
  - `Assets/Resources/UITheme/Panels/`
  - `Assets/Resources/UITheme/Icons/`
  - `Assets/Scripts/UI/UiThemeResources.cs`
  - `Assets/Scripts/UI/BattleUI.cs`
  - `Assets/Scripts/Core/GameFlowController.cs`
  - `Assets/Scripts/UI/CardButtonView.cs`
- Scope of this slice:
  - title screen banner and framed content panel
  - result screen banner and framed content panel
  - battle HUD top/left/bottom panel skins
  - themed button sprites
  - card icons for `Move`, `Dash`, `Strike`, `Push`, `Guard`, and `Heal`
- Deferred to later M12 slices:
  - replacing capsule unit models
  - animation retargeting
  - board-edge props and environment dressing

## Guardrails

- Avoid mixing `pixel art` packs with the current 3D board unless the whole scene direction changes.
- Avoid mixing `realistic` Poly Haven materials with heavily stylized low-poly characters in the mainline scene.
- Prefer `CC0` over `CC BY` so the final coursework package stays simple to document and submit.
- If a fallback pack is used from OpenGameArt or Game-Icons.net, add the exact asset page and credit requirement to the final report evidence.

## License notes used for this register

- Kenney states that assets on its asset pages are public-domain licensed and usable in commercial projects without required attribution: [Kenney support](https://kenney.nl/support).
- Quaternius marks the cited packs above as `CC0` on the official pack pages.
- Poly Haven states its library is `CC0`.
- OpenGameArt is a host, not a single license; verify the individual asset page before import.
- Game-Icons.net is broad and useful, but its default license is `CC BY 3.0`, so treat it as a deliberate exception rather than the baseline.
