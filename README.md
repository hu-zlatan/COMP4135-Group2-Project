# Tactical Cards Coursework Repository

This repository contains the Unity tactical card game demo, the submitted report sources, and the supporting documentation used as coursework evidence.

## Quick links

- Project website: [https://ur31325811.jzfkw.net/](https://ur31325811.jzfkw.net/)
- Canonical repository: [https://github.com/hu-zlatan/COMP4135-Group2-Project.git](https://github.com/hu-zlatan/COMP4135-Group2-Project.git)
- Final report PDF: [doc/src/final-report-latex/main.pdf](doc/src/final-report-latex/main.pdf)
- Final report source: [doc/src/final-report-latex/](doc/src/final-report-latex/)
- Weekly Report 2 PDF: [doc/src/wr2-latex/main.pdf](doc/src/wr2-latex/main.pdf)
- Weekly Report 3 PDF: [doc/src/wr3-latex/main.pdf](doc/src/wr3-latex/main.pdf)
- Unity source tree: [coursework/](coursework/)
- Project records and documentation: [doc/src/project-records/](doc/src/project-records/)
- Course-facing material: [doc/src/course/](doc/src/course/)
- Submission artifacts: [artifacts/submission/](artifacts/submission/)

## Project snapshot

`Tactical Cards` is a compact Unity tactics demo built around one battle-ready vertical slice:

- title screen, battle flow, and result screen in the main playable scene
- 6x6 tactical board with two player-controlled units
- card-driven actions including `Move`, `Dash`, `Strike`, `Long Strike`, `Guard`, `Push`, and `Recover`
- deterministic enemy response plus reinforcement pressure
- themed `uGUI` battle HUD with prompts, battle log, disabled-card feedback, and world-space health bars

The project is intentionally small in content, but it is structured to support testing, documentation, and final demonstration.

## Repository layout

```text
.
|-- coursework
|   `-- Unity project used for the playable game
|-- artifacts
|   `-- uml
|       `-- diagram sources and exports used by the reports
`-- doc
    `-- src
        |-- course
        |-- final-report-latex
        |-- project-records
        |-- wr1-latex
        |-- wr2-latex
        `-- wr3-latex
```

## Run the game in Unity

1. Open the Unity project in `coursework/`.
2. Load `Assets/Scenes/SampleScene.unity`.
3. Press Play in the Unity Editor.
4. In the title screen, press `Start Battle`.

## Basic play flow

1. Select a player unit on the board.
2. Select a card from the hand panel.
3. Click a valid target tile or enemy unit.
4. Use `Clear` to reset the current selection if needed.
5. Press `End Turn` to let the enemy act.
6. Use the result screen to replay the battle or return to the title screen.

## Reports and evidence

- Weekly Report 1 source: [doc/src/wr1-latex/](doc/src/wr1-latex/)
- Weekly Report 2 source and PDF: [doc/src/wr2-latex/](doc/src/wr2-latex/) and [doc/src/wr2-latex/main.pdf](doc/src/wr2-latex/main.pdf)
- Weekly Report 3 source and PDF: [doc/src/wr3-latex/](doc/src/wr3-latex/) and [doc/src/wr3-latex/main.pdf](doc/src/wr3-latex/main.pdf)
- Final report source and PDF: [doc/src/final-report-latex/](doc/src/final-report-latex/) and [doc/src/final-report-latex/main.pdf](doc/src/final-report-latex/main.pdf)
- Course materials and presentation/video material: [doc/src/course/](doc/src/course/)
- Working records, meeting minutes, testing evidence, and planning notes: [doc/src/project-records/](doc/src/project-records/)

## Source code and software package

- Unity project source: [coursework/](coursework/)
- Playable Windows build location note: [artifacts/submission/SOFTWARE_PACKAGE_README.md](artifacts/submission/SOFTWARE_PACKAGE_README.md)
- Source-only submission package note: [artifacts/submission/SOURCE_PACKAGE_README.md](artifacts/submission/SOURCE_PACKAGE_README.md)
- Video handoff package note: [artifacts/submission/VIDEO_HANDOFF_README.md](artifacts/submission/VIDEO_HANDOFF_README.md)

## Process notes

- Review and branching workflow notes are documented under [doc/src/project-records/](doc/src/project-records/).
- The root project records index is [doc/src/project-records/README.md](doc/src/project-records/README.md).
- Open-licensed runtime UI assets are documented in [coursework/Assets/ThirdParty/Kenney/NOTICE.md](coursework/Assets/ThirdParty/Kenney/NOTICE.md).
