# Post-WR3 Visual Smoke

- Date: 2026-04-24
- Purpose: record the second M12 slice that replaces capsule visuals with tactical silhouettes and adds board dressing without regressing battle interaction

## Automated evidence

- EditMode: `73/73 passed`
- PlayMode: `1/1 passed`
- Relevant coverage focus:
  - `UnitController` creates and maintains a runtime presentation shell
  - `GridManager` creates board presentation objects and markers

## Manual smoke notes

- Temporary smoke setup:
  - `GameRoot.disableFrontEndFlow` was toggled to `true` in the editor only for a direct battle-view capture, then restored to `false`
- Visual result:
  - the player and enemy capsules are replaced by low-risk procedural silhouettes
  - the board now has a raised base, perimeter frame, corner posts, and ally/enemy beacons
- Interaction safety check:
  - presentation parts and board props do not keep active colliders, so click selection still routes through the original gameplay colliders
- Runtime screenshot:
  - `coursework/Assets/Screenshots/m12-slice2-battle-smoke.png`

## Follow-up scope

- Deferred to later visual slices:
  - importing external CC0 character models if a coherent pack becomes available locally
  - adding animation states beyond the current static presentation shells
  - dressing the menu background and non-board environment
