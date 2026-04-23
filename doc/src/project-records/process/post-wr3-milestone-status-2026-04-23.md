# Post-WR3 Milestone Status

- Date: 2026-04-23
- Purpose: record the implementation status of the post-WR3 completion push

| Milestone | Current status | Evidence on disk | Closure note |
| --- | --- | --- | --- |
| M7. Front-end flow shell | Implemented | `coursework/Assets/Scripts/Core/GameFlowController.cs`, `coursework/Assets/Scripts/Core/GameRoot.cs` | The prototype now supports title, battle, and result states inside the main scene |
| M8. Battle HUD refactor | Implemented | `coursework/Assets/Scripts/UI/BattleUI.cs`, `coursework/Assets/Scripts/UI/CardButtonView.cs` | Runtime battle information is exposed through a panel-based `uGUI` HUD |
| M9. Moderate gameplay expansion | Implemented | `coursework/Assets/Scripts/Cards/DeckManager.cs`, `coursework/Assets/Scripts/Cards/CardResolver.cs`, `coursework/Assets/Scripts/Common/GameEnums.cs` | The starter deck and encounter pressure were expanded without changing the core turn model |
| M10. Finish pass and evidence packaging | Implemented | `coursework/Assets/Tests/`, `doc/src/project-records/testing/post-wr3-ux-smoke-2026-04-23.md` | Automated reruns and manual smoke now agree that the post-WR3 demo flow is stable enough to move into a visual asset pass |
| M11. Open-licensed art sourcing and style lock | Implemented | `doc/src/project-records/post-wr3-open-asset-register-2026-04-23.md` | The visual direction, approved CC0 sources, and integration order are now fixed for the asset pass |
| M12. Visual asset integration pass | In progress | `coursework/Assets/Resources/UITheme/`, `coursework/Assets/Scripts/UI/UiThemeResources.cs`, `coursework/Assets/Scripts/Presentation/UnitVisualShell.cs`, `doc/src/project-records/testing/post-wr3-visual-smoke-2026-04-24.md` | The first slice replaces HUD chrome with Kenney sprites, and the second slice replaces capsule visuals with tactical silhouettes plus a dressed board frame while preserving interaction stability |
